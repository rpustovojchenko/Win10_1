using System.Windows;
using Win10_1.Domain.Enums;

namespace Win10_1.UI.Services;

public static class LocalizationService
{
    private static ResourceDictionary? _currentLangDictionary;

    public static AppLanguage CurrentLang { get; private set; }

    public static void SetLanguage(AppLanguage language)
    {
        var uri = language switch
        {
            AppLanguage.Russian     => new Uri("Resources/Languages/Strings.ru-RU.xaml", UriKind.Relative),
            AppLanguage.English     => new Uri("Resources/Languages/Strings.en-US.xaml", UriKind.Relative),
            AppLanguage.German      => new Uri("Resources/Languages/Strings.de-DE.xaml", UriKind.Relative),
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, "Unsupported application language.")
        };

        var resources = Application.Current.Resources;

        if (_currentLangDictionary != null)
            resources.MergedDictionaries.Remove(_currentLangDictionary);

        _currentLangDictionary = new ResourceDictionary { Source = uri };

        resources.MergedDictionaries.Add(_currentLangDictionary);

        CurrentLang = language;
    }

    public static string GetString(string key, string fallback = "") =>
        Application.Current.TryFindResource(key) as string ?? fallback;
}
