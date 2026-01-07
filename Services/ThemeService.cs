using Blazored.LocalStorage;
using MudBlazor;

namespace MyPortfolio.Services;

public class ThemeService
{
    private readonly ILocalStorageService _localStorage;
    private MudTheme _currentTheme;
    private bool _isDarkMode;
    //private Stream

    public event Action<bool>? ThemeChanged;

    public MudTheme CurrentTheme => _currentTheme;
    public bool IsDarkMode => _isDarkMode;

    public ThemeService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _currentTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.Blue.Default,
                Secondary = Colors.Green.Default,
                AppbarBackground = Colors.Blue.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten1,
                Secondary = Colors.Green.Lighten1,
                AppbarBackground = Colors.Blue.Darken1,
            }
        };
    }

    public async Task InitializeAsync()
    {
        try
        {
            var savedTheme = await _localStorage.GetItemAsStringAsync("theme-preference");
            _isDarkMode = savedTheme == "dark";
        }
        catch
        {
            _isDarkMode = false; // Par défaut, mode clair
        }
        await NotifyThemeChanged();
    }

    public async Task ToggleThemeAsync()
    {
        _isDarkMode = !_isDarkMode;
        await _localStorage.SetItemAsStringAsync("theme-preference", _isDarkMode ? "dark" : "light");
        await NotifyThemeChanged();
    }

    private async Task NotifyThemeChanged()
    {
        ThemeChanged?.Invoke(_isDarkMode);
        await Task.CompletedTask;
    }
}

