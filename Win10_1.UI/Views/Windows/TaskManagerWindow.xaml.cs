using MahApps.Metro.Controls;
using System.Windows;
using Win10_1.UI.ViewModels;

namespace Win10_1.UI.Views.Windows;

/// <summary>
/// Логика взаимодействия для TaskManagerWindow.xaml
/// </summary>
public partial class TaskManagerWindow : MetroWindow
{
    public TaskManagerWindow(TaskManagerVM vm)
    {
        InitializeComponent();

        DataContext = vm;

        this.Closed += (s, e) => Application.Current.Shutdown();
    }
}
