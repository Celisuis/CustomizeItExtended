using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.UI;
using CustomizeItExtended.Compatibility;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Buildings;
using CustomizeItExtended.Helpers;
using CustomizeItExtended.Translations;
using UnityEngine;

namespace CustomizeItExtended.Internal.Buildings
{
    public class CustomizeItExtendedTool : Singleton<CustomizeItExtendedTool>
    {
        public enum InfoPanelType
        {
            Default,
            Warehouse,
            Factory
        }


        private UIButton _customizeItExtendedButton;

        private bool _isButtonInitialized;

        private bool _isInitialized;

        private UIButton _uniqueFactoryButton;

        private UIButton _warehouseButton;

        private UIButton _zonedInfoButton;

        internal BuildingInfo CurrentSelectedBuilding;


        internal Dictionary<string, NameProperties> CustomBuildingNames = new Dictionary<string, NameProperties>();
        internal Dictionary<string, BuildingProperties> CustomData = new Dictionary<string, BuildingProperties>();

        internal UiPanelWrapper CustomizeItExtendedPanel;
        internal UIButton ExportToDefaultConfig;

        internal UIButton ImportDefaultConfig;
        internal readonly Dictionary<string, NameProperties> OriginalBuildingNames = new Dictionary<string, NameProperties>();
        internal readonly Dictionary<string, BuildingProperties> OriginalData = new Dictionary<string, BuildingProperties>();

        internal InfoPanelType PanelType;

        internal UIButton ResetAll;

        internal UICheckBox SavePerCity;
        internal CityServiceWorldInfoPanel ServiceBuildingPanel;


        internal UIUniqueFactoryPanelWrapper UniqueFactoryPanelWrapper;
        internal UniqueFactoryWorldInfoPanel UniqueFactoryWorldInfoPanel;

        internal WarehouseWorldInfoPanel WarehousePanel;

        internal UIWarehousePanelWrapper WarehousePanelWrapper;

        internal ZonedBuildingWorldInfoPanel ZoneBuildingPanel;

        internal UIZonedBuildingPanelWrapper ZonedBuildingPanelWrapper;

        

        internal string ButtonTooltip =>
            ResetAll != null && ResetAll.isEnabled
                ? null
                : "This option is only available in game.".TranslateInformation();

        internal string CheckboxTooltip => SavePerCity != null && SavePerCity.isEnabled
            ? null
            : "This option is only available in the main menu.".TranslateInformation();

        internal string ImportDefaultConfigTooltip => ImportDefaultConfig != null && ImportDefaultConfig.isEnabled
            ? "This will import your Default City Customize Config. WARNING - This will overwrite all current values."
                .TranslateInformation()
            : "This option is only available in game and when Save Per City is enabled.".TranslateInformation();


        internal string ExportDefaultConfigTooltip => ExportToDefaultConfig != null && ExportToDefaultConfig.isEnabled
            ? "This will export your Current City Customized Options to the Default Profile".TranslateInformation()
            : "This option is only available in game and when Save Per City is enabled.".TranslateInformation();

        public void Initialize()
        {
            if (_isInitialized)
                return;

            AddPanelButtons();
            _isInitialized = true;
        }

        public void Release()
        {
            _isButtonInitialized = false;
            _isInitialized = false;
        }

        public void SaveBuilding(BuildingInfo info)
        {
            if (!CustomData.TryGetValue(info.name, out BuildingProperties props))
                CustomData.Add(info.name, new BuildingProperties(info));
            else
                CustomData[info.name] = new BuildingProperties(info);

            if (!CustomizeItExtendedMod.Settings.SavePerCity) CustomizeItExtendedMod.Settings.Save();
        }


        public void ResetBuilding(BuildingInfo info)
        {
            var originalProperties = info.GetOriginalProperties();

            if (CustomData.TryGetValue(info.name, out BuildingProperties customProps)) CustomData.Remove(info.name);

            info.LoadProperties(originalProperties);

            if (!CustomizeItExtendedMod.Settings.SavePerCity) CustomizeItExtendedMod.Settings.Save();
        }

        private void AddPanelButtons()
        {
            if (_isButtonInitialized)
                return;

            ServiceBuildingPanel = GameObject.Find("(Library) CityServiceWorldInfoPanel")
                .GetComponent<CityServiceWorldInfoPanel>();

            if (ServiceBuildingPanel == null)
                return;

            var defaultPosition = new Vector3(160f, 5f, 0f);

            if (ModToolsCompatibilityPatch.IsModToolsActive() &&
                ModToolsCompatibilityPatch.AreGamePanelExtensionsActive())
                defaultPosition = new Vector3(160, -75f, 0f);

            AddDefaultBuildingPropertiesButton(ServiceBuildingPanel, out _customizeItExtendedButton,
                defaultPosition);

            WarehousePanel = GameObject.Find("(Library) WarehouseWorldInfoPanel")
                .GetComponent<WarehouseWorldInfoPanel>();

            if (WarehousePanel == null)
                return;

            AddWarehouseBuildingPropertiesButton(WarehousePanel, out _warehouseButton, new Vector3(68f, -35f, 0f));


            UniqueFactoryWorldInfoPanel = GameObject.Find("(Library) UniqueFactoryWorldInfoPanel")
                .GetComponent<UniqueFactoryWorldInfoPanel>();

            if (UniqueFactoryWorldInfoPanel == null)
                return;

            AddUniqueFactoriesBuildingPropertiesButton(UniqueFactoryWorldInfoPanel, out _uniqueFactoryButton,
                new Vector3(25f, -90f, 0f));

            ZoneBuildingPanel = GameObject.Find("(Library) ZonedBuildingWorldInfoPanel")
                .GetComponent<ZonedBuildingWorldInfoPanel>();

            if (CustomizeItExtendedMod.DebugMode)
                AddBuildingInformationButton(ZoneBuildingPanel, out _zonedInfoButton, new Vector3(120f, 5f, 0f));

            _isButtonInitialized = true;
        }

