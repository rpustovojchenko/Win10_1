using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для TaskEditorControl.xaml
/// </summary>
public partial class TaskEditorControl : UserControl
{
    #region TaskTitle
    public static readonly DependencyProperty TaskTitleProperty =
        DependencyProperty.Register(
            nameof(TaskTitle),
            typeof(string),
            typeof(TaskEditorControl),
            new PropertyMetadata(string.Empty));

    public string TaskTitle
    {
        get => (string)GetValue(TaskTitleProperty);
        set => SetValue(TaskTitleProperty, value);
    }
    #endregion

    #region TaskDescription
    public static readonly DependencyProperty TaskDescriptionProperty =
        DependencyProperty.Register(
            nameof(TaskDescription),
            typeof(string),
            typeof(TaskEditorControl),
            new PropertyMetadata(string.Empty));

    public string TaskDescription
    {
        get => (string)GetValue(TaskDescriptionProperty);
        set => SetValue(TaskDescriptionProperty, value);
    }
    #endregion

    #region AddTaskCommand
    public static readonly DependencyProperty AddTaskCommandProperty =
        DependencyProperty.Register(
            nameof(AddTaskCommand),
            typeof(ICommand),
            typeof(TaskEditorControl),
            new PropertyMetadata(null));

    public ICommand AddTaskCommand
    {
        get => (ICommand)GetValue(AddTaskCommandProperty);
        set => SetValue(AddTaskCommandProperty, value);
    }
    #endregion

    public TaskEditorControl()
    {
        InitializeComponent();
    }
}
