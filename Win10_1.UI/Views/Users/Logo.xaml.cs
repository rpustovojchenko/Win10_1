using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для Logo.xaml
/// </summary>
public partial class Logo : UserControl
{
    #region ViewBoxWidth
    public static readonly DependencyProperty ViewBoxWidthProperty =
        DependencyProperty.Register(
            nameof(ViewBoxWidth), 
            typeof(double), 
            typeof(Logo));

    public double ViewBoxWidth
    {
        get => (double)GetValue(ViewBoxWidthProperty);
        set => SetValue(ViewBoxWidthProperty, value);
    }
    #endregion

    #region VewBoxHeight
    public static readonly DependencyProperty ViewBoxHeightProperty =
        DependencyProperty.Register(
            nameof(ViewBoxHeight), 
            typeof(double), 
            typeof(Logo));

    public double ViewBoxHeight
    {
        get => (double)GetValue(ViewBoxHeightProperty);
        set => SetValue(ViewBoxHeightProperty, value);
    }
    #endregion

    #region Kind
    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(
            nameof(Kind), 
            typeof(PackIconMaterialDesignKind), 
            typeof(Logo));

    public PackIconMaterialDesignKind Kind
    {
        get => (PackIconMaterialDesignKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }
    #endregion

    #region Text
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text), 
            typeof(string), 
            typeof(Logo));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion

    public Logo()
    {
        InitializeComponent();
    }
}
