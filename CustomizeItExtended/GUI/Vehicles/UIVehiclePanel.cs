using System.Collections.Generic;
using System.Linq;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.Helpers;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Vehicles;
using UnityEngine;

namespace CustomizeItExtended.GUI.Vehicles
{
    public class UIVehiclePanel : UIPanel
    {
        internal static UIVehiclePanel Instance;

        private List<UILabel> _labels;

        internal List<UIComponent> Inputs;

        private VehicleInfo SelectedVehicle => CustomizeItExtendedVehicleTool.instance.SelectedVehicle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            Setup();
        }

        private void Setup()
        {
            name = "CustomizeItVehicleExtendedPanel";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = new Vector3(0f, UiVehicleTitleBar.Instance.height);
            width = parent.width;

            var fields = SelectedVehicle.GetType().GetFields();

            var fieldsToRetrieve = typeof(CustomVehicleProperties).GetFields().Select(x => x.Name);

            Inputs = new List<UIComponent>();
            _labels = new List<UILabel>();

            var widestWidth = 0f;

            foreach (var field in fields.Where(x => fieldsToRetrieve.Contains(x.Name)))
            {
                var label = AddUIComponent<UILabel>();
                label.name = field.Name + "Label";
                label.text = UiUtils.FieldNames[field.Name];
                label.textScale = 0.9f;
                label.isInteractive = false;

                if (field.FieldType == typeof(float) || field.FieldType == typeof(int))
                {
                    Inputs.Add(UiUtils.CreateVehicleTextField(this, field.Name));
                    _labels.Add(label);
                }
                else if (field.FieldType == typeof(bool))
                {
                    Inputs.Add(UiUtils.CreateVehicleCheckBox(this, field.Name));
                    _labels.Add(label);
                }

                if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                    widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
            }


            Inputs.Add(UiUtils.CreateNameTextfield(this, "DefaultName", (component, value) =>
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(SelectedVehicle.name,
                        out var props))
                    {
                        props.CustomName = value;
                        props.DefaultName = true;
                    }
                    else
                    {
                        CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.Add(SelectedVehicle.name,
                            new NameProperties(value, true));
                    }
                }
                else
                {
                    if (CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(SelectedVehicle.name,
                        out var _))
                        CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.Remove(SelectedVehicle.name);
                }

                if (!CustomizeItExtendedMod.Settings.SavePerCity)
                    CustomizeItExtendedMod.Settings.Save();
            }, CustomizeItExtendedVehicleTool
                .instance.CustomVehicleNames.TryGetValue(SelectedVehicle.name, out var customName)
                ? customName.CustomName
                : string.Empty));

            var nameLabel = AddUIComponent<UILabel>();
            nameLabel.name = "DefaultNameLabel";
            nameLabel.text = "Default Name";
            nameLabel.textScale = 0.9f;
            nameLabel.isInteractive = false;

            if (nameLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = nameLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;

            _labels.Add(nameLabel);

            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            _labels.Sort((x, y) => x.name.CompareTo(y.name));

            Inputs.Add(UiUtils.CreateVehicleResetButton(this));

            Inputs.Add(UiUtils.CreateDropdown(this, "SelectedVehicle", VehicleHelper.GetAllVehicleNames().ToArray(),
                (component, value) =>
                {
                    var newVehicle = VehicleHelper.GetAllVehicles()
                        .Find(x =>
                            x.name == VehicleHelper.RetrieveOriginalVehicleName(((UIDropDown) component).items[value]));


                    UiUtils.DeepDestroy(CustomizeItExtendedVehicleTool.instance.VehiclePanelWrapper);
                    CustomizeItExtendedVehicleTool.instance.SelectedVehicle = newVehicle;
                    newVehicle.GenerateVehiclePanel();
                }, VehicleHelper.RetrieveCurrentVehicleName(SelectedVehicle)));

            width = UIVehiclePanelWrapper.Instance.width =
                UiVehicleTitleBar.Instance.width = UiVehicleTitleBar.Instance.DragHandle.width = widestWidth;
            UiVehicleTitleBar.Instance.RecenterElements();
            Align();

            Inputs.Find(x => x.name == "SelectedVehicle").relativePosition = new Vector3(12,
                Inputs.Find(x => x.name == "CustomizeItExtendedVehicleResetButton").relativePosition.y);


            height = Inputs.Count * (UiUtils.FieldHeight + UiUtils.FieldMargin) + UiUtils.FieldMargin * 3;

            UIVehiclePanelWrapper.Instance.height = height + UiVehicleTitleBar.Instance.height;


            UIVehiclePanelWrapper.Instance.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);

            isVisible = UIVehiclePanelWrapper.Instance.isVisible =
                UiVehicleTitleBar.Instance.isVisible = UiVehicleTitleBar.Instance.DragHandle.isVisible = true;
        }

        private void Align()
        {
            var inputX = width - UiUtils.FieldWidth - UiUtils.FieldMargin * 2;

            for (var x = 0; x < Inputs.Count; x++)
            {
                var finalY = x * UiUtils.FieldHeight + UiUtils.FieldMargin * (x + 2);

                if (x < _labels.Count)
                {
                    var labelX = inputX - _labels[x].width - UiUtils.FieldMargin * 2;
                    _labels[x].relativePosition = new Vector3(labelX, finalY + 4);
                }

                Inputs[x].relativePosition = Inputs[x] is UICheckBox
                    ? new Vector3(inputX + (UiUtils.FieldWidth - Inputs[x].width) / 2,
                        finalY + (UiUtils.FieldHeight - Inputs[x].height) / 2)
                    : new Vector3(inputX, finalY);
            }
        }
    }
}