        private void AddBuildingInformationButton(WorldInfoPanel infoPanel, out UIButton button, Vector3 offset)
        {
            button = UiUtils.CreateToggleButton(ZoneBuildingPanel.component, offset, UIAlignAnchor.BottomLeft,
                (comp, e) =>
                {

                    InstanceID instanceID = InstanceHelper.GetInstanceID(infoPanel);
                    
                    var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                    try
                    {
                        if (ZonedBuildingPanelWrapper == null || building != CurrentSelectedBuilding)
                        {
                            ZonedBuildingPanelWrapper = building.GenerateBuildingInformation();
                        }
                        else
                        {
                            ZonedBuildingPanelWrapper.isVisible = false;
                            UiUtils.DeepDestroy(ZonedBuildingPanelWrapper);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"{ex.Message} - {ex.StackTrace}");
                    }

                    if (comp.hasFocus)
                        comp.Unfocus();
                });
        }

        private void AddWarehouseBuildingPropertiesButton(WarehouseWorldInfoPanel infoPanel, out UIButton button,
            Vector3 offset)
        {
            button = UiUtils.CreateToggleButton(infoPanel.component, offset, UIAlignAnchor.BottomLeft, (comp, e) =>
            {
                PanelType = InfoPanelType.Warehouse;

                InstanceID instanceID = InstanceHelper.GetInstanceID(infoPanel);
                
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                try
                {
                    if (WarehousePanelWrapper == null || building != CurrentSelectedBuilding)
                    {
                        WarehousePanelWrapper = building.GenerateWarehouseCustomizeItExtendedPanel();
                    }
                    else
                    {
                        WarehousePanelWrapper.isVisible = false;
                        UiUtils.DeepDestroy(WarehousePanelWrapper);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"{ex.Message} - {ex.StackTrace}");
                }

                if (comp.hasFocus)
                    comp.Unfocus();
            });
        }

        private void AddUniqueFactoriesBuildingPropertiesButton(UniqueFactoryWorldInfoPanel infoPanel,
            out UIButton button,
            Vector3 offset)
        {
            button = UiUtils.CreateToggleButton(infoPanel.component, offset, UIAlignAnchor.BottomLeft, (comp, e) =>
            {
                PanelType = InfoPanelType.Factory;

                InstanceID instanceID = InstanceHelper.GetInstanceID(infoPanel);
                
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                try
                {
                    if (UniqueFactoryPanelWrapper == null || building != CurrentSelectedBuilding)
                    {
                        UniqueFactoryPanelWrapper = building.GenerateUniqueFactoryCustomizeItExtendedPanel();
                    }
                    else
                    {
                        UniqueFactoryPanelWrapper.isVisible = false;
                        UiUtils.DeepDestroy(UniqueFactoryPanelWrapper);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"{ex.Message} - {ex.StackTrace}");
                }

                if (comp.hasFocus)
                    comp.Unfocus();
            });
        }

        private void AddDefaultBuildingPropertiesButton(WorldInfoPanel infoPanel, out UIButton button, Vector3 offset)
        {
            button = UiUtils.CreateToggleButton(infoPanel.component, offset, UIAlignAnchor.BottomLeft,
                (comp, e) =>
                {
                    PanelType = InfoPanelType.Default;

                    InstanceID instanceID = InstanceHelper.GetInstanceID(infoPanel);
                    
                    var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                    try
                    {
                        if (CustomizeItExtendedPanel == null || building != CurrentSelectedBuilding)
                        {
                            CustomizeItExtendedPanel = building.GenerateCustomizeItExtendedPanel();
                        }
                        else
                        {
                            CustomizeItExtendedPanel.isVisible = false;
                            UiUtils.DeepDestroy(CustomizeItExtendedPanel);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"{ex.Message} - {ex.StackTrace}");
                    }

                    if (comp.hasFocus)
                        comp.Unfocus();
                });
        }

        internal void ToggleOptionsPanel(bool isInGame)
        {
            SavePerCity.isEnabled = !isInGame;
            ResetAll.isEnabled = isInGame;
            ResetAll.tooltip = ButtonTooltip;
            SavePerCity.tooltip = CheckboxTooltip;

            SavePerCity.Find<UISprite>("Unchecked").spriteName = isInGame ? "ToggleBaseDisabled" : "ToggleBase";
            ((UISprite) SavePerCity.checkedBoxObject).spriteName =
                isInGame ? "ToggleBaseDisabled" : "ToggleBaseFocused";

            ImportDefaultConfig.isEnabled = CustomizeItExtendedMod.Settings.SavePerCity && isInGame;
            ImportDefaultConfig.tooltip = ImportDefaultConfigTooltip;

            ExportToDefaultConfig.isEnabled = CustomizeItExtendedMod.Settings.SavePerCity && isInGame;
            ExportToDefaultConfig.tooltip = ExportDefaultConfigTooltip;
        }
    }
}