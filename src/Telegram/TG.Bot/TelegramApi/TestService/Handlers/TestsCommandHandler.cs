using Telegram.Bot;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.CacheServices.Base;
using TG.Bot.Enums;
using TG.Bot.TelegramApi.TestService.Views;

namespace TG.Bot.TelegramApi.TestService.Handlers;

/// <summary>
/// Обработчик команды /tests. Отображаются список тестов в виде кнопок
/// </summary>
/// <param name="_testingPlatformApi"></param>
internal class TestsCommandHandler(
    ICachedTestingPlatformApi _testingPlatformApi,
    ICachedBackendApi _backendApi) : MessageHandler
{
    [Command("tests")]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        var backendResponse = await _backendApi.GetTelegramAccountBindingAsync(context.UserId);

        if (!backendResponse.IsSuccessStatusCode)
        {
            await context.BotClient.SendTextMessageAsync(
                context.UserId,
                "Авторизируйтесь с помощью команды /auth");
            return;
        }

        var response = await _testingPlatformApi.GetTestNamesAndIdsAsync();

        if (!response.IsSuccessStatusCode) return;

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
