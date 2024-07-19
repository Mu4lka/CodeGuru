using System;
using System.Collections.Generic;
using Telegram.Bot;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.Enums;

namespace TG.Bot.TelegramApi.AuthService.Handlers;

internal class AuthCommandHandler : MessageHandler
{
    [Command("auth")]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        await context.BotClient.SendTextMessageAsync(context.UserId, "Введите логин");
        await context.State.SetState(nameof(AuthState.Name));
    }
}
