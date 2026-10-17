using System.Windows;
using Win10_1.Domain.Enums;
using Win10_1.UI.Services;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels;

public class SettingsVM : BaseVM
{
    private AppLanguage _selectedLang   = AppLanguage.Russian;
    private AppTheme    _selectedTheme  = AppTheme.Dark;

    public Array Languages  => Enum.GetValues(typeof(AppLanguage));
    public Array Themes     => Enum.GetValues(typeof(AppTheme));

    public AppLanguage SelectedLang
    {
        get => _selectedLang;
        set
        {
            if (Set(ref _selectedLang, value))
                ApplyLanguage(value);
        }
    }

    public AppTheme SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (Set(ref _selectedTheme, value))
                ApplyTheme(value);
        }
    }

    private void ApplyLanguage(AppLanguage language) =>
        LocalizationService.SetLanguage(language);

    private void ApplyTheme(AppTheme theme) =>
        ((App)Application.Current).ChangeTheme(theme);
}
