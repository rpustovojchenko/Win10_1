using System.Windows;
using System.Windows.Controls;
using Win10_1.Domain.Enums;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для StatusControl.xaml
/// </summary>
public partial class StatusControl : UserControl
{
    #region LabelText
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText),
            typeof(string),
            typeof(StatusControl));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    #endregion

    #region Status
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(
            nameof(Status),
            typeof(Status),
            typeof(StatusControl),
            new FrameworkPropertyMetadata(
                Status.NonStarted,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public Status Status
    {
        get => (Status)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
    #endregion

    public StatusControl()
    {
        InitializeComponent();
    }
}
