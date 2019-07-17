using ColossalFramework.IO;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Legacy;
using CustomizeItExtended.Settings;
using Harmony;
using ICities;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace CustomizeItExtended
{
    public class CustomizeItExtendedMod : IUserMod
    {
        public string Name => "Customize It! Extended";

        public string Description => "Change various values on buildings such as garbage accumulation, energy consumption and more!";

        private static CustomizeItExtendedSettings _settings;

        private static HarmonyInstance _harmony;

        public static CustomizeItExtendedSettings Settings
        {
            get
            {
                if (_settings != null)
                    return _settings;

                _settings = CustomizeItExtendedSettings.Load();

                if (_settings != null)
                    return _settings;

                _settings = new CustomizeItExtendedSettings();
                _settings.Save();

                return _settings;
            }
            set => _settings = value;
        }

        private static CustomizeItExtendedTool Instance => CustomizeItExtendedTool.instance;

        public void OnEnabled()
        {
            _harmony = HarmonyInstance.Create("com.github.celisuis.csl.customizeitextended");

            try
            {
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, $"[Customize It Extended] Failed to Patch Building Info. {e.Message} - {e.StackTrace}");
            }
        }

        public void OnDisabled()
        {
            _harmony.UnpatchAll();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddSpace(10);
            Instance.SavePerCity = (UICheckBox)helper.AddCheckbox("Save Per City", Settings.SavePerCity, (x) =>
            {
                Settings.SavePerCity = x;
                Settings.Save();
            });
            Instance.SavePerCity.parent.Find<UILabel>("Label").disabledTextColor = Color.gray;
            helper.AddSpace(10);
            Instance.ResetAll = (UIButton)helper.AddButton("Reset ALL Buildings", () =>
            {
                SimulationManager.instance.AddAction(() =>
                {
                    for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount(); i++)
                    {
                        var building = PrefabCollection<BuildingInfo>.GetLoaded(i);
                        if (building == null || building.m_buildingAI == null || !(building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI))))
                            continue;

                        CustomizeItExtendedTool.instance.ResetBuilding(building);
                    }
                });
            });
            Instance.ToggleOptionsPanel(false);
            helper.AddSpace(10);

            var importButton = (UIButton)helper.AddButton($"Import Old Settings", ImportOldSettings);
            importButton.isEnabled = File.Exists(Path.Combine(DataLocation.localApplicationData, $"CustomizeIt.xml"));
            importButton.tooltip = File.Exists(Path.Combine(DataLocation.localApplicationData, $"CustomizeIt.xml")) ? $"Note: This will import your old Customize It settings into Customize It Extended." : $"No Old Settings Found.";
        }

        private static void ImportOldSettings()
        {
            if (!File.Exists(Path.Combine(DataLocation.localApplicationData, $"CustomizeIt.xml")))
                return;

            var xmlSerializer = new XmlSerializer(typeof(CustomizeItSettings));

            try
            {
                CustomizeItSettings oldSettings;
                using (var reader = new StreamReader(Path.Combine(DataLocation.localApplicationData, $"CustomizeIt.xml")))
                {
                    oldSettings = (CustomizeItSettings)xmlSerializer.Deserialize(reader);
                }

                _settings = new CustomizeItExtendedSettings
                {
                    PanelX = oldSettings.PanelX,
                    PanelY = oldSettings.PanelY,
                    SavePerCity = oldSettings.SavePerCity
                };

                CustomizeItExtendedTool.instance.CustomData.Clear();

                foreach (var entry in oldSettings.Entries)
                {
                    CustomizeItExtendedTool.instance.CustomData.Add(entry.Key, entry.Value);
                }
                Settings.Save();
            }
            catch (Exception e)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, $"{e.Message} - {e.StackTrace}");
            }
        }
    }
}