using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Legacy;
using CustomizeItExtended.Settings;
using Harmony;
using ICities;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace CustomizeItExtended
{
    public class CustomizeItExtendedMod : IUserMod
    {
        internal const string Version = "1.4.0V";

        private static CustomizeItExtendedSettings _settings;

        private static HarmonyInstance _harmony;

        public static bool DebugMode =
            File.Exists(Path.Combine(DataLocation.localApplicationData, "CSharpDebugMode.txt"));

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

        public string Name => "Customize It! Extended";

        public string Description =>
            $"{Version} - A Customization and Information Viewer for Buildings, Vehicles and Citizens!";

        public void OnEnabled()
        {
            _harmony = HarmonyInstance.Create("com.github.celisuis.csl.customizeitextended");

            try
            {
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                Debug.Log($"[Customize It Extended] Failed to Patch. {e.Message} - {e.StackTrace}");
            }

            try
            {
                if (!IsRebalancedIndustriesActive() || Settings.RebalancedMessageShown)
                    return;

                UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                    "[Customize It Extended] Rebalanced Industries Detected.",
                    "Rebalanced Industries has been detected. For compatibility, certain industry properties have been disabled from being altered." +
                    Environment.NewLine +
                    "This can be overridden within the Main Menu Options.", false);
                Settings.RebalancedMessageShown = true;
                Settings.Save();
            }
            catch (Exception e)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning,
                    "Couldn't Load Exception Message. This message should not prevent mod functionality. ");
            }
        }

        public void OnDisabled()
        {
            _harmony.UnpatchAll();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddSpace(10);
            Instance.SavePerCity = (UICheckBox) helper.AddCheckbox("Save Per City", Settings.SavePerCity, x =>
            {
                Settings.SavePerCity = x;
                Settings.Save();
            });
            Instance.SavePerCity.parent.Find<UILabel>("Label").disabledTextColor = Color.gray;
            helper.AddSpace(10);
            var overrideButton = (UICheckBox) helper.AddCheckbox("Override Rebalanced Industries",
                Settings.OverrideRebalancedIndustries,
                x =>
                {
                    Settings.OverrideRebalancedIndustries = x;
                    Settings.Save();
                });
            overrideButton.tooltip =
                "EXPERIMENTAL - This will cause your Industry buildings to revert back to Vanilla";

            overrideButton.isEnabled = IsRebalancedIndustriesActive();
            overrideButton.disabledColor = Color.gray;
            helper.AddSpace(10);
            Instance.ResetAll = (UIButton) helper.AddButton("Reset ALL Buildings", () =>
            {
                SimulationManager.instance.AddAction(() =>
                {
                    for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount(); i++)
                    {
                        var building = PrefabCollection<BuildingInfo>.GetLoaded(i);
                        if (building == null || building.m_buildingAI == null ||
                            !building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))
                            continue;

                        CustomizeItExtendedTool.instance.ResetBuilding(building);
                    }
                });
            });
            Instance.ToggleOptionsPanel(false);
            helper.AddSpace(10);

            var importButton = (UIButton) helper.AddButton("Import Old Settings", ImportOldSettings);
            importButton.isEnabled = File.Exists(Path.Combine(DataLocation.localApplicationData, "CustomizeIt.xml"));
            importButton.tooltip = File.Exists(Path.Combine(DataLocation.localApplicationData, "CustomizeIt.xml"))
                ? "Note: This will import your old Customize It settings into Customize It Extended."
                : "No Old Settings Found.";
            importButton.disabledColor = Color.gray;
            helper.AddSpace(10);
            var version = (UITextField) helper.AddTextfield("Version", Version, text => { }, text => { });
            version.isInteractive = false;
        }

        private static void ImportOldSettings()
        {
            if (!File.Exists(Path.Combine(DataLocation.localApplicationData, "CustomizeIt.xml")))
                return;

            var xmlSerializer = new XmlSerializer(typeof(CustomizeItSettings));

            try
            {
                CustomizeItSettings oldSettings;
                using (var reader =
                    new StreamReader(Path.Combine(DataLocation.localApplicationData, "CustomizeIt.xml")))
                {
                    oldSettings = (CustomizeItSettings) xmlSerializer.Deserialize(reader);
                }

                _settings = new CustomizeItExtendedSettings
                {
                    PanelX = oldSettings.PanelX,
                    PanelY = oldSettings.PanelY,
                    SavePerCity = oldSettings.SavePerCity
                };

                CustomizeItExtendedTool.instance.CustomData.Clear();

                foreach (var entry in oldSettings.Entries)
                    CustomizeItExtendedTool.instance.CustomData.Add(entry.Key, entry.Value);

                Settings.Save();
            }
            catch (Exception e)
            {
                Debug.Log(
                    $"{e.Message} - {e.StackTrace}");
            }
        }

        public static bool IsRebalancedIndustriesActive()
        {
            var plugins = PluginManager.instance.GetPluginsInfo();

            return plugins.Where(x => x.isEnabled).Any(plugin => plugin.publishedFileID.AsUInt64 == 1562650024);
        }
    }
}