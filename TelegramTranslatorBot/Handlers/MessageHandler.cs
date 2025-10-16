using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTranslatorBot.Models;
using TelegramTranslatorBot.Services;

namespace TelegramTranslatorBot.Handlers;

public class MessageHandler
{
    private readonly TranslatorService _translator;
    private readonly Dictionary<long, UserState> _userStates;

    public MessageHandler(TranslatorService translator, Dictionary<long, UserState> userStates)
    {
        _translator = translator;
        _userStates = userStates;
    }
    public async Task HandleMessage(ITelegramBotClient bot, Message msg, CancellationToken token)
    {
        if (string.IsNullOrEmpty(msg.Text)) return;
        long chatId = msg.Chat.Id;
        if (!_userStates.ContainsKey(chatId))
            _userStates[chatId] = new UserState();
        var user = _userStates[chatId];
        var text = msg.Text;
        switch (text)
        {
            case "🌍 Выбрать язык перевода":
                user.Mode = "choose_target";
                await bot.SendMessage(chatId,
                    "😯Выбери язык перевода:",
                    replyMarkup: KeyboardHelper.LanguageMenu(),
                    cancellationToken: token);
                return;

            case "🌐 Перевести текст":
                user.Mode = "choose_source";
                await bot.SendMessage(chatId,
                    "😯С какого языка перевести?",
                    replyMarkup: KeyboardHelper.LanguageMenu(),
                    cancellationToken: token);
                return;
        }
        if (user.Mode == "choose_target" && Language.SupportedLanguages.ContainsKey(text))
        {
            user.TargetLang = Language.SupportedLanguages[text];
            user.Mode = null;
            await bot.SendMessage(chatId,
                $"😯Язык перевода установлен на {text}!",
                replyMarkup: KeyboardHelper.MainMenu(),
                cancellationToken: token);
            return;
        }
        if (user.Mode == "choose_source" && Language.SupportedLanguages.ContainsKey(text))
        {
            user.SourceLang = Language.SupportedLanguages[text];
            user.Mode = "translate";
            await bot.SendMessage(chatId,
                $"😯Введите слово или фразу на выбранном языке ({text}), чтобы перевести на {_userStates[chatId].TargetLang?.ToUpper()}",
                replyMarkup: new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardRemove(),
                cancellationToken: token);
            return;
        }
        if (user.Mode == "translate")
        {
            var result = await _translator.TranslateAsync(text, user.TargetLang ?? "en", user.SourceLang ?? "auto");
            await bot.SendMessage(chatId, $"🗣 Перевод:\n{result}",
                replyMarkup: KeyboardHelper.MainMenu(),
                cancellationToken: token);
            user.Mode = null;
            return;
        }
        await bot.SendMessage(chatId,
            "😯я не знаю о чем ты",
            replyMarkup: KeyboardHelper.MainMenu(),
            cancellationToken: token);
    }
}
