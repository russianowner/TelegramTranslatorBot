using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTranslatorBot.Models;

namespace TelegramTranslatorBot.Handlers;

public class CommandHandler
{
    private readonly Dictionary<long, UserState> _userStates;

    public CommandHandler(Dictionary<long, UserState> userStates)
    {
        _userStates = userStates;
    }
    public async Task HandleCommand(ITelegramBotClient bot, Message msg, CancellationToken token)
    {
        if (string.IsNullOrEmpty(msg.Text)) return;
        long chatId = msg.Chat.Id;
        if (!_userStates.ContainsKey(chatId))
            _userStates[chatId] = new UserState();
        switch (msg.Text)
        {
            case "/start":
                await bot.SendMessage(chatId,
                    "👋 Я Бот переводчик\n\n😯Выбери действие:",
                    replyMarkup: KeyboardHelper.MainMenu(),
                    cancellationToken: token);
                break;
            case "/help":
                await bot.SendMessage(chatId,
                    "💡 Отправь /start, чтобы открыть меню\n\n" +
                    "😯Бот умеет переводить текст и выбирать язык перевода",
                    cancellationToken: token);
                break;
        }
    }
}
