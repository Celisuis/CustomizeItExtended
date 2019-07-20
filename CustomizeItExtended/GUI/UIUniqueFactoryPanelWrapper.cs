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
    public class UIUniqueFactoryPanelWrapper : UIPanel
    {
        public static UIUniqueFactoryPanelWrapper Instance;

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

            var instanceId = (InstanceID)CustomizeItExtendedTool.instance.UniqueFactoryWorldInfoPanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(CustomizeItExtendedTool.instance.UniqueFactoryWorldInfoPanel);

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
            name = "CustomizeItExtendedPanelWrapper";
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _uiTitleBar = AddUIComponent<UiTitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UiCustomizeItExtendedPanel>();
        }
    }
}
