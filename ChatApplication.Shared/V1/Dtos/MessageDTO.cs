namespace ChatApplication.Shared.V1.Dtos;
public class MessageDTO
{
    public string Sender { get; set; }
    public string Message { get; set; }
    public string? Sentiment { get; set; }
    public DateTime CreatedAt { get; set; }
}
