// Credit to TPB - Original Source File added for Backwards Compatibility. See - https://github.com/TPBCS/CustomizeIt

using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomizeItExtended.Legacy
{
    [XmlRoot("CustomizeIt")]
    public class CustomizeItSettings
    {
        public List<CustomizablePropertiesEntry> Entries = new List<CustomizablePropertiesEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity;
    }
}