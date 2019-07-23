using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Vehicles;
using UnityEngine;

namespace CustomizeItExtended.Internal.Vehicles
{
    public class CustomizeItExtendedVehicleTool : Singleton<CustomizeItExtendedVehicleTool>
    {
        private UIButton _citizenButton;

        private UILabel _citizenDefaultLabel;

        private UICheckBox _citizenDefaultNameCheckbox;
        private UIButton _cityServiceButton;
        private UILabel _cityServiceDefaultLabel;
        private UICheckBox _cityServiceDefaultNameCheckbox;

        private bool _isButtonInitialized;

        private bool _isInitialized;
        private UIButton _publicTransportButton;
        private UILabel _publicTransportDefaultLabel;
        private UICheckBox _publicTransportDefaultNameCheckbox;
        private UIButton _touristButton;
        private UILabel _touristDefaultLabel;
        private UICheckBox _touristDefaultNameCheckbox;


        internal CitizenVehicleWorldInfoPanel CitizenVehicleWorldInfoPanel;

        internal CityServiceVehicleWorldInfoPanel CityServiceVehicleWorldInfoPanel;

        internal Dictionary<string, CustomVehicleProperties> CustomVehicleData =
            new Dictionary<string, CustomVehicleProperties>();

        internal Dictionary<string, NameProperties> CustomVehicleNames = new Dictionary<string, NameProperties>();

        internal Dictionary<string, CustomVehicleProperties> OriginalVehicleData =
            new Dictionary<string, CustomVehicleProperties>();

        internal Dictionary<string, NameProperties> OriginalVehicleNames = new Dictionary<string, NameProperties>();

        internal PublicTransportVehicleWorldInfoPanel PublicTransportWorldInfoPanel;

        internal UIButton ResetAll;

        internal InstanceID SelectedInstanceID;

        internal VehicleInfo SelectedVehicle;

        internal TouristVehicleWorldInfoPanel TouristVehicleInfoPanel;

        internal UIVehiclePanelWrapper VehiclePanelWrapper;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            AddPanelButton();
            _isInitialized = true;
        }

        public void Release()
        {
            _isButtonInitialized = false;
            _isInitialized = false;
        }

        public void SaveVehicle(VehicleInfo info)
        {
            if (!CustomVehicleData.TryGetValue(info.name, out CustomVehicleProperties props))
                CustomVehicleData.Add(info.name, new CustomVehicleProperties(info));
            else
                CustomVehicleData[info.name] = new CustomVehicleProperties(info);

            if (!CustomizeItExtendedMod.Settings.SavePerCity)
                CustomizeItExtendedMod.Settings.Save();
        }

        public void ResetVehicle(VehicleInfo info)
        {
            var originalProperties = info.GetOriginalProperties();

            if (CustomVehicleData.TryGetValue(info.name, out CustomVehicleProperties props))
                CustomVehicleData.Remove(info.name);

            info.LoadProperties(originalProperties);

            if (!CustomizeItExtendedMod.Settings.SavePerCity)
                CustomizeItExtendedMod.Settings.Save();
        }

