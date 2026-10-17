namespace Win10_1.UI.Services.Interfaces;

public interface IMessageService
{
    Task ShowInfoAsync(object context, string message, string? title = null);
    Task ShowErrorAsync(object context, string message, string? title = null);
    Task ShowWarningAsync(object context, string message, string? title = null);
    Task<bool> ShowConfirmAsync(object context, string message, string? title = null);
    Task<string?> ConfirmPasswordDialogAsync(object context, string message, string? title = null);
}
