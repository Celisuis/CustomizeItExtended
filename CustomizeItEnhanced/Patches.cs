using CustomizeItEnhanced.Extensions;
using CustomizeItEnhanced.Internal;
using Harmony;

namespace CustomizeItEnhanced
{
    [HarmonyPatch(typeof(BuildingInfo), "InitializePrefab")]
    public static class InitializePrefabPatch
    {
        public static void Postfix(BuildingInfo __instance)
        {
            var info = __instance;

            if (info == null ||info.m_buildingAI == null || !(info.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI))))
                return;

            if(!CustomizeItEnhancedTool.instance.OriginalData.TryGetValue(info.name, out Properties originalProps))
            {
                CustomizeItEnhancedTool.instance.OriginalData.Add(info.name, info.GetProperties());
            }

            if(CustomizeItEnhancedTool.instance.CustomData.TryGetValue(info.name, out Properties customProps))
            {
                info.LoadProperties(customProps);
            }
        }
    }
}
