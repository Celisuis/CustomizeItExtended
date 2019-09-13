using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Citizens;
using CustomizeItExtended.Helpers;
using UnityEngine;

namespace CustomizeItExtended.Internal.Citizens
{
    public class CustomizeItExtendedCitizenTool : Singleton<CustomizeItExtendedCitizenTool>
    {
        private UIButton _citizenCustomizeButton;

        private bool _isButtonInitialized;

        private bool _isInitialized;

        internal UICitizenPanelWrapper CitizenPanelWrapper;

        internal CitizenWorldInfoPanel CitizenWorldInfoPanel;
        internal Dictionary<uint, string> CustomJobTitles = new Dictionary<uint, string>();

        internal Dictionary<uint, string> OriginalJobTitles = new Dictionary<uint, string>();

        internal UIButton ResetAllButton;

        internal uint SelectedCitizen;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            AddPanelButton();
            _isInitialized = true;
        }

        public void AddPanelButton()
        {
            if (_isButtonInitialized)
                return;

            CitizenWorldInfoPanel =
                GameObject.Find("(Library) CitizenWorldInfoPanel").GetComponent<CitizenWorldInfoPanel>();

            if (CitizenWorldInfoPanel == null)
                return;

            AddButton(CitizenWorldInfoPanel, out _citizenCustomizeButton, new Vector3(240f, -31f, 0f));

            _isButtonInitialized = true;
        }

        private void AddButton(CitizenWorldInfoPanel infoPanel, out UIButton button, Vector3 offSet)
        {
            button = UiUtils.CreateToggleButton(infoPanel.component, offSet, UIAlignAnchor.BottomLeft, (component, e) =>
            {
                
                InstanceID instanceID = InstanceHelper.GetInstanceID(infoPanel);

                var citizen = CitizenManager.instance.m_citizens.m_buffer[instanceID.Citizen];

                try
                {
                    if (CitizenPanelWrapper == null || instanceID.Citizen != SelectedCitizen)
                    {
                        CitizenPanelWrapper = citizen.GenerateCitizenPanel(instanceID.Citizen);
                    }
                    else
                    {
                        CitizenPanelWrapper.isVisible = false;
                        UiUtils.DeepDestroy(CitizenPanelWrapper);
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