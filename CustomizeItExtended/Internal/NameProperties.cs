using System;

namespace CustomizeItExtended.Internal
{
    [Serializable]
    public class NameProperties
    {
        public string CustomName;

        public bool DefaultName;

        public NameProperties()
        {
        }

        public NameProperties(string name, bool defaultStatus)
        {
            CustomName = name;
            DefaultName = defaultStatus;
        }
    }
}