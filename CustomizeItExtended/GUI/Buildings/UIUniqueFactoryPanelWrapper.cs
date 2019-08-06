using ColossalFramework.UI;
using CustomizeItExtended.Internal.Buildings;
using UnityEngine;

namespace CustomizeItExtended.GUI.Buildings
{
    public class UIUniqueFactoryPanelWrapper : UIPanel
    {
        public static UIUniqueFactoryPanelWrapper Instance;

        private UiCustomizeItExtendedPanel _customizeItExtendedPanel;

        private UiUniqueFactoryTitleBar _uiTitleBar;

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
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _uiTitleBar = AddUIComponent<UiUniqueFactoryTitleBar>();
            _customizeItExtendedPanel = AddUIComponent<UiCustomizeItExtendedPanel>();
        }
    }
}