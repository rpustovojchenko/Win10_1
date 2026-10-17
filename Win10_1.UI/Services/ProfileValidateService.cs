using Win10_1.Business.Services.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class ProfileValidateService : IProfileValidateService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMessageService _ms;
    private readonly ProfileValidator _pv;

    public ProfileValidateService(
        IPasswordHasher passwordHasher, 
        IMessageService ms, 
        ProfileValidator pv)
    {
        _passwordHasher = passwordHasher;
        _ms             = ms;
        _pv             = pv;
    }

    public async Task<bool> CheckUsername(object context, string? username, string oldUsername)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        if (username == oldUsername)
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfileUsernameUnchanged),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidUsernameTitle));

            return false;
        }

        if (!await _pv.IsUsernameValid(username))
        {
            await _ms.ShowInfoAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidUsername),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidUsernameTitle));

            return false;
        }

        if (!await _pv.IsUsernameLengthValid(username))
        {
            await _ms.ShowErrorAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProfileUsernameMaxLength), UserConstraints.UsernameMaxLength),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidUsernameTitle));

            return false;
        }

        return true;
    }

    public async Task<bool> CheckLogin(object context, string? login, string oldLogin)
    {
        if (string.IsNullOrEmpty(login))
            return false;

        if (login == oldLogin)
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfileLoginUnchanged),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidLoginTitle));

            return false;
        }

        if (!await _pv.IsLoginValid(login))
        {
            await _ms.ShowInfoAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfileLoginLatinLettersAndDigits),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidLoginTitle));

            return false;
        }

        if (!await _pv.IsLoginLengthValid(login))
        {
            await _ms.ShowInfoAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProfileLoginMaxLength), UserConstraints.LoginMaxLength),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidLoginTitle));

            return false;
        }

        if (await _pv.IsLoginExists(login))
        {
            await _ms.ShowInfoAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfileLoginAlreadyExists),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidLoginTitle));

            return false;
        }

        return true;
    }

    public async Task<bool> CheckPassword(object context, string? password, string oldPassword)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (_passwordHasher.VerifyPassword(password, oldPassword ?? string.Empty))
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.ProfilePasswordUnchanged),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidPasswordTitle));

            return false;
        }

        if (!await _pv.IsPasswordValid(password))
        {
            await _ms.ShowInfoAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProfilePasswordRequirements), UserConstraints.PasswordMinLength),
                LocalizationService.GetString(LocalizationKeys.ProfileInvalidPasswordTitle));

            return false;
        }

        return true;
    }

    public async Task<bool> CheckConfirmPassword(object context, string? password, string? confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(confirmPassword) && !string.IsNullOrWhiteSpace(password))
        {
            await _ms.ShowInfoAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.ProfileConfirmPassword));

            return false;
        }

        if (!string.IsNullOrWhiteSpace(confirmPassword) && string.IsNullOrWhiteSpace(password))
        {
            await _ms.ShowInfoAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.ProfileEnterNewPassword));

            return false;
        }

        if (!await _pv.IsConfirmPasswordValid(password, confirmPassword))
        {
            await _ms.ShowErrorAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.ProfilePasswordsDoNotMatch));

            return false;
        }

        return true;
    }
}