namespace ChatApplication.Shared.V1.Models.NotificationModels;
public abstract class BaseNotificationModel
{
    public required int ChatId { get; set; }
    public required string Message { get; set; }
}
