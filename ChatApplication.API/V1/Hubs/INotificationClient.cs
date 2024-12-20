using ChatApplication.Shared.V1.Models.NotificationModels;

namespace ChatApplication.API.V1.Hubs;

public interface INotificationClient
{
    Task BroadcastMessage(string message, CancellationToken token = default);
    Task SendChatNotification(ChatNotificationModel model, CancellationToken token = default);
}
