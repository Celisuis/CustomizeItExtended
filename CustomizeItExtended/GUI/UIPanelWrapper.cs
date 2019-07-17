using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using UnityEngine;

namespace CustomizeItExtended.GUI
{
    public class UiPanelWrapper : UIPanel
    {
        public static UiPanelWrapper Instance;
        private UiCustomizeItExtendedPanel _customizeItExtendedPanel;
        private UiTitleBar _titleBar;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();
            var instanceId = (InstanceID) CustomizeItExtendedTool.instance.ServiceBuildingPanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(CustomizeItExtendedTool.instance.ServiceBuildingPanel);

            var buildingInfo = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;

            if (buildingInfo != CustomizeItExtendedTool.instance.CurrentSelectedBuilding) UiUtils.DeepDestroy(this);
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
            padding = new RectOffset(10, 10, 4, 4);
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _titleBar = AddUIComponent<UiTitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UiCustomizeItExtendedPanel>();
        }
    }
}