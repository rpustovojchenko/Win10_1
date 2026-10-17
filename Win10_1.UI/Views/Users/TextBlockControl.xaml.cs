using System.Windows;
using System.Windows.Controls;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для TextBlockControl.xaml
/// </summary>
public partial class TextBlockControl : UserControl
{
    #region LabelText
    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText), 
            typeof(string), 
            typeof(TextBlockControl));

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
            typeof(TextBlockControl));

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
            typeof(TextBlockControl));

    public Thickness LabelPadding
    {
        get => (Thickness)GetValue(LabelPaddingProperty);
        set => SetValue(LabelPaddingProperty, value);
    }
    #endregion

    #region Text
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text), 
            typeof(string), 
            typeof(TextBlockControl),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion

    #region TextWrapping
    public static readonly DependencyProperty TextWrappingProperty =
        DependencyProperty.Register(
            nameof(TextWrapping),
            typeof(TextWrapping),
            typeof(TextBlockControl));

    public TextWrapping TextWrapping
    {
        get => (TextWrapping)GetValue(TextWrappingProperty);
        set => SetValue(TextWrappingProperty, value);
    }
    #endregion

    #region AcceptsReturn
    public static readonly DependencyProperty AcceptsReturnProperty =
        DependencyProperty.Register(
            nameof(AcceptsReturn),
            typeof(bool),
            typeof(TextBlockControl));

    public bool AcceptsReturn
    {
        get => (bool)GetValue(AcceptsReturnProperty);
        set => SetValue(AcceptsReturnProperty, value);
    }
    #endregion

    #region WatermarkText
    public static readonly DependencyProperty WatermarkTextProperty =
        DependencyProperty.Register(
            nameof(WatermarkText), 
            typeof(string), 
            typeof(TextBlockControl));

    public string WatermarkText
    {
        get => (string)GetValue(WatermarkTextProperty);
        set => SetValue(WatermarkTextProperty, value);
    }
    #endregion

    #region TextBoxMargin
    public static readonly DependencyProperty TextBoxMarginProperty =
        DependencyProperty.Register(
            nameof(TextBoxMargin), 
            typeof(Thickness), 
            typeof(TextBlockControl));

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
            typeof(TextBlockControl));

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
            typeof(TextBlockControl));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    #endregion

    public TextBlockControl()
    {
        InitializeComponent();
    }
}
