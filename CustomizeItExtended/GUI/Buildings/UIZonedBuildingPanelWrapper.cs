using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal.Buildings;
using UnityEngine;

namespace CustomizeItExtended.GUI.Buildings
{
    public class UIZonedBuildingPanelWrapper : UIPanel
    {
        public static UIZonedBuildingPanelWrapper Instance;

        private UiInfoTitleBar _uiTitleBar;

        private UIZonedBuildingPanel _uiZonedBuildingPanel;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();

            var instanceId = (InstanceID) CustomizeItExtendedTool.instance.ZoneBuildingPanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(CustomizeItExtendedTool.instance.ZoneBuildingPanel);

            var buildingInfo = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building].Info;

            if (buildingInfo != CustomizeItExtendedTool.instance.CurrentSelectedBuilding)
                UiUtils.DeepDestroy(this);
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
            name = "CustomizeItExtendedZonedBuildingPanelWrapper";
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _uiTitleBar = AddUIComponent<UiInfoTitleBar>();
            _uiZonedBuildingPanel = AddUIComponent<UIZonedBuildingPanel>();
        }
    }
}