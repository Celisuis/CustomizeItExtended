using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using CustomizeItExtended.GUI.Buildings;
using CustomizeItExtended.GUI.Vehicles;
using CustomizeItExtended.Helpers;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Citizens;
using CustomizeItExtended.Internal.Vehicles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomizeItExtended.GUI
{
    public static class UiUtils
    {
        public const float FieldHeight = 23f;
        public const float FieldWidth = 100f;
        public const float FieldMargin = 5f;

        public static Dictionary<string, string> FieldNames = GetFieldNames();

        private static Dictionary<string, string> GetFieldNames()
        {
            return new Dictionary<string, string>
            {
                ["m_constructionCost"] = "Construction Cost",
                ["m_maintenanceCost"] = "Maintenance Cost",
                ["m_electricityConsumption"] = "Electricity Consumption",
                ["m_waterConsumption"] = "Water Consumption",
                ["m_sewageAccumulation"] = "Sewage Accumulation",
                ["m_garbageAccumulation"] = "Garbage Accumulation",
                ["m_fireHazard"] = "Fire Hazard",
                ["m_fireTolerance"] = "Fire Tolerance",
                ["m_visitPlaceCount0"] = "Low Wealth Tourists",
                ["m_visitPlaceCount1"] = "Medium Wealth Tourists",
                ["m_visitPlaceCount2"] = "High Wealth Tourists",
                ["m_entertainmentAccumulation"] = "Entertainment Accumulation",
                ["m_entertainmentRadius"] = "Entertainment Radius",
                ["m_workPlaceCount0"] = "Uneducated Workers",
                ["m_workPlaceCount1"] = "Educated Workers",
                ["m_workPlaceCount2"] = "Well Educated Workers",
                ["m_workPlaceCount3"] = "Highly Educated Workers",
                ["m_noiseAccumulation"] = "Noise Accumulation",
                ["m_noiseRadius"] = "Noise Radius",
                ["m_cargoTransportAccumulation"] = "Cargo Transport Accumulation",
                ["m_cargoTransportRadius"] = "Cargo Transport Radius",
                ["m_hearseCount"] = "Hearse Count",
                ["m_corpseCapacity"] = "Corpse Capacity",
                ["m_burialRate"] = "Burial Rate",
                ["m_graveCount"] = "Grave Count",
                ["m_deathCareAccumulation"] = "Deathcare Accumulation",
                ["m_deathCareRadius"] = "Deathcare Radius",
                ["m_helicopterCount"] = "Helicopter Count",
                ["m_vehicleCount"] = "Vehicle Count",
                ["m_detectionRange"] = "Detection Range",
                ["m_fireDepartmentAccumulation"] = "Fire Department Accumulation",
                ["m_fireDepartmentRadius"] = "Fire Department Radius",
                ["m_fireTruckCount"] = "Fire Truck Count",
                ["m_firewatchRadius"] = "Firewatch Radius",
                ["m_educationAccumulation"] = "Education Accumulation",
                ["m_educationRadius"] = "Education Radius",
                ["m_studentCount"] = "Student Count",
                ["m_resourceCapacity"] = "Resource Capacity",
                ["m_resourceConsumption"] = "Resource Consumption",
                ["m_heatingProduction"] = "Heating Production",
                ["m_pollutionAccumulation"] = "Pollution Accumulation",
                ["m_pollutionRadius"] = "Pollution Radius",
                ["m_ambulanceCount"] = "Ambulance Count",
                ["m_patientCapacity"] = "Patient Capacity",
                ["m_curingRate"] = "Curing Rate",
                ["m_healthCareAccumulation"] = "Healthcare Accumulation",
                ["m_healthCareRadius"] = "Healthcare Radius",
                ["m_animalCount"] = "Animal Count",
                ["m_collectRadius"] = "Garbage Collection Radius",
                ["m_electricityProduction"] = "Electricity Production",
                ["m_garbageCapacity"] = "Garbage Capacity",
                ["m_garbageConsumption"] = "Garbage Consumption",
                ["m_garbageTruckCount"] = "Garbage Truck Count",
                ["m_materialProduction"] = "Material Production",
                ["m_maintenanceRadius"] = "Maintenance Radius",
                ["m_maintenanceTruckCount"] = "Maintenance Truck Count",
                ["m_monumentLevel"] = "Monument Level",
                ["m_attractivenessAccumulation"] = "Attractiveness Accumulation",
                ["m_landValueAccumulation"] = "Land Value Accumulation",
                ["m_jailCapacity"] = "Jail Capacity",
                ["m_policeCarCount"] = "Police Car Count",
                ["m_policeDepartmentAccumulation"] = "Police Department Accumulation",
                ["m_policeDepartmentRadius"] = "Police Department Radius",
                ["m_sentenceWeeks"] = "Sentence Weeks",
                ["m_batteryFactor"] = "Battery Factor",
                ["m_transmitterPower"] = "Transmitter Power",
                ["m_capacity"] = "Capacity",
                ["m_disasterCoverageAccumulation"] = "Disaster Coverage Accumulation",
                ["m_electricityStockpileAmount"] = "Electricity Stockpile Size",
                ["m_electricityStockpileRate"] = "Electricity Stockpile Rate",
                ["m_waterStockpileAmount"] = "Water Stockpile Size",
                ["m_waterStockpileRate"] = "Water Stockpile Rate",
                ["m_goodsStockpileAmount"] = "Goods Stockpile Size",
                ["m_goodsStockpileRate"] = "Goods Stockpile Rate",
                ["m_snowCapacity"] = "Snow Capacity",
                ["m_snowConsumption"] = "Snow Consumption",
                ["m_snowTruckCount"] = "Snowplow Count",
                ["m_publicTransportAccumulation"] = "Public Transport Accumulation",
                ["m_publicTransportRadius"] = "Public Transport Radius",
                ["m_residentCapacity"] = "Resident Capacity",
                ["m_touristFactor0"] = "Tourist Factor 1",
                ["m_touristFactor1"] = "Tourist Factor 2",
                ["m_touristFactor2"] = "Tourist Factor 3",
                ["m_maxVehicleCount"] = "Max Vehicle Count",
                ["m_maxVehicleCount2"] = "Max Vehicle Count 2",
                ["m_cleaningRate"] = "Cleaning Rate",
                ["m_maxWaterDistance"] = "Max Water Distance",
                ["m_outletPollution"] = "Outlet Pollution",
                ["m_pumpingVehicles"] = "Pumping Vehicles",
                ["m_sewageOutlet"] = "Sewage Outlet",
                ["m_sewageStorage"] = "Sewage Storage",
                ["m_useGroundWater"] = "Use Ground Water",
                ["m_vehicleRadius"] = "Vehicle Radius",
                ["m_waterIntake"] = "Water Intake",
                ["m_waterOutlet"] = "Water Outlet",
                ["m_waterStorage"] = "Water Storage",
                ["m_serviceRadius"] = "Service Radius",
                ["m_serviceAccumulation"] = "Service Accumulation",
                ["m_sortingRate"] = "Sorting Rate",
                ["m_mailCapacity"] = "Mail Capacity",
                ["m_postTruckCount"] = "Post Truck Count",
                ["m_postVanCount"] = "Post Van Count",
                ["m_inputRate1"] = "Input Rate 1",
                ["m_inputRate2"] = "Input Rate 2",
                ["m_inputRate3"] = "Input Rate 3",
                ["m_inputRate4"] = "Input Rate 4",
                ["m_outputRate"] = "Output Rate",
                ["m_outputVehicleCount"] = "Output Vehicle Count",
                ["m_extractRadius"] = "Extract Radius",
                ["m_extractRate"] = "Extract Rate",
                ["m_storageCapacity"] = "Storage Capacity",
                ["m_truckCount"] = "Truck Count",
                ["m_bonusEffectRadius"] = "Bonus Effect Radius",
                ["m_landValueBonus"] = "Land Value Bonus",
                ["m_healthBonus"] = "Health Bonus",
                ["m_academicBoostBonus"] = "Academic Boost Bonus",
                ["m_tourismBonus"] = "Tourism Bonus",
                ["m_facultyBonusFactor"] = "Faculty Bonus Factor",
                ["m_campusAttractiveness"] = "Campus Attractiveness",
                ["m_acceleration"] = "Acceleration",
                ["m_braking"] = "Braking",
                ["m_turning"] = "Turning",
                ["m_maxSpeed"] = "Max Speed",
                ["m_springs"] = "Springs",
                ["m_dampers"] = "Dampers",
                ["m_nodMultiplier"] = "Nod Multiplier",
                ["m_leanMultiplier"] = "Lean Multiplier",
                ["m_attachOffsetFront"] = "Front Attach Offset",
                ["m_attachOffsetBack"] = "Back Attach Offset",
                ["m_maxTrailerCount"] = "Max Trailer Count",
                ["m_useColorVariations"] = "Color Variations",
                ["numEducatedWorkers"] = "Educated Workers",
                ["numHighlyEducatedWorkers"] = "Highly Educated Workers",
                ["numRooms"] = "Number of Rooms",
                ["capacityModifier"] = "Capacity Modifier",
                ["numUneducatedWorkers"] = "Uneducated Workers",
                ["numWellEducatedWorkers"] = "Well Educated Workers",
                ["operationRadius"] = "Operation Radius",
                ["quality"] = "Quality"
            };
        }

        public static void DeepDestroy(UIComponent comp)
        {
            if (comp == null)
                return;

            var childrenComps = comp.GetComponentsInChildren<UIComponent>();

            if (!(childrenComps?.Length > 0))
                return;

            foreach (var t in childrenComps)
                if (t.parent == comp)
                    DeepDestroy(t);

            Object.Destroy(comp);
            comp = null;
        }

        public static UIButton CreateResetButton(UIComponent parent)
        {
            var button = parent.AddUIComponent<UIButton>();
            button.name = "CustomizeItExtendedResetButton";
            button.text = "Reset";
            button.width = FieldWidth;
            button.height = FieldHeight;
            button.textPadding = new RectOffset(0, 0, 5, 0);
            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textScale = 0.8f;
            button.atlas = UIView.GetAView().defaultAtlas;
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenu";
            button.pressedBgSprite = "ButtonMenuPressed";

            button.eventClick += (x, y) =>
            {
                var selectedBuilding = CustomizeItExtendedTool.instance.CurrentSelectedBuilding;
                CustomizeItExtendedTool.instance.ResetBuilding(selectedBuilding);

                foreach (var input in UiCustomizeItExtendedPanel.Instance.Inputs)
                    switch (input)
                    {
                        case UITextField field:
                            field.text = selectedBuilding.m_buildingAI.GetType().GetField(field.name)
                                ?.GetValue(selectedBuilding.m_buildingAI)?.ToString();
                            break;
                        case UICheckBox box:
                            box.isChecked = (bool) selectedBuilding.m_buildingAI.GetType()
                                .GetField(box.name)?.GetValue(selectedBuilding.m_buildingAI);
                            break;
                    }
            };

            return button;
        }

        public static UIButton CreateVehicleResetButton(UIComponent parent)
        {
            var button = parent.AddUIComponent<UIButton>();
            button.name = "CustomizeItExtendedVehicleResetButton";
            button.text = "Reset";
            button.width = FieldWidth;
            button.height = FieldHeight;
            button.textPadding = new RectOffset(0, 0, 5, 0);
            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textScale = 0.8f;
            button.atlas = UIView.GetAView().defaultAtlas;
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenu";
            button.pressedBgSprite = "ButtonMenuPressed";

            button.eventClick += (x, y) =>
            {
                var selectedVehicle = CustomizeItExtendedVehicleTool.instance.SelectedVehicle;
                CustomizeItExtendedVehicleTool.instance.ResetVehicle(selectedVehicle);

                foreach (var input in UIVehiclePanel.Instance.Inputs)
                    switch (input)
                    {
                        case UITextField field:
                            field.text = selectedVehicle.GetType().GetField(field.name)
                                ?.GetValue(selectedVehicle)?.ToString();
                            break;
                        case UICheckBox box:
                            box.isChecked = (bool) selectedVehicle.GetType()
                                .GetField(box.name)?.GetValue(selectedVehicle);
                            break;
                    }
            };

            return button;
        }


        public static UIButton CreateToggleButton(UIComponent parentComponent, Vector3 offset, UIAlignAnchor anchor,
            MouseEventHandler handler)
        {
            var uibutton = UIView.GetAView().AddUIComponent(typeof(UIButton)) as UIButton;
            uibutton.name = "CustomizeItExtendedButton";
            uibutton.width = 26f;
            uibutton.height = 26f;
            uibutton.normalFgSprite = "Options";
            uibutton.disabledFgSprite = "OptionsDisabled";
            uibutton.hoveredFgSprite = "OptionsHovered";
            uibutton.focusedFgSprite = "OptionsFocused";
            uibutton.pressedFgSprite = "OptionsPressed";
            uibutton.normalBgSprite = "OptionBase";
            uibutton.disabledBgSprite = "OptionBaseDisabled";
            uibutton.hoveredBgSprite = "OptionBaseHovered";
            uibutton.focusedBgSprite = "OptionBaseFocused";
            uibutton.pressedBgSprite = "OptionBasePressed";
            uibutton.AlignTo(parentComponent, anchor);
            uibutton.eventClick += handler;
            uibutton.relativePosition += offset;

            return uibutton;
        }

        public static UIButton CreateMovableButton(UIComponent parent, Vector3 position, MouseEventHandler clickHandler)
        {
            var uibutton = parent.AddUIComponent<UIButton>();
            uibutton.name = "CustomizeItExtendedButton";
            uibutton.width = 26f;
            uibutton.height = 26f;
            uibutton.normalFgSprite = "Options";
            uibutton.disabledFgSprite = "OptionsDisabled";
            uibutton.hoveredFgSprite = "OptionsHovered";
            uibutton.focusedFgSprite = "OptionsFocused";
            uibutton.pressedFgSprite = "OptionsPressed";
            uibutton.normalBgSprite = "OptionBase";
            uibutton.disabledBgSprite = "OptionBaseDisabled";
            uibutton.hoveredBgSprite = "OptionBaseHovered";
            uibutton.focusedBgSprite = "OptionBaseFocused";
            uibutton.pressedBgSprite = "OptionBasePressed";
            uibutton.eventClick += clickHandler;
            uibutton.relativePosition = position;

            return uibutton;
        }

        public static UICheckBox CreateCheckBox(UIComponent parent, string fieldName)
        {
            var checkBox = parent.AddUIComponent<UICheckBox>();

            checkBox.name = fieldName;
            checkBox.width = 20f;
            checkBox.height = 20f;
            checkBox.relativePosition = Vector3.zero;

            var sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = new Vector3(2f, 2f);

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite) checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.eventCheckChanged += EventCheckChangedHandler;
            checkBox.isChecked = (bool) CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI.GetType()
                .GetField(fieldName).GetValue(CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI);
            return checkBox;
        }


        public static UICheckBox CreateVehicleCheckBox(UIComponent parent, string fieldName)
        {
            var checkBox = parent.AddUIComponent<UICheckBox>();

            checkBox.name = fieldName;
            checkBox.width = 20f;
            checkBox.height = 20f;
            checkBox.relativePosition = Vector3.zero;

            var sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = new Vector3(2f, 2f);

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite) checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.eventCheckChanged += VehicleCheckChangedHandler;
            checkBox.isChecked = (bool) CustomizeItExtendedVehicleTool.instance.SelectedVehicle.GetType()
                .GetField(fieldName).GetValue(CustomizeItExtendedVehicleTool.instance.SelectedVehicle);

            return checkBox;
        }

        public static UICheckBox CreateDefaultNameCheckbox(UIComponent parent, Vector3 offset, UIAlignAnchor anchor,
            PropertyChangedEventHandler<bool> handler)
        {
            var checkBox = parent.AddUIComponent<UICheckBox>();

            checkBox.name = "DefaultNameCheckbox";
            checkBox.width = 20f;
            checkBox.height = 20f;
            checkBox.relativePosition = Vector3.zero;

            var sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = new Vector3(2f, 2f);

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite) checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.eventCheckChanged += handler;
            checkBox.AlignTo(parent, anchor);
            checkBox.relativePosition += offset;
            checkBox.text = "Use as Default Name?";

            return checkBox;
        }

        public static UILabel CreateDefaultNameLabel(UIComponent parent, Vector3 offset, UIAlignAnchor anchor)
        {
            var label = parent.AddUIComponent<UILabel>();

            label.name = "DefaultNameLabel";
            label.text = "Use as Default Name?";
            label.textScale = 0.8f;

            label.AlignTo(parent, anchor);
            label.relativePosition += offset;

            return label;
        }

        private static void EventCheckChangedHandler(UIComponent component, bool value)
        {
            var ai = CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI;
            var type = ai.GetType();
            type.GetField(component.name)?.SetValue(ai, value);
        }

        private static void VehicleCheckChangedHandler(UIComponent comp, bool value)
        {
            var info = CustomizeItExtendedVehicleTool.instance.SelectedVehicle;

            info.GetType().GetField(comp.name)?.SetValue(info, value);
        }

        private static void DefaultNameChangedHandler(UIComponent comp, bool value)
        {
            CustomizeItExtendedTool.instance
                .CustomBuildingNames[CustomizeItExtendedTool.instance.CurrentSelectedBuilding.name].DefaultName = value;
        }

        public static UITextField CreateTextField(UIComponent parent, string fieldName)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventKeyPress += EventKeyPressedHandler;
            textField.eventTextSubmitted += EventTextSubmittedHandler;
            textField.text = CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI.GetType()
                .GetField(fieldName).GetValue(CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI)
                .ToString();

            return textField;
        }

        public static UITextField CreateVehicleTextField(UIComponent parent, string fieldName)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventKeyPress += EventKeyPressedHandler;
            textField.eventTextSubmitted += VehicleEventSubmittedHandler;
            textField.text = CustomizeItExtendedVehicleTool.instance.SelectedVehicle.GetType()
                .GetField(fieldName).GetValue(CustomizeItExtendedVehicleTool.instance.SelectedVehicle)
                .ToString();

            return textField;
        }

        public static UITextField CreateNameTextfield(UIComponent parent, string fieldName,
            PropertyChangedEventHandler<string> handler, string defaultText)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventTextSubmitted += handler;
            textField.text = defaultText;

            return textField;
        }


        public static UITextField CreateCitizenInputField(UIComponent parent, string fieldName,
            PropertyChangedEventHandler<string> handler)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventTextSubmitted += handler;
            textField.tooltip = textField.text;

            return textField;
        }

        public static UITextField CreateCitizenJobField(UIComponent parent, string fieldName,
            PropertyChangedEventHandler<string> handler)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventTextSubmitted += handler;


            if (CustomizeItExtendedCitizenTool.instance.CustomJobTitles.TryGetValue(
                CustomizeItExtendedCitizenTool.instance.SelectedCitizen, out string title))
            {
                textField.text = title;
            }
            else
            {
                if (CustomizeItExtendedCitizenTool.instance.OriginalJobTitles.TryGetValue(
                    CustomizeItExtendedCitizenTool.instance.SelectedCitizen, out string originalTitle))
                {
                    textField.text = originalTitle;
                }
                else
                {
                    var defaultTitle =
                        CitizenHelper.GetDefaultJobTitle(CustomizeItExtendedCitizenTool.instance.SelectedCitizen);
                    textField.text = defaultTitle;

                    CustomizeItExtendedCitizenTool.instance.OriginalJobTitles.Add(
                        CustomizeItExtendedCitizenTool.instance.SelectedCitizen, defaultTitle);
                }
            }

            textField.tooltip = textField.text;
            return textField;
        }

        public static UITextField CreateInfoField(UIComponent parent, string fieldName, int value = -201,
            string secondaryValue = null)
        {
            var textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = FieldWidth;
            textField.height = FieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventKeyPress += EventKeyPressedHandler;
            textField.eventTextSubmitted += EventTextSubmittedHandler;
            if (value != -201)
            {
                textField.text = $"{value}";
            }
            else
            {
                if (!string.IsNullOrEmpty(secondaryValue))
                    textField.text = secondaryValue;
            }

            textField.tooltip = !string.IsNullOrEmpty(secondaryValue) ? secondaryValue : $"{value}";
            return textField;
        }

        public static UIDropDown CreateDropdown(UIComponent parent, string fieldName, string[] items,
            PropertyChangedEventHandler<int> handler, string defaultItem)
        {
            var dropdown = parent.AddUIComponent<UIDropDown>();

            dropdown.listBackground = "GenericPanelLight";
            dropdown.itemHeight = 30;
            dropdown.itemHover = "ListItemHover";
            dropdown.itemHighlight = "ListItemHighlight";
            dropdown.normalBgSprite = "ButtonMenu";
            dropdown.disabledBgSprite = "ButtonMenuDisabled";
            dropdown.hoveredBgSprite = "ButtonMenuHovered";
            dropdown.focusedBgSprite = "ButtonMenu";
            dropdown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropdown.popupColor = new Color32(45, 52, 61, 255);
            dropdown.popupTextColor = new Color32(170, 170, 170, 255);
            dropdown.name = fieldName;
            dropdown.builtinKeyNavigation = true;
            dropdown.isInteractive = true;
            dropdown.width = 140f;
            dropdown.listWidth = (int) dropdown.width;
            dropdown.height = FieldHeight;
            dropdown.textScale = 0.75f;
            dropdown.textColor = Color.white;
            dropdown.textFieldPadding = new RectOffset(15, 0, 5, 0);
            dropdown.itemPadding = new RectOffset(28, 0, 5, 0);

            dropdown.items = items;
            dropdown.selectedIndex = Array.IndexOf(dropdown.items, defaultItem);
            dropdown.eventSelectedIndexChanged += handler;

            dropdown.tooltip = dropdown.selectedValue + Environment.NewLine +
                               "If the vehicle you're looking for isn't listed here, try using Absolute Names in the options menu and reload this panel.";

            UIButton button = dropdown.AddUIComponent<UIButton>();
            dropdown.triggerButton = button;
            button.text = "";
            button.size = dropdown.size;
            button.relativePosition = new Vector3(0f, 0f);
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Left;
            button.normalFgSprite = "IconDownArrow";
            button.hoveredFgSprite = "IconDownArrowHovered";
            button.pressedFgSprite = "IconDownArrowPressed";
            button.focusedFgSprite = "IconDownArrowFocused";
            button.disabledFgSprite = "IconDownArrowDisabled";
            button.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            button.horizontalAlignment = UIHorizontalAlignment.Right;
            button.verticalAlignment = UIVerticalAlignment.Middle;


            dropdown.eventSizeChanged += (component, value) =>
            {
                button.size = value;
                dropdown.width = value.x;
            };

            return dropdown;
        }

        private static void EventTextSubmittedHandler(UIComponent component, string value)
        {
            if (!int.TryParse(value, out var result))
                return;

            switch (component.name)
            {
                case "m_campusAttractiveness" when !uint.TryParse(value, out uint uintResult):
                case "quality" when result > 5 && result < 0:
                    return;
            }

            var ai = CustomizeItExtendedTool.instance.CurrentSelectedBuilding.m_buildingAI;
            var type = ai.GetType();

            if (component.name.IndexOf("capacity", StringComparison.OrdinalIgnoreCase) >= 0 && result == 0)
            {
                result = 1;
                ((UITextField) component).text = "1";
            }
            


            type.GetField(component.name)?.SetValue(ai, result);
        }

        public static void VehicleEventSubmittedHandler(UIComponent component, string value)
        {
            if (!int.TryParse(value, out var result))
                return;

            var vehicle = CustomizeItExtendedVehicleTool.instance.SelectedVehicle;

            vehicle.GetType().GetField(component.name)?.SetValue(vehicle, result);
        }

        private static void EventKeyPressedHandler(UIComponent component, UIKeyEventParameter eventParam)
        {
            if (!char.IsControl(eventParam.character) && !char.IsDigit(eventParam.character))
                eventParam.Use();
        }
    }
}