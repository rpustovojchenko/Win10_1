namespace Win10_1.UI.Services.Interfaces;

public interface IProjectValidateService
{
    Task<string?> CheckProjectName(string name);
    Task<string?> CheckTheme(string? theme);
    Task<string?> CheckDescription(string? description);
}
