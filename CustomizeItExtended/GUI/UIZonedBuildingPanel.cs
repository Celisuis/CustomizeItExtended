using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework.Math;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using UnityEngine;

namespace CustomizeItExtended.GUI
{
    public class UIZonedBuildingPanel : UIPanel
    {
        public static UIZonedBuildingPanel Instance;

        private List<UILabel> _labels;

        private TransferManager.TransferReason _transferReason;

        public int ElectricityConsumption;

        public int FireHazard;

        public int FireTolerance;
        public int GarbageAccumulation;
        public int IncomeAccumulation;

        public string IncomingResource;

        internal List<UIComponent> Inputs;

        public int Level;

        public string LevelUpInfo;
        public float LevelUpProgress;
        public int MailAccumulation;

        public int NoisePollution;
        public int Pollution;

        public string ProducedProduct;
        public int SewageAccumulation;
        public int WaterConsumption;

        private BuildingInfo SelectedBuilding => CustomizeItExtendedTool.instance.CurrentSelectedBuilding;

        public override void Start()
        {
            base.Start();
            Instance = this;
            Setup();
        }

        private void Setup()
        {
            name = "CustomizeItExtendedInformationPanel";
            isVisible = false;
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = new Vector3(0f, UiInfoTitleBar.Instance.height);
            width = parent.width;

            Inputs = new List<UIComponent>();

            _labels = new List<UILabel>();

            float widestWidth = 0f;

            var ai = SelectedBuilding.m_buildingAI;

            var instanceId = (InstanceID) CustomizeItExtendedTool.instance.ZoneBuildingPanel.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(CustomizeItExtendedTool.instance.ZoneBuildingPanel);

            var building = BuildingManager.instance.m_buildings.m_buffer[instanceId.Building];

            SetupRates(ai, building, instanceId, ref widestWidth);
            SetupLevelInfo(ref widestWidth);


            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            _labels.Sort((x, y) => x.name.CompareTo(y.name));

            width = UIZonedBuildingPanelWrapper.Instance.width = UiInfoTitleBar.Instance.width =
                UiInfoTitleBar.Instance.DragHandle.width = widestWidth;

            UiInfoTitleBar.Instance.RecenterElements();
            Align();

            UIZonedBuildingPanelWrapper.Instance.height = height + UiInfoTitleBar.Instance.height;

            UIZonedBuildingPanelWrapper.Instance.relativePosition = new Vector3(CustomizeItExtendedMod.Settings.PanelX,
                CustomizeItExtendedMod.Settings.PanelY);

            isVisible = UIZonedBuildingPanelWrapper.Instance.isVisible =
                UiInfoTitleBar.Instance.isVisible = UiInfoTitleBar.Instance.DragHandle.isVisible = true;
        }


