using ChatApplication.DataAccess.Context;
using ChatApplication.DataAccess.Entities;
using ChatApplication.Shared.V1.Dtos;

namespace ChatApplication.API.V1.Services.HubService;
public class HubConnectionService : IHubConnectionService
{
    private readonly ChatDbContext _chatDbContext;
    public HubConnectionService(ChatDbContext chatDbContext)
    {
        _chatDbContext = chatDbContext;
    }

    public async Task AddConnectionAsync(HubConnectionDTO hubConnection, CancellationToken cancellationToken = default)
    {
        var entity = new UserConnection()
        {
            ConnectionId = hubConnection.ConnectionId,
            UserId = hubConnection.UserId,
            FirstConnectedUTC = hubConnection.FirstConnectedUTC,
            IsConnected = true,
            LastConnectedUTC = hubConnection.LastConnectedUTC,
            Name = hubConnection.UserId
        };

        _chatDbContext.UserConnections.Add(entity);
        await _chatDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<HubConnectionDTO> GetUserConnectionByUserIdentifierAsync(string userIdentifier, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateConnectionAsync(HubConnectionDTO hubConnection, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
