namespace Win10_1.UI.Services.Interfaces;

public interface ITaskValidateService
{
    Task<string?> CheckTitle(string title);
    Task<string?> CheckDescription(string description);
}
