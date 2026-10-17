using System.Windows;
using System.Windows.Controls;
using Win10_1.Domain.Enums;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для PasswordControl.xaml
/// </summary>
public partial class PasswordControl : UserControl
{
    #region LabelText
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText), 
            typeof(string), 
            typeof(PasswordControl));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    #endregion

    #region LabelMargin
    public static readonly DependencyProperty LabelMarginProperty =
        DependencyProperty.Register(
            nameof(LabelMargin), 
            typeof(Thickness), 
            typeof(PasswordControl));

    public Thickness LabelMargin
    {
        get => (Thickness)GetValue(LabelMarginProperty);
        set => SetValue(LabelMarginProperty, value);
    }
    #endregion

    #region LabelPadding
    public static readonly DependencyProperty LabelPaddingProperty =
        DependencyProperty.Register(
            nameof(LabelPadding), 
            typeof(Thickness), 
            typeof(PasswordControl));

    public Thickness LabelPadding
    {
        get => (Thickness)GetValue(LabelPaddingProperty);
        set => SetValue(LabelPaddingProperty, value);
    }
    #endregion

    #region Password
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(
            nameof(Password), 
            typeof(string), 
            typeof(PasswordControl),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnPasswordPropertyChanged));

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (PasswordControl)d;

        var newPassword = e.NewValue as string ?? string.Empty;
        if (control.PasswordBoxInput.Password != newPassword)
            control.PasswordBoxInput.Password = newPassword;
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e) =>
        Password = PasswordBoxInput.Password;
    #endregion

    #region WatermarkText
    public static readonly DependencyProperty WatermarkTextProperty =
        DependencyProperty.Register(
            nameof(WatermarkText), 
            typeof(string), 
            typeof(PasswordControl));

    public string WatermarkText
    {
        get => (string)GetValue(WatermarkTextProperty);
        set => SetValue(WatermarkTextProperty, value);
    }
    #endregion

    #region Mode
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(
            nameof(Mode), 
            typeof(PasswordFieldMode), 
            typeof(PasswordControl),
            new PropertyMetadata(PasswordFieldMode.Main));

    public PasswordFieldMode Mode
    {
        get => (PasswordFieldMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    #endregion

    #region TextBoxMargin
    public static readonly DependencyProperty TextBoxMarginProperty =
        DependencyProperty.Register(
            nameof(TextBoxMargin), 
            typeof(Thickness), 
            typeof(PasswordControl));

    public Thickness TextBoxMargin
    {
        get => (Thickness)GetValue(TextBoxMarginProperty);
        set => SetValue(TextBoxMarginProperty, value);
    }
    #endregion

    #region TextBoxPadding
    public static readonly DependencyProperty TextBoxPaddingProperty =
        DependencyProperty.Register(
            nameof(TextBoxPadding), 
            typeof(Thickness), 
            typeof(PasswordControl));

    public Thickness TextBoxPadding
    {
        get => (Thickness)GetValue(TextBoxPaddingProperty);
        set => SetValue(TextBoxPaddingProperty, value);
    }
    #endregion

    #region CornerRadius
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius), 
            typeof(CornerRadius), 
            typeof(PasswordControl));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    #endregion

    #region LatinState
    public static readonly DependencyProperty LatinStateProperty =
        DependencyProperty.Register(
            nameof(LatinState), 
            typeof(PasswordRequirementState), 
            typeof(PasswordControl));

    public PasswordRequirementState LatinState
    {
        get => (PasswordRequirementState)GetValue(LatinStateProperty);
        set => SetValue(LatinStateProperty, value);
    }
    #endregion

    #region LengthState
    public static readonly DependencyProperty LengthStateProperty =
        DependencyProperty.Register(
            nameof(LengthState), 
            typeof(PasswordRequirementState), 
            typeof(PasswordControl));

    public PasswordRequirementState LengthState
    {
        get => (PasswordRequirementState)GetValue(LengthStateProperty);
        set => SetValue(LengthStateProperty, value);
    }
    #endregion

    #region SpecialState
    public static readonly DependencyProperty SpecialStateProperty =
        DependencyProperty.Register(
            nameof(SpecialState), 
            typeof(PasswordRequirementState), 
            typeof(PasswordControl));

    public PasswordRequirementState SpecialState
    {
        get => (PasswordRequirementState)GetValue(SpecialStateProperty);
        set => SetValue(SpecialStateProperty, value);
    }
    #endregion

    #region ConfirmStateProperty
    public static readonly DependencyProperty ConfirmStateProperty =
    DependencyProperty.Register(
        nameof(ConfirmState), 
        typeof(PasswordRequirementState), 
        typeof(PasswordControl));

    public PasswordRequirementState ConfirmState
    {
        get => (PasswordRequirementState)GetValue(ConfirmStateProperty);
        set => SetValue(ConfirmStateProperty, value);
    }
    #endregion

    public PasswordControl()
    {
        InitializeComponent();
    }
}
