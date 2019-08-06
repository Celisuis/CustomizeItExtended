namespace CustomizeItExtended.Translations
{
    public static class StringExtensions
    {
        public static string TranslateField(this string text)
        {
            return TranslationFramework.GetTranslation(text, TranslationFramework.TextType.Field);
        }

        public static string TranslateInformation(this string text)
        {
            return TranslationFramework.GetTranslation(text, TranslationFramework.TextType.Information);
        }

        public static string TranslateCitizen(this string text)
        {
            return TranslationFramework.GetTranslation(text, TranslationFramework.TextType.Citizen);
        }
    }
}