        private void SetupLevelInfo(ref float widestWidth)
        {
            var levelUpInfoLabel = AddUIComponent<UILabel>();
            levelUpInfoLabel.name = "Level Up Info Label";
            levelUpInfoLabel.text = "Level Up Requirement";
            levelUpInfoLabel.textScale = 0.9f;
            levelUpInfoLabel.isInteractive = false;

            Inputs.Add(UiUtils.CreateInfoField(this, "LevelUpInfo", -201, LevelUpInfo));
            _labels.Add(levelUpInfoLabel);

            if (levelUpInfoLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = levelUpInfoLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;

            var levelUpProgressLabel = AddUIComponent<UILabel>();
            levelUpProgressLabel.name = "Level Up Progress Label";
            levelUpProgressLabel.text = "Level Up Progress";
            levelUpProgressLabel.textScale = 0.9f;
            levelUpProgressLabel.isInteractive = false;

            Inputs.Add(UiUtils.CreateInfoField(this, "LevelUpProgress", -201,
                $"{Convert.ToInt32(LevelUpProgress * 100)}%"));
            _labels.Add(levelUpProgressLabel);

            if (levelUpProgressLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                widestWidth = levelUpProgressLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;

            var levelLabel = AddUIComponent<UILabel>();
            levelLabel.name = "Building Level Label";
            levelLabel.text = "Building Level";
            levelLabel.textScale = 0.9f;
            levelLabel.isInteractive = false;

            Inputs.Add(UiUtils.CreateInfoField(this, "BuildingLevel", -201, $"{SelectedBuilding.GetClassLevel()}"));
            _labels.Add(levelLabel);
        }

        private void SetupRates(BuildingAI ai, Building building, InstanceID instanceId, ref float widestWidth)
        {
            bool _ = ai.GetFireParameters(instanceId.Building, ref building, out FireHazard, out int _,
                out FireTolerance);

            switch (SelectedBuilding.m_class.m_service)
            {
                case ItemClass.Service.Industrial:
                {
                    var industrialAI = (IndustrialBuildingAI) ai;


                    ElectricityConsumption = industrialAI.GetElectricityRate(instanceId.Building, ref building);
                    WaterConsumption = industrialAI.GetWaterRate(instanceId.Building, ref building);
                    IncomeAccumulation = industrialAI.GetResourceRate(instanceId.Building, ref building,
                        EconomyManager.Resource.PrivateIncome);
                    GarbageAccumulation = industrialAI.GetGarbageRate(instanceId.Building, ref building);
                    LevelUpInfo =
                        industrialAI.GetLevelUpInfo(instanceId.Building, ref building, out LevelUpProgress);

                    Pollution = industrialAI.GetResourceRate(instanceId.Building, ref building,
                        NaturalResourceManager.Resource.Pollution);
                    NoisePollution = industrialAI.GetResourceRate(instanceId.Building, ref building,
                        ImmaterialResourceManager.Resource.NoisePollution);

                    var transferMethod = industrialAI.GetType().GetMethod("GetOutgoingTransferReason",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                    _transferReason = (TransferManager.TransferReason) transferMethod.Invoke(industrialAI, null);

                    ProducedProduct = _transferReason.ToString();
                }
                    break;
                case ItemClass.Service.Commercial:
                {
                    var commercialAI = (CommercialBuildingAI) ai;

                    LevelUpInfo =
                        commercialAI.GetLevelUpInfo(instanceId.Building, ref building, out LevelUpProgress);


                    Pollution = commercialAI.GetResourceRate(instanceId.Building, ref building,
                        NaturalResourceManager.Resource.Pollution);
                    NoisePollution = commercialAI.GetResourceRate(instanceId.Building, ref building,
                        ImmaterialResourceManager.Resource.NoisePollution);


                    ElectricityConsumption = commercialAI.GetElectricityRate(instanceId.Building, ref building);
                    WaterConsumption = commercialAI.GetWaterRate(instanceId.Building, ref building);
                    IncomeAccumulation = commercialAI.GetResourceRate(instanceId.Building, ref building,
                        EconomyManager.Resource.PrivateIncome);
                    GarbageAccumulation = commercialAI.GetGarbageRate(instanceId.Building, ref building);

                    IncomingResource = commercialAI.m_incomingResource.ToString();

                    var transferMethod = commercialAI.GetType().GetMethod("GetOutgoingTransferReason",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                    _transferReason = (TransferManager.TransferReason) transferMethod.Invoke(commercialAI, null);

                    ProducedProduct = _transferReason.ToString();
                }
                    break;

                case ItemClass.Service.Residential:
                {
                    var residentialAI = (ResidentialBuildingAI) ai;
/*
                        ElectricityConsumption = residentialAI.GetElectricityRate(instanceId.Building, ref building);
                        WaterConsumption = residentialAI.GetWaterRate(instanceId.Building, ref building);
                        IncomeAccumulation = residentialAI.GetResourceRate(instanceId.Building, ref building,
                            EconomyManager.Resource.PrivateIncome);
                        GarbageAccumulation = residentialAI.GetGarbageRate(instanceId.Building, ref building);
                        */

                    residentialAI.GetConsumptionRates(SelectedBuilding.GetClassLevel(),
                        new Randomizer(instanceId.Building), 100, out ElectricityConsumption, out WaterConsumption,
                        out SewageAccumulation, out GarbageAccumulation, out IncomeAccumulation, out MailAccumulation);

                    LevelUpInfo =
                        residentialAI.GetLevelUpInfo(instanceId.Building, ref building, out LevelUpProgress);


                    Pollution = residentialAI.GetResourceRate(instanceId.Building, ref building,
                        NaturalResourceManager.Resource.Pollution);
                    NoisePollution = residentialAI.GetResourceRate(instanceId.Building, ref building,
                        ImmaterialResourceManager.Resource.NoisePollution);
                }
                    break;
            }


            Dictionary<string, int> _consumptionRates = new Dictionary<string, int>
            {
                {"Electricity Consumption", ElectricityConsumption},
                {"Water Consumption", WaterConsumption},
                {"Sewage Accumulation", SewageAccumulation},
                {"Garbage Accumulation", GarbageAccumulation},
                {"Income Accumulation", IncomeAccumulation},
                {"Pollution", Pollution},
                {"Noise Pollution", NoisePollution},
                {"Fire Hazard", FireHazard},
                {"Fire Tolerance", FireTolerance},
                {"Mail Accumulation", MailAccumulation}
            };


            foreach (var kvp in _consumptionRates)
            {
                var label = AddUIComponent<UILabel>();
                label.name = kvp.Key + "Label";
                label.text = kvp.Key;
                label.textScale = 0.9f;
                label.isInteractive = false;

                Inputs.Add(UiUtils.CreateInfoField(this, kvp.Key, kvp.Value));
                _labels.Add(label);

                if (label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                    widestWidth = label.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
            }

            if (!string.IsNullOrEmpty(IncomingResource))
            {
                var resourceLabel = AddUIComponent<UILabel>();
                resourceLabel.name = "IncomingResourceLabel";
                resourceLabel.text = "Incoming Resource";
                resourceLabel.textScale = 0.9f;
                resourceLabel.isInteractive = false;

                Inputs.Add(UiUtils.CreateInfoField(this, "IncomingResource", -201, IncomingResource));
                _labels.Add(resourceLabel);


                if (resourceLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                    widestWidth = resourceLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
            }

            if (!string.IsNullOrEmpty(ProducedProduct))
            {
                var producedLabel = AddUIComponent<UILabel>();
                producedLabel.name = "OutgoingResource";
                producedLabel.text = "Outgoing Resource";
                producedLabel.textScale = 0.9f;
                producedLabel.isInteractive = false;

                Inputs.Add(UiUtils.CreateInfoField(this, "OutgoingResource", -201, ProducedProduct));
                _labels.Add(producedLabel);


                if (producedLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6 > widestWidth)
                    widestWidth = producedLabel.width + UiUtils.FieldWidth + UiUtils.FieldMargin * 6;
            }
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