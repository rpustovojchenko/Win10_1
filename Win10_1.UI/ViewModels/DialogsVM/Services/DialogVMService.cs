using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.DialogsVM.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;
using Win10_1.UI.Views.Users;

namespace Win10_1.UI.ViewModels.DialogsVM.Services;

public class DialogVMService : IDialogVMService
{
    private readonly IProjectValidateService    _pv;
    private readonly ITaskValidateService       _tv;
    private readonly ICommentValidateService    _cv;

    public DialogVMService(
        IProjectValidateService     pv, 
        ITaskValidateService        tv, 
        ICommentValidateService     cv)
    {
        _pv = pv;
        _tv = tv;
        _cv = cv;
    }

    public async Task OpenTaskDialogAsync(
        string title,
        string confirmBtnText,
        string initialTitle,
        string initialDescription,
        Func<TaskInputDialogVM, Task> onConfirm)
    {
        var mainWindow = Application.Current.MainWindow as MetroWindow;
        if (mainWindow is null) return;

        var dialogVM = new TaskInputDialogVM(_tv)
        {
            ConfirmBtnText  = confirmBtnText,
            TaskTitle       = initialTitle,
            TaskDescription = initialDescription
        };

        var dialogView = new TaskInputDialog
        {
            DataContext = dialogVM
        };

        var dialog = new CustomDialog
        {
            Title = title,
            Content = dialogView
        };

        dialogVM.CloseRequested += async (s, e) =>
        {
            await mainWindow.HideMetroDialogAsync(dialog);

            if (!dialogVM.IsConfirmed) return;

            await onConfirm(dialogVM);
        };

        await mainWindow.ShowMetroDialogAsync(dialog);
    }

    public async Task OpenCommentDialogAsync(
        string title,
        string confirmBtnText,
        string author,
        string initialComment,
        Func<CommentInputDialogVM, Task> onConfirm)
    {
        var mainWindow = Application.Current.MainWindow as MetroWindow;
        if (mainWindow is null) return;

        var dialogVM = new CommentInputDialogVM(_cv)
        {
            ConfirmBtnText  = confirmBtnText,
            Comment         = initialComment,
            Author          = author,
        };

        var dialogView = new CommentInputDialog
        {
            DataContext = dialogVM
        };

        var dialog = new CustomDialog
        {
            Title   = title,
            Content = dialogView
        };

        dialogVM.CloseRequested += async (s, e) =>
        {
            await mainWindow.HideMetroDialogAsync(dialog);

            if (!dialogVM.IsConfirmed) return;

            await onConfirm(dialogVM);
        };

        await mainWindow.ShowMetroDialogAsync(dialog);
    }

    public async Task OpenProjectDialogAsync(
        string      title,
        ProjectVM project,
        Func<ProjectEditDialogVM, Task> onConfirm)
    {
        var mainWindow = Application.Current.MainWindow as MetroWindow;
        if (mainWindow is null) return;

        var dialogVM = new ProjectEditDialogVM(_pv)
        {
            ProjectName     = project.ProjectName,
            Theme           = project.Theme,
            Description     = project.Description,
            DeadLine        = project.DeadLine,
            PercentComplete = project.PercentComplete,
            Status          = project.Status,
            Priority        = project.Priority
        };

        var dialogView = new ProjectEditDialog
        {
            DataContext = dialogVM
        };

        var dialog = new CustomDialog
        {
            Title = title,
            Content = dialogView
        };

        dialogVM.CloseRequested += async (s, e) =>
        {
            await mainWindow.HideMetroDialogAsync(dialog);

            if (!dialogVM.IsConfirmed) return;

            await onConfirm(dialogVM);
        };

        await mainWindow.ShowMetroDialogAsync(dialog);
    }
}
