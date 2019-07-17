using ColossalFramework.IO;
using ColossalFramework.UI;
using CustomizeItEnhanced.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace CustomizeItEnhanced.Settings
{
    [XmlRoot("CustomizeItEnhanced")]
    public class CustomizeItEnhancedSettings
    {
        [XmlIgnore]
        private static readonly string _configPath = Path.Combine(DataLocation.localApplicationData, "CustomizeIt-Enhanced.xml");
        public List<PropertyEntry> Entries = new List<PropertyEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity = false;

        public CustomizeItEnhancedSettings()
        {

        }

        public void Save()
        {
            if(!CustomizeItEnhancedMod.Settings.SavePerCity)
            {
                Entries.Clear();

                foreach(var entry in CustomizeItEnhancedTool.instance.CustomData)
                {
                    if(entry.Value != null)
                    {
                        Entries.Add(entry);
                    }
                }
            }

            var serializer = new XmlSerializer(typeof(CustomizeItEnhancedSettings));

            using(var writer = new StreamWriter(_configPath))
            {
                serializer.Serialize(writer, CustomizeItEnhancedMod.Settings);
            }
        }

        public static CustomizeItEnhancedSettings Load()
        {

            var serializer = new XmlSerializer(typeof(CustomizeItEnhancedSettings));
            try
            {
                using (var reader = new StreamReader(_configPath))
                {
                    var config = (CustomizeItEnhancedSettings)serializer.Deserialize(reader);

                    if(!config.SavePerCity)
                    {
                        CustomizeItEnhancedTool.instance.CustomData.Clear();

                        foreach(var entry in config.Entries)
                        {
                            if (entry != null)
                                CustomizeItEnhancedTool.instance.CustomData.Add(entry.Key, entry.Value);
                        }
                    }

                    return config;
                }
            }
            catch(Exception e)
            {
                return new CustomizeItEnhancedSettings();
            }
        }
    }
}
