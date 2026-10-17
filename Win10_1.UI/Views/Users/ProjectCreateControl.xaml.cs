using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для ProjectCreateControl.xaml
/// </summary>
public partial class ProjectCreateControl : UserControl
{
    #region Project
    public static readonly DependencyProperty ProjectProperty =
        DependencyProperty.Register(
            nameof(Project),
            typeof(ProjectVM), 
            typeof(ProjectCreateControl),
            new PropertyMetadata(null));

    public Project Project
    {
        get => (Project)GetValue(ProjectProperty);
        set => SetValue(ProjectProperty, value);
    }
    #endregion

    #region AddProjectCommand
    public static readonly DependencyProperty AddProjectCommandProperty =
        DependencyProperty.Register(
            nameof(AddProjectCommand),
            typeof(ICommand),
            typeof(ProjectCreateControl),
            new PropertyMetadata(null));

    public ICommand AddProjectCommand
    {
        get => (ICommand)GetValue(AddProjectCommandProperty);
        set => SetValue(AddProjectCommandProperty, value);
    }
    #endregion

    public ProjectCreateControl()
    {
        InitializeComponent();
    }
}
