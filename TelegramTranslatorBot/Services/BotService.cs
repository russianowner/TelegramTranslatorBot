using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using TelegramTranslatorBot.Handlers;
using TelegramTranslatorBot.Models;

namespace TelegramTranslatorBot.Services;

public class BotService : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly MessageHandler _messageHandler;
    private readonly CommandHandler _commandHandler;
    private readonly Dictionary<long, UserState> _userStates = new();
    private TelegramBotClient? _bot;

    public BotService(IConfiguration config)
    {
        _config = config;
        _commandHandler = new CommandHandler(_userStates);
        var translator = new TranslatorService(config);
        _messageHandler = new MessageHandler(translator, _userStates);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var token = _config["BotToken"];
        if (string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException("нету боттокена в аппсетинг");
        _bot = new TelegramBotClient(token);
        var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        _bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken: stoppingToken);
        var me = await _bot.GetMe();
        Console.WriteLine($"@{me.Username} запущен");
    }
    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        if (update.Message is not { } message) return;
        if (message.Text?.StartsWith("/") == true)
            await _commandHandler.HandleCommand(bot, message, token);
        else
            await _messageHandler.HandleMessage(bot, message, token);
    }
    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
    {
        Console.WriteLine($"{ex.Message}");
        return Task.CompletedTask;
    }
}
