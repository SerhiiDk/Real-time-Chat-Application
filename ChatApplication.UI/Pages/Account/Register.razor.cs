using BlazorBootstrap;
using ChatApplication.Shared.V1.Models.User;
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace ChatApplication.UI.Pages.Account;
public partial class Register
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    [Inject]
    public IAccountService AccountService { get; set; }
    [Inject]
    protected ToastService ToastService { get; set; } = default!;

    private CancellationTokenSource CancellationToken { get; set; } = new();
    private CreateUserModel Model { get; set; } = new();
    private bool loading;

    private async Task OnValidSubmit()
    {
        loading = true;
        try
        {
            await AccountService.CreateUser(Model, CancellationToken.Token);
            StateHasChanged();
            NavigationManager?.NavigateTo("/home");
        }
        catch (Exception ex)
        {
            ToastService.Notify(new(ToastType.Warning, $"Error has occurred while register"));
        }

        loading = false;
    }
}