using Blazored.LocalStorage;

public class UserService
{
    private readonly ILocalStorageService _localStorage;
    private long? _eid = null;
    private bool? _loaded = false;

    public UserService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public long? eid
    {
        get => _eid;
        set
        {
            _eid = value;
            NotifyStateChanged();
            SaveUserSession().ConfigureAwait(false);
        }
    }
    public bool? loaded
    {
        get => _loaded;
        set
        {
            _loaded = value;
            NotifyStateChanged();
            SaveUserSession().ConfigureAwait(false);
        }
    }

    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task LoadUserSession()
    {
        _eid = await _localStorage.GetItemAsync<long?>("eid");
        _loaded = await _localStorage.GetItemAsync<bool?>("loaded");
        NotifyStateChanged();
    }

    private async Task SaveUserSession()
    {
        await _localStorage.SetItemAsync("eid", _eid);
        await _localStorage.SetItemAsync("loaded", _loaded);
    }

    public async Task ClearUserSession()
    {
        await _localStorage.RemoveItemAsync("eid");
        await _localStorage.RemoveItemAsync("loaded");
        _eid = null;
        _loaded = null;
        NotifyStateChanged();
    }
}
