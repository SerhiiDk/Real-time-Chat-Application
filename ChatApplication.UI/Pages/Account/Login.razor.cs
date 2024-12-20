using BlazorBootstrap;
using ChatApplication.Shared.V1.Models.User;
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;

namespace ChatApplication.UI.Pages.Account;
public partial class Login
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    [Inject]
    public IAccountService AccountService { get; set; }
    [Inject]
    public AuthenticationStateService StateService { get; set; }
    [Inject] 
    protected ToastService ToastService { get; set; } = default!;

    private CancellationTokenSource CancellationToken { get; set; } = new();
    private LoginUserModel Model { get; set; } = new();

    private bool loading;

    private async Task OnValidSubmit()
    {
        loading = true;
        try
        {
            var result = await AccountService.Login(Model, CancellationToken.Token);

            if (string.IsNullOrEmpty(result.UserName))
                return;

            StateService.SetLoggedIn(true);
            NavigationManager?.NavigateTo("/home");
        }
        catch (Exception)
        {

            ToastService.Notify(new(ToastType.Warning, $"Error has occurred while login"));
        }
        finally
        {
            loading = false;
        }
    }
}