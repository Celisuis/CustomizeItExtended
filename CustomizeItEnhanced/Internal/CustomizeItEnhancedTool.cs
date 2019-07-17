using ColossalFramework;
using ColossalFramework.UI;
using CustomizeItEnhanced.Extensions;
using CustomizeItEnhanced.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CustomizeItEnhanced.Internal
{
    public class CustomizeItEnhancedTool : Singleton<CustomizeItEnhancedTool>
    {
        internal Dictionary<string, Properties> CustomData = new Dictionary<string, Properties>();
        internal Dictionary<string, Properties> OriginalData = new Dictionary<string, Properties>();

        private bool isInitialized;

        private bool isButtonInitialized;

        internal BuildingInfo CurrentSelectedBuilding;
        internal CityServiceWorldInfoPanel ServiceBuildingPanel;

        private UIButton _customizeItEnhancedButton;

        internal UIPanelWrapper CustomizeItEnhancedPanel;

        internal UICheckBox SavePerCity;

        internal UIButton ResetAll;

        internal string ButtonTooltip => ResetAll != null && ResetAll.isEnabled ? null : "This option is only available in game.";

        internal string CheckboxTooltip => SavePerCity != null && SavePerCity.isEnabled ? null : "This option is only available in the main menu.";


        public void Initialize()
        {
            if (isInitialized)
                return;

            AddPanelButton();
            isInitialized = true;
        }

        public void Release()
        {
            isButtonInitialized = false;
            isInitialized = false;
        }

        public void SaveBuilding(BuildingInfo info)
        {
            if(!CustomData.TryGetValue(info.name, out Properties props))
            {
                CustomData.Add(info.name, new Properties(info));
            }
            else
            {
                CustomData[info.name] = new Properties(info);
            }

            if(!CustomizeItEnhancedMod.Settings.SavePerCity)
            {
                CustomizeItEnhancedMod.Settings.Save();
            }
        }

        public void ResetBuilding(BuildingInfo info)
        {
            var originalProperties = info.GetOriginalProperties();

            if(CustomData.TryGetValue(info.name, out Properties customProps))
            {
                CustomData.Remove(info.name);

            }
            info.LoadProperties(originalProperties);

            if(!CustomizeItEnhancedMod.Settings.SavePerCity)
            {
                CustomizeItEnhancedMod.Settings.Save();
            }


        }

        private void AddPanelButton()
        {
            if(!isButtonInitialized)
            {
                ServiceBuildingPanel = GameObject.Find("(Library) CityServiceWorldInfoPanel").GetComponent<CityServiceWorldInfoPanel>();

                if(ServiceBuildingPanel != null)
                {
                    AddBuildingPropertiesButton(ServiceBuildingPanel, out _customizeItEnhancedButton, new Vector3(-7f, 43f, 0f));
                    isButtonInitialized = true;
                }
            }
        }

        private void AddBuildingPropertiesButton(WorldInfoPanel infoPanel, out UIButton button, Vector3 offset)
        {
            button = UIUtils.CreateToggleButton(infoPanel.component, offset, UIAlignAnchor.BottomRight, delegate (UIComponent comp, UIMouseEventParameter e)
            {
                InstanceID instanceID = (InstanceID)infoPanel.GetType().GetField("m_InstanceID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(infoPanel);
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;

                if (CustomizeItEnhancedPanel == null || building != CurrentSelectedBuilding)
                {
                    CustomizeItEnhancedPanel = building.GenerateCustomizeItEnhancedPanel();
                }
                else
                {
                    CustomizeItEnhancedPanel.isVisible = false;
                    UIUtils.DeepDestroy(CustomizeItEnhancedPanel);
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
            ((UISprite)SavePerCity.checkedBoxObject).spriteName = isInGame ? "ToggleBaseDisabled" : "ToggleBaseFocused";
        }

    }
}
