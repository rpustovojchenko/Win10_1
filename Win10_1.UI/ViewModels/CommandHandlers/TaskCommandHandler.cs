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

public class TaskCommandHandler
{
    private readonly TaskManagerVM              _vm;
    private readonly IProjectTaskRepository     _taskRepo;
    private readonly IDialogVMService           _dialogService;
    private readonly IMessageService            _messageService;

    public ICommand AddTaskCommand      { get; private set; } = null!;
    public ICommand EditTaskCommand     { get; private set; } = null!;
    public ICommand DeleteTaskCommand   { get; private set; } = null!;

    public TaskCommandHandler(
        TaskManagerVM           vm,
        IProjectTaskRepository  taskRepo,
        IDialogVMService        dialogService,
        IMessageService         messageService)
    {
        _vm                 = vm;
        _taskRepo           = taskRepo;
        _dialogService      = dialogService;
        _messageService     = messageService;
    }

    public void InitializeCommands()
    {
        AddTaskCommand      = new AsyncRelayCommand(OnAddTaskCommandExecuted, CanAddTaskCommandExecute);
        EditTaskCommand     = new AsyncRelayCommandWithParam<TaskVM?>(OnEditTaskCommandExecuted     , CanEditTaskCommandExecute);
        DeleteTaskCommand   = new AsyncRelayCommandWithParam<TaskVM?>(OnDeleteTaskCommandExecuted   , CanDeleteTaskCommandExecute);
    }

    private bool CanAddTaskCommandExecute() => true;
    private bool CanEditTaskCommandExecute(TaskVM? task)    => task is not null;
    private bool CanDeleteTaskCommandExecute(TaskVM? task)  => task is not null;

    private async Task OnAddTaskCommandExecuted()
    {
        if (_vm.SelectedProject is null) return;

        await _dialogService.OpenTaskDialogAsync(
            title               : LocalizationService.GetString(LocalizationKeys.AddTaskDialogTitle),
            confirmBtnText      : LocalizationService.GetString(LocalizationKeys.AddTaskDialogButton),
            initialTitle        : string.Empty,
            initialDescription  : string.Empty,

            onConfirm: async vm =>
            {
                var newTask = new ProjectTask
                {
                    Title       = vm.TaskTitle,
                    Description = vm.TaskDescription,
                    CreatedAt   = DateTime.UtcNow,
                    UpdatedAt   = DateTime.UtcNow,
                    ProjectId   = _vm.SelectedProject.Id
                };

                await _taskRepo.AddAsync(newTask);
                await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
            });
    }

    private async Task OnEditTaskCommandExecuted(TaskVM? task)
    {
        if (_vm.SelectedProject is null || task is null) return;

        await _dialogService.OpenTaskDialogAsync(
            title               : LocalizationService.GetString(LocalizationKeys.EditTaskDialogTitle),
            confirmBtnText      : LocalizationService.GetString(LocalizationKeys.EditTaskDialogButton),
            initialTitle        : task.Title,
            initialDescription  : task.Description,

            onConfirm: async vm =>
            {
                var existing = await _taskRepo.GetByIdAsync(task.Id);
                if (existing is null) return;

                existing.Title = vm.TaskTitle;
                existing.Description = vm.TaskDescription;

                await _taskRepo.UpdateAsync(existing);
                await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
            });
    }

    private async Task OnDeleteTaskCommandExecuted(TaskVM? task)
    {
        if (task is null || _vm.SelectedProject is null) return;

        if (!await _messageService.ShowConfirmAsync(_vm,
            string.Format(LocalizationService.GetString(LocalizationKeys.ConfirmDeleteTaskWithName), task.Title)))
            return;

        await _taskRepo.DeleteAsync(task.Id);
        await _vm.UpdateProjectInfoAsync(_vm.SelectedProject.Project);
    }
}