        private void AddPanelButton()
        {
            if (_isButtonInitialized)
                return;

            CitizenVehicleWorldInfoPanel = GameObject.Find("(Library) CitizenVehicleWorldInfoPanel")
                .GetComponent<CitizenVehicleWorldInfoPanel>();

            if (CitizenVehicleWorldInfoPanel == null)
                return;

            AddVehicleCustomizeButton(CitizenVehicleWorldInfoPanel, out _citizenButton, new Vector3(341f, -35f, 0f));
            AddDefaultNameCheckbox(CitizenVehicleWorldInfoPanel, out _citizenDefaultNameCheckbox,
                new Vector3(-114f, 49f, 0));
            AddDefaultNameLabel(CitizenVehicleWorldInfoPanel, out _citizenDefaultLabel, new Vector3(-131f, 52f, 0));

            CityServiceVehicleWorldInfoPanel = GameObject.Find("(Library) CityServiceVehicleWorldInfoPanel")
                .GetComponent<CityServiceVehicleWorldInfoPanel>();

            if (CityServiceVehicleWorldInfoPanel == null)
                return;

            AddVehicleCustomizeButton(CityServiceVehicleWorldInfoPanel, out _cityServiceButton,
                new Vector3(290f, -63f, 0f));
            AddDefaultNameCheckbox(CityServiceVehicleWorldInfoPanel, out _cityServiceDefaultNameCheckbox,
                new Vector3(-148f, 41f, 0));
            AddDefaultNameLabel(CityServiceVehicleWorldInfoPanel, out _cityServiceDefaultLabel,
                new Vector3(-168f, 44f, 0));

            TouristVehicleInfoPanel = GameObject.Find("(Library) TouristVehicleWorldInfoPanel")
                .GetComponent<TouristVehicleWorldInfoPanel>();

            if (TouristVehicleInfoPanel == null)
                return;

            AddVehicleCustomizeButton(TouristVehicleInfoPanel, out _touristButton, new Vector3(0f, 25f, 0f));
            AddDefaultNameCheckbox(TouristVehicleInfoPanel, out _touristDefaultNameCheckbox,
                new Vector3(-269f, 156f, 0));
            AddDefaultNameLabel(TouristVehicleInfoPanel, out _touristDefaultLabel, new Vector3(-269f, 49f, 0));

            PublicTransportWorldInfoPanel = GameObject.Find("(Library) PublicTransportVehicleWorldInfoPanel")
                .GetComponent<PublicTransportVehicleWorldInfoPanel>();

            if (PublicTransportWorldInfoPanel == null)
                return;

            AddVehicleCustomizeButton(PublicTransportWorldInfoPanel, out _publicTransportButton,
                new Vector3(353f, -37f, 0f));
            AddDefaultNameCheckbox(PublicTransportWorldInfoPanel, out _publicTransportDefaultNameCheckbox,
                new Vector3(-344f, 40f, 0));
            AddDefaultNameLabel(PublicTransportWorldInfoPanel, out _publicTransportDefaultLabel,
                new Vector3(-197f, 43f, 0));
        }

        private void AddDefaultNameLabel(VehicleWorldInfoPanel infoPanel, out UILabel label, Vector3 offset)
        {
            label = UiUtils.CreateDefaultNameLabel(infoPanel.component, offset, UIAlignAnchor.TopRight);
        }

        private void AddDefaultNameCheckbox(VehicleWorldInfoPanel infoPanel, out UICheckBox checkBox, Vector3 offset)
        {
            checkBox = UiUtils.CreateDefaultNameCheckbox(infoPanel.component, offset, UIAlignAnchor.TopRight,
                (component, value) =>
                {
                    InstanceID instanceID = (InstanceID) infoPanel.GetType()
                        .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infoPanel);

                    var vehicle = VehicleManager.instance.m_vehicles.m_buffer[instanceID.Vehicle].Info;

                    try
                    {
                        if (!CustomVehicleNames.TryGetValue(vehicle.name, out NameProperties props))
                            return;

                        CustomVehicleNames[vehicle.name].DefaultName = value;
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"{e.Message} - {e.StackTrace}");
                    }
                });
        }

        private void AddVehicleCustomizeButton(VehicleWorldInfoPanel infoPanel, out UIButton button, Vector3 offset)
        {
            button = UiUtils.CreateToggleButton(infoPanel.component, offset, UIAlignAnchor.BottomLeft, (component, e) =>
            {
                InstanceID instanceID = (InstanceID) infoPanel.GetType()
                    .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infoPanel);

                SelectedInstanceID = instanceID;

                var vehicle = VehicleManager.instance.m_vehicles.m_buffer[instanceID.Vehicle].Info;

                try
                {
                    if (VehiclePanelWrapper == null || vehicle != SelectedVehicle)
                    {
                        VehiclePanelWrapper = vehicle.GenerateVehiclePanel();
                    }
                    else
                    {
                        VehiclePanelWrapper.isVisible = false;
                        UiUtils.DeepDestroy(VehiclePanelWrapper);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"{ex.Message} - {ex.StackTrace}");
                }

                if (component.hasFocus)
                    component.Unfocus();
            });
        }
    }
}