using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomizeItExtended.Extensions;
using CustomizeItExtended.Internal;
using ICities;

namespace CustomizeItExtended
{
    public class SerializationExtension : SerializableDataExtensionBase
    {
        private static readonly string MDataId = "Customize-It-Extended";
        private CustomizeItExtendedTool Instance => CustomizeItExtendedTool.instance;

        private List<PropertyEntry> CustomDataList
        {
            get
            {
                var list = new List<PropertyEntry>();

                if (Instance.CustomData == null)
                    return list;

                foreach (var item in Instance.CustomData)
                    list.Add(item);

                return list;
            }
            set
            {
                var collection = new Dictionary<string, Properties>();

                if (value == null)
                    return;

                foreach (var item in value) collection.Add(item.Key, item.Value);

                Instance.CustomData = collection;
            }
        }

        public override void OnSaveData()
        {
            base.OnSaveData();

            if (!CustomizeItExtendedMod.Settings.SavePerCity || Instance.CustomData == null)
                return;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, CustomDataList);
                serializableDataManager.SaveData(MDataId, stream.ToArray());
            }
        }

        public override void OnLoadData()
        {
            base.OnLoadData();

            if (!CustomizeItExtendedMod.Settings.SavePerCity)
                return;

            var data = serializableDataManager.LoadData(MDataId);

            if (data == null || data.Length == 0)
                return;

            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream(data))
            {
                CustomDataList = (List<PropertyEntry>) formatter.Deserialize(stream);
            }

            SimulationManager.instance.AddAction(() =>
            {
                for (uint x = 0; x < PrefabCollection<BuildingInfo>.LoadedCount(); x++)
                    if (CustomizeItExtendedTool.instance.CustomData.TryGetValue(
                        PrefabCollection<BuildingInfo>.GetLoaded(x).name, out var customProps))
                        PrefabCollection<BuildingInfo>.GetLoaded(x).LoadProperties(customProps);
            });
        }
    }
}