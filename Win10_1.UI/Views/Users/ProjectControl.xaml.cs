using System.Windows;
using System.Windows.Controls;
using Win10_1.Domain.Enums;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для ProjectControl.xaml
/// </summary>
public partial class ProjectControl : UserControl
{
    #region ProjectName
    public static readonly DependencyProperty ProjectNameProperty =
        DependencyProperty.Register(
            nameof(ProjectName),
            typeof(string),
            typeof(ProjectControl));

    public string ProjectName
    {
        get => (string)GetValue(ProjectNameProperty);
        set => SetValue(ProjectNameProperty, value);
    }
    #endregion

    #region Theme
    public static readonly DependencyProperty ThemeProperty =
        DependencyProperty.Register(
            nameof(Theme),
            typeof(string),
            typeof(ProjectControl));

    public string Theme
    {
        get => (string)GetValue(ThemeProperty);
        set => SetValue(ThemeProperty, value);
    }
    #endregion

    #region PercentComplete
    public static readonly DependencyProperty PercentCompleteProperty =
        DependencyProperty.Register(
            nameof(PercentComplete),
            typeof(int),
            typeof(ProjectControl),
            new PropertyMetadata(0));

    public int PercentComplete
    {
        get => (int)GetValue(PercentCompleteProperty);
        set => SetValue(PercentCompleteProperty, value);
    }
    #endregion

    #region Priority
    public static readonly DependencyProperty PriorityProperty =
        DependencyProperty.Register(
            nameof(Priority),
            typeof(Priority),
            typeof(ProjectControl),
            new PropertyMetadata(Priority.None));

    public Priority Priority
    {
        get => (Priority)GetValue(PriorityProperty);
        set => SetValue(PriorityProperty, value);
    }
    #endregion

    #region Status
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(Status),
            typeof(ProjectControl),
            new PropertyMetadata(Status.None));

    public Status Status
    {
        get => (Status)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
    #endregion

    #region DeadLine
    public static readonly DependencyProperty DeadLineProperty =
        DependencyProperty.Register(
            nameof(DeadLine),
            typeof(DateTime?),
            typeof(ProjectControl));

    public DateTime? DeadLine
    {
        get => (DateTime?)GetValue(DeadLineProperty);
        set => SetValue(DeadLineProperty, value);
    }
    #endregion

    #region CreatedAt
    public static readonly DependencyProperty CreatedAtProperty =
        DependencyProperty.Register(
            nameof(CreatedAt),
            typeof(DateTime),
            typeof(ProjectControl));

    public DateTime CreatedAt
    {
        get => (DateTime)GetValue(CreatedAtProperty);
        set => SetValue(CreatedAtProperty, value);
    }
    #endregion

    #region UpdatedAt
    public static readonly DependencyProperty UpdatedAtProperty =
        DependencyProperty.Register(
            nameof(UpdatedAt),
            typeof(DateTime),
            typeof(ProjectControl));

    public DateTime UpdatedAt
    {
        get => (DateTime)GetValue(UpdatedAtProperty);
        set => SetValue(UpdatedAtProperty, value);
    }
    #endregion

    public ProjectControl()
    {
        InitializeComponent();
    }
}
