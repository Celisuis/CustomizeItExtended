using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Internal.Citizens;
using UnityEngine;

namespace CustomizeItExtended.GUI.Citizens
{
    public class UICitizenPanelWrapper : UIPanel
    {
        internal static UICitizenPanelWrapper Instance;

        private UICitizenPanel _citizenPanel;

        private UiCitizenTitleBar _titleBar;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();

            var instanceID = (InstanceID) CustomizeItExtendedCitizenTool.instance.CitizenWorldInfoPanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(CustomizeItExtendedCitizenTool.instance.CitizenWorldInfoPanel);

            if (instanceID.Citizen != CustomizeItExtendedCitizenTool.instance.SelectedCitizen)
                UiUtils.DeepDestroy(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void Setup()
        {
            isVisible = false;
            isInteractive = false;
            name = "CustomizeItExtendedCitizenPanelWrapper";
            padding = new RectOffset(10, 10, 4, 4);
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _titleBar = AddUIComponent<UiCitizenTitleBar>();
            _citizenPanel = AddUIComponent<UICitizenPanel>();
        }
    }
}