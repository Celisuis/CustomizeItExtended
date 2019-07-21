using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using UnityEngine;

namespace CustomizeItExtended.GUI
{
    public class UIWarehousePanelWrapper : UIPanel
    {
        public static UIWarehousePanelWrapper Instance;

        private UiCustomizeItExtendedPanel _customizeItExtendedPanel;

        private UiWarehouseTitleBar _uiTitleBar;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();

            var instanceId = (InstanceID) CustomizeItExtendedTool.instance.WarehousePanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(CustomizeItExtendedTool.instance.WarehousePanel);

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
            name = "CustomizeItExtendedWarehousePanelWrapper";
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _uiTitleBar = AddUIComponent<UiWarehouseTitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UiCustomizeItExtendedPanel>();
        }
    }
}