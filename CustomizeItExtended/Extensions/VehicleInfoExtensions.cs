using System;
using System.Linq;
using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Vehicles;
using CustomizeItExtended.Internal.Vehicles;
using UnityEngine;

namespace CustomizeItExtended.Extensions
{
    public static class VehicleInfoExtensions
    {
        public static CustomVehicleProperties GetOriginalProperties(this VehicleInfo info)
        {
            return CustomizeItExtendedVehicleTool.instance.OriginalVehicleData.TryGetValue(info.name,
                out CustomVehicleProperties props)
                ? props
                : null;
        }

        public static void LoadProperties(this VehicleInfo info, CustomVehicleProperties props)
        {
            var customFields = props.GetType().GetFields();

            var originalFields = info.GetType().GetFields();

            var namedFields = customFields.ToDictionary(field => field.Name);

            foreach (var field in originalFields)
                if (namedFields.TryGetValue(field.Name, out FieldInfo fieldInfo))
                    field.SetValue(info, fieldInfo.GetValue(props));
        }

        public static CustomVehicleProperties GetProperties(this VehicleInfo info)
        {
            return new CustomVehicleProperties(info);
        }

        public static UIVehiclePanelWrapper GenerateVehiclePanel(this VehicleInfo info)
        {
            try
            {
                CustomizeItExtendedVehicleTool.instance.SelectedVehicle = info;
                UiUtils.DeepDestroy(UIView.Find("CustomizeItExtendedVehiclePanelWrapper"));

                return UIView.GetAView().AddUIComponent(typeof(UIVehiclePanelWrapper)) as UIVehiclePanelWrapper;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }
    }
}