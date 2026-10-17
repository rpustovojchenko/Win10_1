namespace Win10_1.UI.Services.Interfaces;

public interface ICommentValidateService
{
    Task<string?> CheckComment(string comment);
}
