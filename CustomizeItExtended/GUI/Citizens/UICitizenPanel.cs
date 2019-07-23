using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using CustomizeItExtended.Helpers;
using CustomizeItExtended.Internal.Citizens;
using UnityEngine;

namespace CustomizeItExtended.GUI.Citizens
{
    public class UICitizenPanel : UIPanel
    {
        private List<UILabel> _labels;

        internal List<UIComponent> Inputs;
        internal UICitizenPanel Instance;

        public uint SelectedCitizenID => CustomizeItExtendedCitizenTool.instance.SelectedCitizen;

        internal Citizen SelectedCitizen => CitizenManager.instance.m_citizens.m_buffer[SelectedCitizenID];

        public override void Start()
        {
            base.Start();
            Instance = this;
            Setup();
        }

        private void Setup()
        {
            name = "CustomizeItExtendedCitizenPanel";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = new Vector3(0f, UiCitizenTitleBar.Instance.height);
            width = parent.width;

            Inputs = new List<UIComponent>();

            _labels = new List<UILabel>();

            var widestWidth = 0f;

            try
            {
                JobTitle(ref widestWidth);
                Age(ref widestWidth);
                CriminalStatus(ref widestWidth);
                Education(ref widestWidth);
                Wealth(ref widestWidth);
                Gender(ref widestWidth);
                Health(ref widestWidth);
                Wellbeing(ref widestWidth);
                Happiness(ref widestWidth);
                WorkEfficiency(ref widestWidth);
                IncomeRate(ref widestWidth);
                CrimeRate(ref widestWidth);
                WorkProbability(ref widestWidth);
                AgeGroup(ref widestWidth);
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
            }

            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            _labels.Sort((x, y) => x.name.CompareTo(y.name));

            width = UICitizenPanelWrapper.Instance.width =
                UiCitizenTitleBar.Instance.width = UiCitizenTitleBar.Instance.DragHandle.width = widestWidth;
            UiCitizenTitleBar.Instance.RecenterElements();
            Align();
            height = Inputs.Count * (UiUtils.FieldHeight + UiUtils.FieldMargin) + UiUtils.FieldMargin * 3;

            UICitizenPanelWrapper.Instance.height = height + UiCitizenTitleBar.Instance.height;

            UICitizenPanelWrapper.Instance.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);

            isVisible = UICitizenPanelWrapper.Instance.isVisible =
                UiCitizenTitleBar.Instance.isVisible = UiCitizenTitleBar.Instance.DragHandle.isVisible = true;
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

