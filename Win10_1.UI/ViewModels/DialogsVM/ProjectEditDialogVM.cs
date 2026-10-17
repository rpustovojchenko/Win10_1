using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.Domain.Enums;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.DialogsVM;

public class ProjectEditDialogVM : BaseVM
{
    private readonly IProjectValidateService _projectValidator;

    private string      _projectName    = string.Empty;
    private string?     _theme;
    private string?     _description;
    private DateTime?   _deadLine;
    private int         _percentComplete;
    private Status      _status;
    private Priority    _priority;

    private string? _projectNameError;
    private string? _themeError;
    private string? _descriptionError;

    public string ProjectName
    {
        get => _projectName;
        set
        {
            Set(ref _projectName, value);
            OnProjectNameChange();
        }
    }

    public string? Theme
    {
        get => _theme;
        set
        {
            Set(ref _theme, value);
            OnThemeChange();
        }
    }

    public string? Description
    {
        get => _description;
        set
        {
            Set(ref _description, value);
            OnDescriptionChange();
        }
    }

    public DateTime? DeadLine
    {
        get => _deadLine;
        set => Set(ref _deadLine, value);
    }

    public int PercentComplete
    {
        get => _percentComplete;
        set => Set(ref _percentComplete, value);
    }

    public Status Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    public Priority Priority
    {
        get => _priority; 
        set => Set(ref _priority, value);
    }

    public string? ProjectNameError
    {
        get => _projectNameError;
        private set => Set(ref _projectNameError, value);
    }

    public string? ThemeError
    {
        get => _themeError;
        private set => Set(ref _themeError, value);
    }

    public string? DescriptionError
    {
        get => _descriptionError;
        private set => Set(ref _descriptionError, value);
    }

    public bool IsConfirmed { get; private set; }

    public ICommand ConfirmCommand  { get; }
    public ICommand CancelCommand   { get; }

    public event EventHandler? CloseRequested;

    public ProjectEditDialogVM(IProjectValidateService projectValidator)
    {
        _projectValidator = projectValidator;

        ConfirmCommand  = new AsyncRelayCommand(OnConfirmCommandExecuted, CanConfirmCommandExecute);
        CancelCommand   = new AsyncRelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);
    }

    private bool CanConfirmCommandExecute() => true;
    private bool CanCancelCommandExecute() => true;

    private async Task OnConfirmCommandExecuted()
    {
        ProjectNameError    = await _projectValidator.CheckProjectName(ProjectName);
        ThemeError          = await _projectValidator.CheckTheme(Theme);
        DescriptionError    = await _projectValidator.CheckDescription(Description);

        if (!string.IsNullOrEmpty(ProjectNameError) || !string.IsNullOrEmpty(ThemeError) || !string.IsNullOrEmpty(DescriptionError))
            return;

        IsConfirmed = true;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async Task OnCancelCommandExecuted()
    {
        IsConfirmed = false;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async void OnProjectNameChange() =>
        ProjectNameError = await _projectValidator.CheckProjectName(ProjectName);

    private async void OnThemeChange() =>
        ThemeError = await _projectValidator.CheckTheme(Theme);

    private async void OnDescriptionChange() =>
        DescriptionError = await _projectValidator.CheckDescription(Description);
}