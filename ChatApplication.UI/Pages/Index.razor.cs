using ChatApplication.Shared.V1.Models.User;
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace ChatApplication.UI.Pages;
public partial class Index
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IAccountService AccountService { get; set; }
    [Inject]
    public AuthenticationStateService StateService { get; set; }

    private bool loading = default;
    public bool LoggedIn { 
        get { return AccountService.User != null; } 
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await CheckIfUserExists();
    }

    private async Task CheckIfUserExists()
    {
        loading = true;
        await AccountService.Initialize();

        if (LoggedIn)
        {
            StateService.SetLoggedIn(true);
            NavigationManager?.NavigateTo("/home");
        }
        else
        {
            NavigationManager?.NavigateTo("/login");
        }
    }
}