        private void JobTitle(ref float widestWidth)
        {
            var jobTitleLabel = AddUIComponent<UILabel>();
            jobTitleLabel.name = "JobTitleLabel";
            jobTitleLabel.text = "Job Title";
            jobTitleLabel.textScale = 0.9f;
            jobTitleLabel.isInteractive = false;

            var jobTitleInputField = UiUtils.CreateCitizenJobField(this, "JobTitle", (component, value) =>
            {
                if (!CustomizeItExtendedCitizenTool.instance.CustomJobTitles.TryGetValue(SelectedCitizenID,
                    out string _))
                    CustomizeItExtendedCitizenTool.instance.CustomJobTitles.Add(SelectedCitizenID, value);
                else
                    CustomizeItExtendedCitizenTool.instance.CustomJobTitles[SelectedCitizenID] = value;
            });

            Inputs.Add(jobTitleInputField);
            _labels.Add(jobTitleLabel);

            if (jobTitleLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = jobTitleLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Age(ref float widestWidth)
        {
            var ageLabel = AddUIComponent<UILabel>();
            ageLabel.name = "AgeLabel";
            ageLabel.text = "Age";
            ageLabel.textScale = 0.9f;
            ageLabel.isInteractive = false;

            var ageInfoField = UiUtils.CreateCitizenInputField(this, "Age", (component, value) => { });
            ageInfoField.text = SelectedCitizen.Age.ToString();
            ageInfoField.isInteractive = false;
            ageInfoField.tooltip = ageInfoField.text;

            Inputs.Add(ageInfoField);
            _labels.Add(ageLabel);


            if (ageLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = ageLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void CriminalStatus(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "CriminalLabel";
            label.text = "Criminal Status";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Criminal", (component, value) => { });
            inputField.text = SelectedCitizen.Criminal.ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;
            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Education(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "EducationLevelLabel";
            label.text = "Education Level";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Education", (component, value) => { });
            inputField.text = CitizenHelper.GetEducationText(SelectedCitizen.EducationLevel);
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Wealth(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "WealthLabel";
            label.text = "Wealth Status";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Wealth", (component, value) => { });
            inputField.text = SelectedCitizen.WealthLevel.ToString();
            inputField.isInteractive = false;

            inputField.tooltip = inputField.text;
            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Gender(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "GenderLevel";
            label.text = "Gender";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Gender", (component, value) => { });
            inputField.text = Citizen.GetGender(SelectedCitizenID).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Health(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "HealthLabel";
            label.text = "Health Level";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Health", (component, value) => { });
            inputField.text = CitizenHelper.GetHealthText(Citizen.GetHealthLevel(SelectedCitizen.m_health));
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Wellbeing(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "WellbeingLabel";
            label.text = "Wellbeing Level";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Wellbeing", (component, value) => { });
            inputField.text =
                CitizenHelper.GetWellbeingText(Citizen.GetWellbeingLevel(SelectedCitizen.EducationLevel,
                    SelectedCitizen.m_wellbeing));
            inputField.isInteractive = false;

            inputField.tooltip = inputField.text;
            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void Happiness(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "HappinessLabel";
            label.text = "Happiness Level";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "Happiness", (component, value) => { });
            inputField.text = Citizen
                .GetHappinessLevel(Citizen.GetHappiness(SelectedCitizen.m_health, SelectedCitizen.m_wellbeing))
                .ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void WorkEfficiency(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "WorkEfficiencyLabel";
            label.text = "Work Efficiency";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "WorkEfficiency", (component, value) => { });
            inputField.text = Citizen.GetWorkEfficiency(Citizen.GetHealthLevel(SelectedCitizen.m_health)).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void IncomeRate(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "IncomeRateLabel";
            label.text = "Income Rate";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "IncomeRate", (component, value) => { });
            inputField.text = Citizen
                .GetIncomeRate(Citizen.GetAgePhase(SelectedCitizen.EducationLevel, SelectedCitizen.Age),
                    SelectedCitizen.Unemployed).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void CrimeRate(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "CrimeRateLabel";
            label.text = "Crime Rate";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "CrimeRate", (component, value) => { });
            inputField.text = Citizen
                .GetCrimeRate(Citizen.GetWellbeingLevel(SelectedCitizen.EducationLevel, SelectedCitizen.m_wellbeing),
                    SelectedCitizen.Unemployed, SelectedCitizen.Criminal).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void WorkProbability(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "WorkProbabilityLabel";
            label.text = "Work Probability";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "WorkProbability", (component, value) => { });
            inputField.text = Citizen
                .GetWorkProbability(
                    Citizen.GetWellbeingLevel(SelectedCitizen.EducationLevel, SelectedCitizen.m_wellbeing),
                    SelectedCitizen.WealthLevel).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }

        private void AgeGroup(ref float widestWidth)
        {
            var label = AddUIComponent<UILabel>();
            label.name = "AgeGroupLabel";
            label.text = "Age Group";
            label.textScale = 0.9f;
            label.isInteractive = false;

            var inputField = UiUtils.CreateCitizenInputField(this, "AgeGroup", (component, value) => { });
            inputField.text = Citizen.GetAgeGroup(SelectedCitizen.Age).ToString();
            inputField.isInteractive = false;
            inputField.tooltip = inputField.text;

            Inputs.Add(inputField);
            _labels.Add(label);

            if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
        }
    }
}