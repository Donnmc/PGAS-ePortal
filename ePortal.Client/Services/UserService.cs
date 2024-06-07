using Blazored.LocalStorage;
using System.Text.Json;

public class UserService
{
    private readonly ILocalStorageService _localStorage;
    private eportalUser? _currentUser;
    private bool isDarkMode;

    public event Action? OnUserChanged;
    public event Func<Task>? OnThemeChanged;

    public UserService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<eportalUser?> GetCurrentUserAsync()
    {
        if (_currentUser == null && await _localStorage.ContainKeyAsync("eid"))
        {
            var jsonString = await _localStorage.GetItemAsync<string>("eid");
            _currentUser = JsonSerializer.Deserialize<eportalUser>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return _currentUser;
    }

    public async Task SetCurrentUserAsync(eportalUser user)
    {
        _currentUser = user;
        var jsonString = JsonSerializer.Serialize(user);
        await _localStorage.SetItemAsync("eid", jsonString);
        NotifyUserChanged();
    }

    public async Task ClearCurrentUserAsync()
    {
        _currentUser = null;
        await _localStorage.RemoveItemAsync("eid");
        NotifyUserChanged();
    }

    private void NotifyUserChanged() => OnUserChanged?.Invoke();

    public class eportalUser
    {
        public long? eid { get; set; }
    }

    // Dark Mode Methods
    public async Task<bool> GetDarkModeAsync()
    {
        if (await _localStorage.ContainKeyAsync("isDarkMode"))
        {
            isDarkMode = await _localStorage.GetItemAsync<bool>("isDarkMode");
        }

        return isDarkMode;
    }

    public async Task SetDarkModeAsync(bool isDarkMode)
    {
        isDarkMode = isDarkMode;
        await _localStorage.SetItemAsync("isDarkMode", isDarkMode);
        await NotifyThemeChangedAsync();
    }

    private async Task NotifyThemeChangedAsync()
    {
        if (OnThemeChanged != null)
        {
            await OnThemeChanged.Invoke();
        }
    }
}
