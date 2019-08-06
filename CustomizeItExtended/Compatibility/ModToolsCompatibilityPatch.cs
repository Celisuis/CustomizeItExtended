using System.Linq;
using ColossalFramework.Plugins;
using ModTools;

namespace CustomizeItExtended.Compatibility
{
    public class ModToolsCompatibilityPatch
    {
        public static bool IsModToolsActive()
        {
            return PluginManager.instance.GetPluginsInfo().Where(x => x.isEnabled)
                .Any(plugin => plugin.publishedFileID.AsUInt64 == 450877484);
        }

        public static bool AreGamePanelExtensionsActive()
        {
            return ModConfiguration.Deserialize("ModToolsConfig.xml").ExtendGamePanels;
        }
    }
}