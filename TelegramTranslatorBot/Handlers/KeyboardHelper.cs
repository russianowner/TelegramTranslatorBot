using Telegram.Bot.Types.ReplyMarkups;
using TelegramTranslatorBot.Models;

namespace TelegramTranslatorBot.Handlers;

public static class KeyboardHelper
{
    public static ReplyKeyboardMarkup MainMenu() =>
        new(new[]
        {
            new[] { new KeyboardButton("🌐 Перевести текст") },
            new[] { new KeyboardButton("🌍 Выбрать язык перевода") }
        })
        {
            ResizeKeyboard = true
        };
    public static ReplyKeyboardMarkup LanguageMenu()
    {
        var buttons = Language.SupportedLanguages.Keys
            .Chunk(2) 
            .Select(pair => pair.Select(name => new KeyboardButton(name)).ToArray())
            .ToArray();
        return new(buttons)
        {
            ResizeKeyboard = true
        };
    }
}
