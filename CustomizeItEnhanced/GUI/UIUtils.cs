﻿using ColossalFramework.UI;
using CustomizeItEnhanced.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomizeItEnhanced
{
    public static class UIUtils
    {
        public const float FieldHeight = 23f;
        public const float FieldWidth = 100f;
        public const float FieldMargin = 5f;

        public static Dictionary<string, string> FieldNames = GetFieldNames();

        private static Dictionary<string, string> GetFieldNames()
        {
            return new Dictionary<string, string>()
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
                ["m_tourismBonus"] = "Tourism Bonus",
                ["m_facultyBonusFactor"] = "Faculty Bonus Factor",

            };
        }

        public static void DeepDestroy(UIComponent comp)
        {
            if (comp == null)
                return;

            UIComponent[] childrenComps = comp.GetComponentsInChildren<UIComponent>();

            if(childrenComps != null && childrenComps.Length > 0)
            {
                for(int i = 0; i < childrenComps.Length; i++)
                {
                    if (childrenComps[i].parent == comp)
                        DeepDestroy(childrenComps[i]);
                }
                GameObject.Destroy(comp);
                comp = null;
            }
        }

        public static UIButton CreateResetButton(UIComponent parent)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = "CustomizeItEnhancedResetButton";
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
                var selectedBuilding = CustomizeItEnhancedTool.instance.CurrentSelectedBuilding;
                CustomizeItEnhancedTool.instance.ResetBuilding(selectedBuilding);

                foreach (var input in UICustomizeItEnhancedPanel.Instance.Inputs)
                {
                    if (input is UITextField)
                    {
                        ((UITextField)input).text = selectedBuilding.m_buildingAI.GetType().GetField(input.name)?.GetValue(selectedBuilding.m_buildingAI)?.ToString();
                    }
                    else if (input is UICheckBox)
                    {
                        ((UICheckBox)input).isChecked = (bool)selectedBuilding.m_buildingAI.GetType().GetField(input.name)?.GetValue(selectedBuilding.m_buildingAI);
                    }
                }
            };

            return button;
        }

        public static UIButton CreateToggleButton(UIComponent parentComponent, Vector3 offset, UIAlignAnchor anchor, MouseEventHandler handler)
        {
            UIButton uibutton = UIView.GetAView().AddUIComponent(typeof(UIButton)) as UIButton;
            uibutton.name = "CustomizeItEnhancedButton";
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
            uibutton.eventClick += handler;
            uibutton.AlignTo(parentComponent, anchor);
            uibutton.relativePosition += offset;
            return uibutton;
        }

        public static UICheckBox CreateCheckBox(UIComponent parent, string fieldName)
        {
            UICheckBox checkBox = parent.AddUIComponent<UICheckBox>();

            checkBox.name = fieldName;
            checkBox.width = 20f;
            checkBox.height = 20f;
            checkBox.relativePosition = Vector3.zero;

            UISprite sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = new Vector3(2f, 2f);

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite)checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.eventCheckChanged += EventCheckChangedHandler;
            checkBox.isChecked = (bool)CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI.GetType().GetField(fieldName).GetValue(CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI);
            return checkBox;
        }

        private static void EventCheckChangedHandler(UIComponent component, bool value)
        {
            var ai = CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI;
            var type = ai.GetType();
            type.GetField(component.name)?.SetValue(ai, value);
        }
        public static UITextField CreateTextField(UIComponent parent, string fieldName)
        {
            UITextField textField = parent.AddUIComponent<UITextField>();

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
            textField.text = CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI.GetType().GetField(fieldName).GetValue(CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI).ToString();

            return textField;
        }

        private static void EventTextSubmittedHandler(UIComponent component, string value)
        {
            if (int.TryParse(value, out int result))
            {
                var ai = CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.m_buildingAI;
                var type = ai.GetType();
                if (component.name.IndexOf("capacity", StringComparison.OrdinalIgnoreCase) >= 0 && result == 0)
                {
                    result = 1;
                    ((UITextField)component).text = "1";
                }
                type.GetField(component.name)?.SetValue(ai, result);
            }
        }

        private static void EventKeyPressedHandler(UIComponent component, UIKeyEventParameter eventParam)
        {
            if (!char.IsControl(eventParam.character) && !char.IsDigit(eventParam.character))
                eventParam.Use();
        }
    }
}

