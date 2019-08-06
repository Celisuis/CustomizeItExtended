using System;
using System.Collections.Generic;

namespace CustomizeItExtended.Internal
{
    [Serializable]
    public class NameProperties
    {
        public string CustomName;


        public bool DefaultName;

        public List<ushort> Unaffected;

        public NameProperties()
        {
        }

        public NameProperties(string name, bool defaultStatus)
        {
            CustomName = name;
            DefaultName = defaultStatus;
            Unaffected = new List<ushort>();
        }
    }
}