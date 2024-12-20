using ChatApplication.Shared.V1.Dtos;

namespace ChatApplication.API.V1.Services.HubService;

public interface IHubConnectionService
{
    Task AddConnectionAsync(HubConnectionDTO hubConnection, CancellationToken cancellationToken = default);
    Task<HubConnectionDTO> GetUserConnectionByUserIdentifierAsync(string userIdentifier, CancellationToken cancellationToken = default);
    Task UpdateConnectionAsync(HubConnectionDTO hubConnection, CancellationToken cancellationToken = default);
}
