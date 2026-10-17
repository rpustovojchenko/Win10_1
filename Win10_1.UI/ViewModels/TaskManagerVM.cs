using System.Collections.ObjectModel;
using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Domain.Enums;
using Win10_1.Domain.Models;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;
using Win10_1.UI.ViewModels.CommandHandlers;
using Win10_1.UI.ViewModels.DialogsVM.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;
using Win10_1.UI.ViewModels.Session;

namespace Win10_1.UI.ViewModels;

public class TaskManagerVM : BaseVM
{
    public UserVM                   User        { get; }
    public PasswordRequirementsVM   Password    { get; }
    public ProjectVM                Project     { get; private set; }
    public ProjectFilterVM          Filters     { get; }
    public ProfileVM                Profile     { get; }
    public SettingsVM               Settings    { get; }

    private ProjectVM?                      _selectedProject;
    private ObservableCollection<ProjectVM> _projects = new();

    private int         _tabControlIndex            = 0;
    private bool        _isProjectTabItemVisible    = false;
    private bool        _isTasksVisible             = false;
    private bool        _isCommentsVisible          = false;

    public ProjectVM? SelectedProject
    {
        get => _selectedProject;
        set
        {
            Set(ref _selectedProject, value);
            OnSelectedProjectChange();
        }
    }

    public ObservableCollection<ProjectVM> Projects
    {
        get => _projects;
        set => Set(ref _projects, value);
    }

    public int TabControlIndex
    {
        get => _tabControlIndex;
        set => Set(ref _tabControlIndex, value);
    }

    public bool IsProjectTabItemVisible
    {
        get => _isProjectTabItemVisible;
        set => Set(ref _isProjectTabItemVisible, value);
    }

    public bool IsTasksVisible
    {
        get => _isTasksVisible;
        set => Set(ref _isTasksVisible, value);
    }

    public bool IsCommentsVisible
    {
        get => _isCommentsVisible;
        set => Set(ref _isCommentsVisible, value);
    }

    private readonly AppSession                     _session;
    private readonly IProjectRepository             _projectRepo;
    private readonly IProjectTaskRepository         _taskRepo;
    private readonly IProjectCommentRepository      _commentRepo;
    private readonly IFilterService                 _filterService;
    private readonly ProjectCommandHandler          _projectHandler;
    private readonly TaskCommandHandler             _taskHandler;   
    private readonly CommentCommandHandler          _commentHandler;  

    public ICommand AddProjectCommand       => _projectHandler.AddProjectCommand;
    public ICommand EditProjectCommand      => _projectHandler.EditProjectCommand;
    public ICommand DeleteProjectCommand    => _projectHandler.DeleteProjectCommand;
    public ICommand AddTaskCommand          => _taskHandler.AddTaskCommand;
    public ICommand EditTaskCommand         => _taskHandler.EditTaskCommand;
    public ICommand DeleteTaskCommand       => _taskHandler.DeleteTaskCommand;
    public ICommand AddCommentCommand       => _commentHandler.AddCommentCommand;
    public ICommand EditCommentCommand      => _commentHandler.EditCommentCommand;
    public ICommand DeleteCommentCommand    => _commentHandler.DeleteCommentCommand;
    public ICommand ResetCommand { get; }

    public TaskManagerVM(
        AppSession                  session,
        IProjectRepository          projectRepo,
        IProjectTaskRepository      taskRepo,
        IProjectCommentRepository   commentRepo,
        IUserRepository             userRepo,
        IMessageService             ms,
        ITaskManagerValidateService tmvs,
        IProfileValidateService     pvs,
        IDialogVMService            dialogService,
        IFilterService              filterService,
        IPasswordHasher             passwordHasher)
    {
        _session                        = session;
        _projectRepo                    = projectRepo;
        _taskRepo                       = taskRepo;
        _commentRepo                    = commentRepo;
        _filterService                  = filterService;

        User        = _session.User;
        Password    = _session.Password;
        Project     = new();
        Filters     = new();
        Profile     = new(User, userRepo, ms, pvs, passwordHasher);
        Settings    = new();

        _projectHandler = new(this, projectRepo, tmvs, dialogService, ms);  
        _taskHandler    = new(this, taskRepo, dialogService, ms);          
        _commentHandler = new(this, commentRepo, dialogService, ms);       

        Projects = User.Projects;

        Filters.PropertyChanged += (_, _) => ApplyFilters();

        _projectHandler .InitializeCommands();
        _taskHandler    .InitializeCommands();
        _commentHandler .InitializeCommands();

        ResetCommand = new AsyncRelayCommand(OnResetCommandExecuted, CanResetCommandExecute);

        _ = LoadProjectsAsync();
    }

    private bool CanResetCommandExecute() =>    
        !string.IsNullOrWhiteSpace(Filters.ProjectNameFilter)   ||
        !string.IsNullOrWhiteSpace(Filters.ThemeFilter)         ||
        Filters.StatusFilter    != Status.None                  ||
        Filters.PriorityFilter  != Priority.None;

    private async Task OnResetCommandExecuted()
    {
        Filters.ProjectNameFilter   = string.Empty;
        Filters.ThemeFilter         = string.Empty;
        Filters.StatusFilter        = Status.None;
        Filters.PriorityFilter      = Priority.None;
    }

    public async Task LoadProjectsAsync()
    {
        var projects = (await _projectRepo.GetByUserIdAsync(User.Id))
                                          .OrderByDescending(p => p.UpdatedAt);
        User.InitProjectsVM(projects);
        ApplyFilters();
    }

    private async Task LoadTasksAndCommentsAsync()
    {
        await LoadTasksAsync();
        await LoadCommentsAsync();
    }

    private async Task LoadTasksAsync()
    {
        if (SelectedProject is null) return;

        var tasks = (await _taskRepo.GetByProjectIdAsync(SelectedProject.Id))
                                    .OrderByDescending(t => t.UpdatedAt);
        SelectedProject.InitTasks(tasks);

        IsTasksVisible = CanShowTasks();
    }

    private async Task LoadCommentsAsync()
    {
        if (SelectedProject is null) return;

        var comments = (await _commentRepo.GetByProjectIdAsync(SelectedProject.Id))
                                          .OrderByDescending(c => c.UpdatedAt);
        SelectedProject.InitComments(comments);

        IsCommentsVisible = CanShowComments();
    }

    public async Task UpdateProjectInfoAsync(Project proj)
    {
        proj.DeadLine = proj.DeadLine.HasValue 
            ? DateTime.SpecifyKind(proj.DeadLine.Value, DateTimeKind.Utc) 
            : null;

        var updatedProject = await _projectRepo.UpdateAsync(proj);
        await LoadProjectsAsync();
        SelectedProject = User.Projects?.FirstOrDefault(p => p.Id == updatedProject.Id);
    }

    private async void OnSelectedProjectChange()
    {
        if (SelectedProject is null) return;

        IsProjectTabItemVisible = true;
        TabControlIndex = 1;
        await LoadTasksAndCommentsAsync();

        IsTasksVisible = CanShowTasks();
        IsCommentsVisible = CanShowComments();
    }

    private void ApplyFilters()
    {
        Projects = _filterService.Filter(
            User.Projects, 
            Filters.ProjectNameFilter, 
            Filters.ThemeFilter, 
            Filters.StatusFilter, 
            Filters.PriorityFilter);

        (ResetCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
    }

    private bool CanShowTasks() =>
        SelectedProject?.Tasks.Any() ?? false;

    private bool CanShowComments() =>
        SelectedProject?.Comments.Any() ?? false;
}