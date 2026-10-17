using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для TaskItemControl.xaml
/// </summary>
public partial class TaskItemControl : UserControl
{
    #region TaskTitle
    public static readonly DependencyProperty TaskTitleProperty =
        DependencyProperty.Register(
            nameof(TaskTitle),
            typeof(string),
            typeof(TaskItemControl),
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
            typeof(TaskItemControl),
            new PropertyMetadata(string.Empty));

    public string TaskDescription
    {
        get => (string)GetValue(TaskDescriptionProperty);
        set => SetValue(TaskDescriptionProperty, value);
    }
    #endregion

    #region UpdatedAt
    public static readonly DependencyProperty UpdatedAtProperty =
        DependencyProperty.Register(
            nameof(UpdatedAt),
            typeof(DateTime),
            typeof(TaskItemControl),
            new PropertyMetadata(DateTime.Now));

    public DateTime UpdatedAt
    {
        get => (DateTime)GetValue(UpdatedAtProperty);
        set => SetValue(UpdatedAtProperty, value);
    }
    #endregion

    #region EditCommand
    public static readonly DependencyProperty EditCommandProperty =
        DependencyProperty.Register(
            nameof(EditCommand),
            typeof(ICommand),
            typeof(TaskItemControl),
            new PropertyMetadata(null));

    public static readonly DependencyProperty EditCommandParameterProperty =
        DependencyProperty.Register(
            nameof(EditCommandParameter),
            typeof(TaskVM),
            typeof(TaskItemControl));

    public ICommand EditCommand
    {
        get => (ICommand)GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public TaskVM? EditCommandParameter
    {
        get => (TaskVM?)GetValue(EditCommandParameterProperty);
        set => SetValue(EditCommandParameterProperty, value);
    }
    #endregion

    #region DeleteCommand
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register(
            nameof(DeleteCommand),
            typeof(ICommand),
            typeof(TaskItemControl),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DeleteCommandParameterProperty =
        DependencyProperty.Register(
            nameof(DeleteCommandParameter),
            typeof(TaskVM),
            typeof(TaskItemControl));

    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public TaskVM? DeleteCommandParameter
    {
        get => (TaskVM?)GetValue(DeleteCommandParameterProperty);
        set => SetValue(DeleteCommandParameterProperty, value);
    }
    #endregion

    public TaskItemControl()
    {
        InitializeComponent();
    }
}
