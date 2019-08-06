using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal.Buildings;
using UnityEngine;

namespace CustomizeItExtended.GUI.Buildings
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