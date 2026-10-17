using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.DialogsVM;

public class TaskInputDialogVM : BaseVM
{
    private readonly ITaskValidateService _taskValidator;

    private string _taskTitle       = string.Empty;
    private string _taskDescription = string.Empty;
    private string _confirmBtnText  = string.Empty;

    private string? _titleError;
    private string? _descriptionError;

    public string TaskTitle
    {
        get => _taskTitle;
        set
        {
            Set(ref _taskTitle, value);
            OnTitleChange();
        }
    }

    public string TaskDescription
    {
        get => _taskDescription;
        set
        {
            Set(ref _taskDescription, value);
            OnDescriptionChange();
        }
    }

    public string ConfirmBtnText
    {
        get => _confirmBtnText;
        set => Set(ref _confirmBtnText, value);
    }

    public string? TitleError
    {
        get => _titleError;
        private set => Set(ref _titleError, value);
    }

    public string? DescriptionError
    {
        get => _descriptionError;
        private set => Set(ref _descriptionError, value);
    }

    public bool IsConfirmed { get; private set; }

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    public event EventHandler? CloseRequested;

    public TaskInputDialogVM(ITaskValidateService taskValidator)
    {
        _taskValidator = taskValidator;

        ConfirmCommand  = new AsyncRelayCommand(OnConfirmCommandExecuted,   CanConfirmCommandExecute);
        CancelCommand   = new AsyncRelayCommand(OnCancelCommandExecuted,    CanCancelCommandExecute);
    }

    private bool CanConfirmCommandExecute() => true;
    private bool CanCancelCommandExecute() => true;

    private async Task OnConfirmCommandExecuted()
    {
        TitleError          = await _taskValidator.CheckTitle(TaskTitle);
        DescriptionError    = await _taskValidator.CheckDescription(TaskDescription);

        if (!string.IsNullOrEmpty(TitleError) || !string.IsNullOrEmpty(DescriptionError)) return;

        IsConfirmed = true;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async Task OnCancelCommandExecuted()
    {
        IsConfirmed = false;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private async void OnTitleChange() =>
        TitleError = await _taskValidator.CheckTitle(TaskTitle);

    private async void OnDescriptionChange() =>
        DescriptionError = await _taskValidator.CheckDescription(TaskDescription);
}
