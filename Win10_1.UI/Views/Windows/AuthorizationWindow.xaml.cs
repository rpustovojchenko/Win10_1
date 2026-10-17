using MahApps.Metro.Controls;
using Win10_1.UI.ViewModels;

namespace Win10_1.UI.Views.Windows;

/// <summary>
/// Логика взаимодействия для AuthorizationWindow.xaml
/// </summary>
public partial class AuthorizationWindow : MetroWindow
{
    public AuthorizationWindow(AuthorizationVM vm)
    {
        InitializeComponent();

        DataContext = vm;
    }
}
