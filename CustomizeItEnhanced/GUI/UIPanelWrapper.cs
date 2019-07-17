using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomizeItExtended.GUI
{
    public class UIPanelWrapper : UIPanel
    {
        private UITitleBar _titleBar;
        private UICustomizeItExtendedPanel _customizeItExtendedPanel;

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
            InstanceID instanceId = (InstanceID)CustomizeItExtendedTool.instance.ServiceBuildingPanel.GetType().GetField("m_InstanceID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(CustomizeItExtendedTool.instance.ServiceBuildingPanel);

            var buildingInfo = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;

            if(buildingInfo != CustomizeItExtendedTool.instance.CurrentSelectedBuilding)
            {
                UIUtils.DeepDestroy(this);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            CustomizeItExtendedTool.instance.SaveBuilding(CustomizeItExtendedTool.instance.CurrentSelectedBuilding);
        }

        private void Setup()
        {
            isVisible = false;
            isInteractive = false;
            name = "CustomizeItExtendedPanelWrapper";
            padding = new UnityEngine.RectOffset(10, 10, 4, 4);
            relativePosition = new UnityEngine.Vector3(CustomizeItExtendedMod.Settings.PanelX, CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _titleBar = AddUIComponent<UITitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UICustomizeItExtendedPanel>();
        }
    }
}
