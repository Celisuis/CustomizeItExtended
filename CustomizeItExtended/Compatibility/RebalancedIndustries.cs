using System.Collections.Generic;
using System.Linq;
using ColossalFramework.Plugins;

namespace CustomizeItExtended.Compatibility
{
    public class RebalancedIndustries
    {
        public static List<string> RebalancedFields = new List<string>
        {
            "m_inputRate1",
            "m_inputRate2",
            "m_inputRate3",
            "m_inputRate4",
            "m_outputRate",
            "m_outputVehicleCount",
            "m_extractRate",
            "m_extractRadius",
            "m_truckCount"
        };

        public static bool IsRebalancedIndustriesActive()
        {
            var plugins = PluginManager.instance.GetPluginsInfo();

            return plugins.Where(x => x.isEnabled).Any(plugin => plugin.publishedFileID.AsUInt64 == 1562650024);
        }
    }
}