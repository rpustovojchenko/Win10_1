using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class MahAppsMessageService : IMessageService
{
    private readonly IDialogCoordinator _coordinator;

    public MahAppsMessageService(IDialogCoordinator coordinator)
    {
        _coordinator = coordinator;
    }

    public async Task ShowInfoAsync(object context, string message, string? title = null) =>
        await _coordinator.ShowMessageAsync(
            context, 
            title ?? LocalizationService.GetString(LocalizationKeys.DialogTitleInfo),
            message, 
            MessageDialogStyle.Affirmative);

    public async Task ShowErrorAsync(object context, string message, string? title = null) =>
        await _coordinator.ShowMessageAsync(
            context,
            title ?? LocalizationService.GetString(LocalizationKeys.DialogTitleError),
            message,
            MessageDialogStyle.Affirmative,
            new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Accented
            });
   
    public async Task ShowWarningAsync(object context, string message, string? title = null) =>
        await _coordinator.ShowMessageAsync(
            context,
            title ?? LocalizationService.GetString(LocalizationKeys.DialogTitleWarning),
            message,
            MessageDialogStyle.Affirmative, 
            new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Inverted
            });

    public async Task<bool> ShowConfirmAsync(object context, string message, string? title = null) =>
        await _coordinator.ShowMessageAsync(
            context,
            title ?? LocalizationService.GetString(LocalizationKeys.DialogTitleConfirm),
            message,
            MessageDialogStyle.AffirmativeAndNegative
            ) == MessageDialogResult.Affirmative;

    public async Task<string?> ConfirmPasswordDialogAsync(object context, string message, string? title = null)
    {
        var password = await _coordinator.ShowLoginAsync(
            context,
            title ?? LocalizationService.GetString(LocalizationKeys.DialogTitleConfirmChanges),
            message,
            new LoginDialogSettings
            {
                ShouldHideUsername = true,
                EnablePasswordPreview = false,
                AffirmativeButtonText = LocalizationService.GetString(LocalizationKeys.DialogButtonConfirm),
                NegativeButtonText = LocalizationService.GetString(LocalizationKeys.DialogButtonCancel),
                NegativeButtonVisibility = Visibility.Visible
            });

        return password is null ? null : password.Password;
    }
}
