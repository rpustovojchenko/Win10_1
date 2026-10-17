using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _provider;

    public NavigationService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void OpenWindow<TWindow>() where TWindow : Window
    {
        var window = _provider.GetRequiredService<TWindow>();
        window.Show();
    }

    public void CloseWindow<TWindow>() where TWindow : Window
    {
        var window = Application.Current.Windows.OfType<TWindow>().FirstOrDefault();
        window?.Close();
    }

    public void SetMainWindow<TWindow>() where TWindow : Window
    {
        var window = Application.Current.Windows.OfType<TWindow>().FirstOrDefault();
        if (window != null)
        {
            Application.Current.MainWindow = window;
        }
    }
}
