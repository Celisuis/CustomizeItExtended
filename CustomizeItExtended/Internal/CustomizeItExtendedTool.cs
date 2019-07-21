using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.GUI;
using UnityEngine;

namespace CustomizeItExtended.Internal
{
    public class CustomizeItExtendedTool : Singleton<CustomizeItExtendedTool>
    {

        public enum InfoPanelType
        {
            Default,
            Warehouse,
            Factory,
        }


        private UIButton _customizeItExtendedButton;

        private bool _isButtonInitialized;

        private bool _isInitialized;

        private UIButton _uniqueFactoryButton;

        private UIButton _warehouseButton;

        private UIButton _zonedInfoButton;

        internal BuildingInfo CurrentSelectedBuilding;
        internal Dictionary<string, Properties> CustomData = new Dictionary<string, Properties>();

        internal UiPanelWrapper CustomizeItExtendedPanel;
        internal Dictionary<string, Properties> OriginalData = new Dictionary<string, Properties>();

        internal UIButton ResetAll;

        internal UICheckBox SavePerCity;
        internal CityServiceWorldInfoPanel ServiceBuildingPanel;

        internal UIUniqueFactoryPanelWrapper UniqueFactoryPanelWrapper;
        internal UniqueFactoryWorldInfoPanel UniqueFactoryWorldInfoPanel;

        internal WarehouseWorldInfoPanel WarehousePanel;

        internal UIWarehousePanelWrapper WarehousePanelWrapper;

        internal ZonedBuildingWorldInfoPanel ZoneBuildingPanel;

        internal UIZonedBuildingPanelWrapper ZonedBuildingPanelWrapper;

        internal InfoPanelType PanelType;

        internal string ButtonTooltip =>
            ResetAll != null && ResetAll.isEnabled ? null : "This option is only available in game.";

        internal string CheckboxTooltip => SavePerCity != null && SavePerCity.isEnabled
            ? null
            : "This option is only available in the main menu.";

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
            if (!CustomData.TryGetValue(info.name, out Properties props))
                CustomData.Add(info.name, new Properties(info));
            else
                CustomData[info.name] = new Properties(info);

            if (!CustomizeItExtendedMod.Settings.SavePerCity) CustomizeItExtendedMod.Settings.Save();
        }

        public void ResetBuilding(BuildingInfo info)
        {
            var originalProperties = info.GetOriginalProperties();

            if (CustomData.TryGetValue(info.name, out Properties customProps)) CustomData.Remove(info.name);

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

            AddDefaultBuildingPropertiesButton(ServiceBuildingPanel, out _customizeItExtendedButton,
                new Vector3(120f, 5f, 0f));

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

                    InstanceID instanceID = (InstanceID) infoPanel.GetType()
                        .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infoPanel);

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
                        DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{ex.Message} - {ex.StackTrace}");
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

                InstanceID instanceId = (InstanceID) infoPanel.GetType().GetField("m_InstanceID",
                        BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(infoPanel);
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;
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
                    DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{ex.Message} - {ex.StackTrace}");
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

                InstanceID instanceId = (InstanceID) infoPanel.GetType().GetField("m_InstanceID",
                        BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(infoPanel);
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;
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
                    DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{ex.Message} - {ex.StackTrace}");
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

                    InstanceID instanceId = (InstanceID) infoPanel.GetType().GetField("m_InstanceID",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        ?.GetValue(infoPanel);
                    var building = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;
                    try
                    {
                        if (CustomizeItExtendedPanel == null || building != CurrentSelectedBuilding)
                            switch (building.m_class.name)
                            {
                                case "Warehouses":
                                    WarehousePanelWrapper = building.GenerateWarehouseCustomizeItExtendedPanel();
                                    break;
                                case "Unique Factories":
                                    UniqueFactoryPanelWrapper =
                                        building.GenerateUniqueFactoryCustomizeItExtendedPanel();
                                    break;
                                default:
                                    CustomizeItExtendedPanel = building.GenerateCustomizeItExtendedPanel();
                                    break;
                            }
                        else
                            switch (building.m_class.name)
                            {
                                case "Warehouses":
                                {
                                    WarehousePanelWrapper.isVisible = false;
                                    UiUtils.DeepDestroy(WarehousePanelWrapper);
                                }
                                    break;
                                case "Unique Factories":
                                {
                                    UniqueFactoryPanelWrapper.isVisible = false;
                                    UiUtils.DeepDestroy(UniqueFactoryPanelWrapper);
                                }
                                    break;
                                default:
                                {
                                    CustomizeItExtendedPanel.isVisible = false;
                                    UiUtils.DeepDestroy(CustomizeItExtendedPanel);
                                }
                                    break;
                            }
                    }
                    catch (Exception ex)
                    {
                        DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{ex.Message} - {ex.StackTrace}");
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
        }
    }
}