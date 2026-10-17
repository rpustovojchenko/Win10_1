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

public class ProjectCommandHandler
{
    private readonly TaskManagerVM                  _vm;
    private readonly IProjectRepository             _projectRepo;
    private readonly ITaskManagerValidateService    _validateService;
    private readonly IDialogVMService               _dialogService;
    private readonly IMessageService                _messageService;

    public ICommand AddProjectCommand       { get; private set; } = null!;
    public ICommand EditProjectCommand      { get; private set; } = null!;
    public ICommand DeleteProjectCommand    { get; private set; } = null!;

    public ProjectCommandHandler(
        TaskManagerVM               vm,
        IProjectRepository          projectRepo,
        ITaskManagerValidateService validateService,
        IDialogVMService            dialogService,
        IMessageService             messageService)
    {
        _vm                 = vm;
        _projectRepo        = projectRepo;
        _validateService    = validateService;
        _dialogService      = dialogService;
        _messageService     = messageService;
    }

    public void InitializeCommands()
    {
        AddProjectCommand       = new AsyncRelayCommand(OnAddProjectCommandExecuted, CanAddProjectCommandExecute);
        EditProjectCommand      = new AsyncRelayCommandWithParam<ProjectVM?>(OnEditProjectCommandExecuted       , CanEditProjectCommandExecute);
        DeleteProjectCommand    = new AsyncRelayCommandWithParam<ProjectVM?>(OnDeleteProjectCommandExecuted     , CanDeleteProjectCommandExecute);
    }

    private bool CanAddProjectCommandExecute() => true;
    private bool CanDeleteProjectCommandExecute(ProjectVM? project) => project is not null;
    private bool CanEditProjectCommandExecute(ProjectVM? project)   => project is not null;

    private async Task OnAddProjectCommandExecuted()
    {
        var proj = new Project
        {
            ProjectName     = _vm.Project.ProjectName,
            Description     = _vm.Project.Description,
            Theme           = _vm.Project.Theme,
            PercentComplete = _vm.Project.PercentComplete,
            Priority        = _vm.Project.Priority,
            Status          = _vm.Project.Status,
            DeadLine        = _vm.Project.DeadLine.HasValue
                                ? DateTime.SpecifyKind(_vm.Project.DeadLine.Value, DateTimeKind.Utc)
                                : null,
            CreatedAt       = DateTime.UtcNow,
            UpdatedAt       = DateTime.UtcNow,
            UserId          = _vm.User.Id,
        };

        if (!await _validateService.CheckNewProjectForm(_vm, proj)) return;

        await _projectRepo.AddAsync(proj);
        await _vm.LoadProjectsAsync();
        _vm.Project.Clear();

        _vm.SelectedProject = _vm.User.Projects?.FirstOrDefault();
    }

    private async Task OnEditProjectCommandExecuted(ProjectVM? project)
    {
        if (project is null) return;

        await _dialogService.OpenProjectDialogAsync(
            title       : LocalizationService.GetString(LocalizationKeys.EditProjectDialogTitle),
            project     : project,

            onConfirm: async vm =>
            {
                var existing = await _projectRepo.GetByIdAsync(project.Id);
                if (existing is null) return;

                existing.ProjectName        = vm.ProjectName;
                existing.Theme              = vm.Theme;
                existing.Description        = vm.Description;
                existing.DeadLine           = vm.DeadLine;
                existing.PercentComplete    = vm.PercentComplete;
                existing.Status             = vm.Status;
                existing.Priority           = vm.Priority;

                await _vm.UpdateProjectInfoAsync(existing);
            });
    }

    private async Task OnDeleteProjectCommandExecuted(ProjectVM? project)
    {
        if (project is null) return;

        if (!await _messageService.ShowConfirmAsync(_vm,
            string.Format(LocalizationService.GetString(LocalizationKeys.ConfirmDeleteProjectWithName), project.ProjectName)))
            return;

        if (_vm.SelectedProject is not null && _vm.SelectedProject.Id == project.Id)
        {
            _vm.TabControlIndex             = 0;
            _vm.IsProjectTabItemVisible     = false;
            _vm.SelectedProject             = null;
        }

        await _projectRepo.DeleteAsync(project.Id);
        await _vm.LoadProjectsAsync();
    }
}
