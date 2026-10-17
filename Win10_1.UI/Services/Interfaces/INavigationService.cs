using System.Windows;

namespace Win10_1.UI.Services.Interfaces;

public interface INavigationService
{
    void OpenWindow<TWindow>()      where TWindow : Window;
    void CloseWindow<TWindow>()     where TWindow : Window;
    void SetMainWindow<TWindow>()   where TWindow : Window;
}
