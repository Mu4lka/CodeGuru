using Microsoft.Extensions.DependencyInjection;
using TelegramBotExtension.Handling;
using TG.Bot.TelegramApi.AuthService.Handlers;
using TG.Bot.TelegramApi.TestService.Handlers;

namespace TG.Bot.TelegramApi.AuthService;

internal static class Entry
{
    public static IServiceCollection ConfigureAuthService(this IServiceCollection services)
    {
        services
            .AddTransient<IUpdateTypeHandler, AuthCommandHandler>()
            .AddTransient<IUpdateTypeHandler, EnteringNameMessageHandler>()
            .AddTransient<IUpdateTypeHandler, EnteringPasswordMessageHandler>();
        return services;
    }
}
