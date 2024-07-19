using System.Diagnostics;
using Telegram.Bot;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.Enums;
using TG.Bot.Intagrations.BackendApi;
using TG.Bot.TelegramApi.TestService.Views;

namespace TG.Bot.TelegramApi.TestService.Handlers;

/// <summary>
/// Обработчик команды /tests. Отображаются список тестов в виде кнопок
/// </summary>
/// <param name="_backendApi"></param>
internal class TestsCommandHandler(IBackendApi _backendApi) : MessageHandler
{
    [Command("tests")]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        ///TODO: Время выполнения запроса GetTestNamesAndIdsAsync в backend (276 мс)
        ///нужна оптимизация (кэширование и/или обращение напрямую к контроллеру)
        var response = await _backendApi.GetTestNamesAndIdsAsync();
        var testNamesAndIds = response.Content!;

        if (testNamesAndIds.Count == 0)
        {
            await context.BotClient.SendTextMessageAsync(
                context.UserId,
                "Тестов пока что нет, попробуйте позже... :(");
            return;
        }
        await TestsView.ShowAsync(context, testNamesAndIds);

        await context.State.Clear();
        await context.State.SetState(nameof(TestState.SelectingTest));
    }
}
