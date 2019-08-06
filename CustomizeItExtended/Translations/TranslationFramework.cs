using System;
using System.Collections.Generic;
using System.Linq;
using CustomizeItExtended.Translations;
using CustomizeItExtended.Translations.Languages;

namespace CustomizeItExtended
{
    public class TranslationFramework
    {
        public enum TextType
        {
            Field,
            Information,
            Citizen
        }

        public static List<BaseLanguage> Languages = new List<BaseLanguage>();

        public static BaseLanguage CurrentBaseLanguage = new English();


        public static void Initialize()
        {
            var types = typeof(BaseLanguage).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseLanguage)))
                .ToArray();

            Languages = types.Select(type => (BaseLanguage) Activator.CreateInstance(type)).ToList();

            CurrentBaseLanguage = Languages.Find(x => x.Name == CustomizeItExtendedMod.Settings.Language);
        }

        public static string GetTranslation(string text, TextType type)
        {
            switch (type)
            {
                case TextType.Field:
                    return GetFieldTranslation(text);
                case TextType.Information:
                    return GetInformationTranslation(text);
                case TextType.Citizen:
                    return GetCitizenTranslation(text);

                default:
                    return string.Empty;
            }
        }

        private static string GetFieldTranslation(string text)
        {
            return CurrentBaseLanguage.FieldTranslations.TryGetValue(text, out string value) ? value : text;
        }

        private static string GetInformationTranslation(string text)
        {
            return CurrentBaseLanguage.InformationTranslations.TryGetValue(text, out string value) ? value : text;
        }

        private static string GetCitizenTranslation(string text)
        {
            return CurrentBaseLanguage.CitizenTranslations.TryGetValue(text, out string value) ? value : text;
        }
    }
}