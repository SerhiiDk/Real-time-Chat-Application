
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace ChatApplication.UI.Pages;
public partial class Home
{
    [Inject]
    private IAccountService AccountService { get; set; } = default!;

    [Inject]
    public AuthenticationStateService StateService { get; set; }

    public bool LoggedIn
    {
        get { return AccountService.User != null; }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await CheckIfUserExists();
    }

    private async Task CheckIfUserExists()
    {
        await AccountService.Initialize();

        if (LoggedIn)
        {
            StateService.SetLoggedIn(true);
            StateHasChanged();
        }
    }
}