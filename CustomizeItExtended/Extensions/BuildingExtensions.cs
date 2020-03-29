using System;
using System.Linq;
using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Compatibility;
using CustomizeItExtended.GUI;
using CustomizeItExtended.GUI.Buildings;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Buildings.RICO;
using PloppableRICO;
using UnityEngine;

namespace CustomizeItExtended.Extensions
{
    public static class BuildingExtensions
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

        public static RICOBuildingProperties GetOriginalRICOProperties(this BuildingInfo info)
        {
            return CustomizeItExtendedRICOTool.instance.OriginalRICOData.TryGetValue(info.name,
                out RICOBuildingProperties props)
                ? props
                : null;
        }

        public static RICOBuilding GetRICOBuildingData(this BuildingInfo info)
        {
            switch (info.m_buildingAI)
            {
                case PloppableResidential residential:
                    return residential.m_ricoData;
                case PloppableCommercial commercial:
                    return commercial.m_ricoData;
                case PloppableExtractor extractor:
                    return extractor.m_ricoData;
                case PloppableIndustrial industrial:
                    return industrial.m_ricoData;
                case PloppableOffice office:
                    return office.m_ricoData;
                default:
                    return null;   
            }
        }

        public static void LoadRICOProperties(this BuildingInfo info, RICOBuildingProperties props)
        {
            var customFields = props.GetType().GetFields();

            var originalFields = info.m_buildingAI.GetType().GetFields();

            FieldInfo[] originalDataFields;

            RICOBuilding building;
            
            switch (info.m_buildingAI)
            {
                case PloppableResidential residential:
                    originalDataFields = residential.m_ricoData.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    building = residential.m_ricoData;
                    break;
                case PloppableCommercial commercial:
                    originalDataFields = commercial.m_ricoData.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    building = commercial.m_ricoData;
                    break;
                case PloppableExtractor extractor:
                    originalDataFields = extractor.m_ricoData.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    building = extractor.m_ricoData;
                    break;
                case PloppableIndustrial industrial:
                    originalDataFields = industrial.m_ricoData.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    building = industrial.m_ricoData;
                    break;
                case PloppableOffice office:
                    originalDataFields = office.m_ricoData.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    building = office.m_ricoData;
                    break;
                default:
                    return;
            }

            var namedFields = customFields.ToDictionary(field => field.Name);

            foreach (var field in originalFields)
            {
                try
                {
                    if(namedFields.TryGetValue(field.Name, out FieldInfo fieldInfo))
                        field.SetValue(info.m_buildingAI, fieldInfo.GetValue(props));
                }
                catch (Exception e)
                {
                    Debug.Log(
                        $"[Customize It! Extended] Failed to Load RICOBuildingProperties. {e.Message} - {e.StackTrace}");
                }
            }

            foreach (var field in originalDataFields)
            {
                try
                {
                    if(namedFields.TryGetValue(field.Name, out FieldInfo fieldInfo) && building != null)
                        field.SetValue(building, fieldInfo.GetValue(props));
                }
                catch (Exception e)
                {
                    Debug.Log(
                        $"[Customize It! Extended] Failed to Load RICOBuildingDataProperties. {e.Message} - {e.StackTrace}");
                }
            }

        }

        public static Internal.Buildings.BuildingProperties GetProperties(this BuildingInfo info)
        {
            return new Internal.Buildings.BuildingProperties(info);
        }

        public static RICOBuildingProperties GetRICOInfoProperties(this BuildingInfo info)
        {
            var ai = info.m_buildingAI;

            switch (ai)
            {
                case PloppableResidential residential:
                    return new RICOBuildingProperties(info, residential.m_ricoData);
                case PloppableCommercial commercial:
                    return new RICOBuildingProperties(info, commercial.m_ricoData);
                case PloppableExtractor extractor:
                    return new RICOBuildingProperties(info, extractor.m_ricoData);
                case PloppableIndustrial industrial:
                    return new RICOBuildingProperties(info, industrial.m_ricoData);
                case PloppableOffice office:
                    return new RICOBuildingProperties(info, office.m_ricoData);
                default:
                    return null;
            }
        }

        public static bool IsRICOBuilding(this BuildingInfo info)
        {
            return info.m_buildingAI.GetType() == typeof(PloppableCommercial)
                   || info.m_buildingAI.GetType() == typeof(PloppableResidential)
                   || info.m_buildingAI.GetType() == typeof(PloppableIndustrial)
                   || info.m_buildingAI.GetType() == typeof(PloppableExtractor)
                   || info.m_buildingAI.GetType() == typeof(PloppableOffice);
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