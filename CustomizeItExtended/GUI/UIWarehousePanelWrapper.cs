using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using UnityEngine;

namespace CustomizeItExtended.GUI
{
    public class UIWarehousePanelWrapper : UIPanel
    {
        public static UIWarehousePanelWrapper Instance;

        private UiCustomizeItExtendedPanel _customizeItExtendedPanel;

        private UiTitleBar _uiTitleBar;

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
            _uiTitleBar = AddUIComponent<UiTitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UiCustomizeItExtendedPanel>();
        }
    }
}
