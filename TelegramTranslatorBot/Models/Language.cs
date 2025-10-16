namespace TelegramTranslatorBot.Models;

public static class Language
{
    public static readonly Dictionary<string, string> SupportedLanguages = new()
    {
        { "Английский 🇬🇧", "en" },
        { "Русский 🇷🇺", "ru" },
        { "Немецкий 🇩🇪", "de" },
        { "Французский 🇫🇷", "fr" },
        { "Испанский 🇪🇸", "es" },
        { "Итальянский 🇮🇹", "it" },
        { "Украинский 🇺🇦", "uk" },
        { "Польский 🇵🇱", "pl" },
        { "Китайский 🇨🇳", "zh" },
        { "Японский 🇯🇵", "ja" }
    };
}
