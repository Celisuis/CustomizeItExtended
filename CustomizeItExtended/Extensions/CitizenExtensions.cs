using System;
using ColossalFramework.UI;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Citizens;
using CustomizeItExtended.Internal.Citizens;
using UnityEngine;

namespace CustomizeItExtended.Extensions
{
    public static class CitizenExtensions
    {
        public static UICitizenPanelWrapper GenerateCitizenPanel(this Citizen citizen, uint citizenID)
        {
            try
            {
                CustomizeItExtendedCitizenTool.instance.SelectedCitizen = citizenID;
                UiUtils.DeepDestroy(UIView.Find("CustomizeItExtendedCitizenPanelWrapper"));

                return UIView.GetAView().AddUIComponent(typeof(UICitizenPanelWrapper)) as UICitizenPanelWrapper;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }
    }
}