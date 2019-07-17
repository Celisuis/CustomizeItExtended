using ColossalFramework.UI;
using CustomizeItEnhanced.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomizeItEnhanced.GUI
{
    public class UIPanelWrapper : UIPanel
    {
        private UITitleBar _titleBar;
        private UICustomizeItEnhancedPanel _customizeItEnhancedPanel;

        public static UIPanelWrapper Instance;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();
            InstanceID instanceId = (InstanceID)CustomizeItEnhancedTool.instance.ServiceBuildingPanel.GetType().GetField("m_InstanceID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(CustomizeItEnhancedTool.instance.ServiceBuildingPanel);

            var buildingInfo = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;

            if(buildingInfo != CustomizeItEnhancedTool.instance.CurrentSelectedBuilding)
            {
                UIUtils.DeepDestroy(this);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            CustomizeItEnhancedTool.instance.SaveBuilding(CustomizeItEnhancedTool.instance.CurrentSelectedBuilding);
        }

        private void Setup()
        {
            isVisible = false;
            isInteractive = false;
            name = "CustomizeItEnhancedPanelWrapper";
            padding = new UnityEngine.RectOffset(10, 10, 4, 4);
            relativePosition = new UnityEngine.Vector3(CustomizeItEnhancedMod.Settings.PanelX, CustomizeItEnhancedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _titleBar = AddUIComponent<UITitleBar>();
            _customizeItEnhancedPanel = AddUIComponent<UICustomizeItEnhancedPanel>();
        }
    }
}
