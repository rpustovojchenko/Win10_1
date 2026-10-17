using System.Windows;
using System.Windows.Controls;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для PercentageControl.xaml
/// </summary>
public partial class PercentageControl : UserControl
{
    #region LabelText
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText),
            typeof(string),
            typeof(PercentageControl));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    #endregion

    #region Percent
    public static readonly DependencyProperty PercentProperty =
        DependencyProperty.Register(
            nameof(Percent),
            typeof(double),
            typeof(PercentageControl),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnPercentChanged));

    private static void OnPercentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (PercentageControl)d;

        double value = (double)e.NewValue;

        double validValue = Math.Max(0, Math.Min(100, value));

        if (!value.Equals(validValue))
            control.Percent = validValue;
    }

    public double Percent
    {
        get => (double)GetValue(PercentProperty);
        set => SetValue(PercentProperty, value);
    }
    #endregion

    public PercentageControl()
    {
        InitializeComponent();
    }
}
