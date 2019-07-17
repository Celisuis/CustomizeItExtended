﻿using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CustomizeItExtended.Legacy
{
    [XmlRoot("CustomizeIt")]
    public class CustomizeItSettings
    {
        public List<CustomizablePropertiesEntry> Entries = new List<CustomizablePropertiesEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity = false;
    }
}
