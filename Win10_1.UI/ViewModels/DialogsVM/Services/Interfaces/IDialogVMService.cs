using Win10_1.Domain.Enums;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.ViewModels.DialogsVM.Services.Interfaces;

public interface IDialogVMService
{
    Task OpenTaskDialogAsync(
        string title,
        string confirmBtnText,
        string initialTitle,
        string initialDescription,
        Func<TaskInputDialogVM, Task> onConfirm);

    Task OpenCommentDialogAsync(
        string title,
        string confirmBtnText,
        string author,
        string initialComment,
        Func<CommentInputDialogVM, Task> onConfirm);

    Task OpenProjectDialogAsync(
        string title,
        ProjectVM project,
        Func<ProjectEditDialogVM, Task> onConfirm);
}