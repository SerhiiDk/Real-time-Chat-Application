using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;

namespace ChatApplication.UI.Layout;
public partial class NavMenu : IDisposable
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IAccountService AccountService { get; set; }

    [Inject]
    public AuthenticationStateService StateService { get; set; }

    private bool loading = default;
    public bool LoggedIn
    {
        get { return AccountService.User != null; }
    }

    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    protected override async Task OnInitializedAsync()
    {
        await GetAccess();
        await base.OnInitializedAsync();
    }

    private async Task GetAccess()
    {
        await AccountService.Initialize();
        StateService.OnChange += StateHasChanged;
        if (AccountService?.User?.UserName != null)
        {
            StateService.SetLoggedIn(true);
        }
    }
    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    public void Dispose()
    {
        StateService.OnChange -= StateHasChanged;
    }
}