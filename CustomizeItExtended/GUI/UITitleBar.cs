using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Citizens;
using CustomizeItExtended.Internal.Vehicles;
using UnityEngine;

namespace CustomizeItExtended.GUI
{
    public class UiTitleBar : UIPanel
    {
        public static UiTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleLabel = AddUIComponent<UILabel>();
            _titleLabel.text =
                CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(
                    CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name, out var props)
                    ? props.CustomName
                    : CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name;
            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedTool.instance.CustomizeItExtendedPanel.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.CustomizeItExtendedPanel);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiWarehouseTitleBar : UIPanel
    {
        public static UiWarehouseTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedWarehouseTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleLabel = AddUIComponent<UILabel>();
            _titleLabel.text =
                CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(
                    CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name, out var props)
                    ? props.CustomName
                    : CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name;
            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedTool.instance.WarehousePanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.WarehousePanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiUniqueFactoryTitleBar : UIPanel
    {
        public static UiUniqueFactoryTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedFactoryTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleLabel = AddUIComponent<UILabel>();
            _titleLabel.text =
                CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(
                    CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name, out var props)
                    ? props.CustomName
                    : CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name;
            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedTool.instance.UniqueFactoryPanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.UniqueFactoryPanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiInfoTitleBar : UIPanel
    {
        public static UiInfoTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedInfoTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleLabel = AddUIComponent<UILabel>();
            _titleLabel.text = CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name;
            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedTool.instance.ZonedBuildingPanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.ZonedBuildingPanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiCitizenTitleBar : UIPanel
    {
        public static UiCitizenTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedCitizenTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };
            var nameField = CustomizeItExtendedCitizenTool.instance.CitizenWorldInfoPanel.GetType()
                .GetField("m_NameField", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(CustomizeItExtendedCitizenTool.instance.CitizenWorldInfoPanel) as UITextComponent;

            _titleLabel = AddUIComponent<UILabel>();
            _titleLabel.text = nameField.GetType().GetField("m_Text", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(nameField) as string;
            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedCitizenTool.instance.CitizenPanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedCitizenTool.instance.CitizenPanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiVehicleTitleBar : UIPanel
    {
        public static UiVehicleTitleBar Instance;
        private UIButton _closeButton;
        private UILabel _titleLabel;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedVehicleTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleLabel = AddUIComponent<UILabel>();

            if (CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(
                CustomizeItExtendedVehicleTool.instance.SelectedVehicle.name, out NameProperties props))
                _titleLabel.text = props.CustomName;
            else
                _titleLabel.text = CustomizeItExtendedVehicleTool.instance
                    .SelectedVehicle.name;

            _titleLabel.textScale = 0.9f;
            _titleLabel.isInteractive = false;

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleLabel.relativePosition =
                new Vector3((width - _titleLabel.width) / 2f, (height - _titleLabel.height) / 2);
        }
    }

    public class UiVehicleSelectionTitleBar : UIPanel
    {
        public static UiVehicleSelectionTitleBar Instance;
        private UIButton _closeButton;
        private UIDropDown _titleDropdown;
        public UIDragHandle DragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItExtendedVehicleSelectionTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            DragHandle = AddUIComponent<UIDragHandle>();
            DragHandle.height = height;
            DragHandle.relativePosition = Vector3.zero;
            DragHandle.target = parent;
            DragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItExtendedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItExtendedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItExtendedMod.Settings.Save();
            };

            _titleDropdown = AddUIComponent<UIDropDown>();

            List<string> vehicleNames = new List<string>();

            foreach (var kvp in CustomizeItExtendedVehicleTool.instance.AllLoadedVehicles)
            {
                vehicleNames.Add(
                    CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(kvp.Value.name,
                        out var props)
                        ? props.CustomName
                        : kvp.Value.name);
            }

            _titleDropdown.items = vehicleNames.ToArray();

            _titleDropdown.selectedIndex = Array.IndexOf(_titleDropdown.items, CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(
                CustomizeItExtendedVehicleTool.instance.SelectedVehicle.name, out var nameprops) ? nameprops.CustomName : CustomizeItExtendedVehicleTool.instance.SelectedVehicle.name);

            _titleDropdown.textScale = 0.9f;
            _titleDropdown.isInteractive = true;

            _titleDropdown.eventSelectedIndexChanged += (component, index) =>
            {;
                var vehicleInfo =
                    CustomizeItExtendedVehicleTool.instance.AllLoadedVehicles[_titleDropdown.items[index]];


                if (CustomizeItExtendedVehicleTool.instance.SelectedVehicle == vehicleInfo)
                    return;

                CustomizeItExtendedVehicleTool.instance.SaveVehicle(CustomizeItExtendedVehicleTool.instance.SelectedVehicle);
                CustomizeItExtendedVehicleTool.instance.SelectedVehicle = vehicleInfo;

                UiUtils.DeepDestroy(CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper);
                CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper = vehicleInfo.GenerateVehiclePanel();
            };

            _closeButton = AddUIComponent<UIButton>();
            _closeButton.size = new Vector2(20, 20);
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _closeButton.normalBgSprite = "DeleteLineButton";
            _closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            _closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            _closeButton.eventClick += (component, param) =>
            {
                CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper.isVisible = false;
                UiUtils.DeepDestroy(CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper);
            };
        }

        public void RecenterElements()
        {
            _closeButton.relativePosition = new Vector3(width - _closeButton.width - 10f, 10f);
            _titleDropdown.relativePosition =
                new Vector3((width - _titleDropdown.width) / 2f, (height - _titleDropdown.height) / 2);
        }
    }
}