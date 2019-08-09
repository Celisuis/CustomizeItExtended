using System;
using System.Linq;
using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Compatibility;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Buildings;
using CustomizeItExtended.Internal.Buildings;
using UnityEngine;

namespace CustomizeItExtended.Extensions
{
    public static class BuildingInfoExtensions
    {
        public static Internal.Buildings.BuildingProperties GetOriginalProperties(this BuildingInfo info)
        {
            return CustomizeItExtendedTool.instance.OriginalData.TryGetValue(info.name,
                out Internal.Buildings.BuildingProperties props)
                ? props
                : null;
        }

        public static void LoadProperties(this BuildingInfo info, Internal.Buildings.BuildingProperties props)
        {
            var customFields = props.GetType().GetFields();

            var originalFields = info.m_buildingAI.GetType().GetFields();

            var namedFields = customFields.ToDictionary(customField => customField.Name);

            if (!CustomizeItExtendedMod.Settings.OverrideRebalancedIndustries)
                foreach (var originalField in originalFields)
                    try
                    {
                        if (RebalancedIndustries.IsRebalancedIndustriesActive() &&
                            RebalancedIndustries.RebalancedFields.Contains(originalField.Name))
                            continue;

                        if (namedFields.TryGetValue(originalField.Name, out FieldInfo fieldInfo))
                            originalField.SetValue(info.m_buildingAI, fieldInfo.GetValue(props));
                    }
                    catch (Exception e)
                    {
                        Debug.Log(
                            $"[Customize It! Extended] Failed to Load BuildingProperties. {e.Message} - {e.StackTrace}");
                    }
            else
                foreach (var originalField in originalFields)
                    try
                    {
                        if (namedFields.TryGetValue(originalField.Name, out FieldInfo fieldInfo))
                            originalField.SetValue(info.m_buildingAI, fieldInfo.GetValue(props));
                    }
                    catch (Exception e)
                    {
                        Debug.Log(
                            $"[Customize It! Extended] Failed to Load BuildingProperties. {e.Message} - {e.StackTrace}");
                    }
        }

        public static Internal.Buildings.BuildingProperties GetProperties(this BuildingInfo info)
        {
            return new Internal.Buildings.BuildingProperties(info);
        }

        public static UiPanelWrapper GenerateCustomizeItExtendedPanel(this BuildingInfo info)
        {
            try
            {
                CustomizeItExtendedTool.instance.CurrentSelectedBuilding = info;
                DestroyUIs();

                return UIView.GetAView().AddUIComponent(typeof(UiPanelWrapper)) as UiPanelWrapper;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }

        public static UIWarehousePanelWrapper GenerateWarehouseCustomizeItExtendedPanel(this BuildingInfo info)
        {
            try
            {
                CustomizeItExtendedTool.instance.CurrentSelectedBuilding = info;
                DestroyUIs();


                return UIView.GetAView().AddUIComponent(typeof(UIWarehousePanelWrapper)) as UIWarehousePanelWrapper;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }

        public static UIUniqueFactoryPanelWrapper GenerateUniqueFactoryCustomizeItExtendedPanel(this BuildingInfo info)
        {
            try
            {
                CustomizeItExtendedTool.instance.CurrentSelectedBuilding = info;
                DestroyUIs();

                return UIView.GetAView().AddUIComponent(typeof(UIUniqueFactoryPanelWrapper)) as
                    UIUniqueFactoryPanelWrapper;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }

        public static UIZonedBuildingPanelWrapper GenerateBuildingInformation(this BuildingInfo info)
        {
            try
            {
                CustomizeItExtendedTool.instance.CurrentSelectedBuilding = info;
                UiUtils.DeepDestroy(UIView.Find("CustomizeItExtendedZonedBuildingPanelWrapper"));

                return UIView.GetAView().AddUIComponent(typeof(UIZonedBuildingPanelWrapper)) as
                    UIZonedBuildingPanelWrapper;
                ;
            }
            catch (Exception e)
            {
                Debug.Log($"{e.Message} - {e.StackTrace}");
                return null;
            }
        }

        private static void DestroyUIs()
        {
            if (CustomizeItExtendedTool.instance.CustomizeItExtendedPanel != null)
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.CustomizeItExtendedPanel);

            if (CustomizeItExtendedTool.instance.UniqueFactoryPanelWrapper != null)
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.UniqueFactoryPanelWrapper);

            if (CustomizeItExtendedTool.instance.WarehousePanelWrapper != null)
                UiUtils.DeepDestroy(CustomizeItExtendedTool.instance.WarehousePanelWrapper);

        }
    }
}