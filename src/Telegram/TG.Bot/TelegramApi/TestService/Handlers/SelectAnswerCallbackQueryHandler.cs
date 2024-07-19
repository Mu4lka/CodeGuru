using Api.Contracts.Tests.Dto;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Handling;
using TelegramBotExtension.Types;
using TG.Bot.Enums;
using TG.Bot.Extensions;
using TG.Bot.TelegramApi.TestService.Views;

namespace TG.Bot.TelegramApi.TestService.Handlers;

/// <summary>
/// Обработчик нажатия кнопки варианта ответа
/// </summary>
internal class SelectAnswerCallbackQueryHandler : CallbackQueryHandler
{
    [StateFilter(nameof(TestState.PassingTest))]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        var userData = await context.State.GetData();
        GetQuestionDto currentQuestion = (GetQuestionDto)userData[nameof(currentQuestion)];

        int messageId = (int)userData[nameof(messageId)];
        var lastMessageId = context.Update.CallbackQuery!.Message!.MessageId;

        var success = Guid.TryParse(context.Data, out Guid answerId);

        if (messageId != lastMessageId || !success)
        {
            await QuestionView.ErrorAsync(context);
            return;
        }

        var answers = currentQuestion.Answers.MarkAnswer(context.Data);
        await QuestionView.EditAnswerAsync(context, answers);
        await context.State.UpdateData(nameof(answerId), answerId);
    }
}
