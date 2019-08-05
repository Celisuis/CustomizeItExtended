using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework.Plugins;

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
            return ModTools.ModConfiguration.Deserialize("ModToolsConfig.xml").ExtendGamePanels;
        }
    }
}
