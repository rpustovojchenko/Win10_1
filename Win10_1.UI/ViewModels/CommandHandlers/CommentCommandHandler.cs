using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Localization;
using Win10_1.Domain.Models;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.DialogsVM.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.ViewModels.CommandHandlers;

public class CommentCommandHandler
{
    private readonly TaskManagerVM              _vm;
    private readonly IProjectCommentRepository  _commentRepo;
    private readonly IDialogVMService           _dialogService;
    private readonly IMessageService            _messageService;

    public ICommand AddCommentCommand       { get; private set; } = null!;
    public ICommand EditCommentCommand      { get; private set; } = null!;
    public ICommand DeleteCommentCommand    { get; private set; } = null!;

    public CommentCommandHandler(
        TaskManagerVM               vm,
        IProjectCommentRepository   commentRepo,
        IDialogVMService            dialogService,
        IMessageService             messageService)
    {
        _vm                 = vm;
        _commentRepo        = commentRepo;
        _dialogService      = dialogService;
        _messageService     = messageService;
    }

    public void InitializeCommands()
    {
        AddCommentCommand       = new AsyncRelayCommand(OnAddCommentCommandExecuted, CanAddCommentCommandExecute);
        EditCommentCommand      = new AsyncRelayCommandWithParam<CommentVM?>(OnEditCommentCommandExecuted   , CanEditCommentCommandExecute);
        DeleteCommentCommand    = new AsyncRelayCommandWithParam<CommentVM?>(OnDeleteCommentCommandExecuted , CanDeleteCommentCommandExecute);
    }

    private bool CanAddCommentCommandExecute() => true;
    private bool CanEditCommentCommandExecute(CommentVM? comment)   => comment is not null;
    private bool CanDeleteCommentCommandExecute(CommentVM? comment) => comment is not null;

    private async Task OnAddCommentCommandExecuted()
    {
        if (_vm.SelectedProject is null) return;

        await _dialogService.OpenCommentDialogAsync(
            title           : LocalizationService.GetString(LocalizationKeys.AddCommentTitle),
            confirmBtnText  : LocalizationService.GetString(LocalizationKeys.AddCommentButton),
            author          : _vm.User.Login ?? string.Empty,
            initialComment  : string.Empty,

            onConfirm: async vm =>
            {
                var newComment = new ProjectComment
                {
                    Author      = vm.Author,
                    Comment     = vm.Comment,
                    CreatedAt   = DateTime.UtcNow,
                    UpdatedAt   = DateTime.UtcNow,
                    ProjectId   = _vm.SelectedProject.Id
                };

                await _commentRepo.AddAsync(newComment);
                await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
            });
    }

    private async Task OnEditCommentCommandExecuted(CommentVM? comment)
    {
        if (comment is null || _vm.SelectedProject is null) return;

        await _dialogService.OpenCommentDialogAsync(
            title           : LocalizationService.GetString(LocalizationKeys.EditCommentTitle),
            confirmBtnText  : LocalizationService.GetString(LocalizationKeys.EditCommentButton),
            author          : comment.Author,
            initialComment  : comment.Comment,

            onConfirm: async vm =>
            {
                var existing = await _commentRepo.GetByIdAsync(comment.Id);
                if (existing is null) return;

                existing.Comment = vm.Comment;

                await _commentRepo.UpdateAsync(existing);
                await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
            });
    }

    private async Task OnDeleteCommentCommandExecuted(CommentVM? comment)
    {
        if (comment is null || _vm.SelectedProject is null) return;

        if (!await _messageService.ShowConfirmAsync(_vm, LocalizationService.GetString(LocalizationKeys.ConfirmDeleteComment)))
            return;

        await _commentRepo.DeleteAsync(comment.Id);
        await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
    }
}
