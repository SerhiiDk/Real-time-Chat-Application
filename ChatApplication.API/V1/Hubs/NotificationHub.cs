using ChatApplication.API.V1.Services.HubService;
using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.ConnectionModels;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.API.V1.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
    private readonly IHubConnectionService _hubConnectionService;

    public NotificationHub(IHubConnectionService hubConnectionService)
    {
        _hubConnectionService = hubConnectionService;
    }

    public async Task BroadcastMessage(string message)
    {
        await Clients.All.BroadcastMessage(message);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        //await RegisterConnection(new RegisterConnectionModel(UserIdentifier: Context.UserIdentifier, ConnectionId: Context.ConnectionId ), CancellationToken.None);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }


    private async Task RegisterConnection(RegisterConnectionModel model, CancellationToken cancellationToken)
    {
        var existingConnection = await _hubConnectionService.GetUserConnectionByUserIdentifierAsync(model.UserIdentifier, cancellationToken);

        if(existingConnection is null)
        {
            await _hubConnectionService.AddConnectionAsync(new HubConnectionDTO 
            {
                UserId = model.UserIdentifier,
                ConnectionId = model.ConnectionId,
                IsConnected = true,
                FirstConnectedUTC = DateTime.UtcNow,
                LastConnectedUTC = DateTime.UtcNow,
            }, cancellationToken);
        }
        else
        {
            await _hubConnectionService.UpdateConnectionAsync(new HubConnectionDTO
            {
                UserId = model.UserIdentifier,
                ConnectionId = model.ConnectionId,
                IsConnected = true,
                FirstConnectedUTC = existingConnection.FirstConnectedUTC,
                LastConnectedUTC = DateTime.UtcNow,
            }, cancellationToken);
        }
    }
}
