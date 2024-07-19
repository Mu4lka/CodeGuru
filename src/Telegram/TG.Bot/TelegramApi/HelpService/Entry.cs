using Microsoft.Extensions.DependencyInjection;
using TelegramBotExtension.Handling;
using TG.Bot.TelegramApi.HelpService.Handlers;

namespace TG.Bot.TelegramApi.HelpService;

internal static class Entry
{
    public static IServiceCollection ConfigureHelpService(this IServiceCollection services)
    {
        services
            .AddTransient<IUpdateTypeHandler, HelpCommandHandler>();
        return services;
    }
}
