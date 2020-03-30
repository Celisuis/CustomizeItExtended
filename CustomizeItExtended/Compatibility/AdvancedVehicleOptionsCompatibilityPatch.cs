using System.Collections.Generic;
using System.Linq;
using ColossalFramework.Plugins;

namespace CustomizeItExtended.Compatibility
{
    public class AdvancedVehicleOptionsCompatibilityPatch
    {
        public static bool IsAVOActive()
        {
            return PluginManager.instance.GetPluginsInfo().Where(x => x.isEnabled)
                .Any(plugin => plugin.publishedFileID.AsUInt64 == 1548831935);
        }
        
        public static List<string> AVOFields = new List<string>
        {
            "m_maxSpeed",
            "m_braking",
            "m_acceleration",
            "m_turning",
            "m_springs",
            "m_dampers",
            "m_leanMultiplier",
            "m_nodMultiplier",
            "m_useColorVariations",
            
            
        };
    }
}