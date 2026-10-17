using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.DialogsVM;

public class CommentInputDialogVM : BaseVM
{
    private readonly ICommentValidateService _commentValidator;

    private string _author          = string.Empty;
    private string _comment         = string.Empty;
    private string _confirmBtnText  = string.Empty;

    private string? _commentError;

    public string Author
    {
        get => _author;
        set => Set(ref _author, value);
    }

    public string Comment
    {
        get => _comment;
        set
        {
            Set(ref _comment, value);
            OnCommentChange();
        }
    }

    public string ConfirmBtnText
    {
        get => _confirmBtnText;
        set => Set(ref _confirmBtnText, value);
    }

    public string? CommentError
    {
        get => _commentError;
        private set => Set(ref _commentError, value);
    }

    public bool IsConfirmed { get; private set; }

    public ICommand ConfirmCommand  { get; }
    public ICommand CancelCommand   { get; }

    public event EventHandler? CloseRequested;

    public CommentInputDialogVM(ICommentValidateService commentValidator)
    {
        _commentValidator = commentValidator;

        ConfirmCommand  = new AsyncRelayCommand(OnConfirmCommandExecuted, CanConfirmCommandExecute);
        CancelCommand   = new AsyncRelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);
    }

    private bool CanConfirmCommandExecute() => true;
    private bool CanCancelCommandExecute()  => true;

    private async Task OnConfirmCommandExecuted()
    {
        CommentError = await _commentValidator.CheckComment(Comment);

        if (!string.IsNullOrEmpty(CommentError)) return;

        IsConfirmed = true;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async Task OnCancelCommandExecuted()
    {
        IsConfirmed = false;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async void OnCommentChange() =>
        CommentError = await _commentValidator.CheckComment(Comment);
}
