using Microsoft.JSInterop;
using System.Text.Json;

namespace ChatApplication.UI.Service;


public interface ILocalStorageService
{
    Task<T> GetItemFromLocalStorage<T>(string key);
    Task SetItemToLocalStorage<T>(string key, T value);
    Task RemoveItem(string key);
}
public class LocalStorageService : ILocalStorageService
{
    private IJSRuntime _jsRuntime;
    public LocalStorageService(IJSRuntime jsRuntim)
    {
        _jsRuntime = jsRuntim;
    }
    public async Task<T> GetItemFromLocalStorage<T>(string key)
    {
        var json =  await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

        if (json == null)
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveItem(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task SetItemToLocalStorage<T>(string key, T value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }
}
