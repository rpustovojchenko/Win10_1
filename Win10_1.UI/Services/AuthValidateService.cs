using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Services;

public class AuthValidateService : IAuthValidateService
{
    private readonly IMessageService _ms;
    private readonly SignInValidator _siv;
    private readonly SignUpValidator _suv;

    public AuthValidateService(
        IMessageService ms, 
        SignInValidator siv,
        SignUpValidator suv)
    {
        _ms = ms;
        _siv = siv;
        _suv = suv;
    }

    public async Task<bool> CheckSignInForm(object context, UserVM user, PasswordRequirementsVM password) =>
        await CheckLogin(context, user) &&
        await CheckPassword(context, password);

    public async Task<bool> CheckSignUpForm(object context, UserVM user, PasswordRequirementsVM password) =>
        await CheckUser(context, user)          &&
        await CheckLogin(context, user)         &&
        await CheckPassword(context, password)  &&
        await CheckConfirmPassword(context, password);

    public async Task<bool> CanSignIn(object context, UserVM user, PasswordRequirementsVM password)
    {
        if (!await _siv.CanSignInAsync(user.User, password.Password ?? ""))
        {
            await _ms.ShowErrorAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.AuthInvalidCredentials));

            return false;
        }

        return true;
    }

    public async Task<bool> CanSignUp(object context, UserVM user)
    {
        if (!await _suv.CanSignUpAsync(user.User))
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.AuthLoginAlreadyExists));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckUser(object context, UserVM user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
        {
            await _ms.ShowWarningAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.AuthEnterUsername));

            return false;
        }

        if (!UserValidator.IsUsernameValid(user.Username))
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.AuthUsernameLettersOnly),
                LocalizationService.GetString(LocalizationKeys.AuthInvalidUsernameTitle));

            return false;
        }

        if (!UserValidator.IsUsernameLengthValid(user.Username))
        {
            await _ms.ShowErrorAsync(
                context, 
                string.Format(LocalizationService.GetString(LocalizationKeys.AuthUsernameMaxLength), UserConstraints.UsernameMaxLength),
                LocalizationService.GetString(LocalizationKeys.AuthInvalidUsernameTitle));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckLogin(object context, UserVM user)
    {
        if (string.IsNullOrWhiteSpace(user.Login))
        {
            await _ms.ShowWarningAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.AuthEnterLogin));

            return false;
        }

        if (!UserValidator.IsLoginValid(user.Login))
        {
            await _ms.ShowErrorAsync(
                context,
                LocalizationService.GetString(LocalizationKeys.AuthLoginLatinLettersAndDigits),
                LocalizationService.GetString(LocalizationKeys.AuthInvalidLoginTitle));

            return false;
        }

        if (!UserValidator.IsLoginLengthValid(user.Login))
        {
            await _ms.ShowErrorAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.AuthLoginMaxLength), UserConstraints.LoginMaxLength),
                LocalizationService.GetString(LocalizationKeys.AuthInvalidLoginTitle));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckPassword(object context, PasswordRequirementsVM password)
    {
        if (string.IsNullOrWhiteSpace(password.Password))
        {
            await _ms.ShowWarningAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.AuthEnterPassword));

            return false;
        }

        if (PasswordValidator.IsPasswordValid(password.Password) != (true, true, true))
        {
            await _ms.ShowErrorAsync(
                context, 
                string.Format(LocalizationService.GetString(LocalizationKeys.AuthPasswordRequirements), UserConstraints.PasswordMinLength), 
                LocalizationService.GetString(LocalizationKeys.AuthInvalidPasswordTitle));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckConfirmPassword(object context, PasswordRequirementsVM password)
    {
        if (string.IsNullOrWhiteSpace(password.ConfirmPassword))
        {
            await _ms.ShowWarningAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.AuthRepeatPassword));

            return false;
        }

        if (!PasswordValidator.IsConfirmPasswordValid(password.Password, password.ConfirmPassword))
        {
            await _ms.ShowErrorAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.AuthPasswordsDoNotMatch));

            return false;
        }

        return true;
    }
}
