using System.Collections.Generic;

namespace CustomizeItExtended.Translations
{
    public class BaseLanguage
    {
        public Dictionary<string, string> CitizenTranslations = new Dictionary<string, string>();
        public Dictionary<string, string> FieldTranslations = new Dictionary<string, string>();

        public Dictionary<string, string> InformationTranslations = new Dictionary<string, string>();

        public string Name;
    }
}