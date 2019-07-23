using System.Linq;

namespace CustomizeItExtended.Internal.Vehicles
{
    public class CustomVehicleProperties
    {
        public float m_acceleration;
        public float m_attachOffsetBack;
        public float m_attachOffsetFront;
        public float m_braking;
        public float m_dampers;
        public float m_leanMultiplier;
        public float m_maxSpeed;
        public int m_maxTrailerCount;
        public float m_nodMultiplier;
        public float m_springs;
        public float m_turning;

        public bool m_useColorVariations;

        public CustomVehicleProperties()
        {
        }

        public CustomVehicleProperties(VehicleInfo info)
        {
            var fields = info.GetType().GetFields();

            var oldFields = fields.ToDictionary(field => field.Name);

            fields = GetType().GetFields();

            foreach (var customField in fields)
                if (oldFields.ContainsKey(customField.Name))
                    customField.SetValue(this, oldFields[customField.Name].GetValue(info));
        }
    }
}