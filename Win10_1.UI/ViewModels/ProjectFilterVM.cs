using Win10_1.Domain.Enums;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels;

public class ProjectFilterVM : BaseVM
{
    private string      _projectNameFilter      = string.Empty;
    private string      _themeFilter            = string.Empty;
    private Status      _statusFilter           = Status.None;
    private Priority    _priorityFilter         = Priority.None;

    public string ProjectNameFilter
    {
        get => _projectNameFilter;
        set => Set(ref _projectNameFilter, value);
    }

    public string ThemeFilter
    {
        get => _themeFilter;
        set => Set(ref _themeFilter, value);
    }

    public Status StatusFilter
    {
        get => _statusFilter;
        set => Set(ref _statusFilter, value);
    }

    public Priority PriorityFilter
    {
        get => _priorityFilter;
        set => Set(ref _priorityFilter, value);
    }
}
