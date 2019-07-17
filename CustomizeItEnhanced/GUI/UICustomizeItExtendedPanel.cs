using ColossalFramework.PlatformServices;
using ColossalFramework.UI;
using CustomizeItExtended.GUI;
using CustomizeItExtended.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomizeItExtended
{
    public class UICustomizeItExtendedPanel : UIPanel
    {
        private BuildingInfo SelectedBuilding => CustomizeItExtendedTool.instance.CurrentSelectedBuilding;

        internal static UICustomizeItExtendedPanel Instance;

        private List<UILabel> Labels;

        internal List<UIComponent> Inputs;

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
            relativePosition = new UnityEngine.Vector3(0f, UITitleBar.Instance.height);
            width = parent.width;

            var ai = SelectedBuilding.m_buildingAI;

            var fields = ai.GetType().GetFields();

            var fieldsToRetrieve = typeof(Properties).GetFields().Select(x => x.Name);

            Inputs = new List<UIComponent>();

            Labels = new List<UILabel>();

            float widestWidth = 0f;

            foreach(var field in fields.Where(x => fieldsToRetrieve.Contains(x.Name)))
            {
                var label = AddUIComponent<UILabel>();
                label.name = field.Name + "Label";
                label.text = UIUtils.FieldNames[field.Name];
                label.textScale = 0.9f;
                label.isInteractive = false;

                if(field.FieldType == typeof(int) || field.FieldType == typeof(float))
                {
                    Inputs.Add(UIUtils.CreateTextField(this, field.Name));
                    Labels.Add(label);
                }
                else if(field.FieldType == typeof(bool))
                {
                    Inputs.Add(UIUtils.CreateCheckBox(this, field.Name));
                    Labels.Add(label);
                }

                if ((label.width + UIUtils.FieldWidth + (UIUtils.FieldMargin * 6)) > widestWidth)
                    widestWidth = label.width + UIUtils.FieldWidth + (UIUtils.FieldMargin * 6);
            }

            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            Labels.Sort((x, y) => x.name.CompareTo(y.name));

            Inputs.Add(UIUtils.CreateResetButton(this));
            width = UIPanelWrapper.Instance.width = UITitleBar.Instance.width = UITitleBar.Instance.dragHandle.width = widestWidth;
            UITitleBar.Instance.RecenterElements();
            Align();
            height = (Inputs.Count * (UIUtils.FieldHeight + UIUtils.FieldMargin)) + (UIUtils.FieldMargin * 3);
            UIPanelWrapper.Instance.height = height + UITitleBar.Instance.height;

            var anchor = CustomizeItExtendedTool.instance.ServiceBuildingPanel.component;

            UIPanelWrapper.Instance.relativePosition = new UnityEngine.Vector3(CustomizeItExtendedMod.Settings.PanelX, CustomizeItExtendedMod.Settings.PanelY);
            isVisible = UIPanelWrapper.Instance.isVisible = UITitleBar.Instance.isVisible = UITitleBar.Instance.dragHandle.isVisible = true;
        }

        private void Align()
        {
            float inputX = width - UIUtils.FieldWidth - (UIUtils.FieldMargin * 2);

            for(int x = 0; x < Inputs.Count; x++)
            {
                float finalY = (x * UIUtils.FieldHeight) + ((UIUtils.FieldMargin) * (x + 2));

                if(x < Labels.Count)
                {
                    float labelX = inputX - Labels[x].width - (UIUtils.FieldMargin * 2);
                    Labels[x].relativePosition = new UnityEngine.Vector3(labelX, finalY + 4);
                }
                Inputs[x].relativePosition = Inputs[x] is UICheckBox ? new Vector3(inputX + ((UIUtils.FieldWidth - Inputs[x].width) / 2), finalY + ((UIUtils.FieldHeight - Inputs[x].height) / 2)) : new Vector3(inputX, finalY);
            }
        }
        }
    }

