using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;

namespace ChatApplication.UI.Pages.Account;
public partial class Logout
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    [Inject]
    public IAccountService AccountService { get; set; }
    [Inject]
    public AuthenticationStateService StateService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await AccountService.Logout();
        StateService.SetLoggedIn(false);
        NavigationManager?.NavigateTo("/login");
    }
}