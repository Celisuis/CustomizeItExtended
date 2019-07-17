using ColossalFramework.IO;
using ColossalFramework.UI;
using CustomizeItExtended.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace CustomizeItExtended.Settings
{
    [XmlRoot("CustomizeItExtended")]
    public class CustomizeItExtendedSettings
    {
        [XmlIgnore]
        private static readonly string _configPath = Path.Combine(DataLocation.localApplicationData, "CustomizeIt-Extended.xml");
        public List<PropertyEntry> Entries = new List<PropertyEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity = false;

        public CustomizeItExtendedSettings()
        {

        }

        public void Save()
        {
            if(!CustomizeItExtendedMod.Settings.SavePerCity)
            {
                Entries.Clear();

                foreach(var entry in CustomizeItExtendedTool.instance.CustomData)
                {
                    if(entry.Value != null)
                    {
                        Entries.Add(entry);
                    }
                }
            }

            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));

            using(var writer = new StreamWriter(_configPath))
            {
                serializer.Serialize(writer, CustomizeItExtendedMod.Settings);
            }
        }

        public static CustomizeItExtendedSettings Load()
        {

            var serializer = new XmlSerializer(typeof(CustomizeItExtendedSettings));
            try
            {
                using (var reader = new StreamReader(_configPath))
                {
                    var config = (CustomizeItExtendedSettings)serializer.Deserialize(reader);

                    if(!config.SavePerCity)
                    {
                        CustomizeItExtendedTool.instance.CustomData.Clear();

                        foreach(var entry in config.Entries)
                        {
                            if (entry != null)
                                CustomizeItExtendedTool.instance.CustomData.Add(entry.Key, entry.Value);
                        }
                    }

                    return config;
                }
            }
            catch(Exception e)
            {
                return new CustomizeItExtendedSettings();
            }
        }
    }
}
