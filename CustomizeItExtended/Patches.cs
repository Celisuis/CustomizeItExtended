using System.Reflection;
using ColossalFramework.UI;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.Internal;
using CustomizeItExtended.Internal.Buildings;
using CustomizeItExtended.Internal.Citizens;
using CustomizeItExtended.Internal.Vehicles;
using Harmony;

namespace CustomizeItExtended
{
    [HarmonyPatch(typeof(BuildingInfo), "InitializePrefab")]
    public static class InitializePrefabPatch
    {
        public static void Postfix(BuildingInfo __instance)
        {
            var info = __instance;

            if (info == null)
                return;

            if (!CustomizeItExtendedTool.instance.OriginalBuildingNames.TryGetValue(info.name, out NameProperties name))
                CustomizeItExtendedTool.instance.OriginalBuildingNames.Add(info.name,
                    new NameProperties(info.name, false));

            if (info.m_buildingAI == null)
                return;

            if (!info.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))
                return;

            if (!CustomizeItExtendedTool.instance.OriginalData.TryGetValue(info.name, out var originalProps))
                CustomizeItExtendedTool.instance.OriginalData.Add(info.name, info.GetProperties());

            if (CustomizeItExtendedTool.instance.CustomData.TryGetValue(info.name, out var customProps))
                info.LoadProperties(customProps);
        }
    }

    [HarmonyPatch(typeof(HumanWorldInfoPanel), "GetJobTitle")]
    public static class HumanWorldInfoPanelPatch
    {
        public static void Postfix(HumanWorldInfoPanel __instance, ref string __result, uint citizenID)
        {
            if (CustomizeItExtendedCitizenTool.instance.CustomJobTitles.TryGetValue(citizenID, out string title))
                __result = title;
        }
    }

    [HarmonyPatch(typeof(VehicleInfo), "InitializePrefab")]
    public static class VehicleInfoPatch
    {
        public static void Postfix(VehicleInfo __instance)
        {
            var info = __instance;

            if (info == null)
                return;

            if (!CustomizeItExtendedVehicleTool.instance.OriginalVehicleNames.TryGetValue(info.name, out var props))
                CustomizeItExtendedVehicleTool.instance.OriginalVehicleNames.Add(info.name,
                    new NameProperties(info.name, false));

            if (!CustomizeItExtendedVehicleTool.instance.OriginalVehicleData.TryGetValue(info.name,
                out var originalProps))
                CustomizeItExtendedVehicleTool.instance.OriginalVehicleData.Add(info.name, info.GetProperties());

            if (CustomizeItExtendedVehicleTool.instance.CustomVehicleData.TryGetValue(info.name, out var customProps))
                info.LoadProperties(customProps);
        }
    }

    [HarmonyPatch(typeof(BuildingWorldInfoPanel), "GetName")]
    public static class BuildingGetNamePatch
    {
        public static void Postfix(BuildingWorldInfoPanel __instance, ref string __result)
        {
            InstanceID instanceID = (InstanceID) __instance.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

            var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;

            if (CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(building.name,
                    out NameProperties customName) && customName.DefaultName && !customName.Unaffected.Contains(instanceID.Building))
                __result = customName.CustomName;
        }
    }

    [HarmonyPatch(typeof(BuildingWorldInfoPanel), "OnRename")]
    public static class BuildingRenamePatch
    {
        public static void Postfix(BuildingWorldInfoPanel __instance, string text)
        {
            InstanceID instanceID = (InstanceID)__instance.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

            var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;


            if (!CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(building.name, out var nameProps))
                return;

            if (!nameProps.Unaffected.Contains(instanceID.Building))
                nameProps.Unaffected.Add(instanceID.Building);

            if (!CustomizeItExtendedMod.Settings.SavePerCity)
                CustomizeItExtendedMod.Settings.Save();
        }
    }

  
    [HarmonyPatch(typeof(VehicleWorldInfoPanel), "GetName")]
    public static class VehicleGetNamePatch
    {
        public static void Postfix(VehicleWorldInfoPanel __instance, ref string __result)
        {
            InstanceID instanceID = (InstanceID) __instance.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

            var vehicle = VehicleManager.instance.m_vehicles.m_buffer[instanceID.Vehicle].Info;

            if (CustomizeItExtendedVehicleTool.instance.CustomVehicleNames.TryGetValue(vehicle.name,
                out NameProperties props) && props.DefaultName && !props.Unaffected.Contains(instanceID.Vehicle))
                __result = props.CustomName;
        }
    }

    [HarmonyPatch(typeof(VehicleWorldInfoPanel), "OnRename")]
    public static class VehicleRenamePatch

    {
    public static void Postfix(VehicleWorldInfoPanel __instance, string text)
    {
        InstanceID instanceID = (InstanceID) __instance.GetType()
            .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

        var vehicle = VehicleManager.instance.m_vehicles.m_buffer[instanceID.Vehicle].Info;


        if (!CustomizeItExtendedTool.instance.CustomBuildingNames.TryGetValue(vehicle.name, out var nameProps))
            return;

        if (!nameProps.Unaffected.Contains(instanceID.Vehicle))
            nameProps.Unaffected.Add(instanceID.Vehicle);

        if (!CustomizeItExtendedMod.Settings.SavePerCity)
            CustomizeItExtendedMod.Settings.Save();
        }
    }

}