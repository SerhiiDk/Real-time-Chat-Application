using ChatApplication.Shared.V1.Models.NotificationModels;

namespace ChatApplication.API.V1.Services.TextAnalyzeService;

public interface ITextAnalyzeService
{
    Task<MessageSentiment> HandleMessage(ChatNotificationModel model, CancellationToken cancellation);
}
