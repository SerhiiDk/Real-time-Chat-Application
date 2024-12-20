using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.User;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ChatApplication.UI.Service;

public interface IAccountService
{
    Task CreateUser(CreateUserModel model, CancellationToken cancellationToken);
    Task<UserDTO> Login(LoginUserModel model, CancellationToken cancellationToken);
    Task Logout();
    UserDTO User { get; }
    Task Initialize();
}

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;

    private const string USER_KEY = "user";
    public UserDTO? User { get; private set; }

    public AccountService(HttpClient httpClient, ILocalStorageService localStorageService, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
    }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItemFromLocalStorage<UserDTO>(USER_KEY);
    }

    public async Task CreateUser(CreateUserModel model, CancellationToken cancellationToken)
    {
        var response = await PostJsonAsync("/api/realtime/v1/Account/Register", model, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create user.");
        }
        await _localStorageService.SetItemToLocalStorage(USER_KEY, new UserDTO { UserName = model.UserName});
    }

    public async Task<UserDTO> Login(LoginUserModel model, CancellationToken cancellationToken)
    {
        var response = await PostJsonAsync("/api/realtime/v1/Account/Login", model, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDTO>(GetJsonSerializerOptions(), cancellationToken);
            if (user != null)
            {
                await _localStorageService.SetItemToLocalStorage(USER_KEY, user);
                User = user;
                return user;
            }
        }

        throw new Exception("Invalid login attempt.");
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem(USER_KEY);
    }

    private async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, CancellationToken cancellationToken)
    {
        return await _httpClient.PostAsJsonAsync(url, data, cancellationToken);
    }

    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
