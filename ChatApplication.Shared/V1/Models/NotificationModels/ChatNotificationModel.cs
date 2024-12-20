namespace ChatApplication.Shared.V1.Models.NotificationModels;
public class ChatNotificationModel : BaseNotificationModel
{
    public string? Sender { get; set; }
    public MessageSentiment? MessageSentiment { get; set; }
}
