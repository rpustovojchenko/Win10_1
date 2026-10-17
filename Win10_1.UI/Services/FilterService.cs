using System.Collections.ObjectModel;
using Win10_1.Domain.Enums;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Services;

public class FilterService : IFilterService
{
    public ObservableCollection<ProjectVM> Filter(
        ObservableCollection<ProjectVM> projects, 
        string projectNameFilter, 
        string themeFilter, 
        Status statusFilter, 
        Priority priorityFilter)
    {
        projects = FilterByName(projects, projectNameFilter);
        projects = FilterByTheme(projects, themeFilter);
        projects = FilterByStatus(projects, statusFilter);
        projects = FilterByPriority(projects, priorityFilter);

        return projects;
    }

    private ObservableCollection<ProjectVM> FilterByName(ObservableCollection<ProjectVM> projects, string projectName)
    {
        if (string.IsNullOrWhiteSpace(projectName)) return projects;

        return new(projects.Where(p => p.ProjectName.Contains(projectName, StringComparison.OrdinalIgnoreCase)));
    }

    private ObservableCollection<ProjectVM> FilterByTheme(ObservableCollection<ProjectVM> projects, string theme)
    {
        if (string.IsNullOrWhiteSpace(theme)) return projects;

        return new(projects.Where(p => p.Theme is not null && p.Theme.Contains(theme, StringComparison.OrdinalIgnoreCase)));
    }

    private ObservableCollection<ProjectVM> FilterByStatus(ObservableCollection<ProjectVM> projects, Status status)
    {
        if (status == Status.None) return projects;

        return new(projects.Where(p => p.Status == status));
    }

    private ObservableCollection<ProjectVM> FilterByPriority(ObservableCollection<ProjectVM> projects, Priority priority)
    {
        if (priority == Priority.None) return projects;

        return new(projects.Where(p => p.Priority == priority));
    }
}
