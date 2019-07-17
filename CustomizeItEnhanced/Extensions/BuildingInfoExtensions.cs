using ColossalFramework.UI;
using CustomizeItEnhanced.GUI;
using CustomizeItEnhanced.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomizeItEnhanced.Extensions
{
    public static class BuildingInfoExtensions
    {
        public static Properties GetOriginalProperties(this BuildingInfo info)
        {
            if (CustomizeItEnhancedTool.instance.OriginalData.TryGetValue(info.name, out Properties props))
                return props;

            return null;
        }

        public static void LoadProperties(this BuildingInfo info, Properties props)
        {
            var customFields = props.GetType().GetFields();

            var originalFields = info.m_buildingAI.GetType().GetFields();

            var namedFields = new Dictionary<string, FieldInfo>();

            foreach(var customField in customFields)
            {
                namedFields.Add(customField.Name, customField);
            }

            foreach(var originalField in originalFields)
            {
                try
                {
                    if(namedFields.TryGetValue(originalField.Name, out FieldInfo fieldInfo))
                    {
                        originalField.SetValue(info.m_buildingAI, fieldInfo.GetValue(props));
                    }
                }
                catch(Exception e)
                {
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, $"[Customize It! Enhanced] Failed to Load Properties. {e.Message} - {e.StackTrace}");
                }
            }
        }

        public static Properties GetProperties(this BuildingInfo info)
        {
            return new Properties(info);
        }

        public static UIPanelWrapper GenerateCustomizeItEnhancedPanel(this BuildingInfo info)
        {
            CustomizeItEnhancedTool.instance.CurrentSelectedBuilding = info;
            UIUtils.DeepDestroy(UIView.Find("CustomizeItEnhancedPanelWrapper"));

            return UIView.GetAView().AddUIComponent(typeof(UIPanelWrapper)) as UIPanelWrapper;
        }
    }
}
