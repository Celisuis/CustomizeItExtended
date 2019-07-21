using System.Collections.Generic;
using System.Linq;
using ColossalFramework.UI;
using CustomizeItExtended.Compatibility;
using CustomizeItExtended.GUI;
using CustomizeItExtended.Internal;
using UnityEngine;

namespace CustomizeItExtended
{
    public class UiCustomizeItExtendedPanel : UIPanel
    {
        internal static UiCustomizeItExtendedPanel Instance;

        private List<UILabel> _labels;

        internal List<UIComponent> Inputs;
        private BuildingInfo SelectedBuilding => CustomizeItExtendedTool.instance.CurrentSelectedBuilding;

        public override void Start()
        {
            base.Start();
            Instance = this;
            Setup();
        }

        private void Setup()
        {
            name = "CustomizeItExtendedPanel";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = new Vector3(0f, Titlebar().height);
            width = parent.width;

            var ai = SelectedBuilding.m_buildingAI;

            var fields = ai.GetType().GetFields();

            var fieldsToRetrieve = typeof(Properties).GetFields().Select(x => x.Name);

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

                if (field.FieldType == typeof(int) || field.FieldType == typeof(float) ||
                    field.FieldType == typeof(uint))
                {
                    Inputs.Add(UiUtils.CreateTextField(this, field.Name));
                    _labels.Add(label);
                }
                else if (field.FieldType == typeof(bool))
                {
                    Inputs.Add(UiUtils.CreateCheckBox(this, field.Name));
                    _labels.Add(label);
                }

                if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                    widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
            }

            if (!CustomizeItExtendedMod.Settings.OverrideRebalancedIndustries)
                foreach (var input in Inputs)
                {
                    if (!CustomizeItExtendedMod.IsRebalancedIndustriesActive() ||
                        !RebalancedIndustries.RebalancedFields.Contains(input.name))
                        continue;

                    input.isEnabled = false;
                    input.isInteractive = false;

                    if (input is UITextField textField) textField.text = "DISABLED";
                }


            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            _labels.Sort((x, y) => x.name.CompareTo(y.name));

            Inputs.Add(UiUtils.CreateResetButton(this));


            switch (CustomizeItExtendedTool.instance.PanelType)
            {
                case CustomizeItExtendedTool.InfoPanelType.Warehouse:
                    SetupWarehousePanel(widestWidth, ref UIWarehousePanelWrapper.Instance);
                    break;
                case CustomizeItExtendedTool.InfoPanelType.Factory:
                    SetupUniqueFactoryPanel(widestWidth, ref UIUniqueFactoryPanelWrapper.Instance);
                    break;
                default:
                    SetupDefaultPanel(widestWidth, ref UiPanelWrapper.Instance);
                    break;
            }
        }

        private void SetupDefaultPanel(float widestWidth, ref UiPanelWrapper panelWrapper)
        {
            width = panelWrapper.width =
                UiTitleBar.Instance.width = UiTitleBar.Instance.DragHandle.width = widestWidth;
            UiTitleBar.Instance.RecenterElements();
            Align();
            height = Inputs.Count * (UiUtils.FieldHeight + UiUtils.FieldMargin) + UiUtils.FieldMargin * 3;

            panelWrapper.height = height + UiTitleBar.Instance.height;


            panelWrapper.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);
            isVisible = panelWrapper.isVisible =
                UiTitleBar.Instance.isVisible = UiTitleBar.Instance.DragHandle.isVisible = true;
        }

        private void SetupWarehousePanel(float widestWidth, ref UIWarehousePanelWrapper panelWrapper)
        {
            width = panelWrapper.width =
                UiWarehouseTitleBar.Instance.width = UiWarehouseTitleBar.Instance.DragHandle.width = widestWidth;
            UiWarehouseTitleBar.Instance.RecenterElements();
            Align();
            height = Inputs.Count * (UiUtils.FieldHeight + UiUtils.FieldMargin) + UiUtils.FieldMargin * 3;

            panelWrapper.height = height + UiWarehouseTitleBar.Instance.height;


            panelWrapper.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);
            isVisible = panelWrapper.isVisible =
                UiWarehouseTitleBar.Instance.isVisible = UiWarehouseTitleBar.Instance.DragHandle.isVisible = true;
        }

        private void SetupUniqueFactoryPanel(float widestWidth, ref UIUniqueFactoryPanelWrapper panelWrapper)
        {
            width = panelWrapper.width =
                UiUniqueFactoryTitleBar.Instance.width =
                    UiUniqueFactoryTitleBar.Instance.DragHandle.width = widestWidth;
            UiUniqueFactoryTitleBar.Instance.RecenterElements();
            Align();
            height = Inputs.Count * (UiUtils.FieldHeight + UiUtils.FieldMargin) + UiUtils.FieldMargin * 3;

            panelWrapper.height = height + UiUniqueFactoryTitleBar.Instance.height;


            panelWrapper.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);
            isVisible = panelWrapper.isVisible =
                UiUniqueFactoryTitleBar.Instance.isVisible =
                    UiUniqueFactoryTitleBar.Instance.DragHandle.isVisible = true;
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

        private UIPanel Titlebar()
        {
            switch (CustomizeItExtendedTool.instance.PanelType)
            {
                case CustomizeItExtendedTool.InfoPanelType.Warehouse:
                    return UiWarehouseTitleBar.Instance;
                case CustomizeItExtendedTool.InfoPanelType.Factory:
                    return UiUniqueFactoryTitleBar.Instance;
                default:
                    return UiTitleBar.Instance;
            }
        }
    }
}