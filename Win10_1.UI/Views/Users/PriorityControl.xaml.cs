using System.Windows;
using System.Windows.Controls;
using Win10_1.Domain.Enums;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для PriorityControl.xaml
/// </summary>
public partial class PriorityControl : UserControl
{
    #region LabelText
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText),
            typeof(string),
            typeof(PriorityControl));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    #endregion

    #region Priority
    public static readonly DependencyProperty PriorityProperty =
        DependencyProperty.Register(
            nameof(Priority),
            typeof(Priority),
            typeof(PriorityControl),
            new FrameworkPropertyMetadata(
                Priority.Low,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public Priority Priority
    {
        get => (Priority)GetValue(PriorityProperty);
        set => SetValue(PriorityProperty, value);
    }
    #endregion

    public PriorityControl()
    {
        InitializeComponent();
    }
}
