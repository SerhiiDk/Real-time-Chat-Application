namespace ChatApplication.UI.Service;

public class AuthenticationStateService
{
    public bool LoggedIn { get; private set; }

    public event Action OnChange;

    public void SetLoggedIn(bool loggedIn)
    {
        LoggedIn = loggedIn;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
