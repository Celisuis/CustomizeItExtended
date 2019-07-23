using System;

namespace CustomizeItExtended.Internal.Citizens
{
    [Serializable]
    public class CitizenProperties
    {
        public string CustomJobTitle;

        public CitizenProperties()
        {
        }

        public CitizenProperties(string title)
        {
            CustomJobTitle = title;
        }
    }
}