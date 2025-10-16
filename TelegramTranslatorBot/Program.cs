using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramTranslatorBot.Services;
using TelegramTranslatorBot.Handlers;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<BotService>();
        services.AddSingleton<TranslatorService>();
        services.AddSingleton<MessageHandler>();
        services.AddSingleton<CommandHandler>();
        services.AddHostedService<BotService>();
    })
    .Build()
    .Run();
