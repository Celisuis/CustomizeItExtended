using ColossalFramework.UI;
using CustomizeItExtended.Internal.Vehicles;
using UnityEngine;

namespace CustomizeItExtended.GUI.Vehicles
{
    public class UIVehiclePanelWrapper : UIPanel
    {
        public static UIVehiclePanelWrapper Instance;

        private UiVehicleTitleBar _titleBar;

        private UIVehiclePanel _vehiclePanel;

        public override void Start()
        {
            base.Start();

            Instance = this;
            Setup();
        }

        public override void Update()
        {
            base.Update();

            var instanceId = CustomizeItExtendedVehicleTool.instance.SelectedInstanceID;

            var vehicleInfo = VehicleManager.instance.m_vehicles.m_buffer[instanceId.Vehicle].Info;

            if (vehicleInfo != CustomizeItExtendedVehicleTool.instance.SelectedVehicle) UiUtils.DeepDestroy(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            CustomizeItExtendedVehicleTool.instance.SaveVehicle(CustomizeItExtendedVehicleTool.instance
                .SelectedVehicle);
        }

        private void Setup()
        {
            isVisible = false;
            isInteractive = false;
            name = "CustomizeItExtendedVehiclePanelWrapper";
            padding = new RectOffset(10, 10, 4, 4);
            relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelX);
            backgroundSprite = "MenuPanel";
            _titleBar = AddUIComponent<UiVehicleTitleBar>();
            _vehiclePanel = AddUIComponent<UIVehiclePanel>();
        }
    }
}