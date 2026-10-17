using System.Collections.ObjectModel;
using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.ModelsVM;

public class UserVM : BaseVM
{
    public User User { get; }

    public UserVM() 
    { 
        User = new User();
    }

    public UserVM(User user)
    {
        User = user;
    }

    public int Id
    {
        get => User.Id;
    }

    public string? Username
    {
        get => User.Username;
        set
        {
            if (User.Username == value) return;
            User.Username = value?.Trim() ?? "";
            OnPropertyChanged();
        }
    }

    public string? Login
    {
        get => User.Login;
        set
        {
            if (User.Login == value) return;
            User.Login = value?.Trim() ?? "";
            OnPropertyChanged();
        }
    }

    public string HashPassword
    {
        get => User.HashPassword;
        set
        {
            if (User.HashPassword == value) return;
            User.HashPassword = value?.Trim() ?? "";
            OnPropertyChanged();
        }
    }

    private ObservableCollection<ProjectVM> _projects = new();
    public ObservableCollection<ProjectVM> Projects => _projects;

    public void InitProjectsVM(IEnumerable<Project> projects)
    {
        _projects.Clear();

        foreach (var p in projects)
            _projects.Add(new(p));
    }
}
