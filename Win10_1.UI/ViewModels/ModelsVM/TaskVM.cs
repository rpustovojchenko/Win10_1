using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.ModelsVM;

public class TaskVM : BaseVM
{
    public ProjectTask Task { get; }

    public TaskVM()
    {
        Task = new();
    }

    public TaskVM(ProjectTask task)
    {
        Task = task;
    }

    public int Id
    {
        get => Task.Id;
    }

    public string Title
    {
        get => Task.Title;
        set
        {
            if (Task.Title == value) return;
            Task.Title = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => Task.Description;
        set
        {
            if (Task.Description == value) return;
            Task.Description = value;
            OnPropertyChanged();
        }
    }

    public DateTime CreatedAt
    {
        get => Task.CreatedAt;
        set
        {
            if (Task.CreatedAt == value) return;
            Task.CreatedAt = value;
            OnPropertyChanged();
        }
    }

    public DateTime UpdatedAt
    {
        get => Task.UpdatedAt;
        set
        {
            if (Task.UpdatedAt == value) return;
            Task.UpdatedAt = value;
            OnPropertyChanged();
        }
    }
}
