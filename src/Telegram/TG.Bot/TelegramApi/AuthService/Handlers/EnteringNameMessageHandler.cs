using Telegram.Bot;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.Enums;

namespace TG.Bot.TelegramApi.AuthService.Handlers;

internal class EnteringNameMessageHandler : MessageHandler
{
    [StateFilter(nameof(AuthState.Name))]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        await context.State.UpdateData(nameof(AuthState.Name), context.Data);
        await context.BotClient.SendTextMessageAsync(context.UserId, "Введите пароль");
        await context.State.SetState(nameof(AuthState.Password));
    }
}
