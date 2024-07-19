using System.Net;
using Telegram.Bot;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.Enums;
using TG.Bot.Intagrations.BackendApi;

namespace TG.Bot.TelegramApi.AuthService.Handlers;

internal class EnteringPasswordMessageHandler(IBackendApi backendApi) : MessageHandler
{
    [StateFilter(nameof(AuthState.Password))]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        var userData = await context.State.GetData();
        var name = (string)userData[nameof(AuthState.Name)];
        var password = context.Data;

        var response = await backendApi.LoginAsync(new() {Name = name, Password = password });
        if (response.IsSuccessStatusCode)
        {
            await backendApi.LinkTelegramAsync(new() {Name = name, Password = password, TelegramUserId = context.UserId });
            await context.BotClient.SendTextMessageAsync(context.UserId, "Вы успешно привязали аккаунт");
        }
        else
        {
            await context.BotClient.SendTextMessageAsync(
                context.UserId,
                "Не удалось привязать аккаунт, попробуте еще раз /auth");
        }
        await context.State.Clear();
    }
}
