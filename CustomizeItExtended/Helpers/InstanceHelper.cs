using System.Reflection;
using ColossalFramework.UI;

namespace CustomizeItExtended.Helpers
{
    public class InstanceHelper
    {
        public static InstanceID GetInstanceID(UICustomControl control)
        {
            return (InstanceID) control.GetType()
                .GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(control);
        }
    }
}