using System;
using System.Linq;
using System.Reflection;
using PloppableRICO;

namespace CustomizeItExtended.Internal.Buildings
{
    [Serializable]
    public class RICOBuildingProperties
    {
        // Building Info Data
        public int m_constructionCost;
        public int m_homeCount;

        public string m_subtype;
        public int[] workplaceCount;

        public bool m_pollutionEnabled;
        
        // RICO Building Data
        private int _level;
        private bool _ricoEnabled;

        public string _service;
        public string _subService;
        public string _UICategory;

        public RICOBuildingProperties()
        {
            
        }

        public RICOBuildingProperties(BuildingInfo info, RICOBuilding building)
        {
            var ai = info.m_buildingAI;

            var fields = ai.GetType().GetFields();
            
            var oldFields = fields.ToDictionary(field => field.Name);

            fields = GetType().GetFields();

            foreach (var customField in fields)
                if (oldFields.ContainsKey(customField.Name))
                    customField.SetValue(this, oldFields[customField.Name].GetValue(ai));

            var buildingFields = building.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            var oldBuildingFields = buildingFields.ToDictionary(field => field.Name);

            foreach (var customField in fields)
            {
                if(oldBuildingFields.ContainsKey(customField.Name))
                    customField.SetValue(this, oldBuildingFields[customField.Name].GetValue(building));
            }
        }
        
    }
}