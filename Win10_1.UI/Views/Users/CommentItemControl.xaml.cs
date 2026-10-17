using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Views.Users;

/// <summary>
/// Логика взаимодействия для CommentItemControl.xaml
/// </summary>
public partial class CommentItemControl : UserControl
{
    #region Author
    public static readonly DependencyProperty AuthorProperty =
        DependencyProperty.Register(
            nameof(Author),
            typeof(string),
            typeof(CommentItemControl),
            new PropertyMetadata(string.Empty));

    public string Author
    {
        get => (string)GetValue(AuthorProperty);
        set => SetValue(AuthorProperty, value);
    }
    #endregion

    #region Comment
    public static readonly DependencyProperty CommentProperty =
        DependencyProperty.Register(
            nameof(Comment),
            typeof(string),
            typeof(CommentItemControl),
            new PropertyMetadata(string.Empty));

    public string Comment
    {
        get => (string)GetValue(CommentProperty);
        set => SetValue(CommentProperty, value);
    }
    #endregion

    #region UpdatedAt
    public static readonly DependencyProperty UpdatedAtProperty =
        DependencyProperty.Register(
            nameof(UpdatedAt),
            typeof(DateTime),
            typeof(CommentItemControl),
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
            typeof(CommentItemControl),
            new PropertyMetadata(null));

    public static readonly DependencyProperty EditCommandParameterProperty =
        DependencyProperty.Register(
            nameof(EditCommandParameter),
            typeof(CommentVM),
            typeof(CommentItemControl));

    public ICommand EditCommand
    {
        get => (ICommand)GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public CommentVM? EditCommandParameter
    {
        get => (CommentVM?)GetValue(EditCommandParameterProperty);
        set => SetValue(EditCommandParameterProperty, value);
    }
    #endregion

    #region DeleteCommand
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register(
            nameof(DeleteCommand),
            typeof(ICommand),
            typeof(CommentItemControl),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DeleteCommandParameterProperty =
        DependencyProperty.Register(
            nameof(DeleteCommandParameter),
            typeof(CommentVM),
            typeof(CommentItemControl));

    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public CommentVM? DeleteCommandParameter
    {
        get => (CommentVM?)GetValue(DeleteCommandParameterProperty);
        set => SetValue(DeleteCommandParameterProperty, value);
    }
    #endregion

    public CommentItemControl()
    {
        InitializeComponent();
    }
}
