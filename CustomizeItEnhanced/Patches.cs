using CustomizeItExtended.Extensions;
using CustomizeItExtended.Internal;
using Harmony;

namespace CustomizeItExtended
{
    [HarmonyPatch(typeof(BuildingInfo), "InitializePrefab")]
    public static class InitializePrefabPatch
    {
        public static void Postfix(BuildingInfo __instance)
        {
            var info = __instance;

            if (info == null ||info.m_buildingAI == null || !(info.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI))))
                return;

            if(!CustomizeItExtendedTool.instance.OriginalData.TryGetValue(info.name, out Properties originalProps))
            {
                CustomizeItExtendedTool.instance.OriginalData.Add(info.name, info.GetProperties());
            }

            if(CustomizeItExtendedTool.instance.CustomData.TryGetValue(info.name, out Properties customProps))
            {
                info.LoadProperties(customProps);
            }
        }
    }
}
