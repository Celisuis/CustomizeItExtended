using CustomizeItEnhanced.Extensions;
using CustomizeItEnhanced.Internal;
using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CustomizeItEnhanced
{
    public class SerializationExtension : SerializableDataExtensionBase
    {
        private CustomizeItEnhancedTool Instance => CustomizeItEnhancedTool.instance;

        private static readonly string m_dataID = "Customize-It-Enhanced";

        private List<PropertyEntry> CustomDataList
        {
            get
            {
                var list = new List<PropertyEntry>();

                if(Instance.CustomData != null)
                {
                    foreach(var item in Instance.CustomData)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
            set
            {
                var collection = new Dictionary<string, Properties>();

                if(value != null)
                {
                    foreach(var item in value)
                    {
                        collection.Add(item.Key, item.Value);
                    }

                    Instance.CustomData = collection;
                }
            }
        }

        public override void OnSaveData()
        {
            base.OnSaveData();

            if (!CustomizeItEnhancedMod.Settings.SavePerCity || Instance.CustomData == null)
                return;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, CustomDataList);
                serializableDataManager.SaveData(m_dataID, stream.ToArray());
            }
        }

        public override void OnLoadData()
        {
            base.OnLoadData();

            if (!CustomizeItEnhancedMod.Settings.SavePerCity)
                return;

            var data = serializableDataManager.LoadData(m_dataID);

            if (data == null || data.Length == 0)
                return;

            var formatter = new BinaryFormatter();

            using(var stream = new MemoryStream(data))
            {
                CustomDataList = (List<PropertyEntry>)formatter.Deserialize(stream);
            }

            SimulationManager.instance.AddAction(() =>
            {
                for (uint x = 0; x < PrefabCollection<BuildingInfo>.LoadedCount(); x++)
                {
                    if (CustomizeItEnhancedTool.instance.CustomData.TryGetValue(PrefabCollection<BuildingInfo>.GetLoaded(x).name, out Properties customProps))
                    {
                        PrefabCollection<BuildingInfo>.GetLoaded(x).LoadProperties(customProps);
                    }
                }
            });
        }
    }
}
