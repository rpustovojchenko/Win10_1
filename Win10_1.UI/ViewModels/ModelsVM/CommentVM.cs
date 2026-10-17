using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.ModelsVM;

public class CommentVM : BaseVM
{
    public ProjectComment ProjectComment { get; }

    public CommentVM()
    {
        ProjectComment = new();
    }

    public CommentVM(ProjectComment comment)
    {
        ProjectComment = comment;
    }

    public int Id
    {
        get => ProjectComment.Id;
    }

    public string Author
    {
        get => ProjectComment.Author;
        set
        {
            if (ProjectComment.Author == value) return;
            ProjectComment.Author = value;
            OnPropertyChanged();
        }
    }

    public string Comment
    {
        get => ProjectComment.Comment;
        set
        {
            if (ProjectComment.Comment == value) return;
            ProjectComment.Comment = value;
            OnPropertyChanged();
        }
    }

    public DateTime CreatedAt
    {
        get => ProjectComment.CreatedAt;
        set
        {
            if (ProjectComment.CreatedAt == value) return;
            ProjectComment.CreatedAt = value;
            OnPropertyChanged();
        }
    }

    public DateTime UpdatedAt
    {
        get => ProjectComment.UpdatedAt;
        set
        {
            if (ProjectComment.UpdatedAt == value) return;
            ProjectComment.UpdatedAt = value;
            OnPropertyChanged();
        }
    }
}
