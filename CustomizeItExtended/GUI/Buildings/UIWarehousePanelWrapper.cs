using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal.Buildings;
using UnityEngine;

namespace CustomizeItExtended.GUI.Buildings
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