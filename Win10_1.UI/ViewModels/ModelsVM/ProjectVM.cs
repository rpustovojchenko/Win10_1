using System.Collections.ObjectModel;
using Win10_1.Domain.Enums;
using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.ModelsVM;

public class ProjectVM : BaseVM
{
    public Project Project { get; }

    public ProjectVM()
    {
        Project = new Project();
    }

    public ProjectVM(Project proj)
    {
        Project = proj;
    }

    public int Id
    {
        get => Project.Id;
    }

    public string ProjectName
    {
        get => Project.ProjectName;
        set
        {
            if (Project.ProjectName == value) return;
            Project.ProjectName = value;
            OnPropertyChanged();
        }
    }

    public string? Description
    {
        get => Project.Description;
        set
        {
            if (Project.Description == value) return;
            Project.Description = value;
            OnPropertyChanged();
        }
    }

    public string? Theme
    {
        get => string.IsNullOrWhiteSpace(Project.Theme) ? null : Project.Theme;
        set
        {
            if (Project.Theme == value) return;
            Project.Theme = value;
            OnPropertyChanged();
        }
    }

    public int PercentComplete
    {
        get => Project.PercentComplete;
        set
        {
            if (Project.PercentComplete == value) return;
            Project.PercentComplete = value;
            OnPropertyChanged();
        }
    }

    public Priority Priority
    {
        get => Project.Priority;
        set
        {
            if (Project.Priority == value) return;
            Project.Priority = value;
            OnPropertyChanged();
        }
    }

    public Status Status
    {
        get => Project.Status;
        set
        {
            if (Project.Status == value) return;
            Project.Status = value;
            OnPropertyChanged();
        }
    }

    public DateTime? DeadLine
    {
        get => Project?.DeadLine;
        set
        {
            if (Project.DeadLine == value) return;
            Project.DeadLine = value;
            OnPropertyChanged();
        }
    }

    public DateTime CreatedAt
    {
        get => Project.CreatedAt;
        set
        {
            if (Project.CreatedAt == value) return;
            Project.CreatedAt = value;
            OnPropertyChanged();
        }
    }

    public DateTime UpdatedAt
    {
        get => Project.UpdatedAt;
        set
        {
            if (Project.UpdatedAt == value) return;
            Project.UpdatedAt = value;
            OnPropertyChanged();
        }
    }

    public int UserId
    {
        get => Project.UserId;
        set
        {
            if (Project.UserId == value) return;
            Project.UserId = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TaskVM> _tasks = new();
    public ObservableCollection<TaskVM> Tasks => _tasks;

    private ObservableCollection<CommentVM> _comments = new();
    public ObservableCollection<CommentVM> Comments => _comments;

    public void InitTasks(IEnumerable<ProjectTask> tasks)
    {
        Tasks.Clear();

        foreach (var t in tasks)
            Tasks.Add(new(t));
    }

    public void InitComments(IEnumerable<ProjectComment> comments)
    {
        Comments.Clear();

        foreach (var c in comments)
            Comments.Add(new(c));
    }

    public void Clear()
    {
        ProjectName     = string.Empty;
        Description     = string.Empty;
        Theme           = string.Empty;
        PercentComplete = 0;
        Priority        = Priority.Low;
        Status          = Status.NonStarted;
        DeadLine        = null;
    }
}