namespace ChatApplication.Shared.V1.Dtos;
public class HubConnectionDTO
{
    public required string UserId { get; set; }
    public required string ConnectionId { get; set; }
    public bool IsConnected { get; set; }
    public DateTime FirstConnectedUTC { get; set; }
    public DateTime LastConnectedUTC { get; set; }
}
