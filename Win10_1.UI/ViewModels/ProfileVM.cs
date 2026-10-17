using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.ViewModels;

public class ProfileVM : BaseVM
{
	private readonly IUserRepository			_userRepository;
	private readonly IMessageService			_messageService;
	private readonly IProfileValidateService	_profileValidateService;
	private readonly IPasswordHasher			_passwordHasher;

	public UserVM User { get; }

	public ICommand SaveProfileCommand { get; }

	private string _newUsername		= string.Empty;
	private string _newLogin		= string.Empty;
	private string _newPassword		= string.Empty;
	private string _confirmPassword = string.Empty;

	public string NewUsername
	{
		get => _newUsername;
		set => Set(ref _newUsername, value);
	}

	public string NewLogin
	{
		get => _newLogin;
		set => Set(ref _newLogin, value);
	}

	public string NewPassword
	{
		get => _newPassword;
		set => Set(ref _newPassword, value);
	}

	public string ConfirmPassword
	{
		get => _confirmPassword;
		set => Set(ref _confirmPassword, value);
	}

    public ProfileVM(
		UserVM					user, 
		IUserRepository			userRepository, 
		IMessageService			messageService,
		IProfileValidateService profileValidateService,
		IPasswordHasher			passwordHasher)
    {
        User					= user;
		_userRepository			= userRepository;
		_messageService			= messageService;
		_profileValidateService = profileValidateService;
		_passwordHasher			= passwordHasher;

		SaveProfileCommand = new AsyncRelayCommand(OnSaveProfileCommandExecuted, CanSaveProfileCommandExecute);
    }

	private bool CanSaveProfileCommandExecute() => true;

	private async Task OnSaveProfileCommandExecuted()
	{
		UserVM profile = new()
		{
			Username		= User.Username,
			Login			= User.Login,
			HashPassword	= User.HashPassword
		};

		if (!string.IsNullOrWhiteSpace(NewUsername))
		{
			if (!await _profileValidateService.CheckUsername(this, NewUsername, User.Username ?? string.Empty)) return;

			profile.Username = NewUsername;
        }

		if (!string.IsNullOrWhiteSpace(NewLogin))
		{
			if (!await _profileValidateService.CheckLogin(this, NewLogin, User.Login ?? string.Empty)) return;

			profile.Login = NewLogin;
        }

		if (!string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(ConfirmPassword))
		{
			if (!await _profileValidateService.CheckConfirmPassword(this, NewPassword, ConfirmPassword)) return;

			if (!await _profileValidateService.CheckPassword(this, NewPassword, User.HashPassword)) return;

            profile.HashPassword = _passwordHasher.HashPassword(NewPassword);
        }

		if (!CanSave())
		{
			await _messageService.ShowInfoAsync(this, 
				LocalizationService.GetString(LocalizationKeys.ProfileFillAtLeastOneField));

			return;
		}

        var passwordAccept = await _messageService.ConfirmPasswordDialogAsync(this,
            LocalizationService.GetString(LocalizationKeys.ProfileEnterPasswordToSave));

        if (string.IsNullOrEmpty(passwordAccept)) return;

        if (!_passwordHasher.VerifyPassword(passwordAccept, User.HashPassword))
        {
            await _messageService.ShowErrorAsync(this, 
				LocalizationService.GetString(LocalizationKeys.ProfileIncorrectPassword));

            return;
        }

		User.Username		= profile.Username;
		User.Login			= profile.Login;
		User.HashPassword	= profile.HashPassword;

        await _userRepository.UpdateAsync(User.User);

		ClearFields();
	}

	private void ClearFields()
	{
		NewUsername		= string.Empty;
		NewLogin		= string.Empty;
		NewPassword		= string.Empty;
		ConfirmPassword = string.Empty;
	}

	private bool CanSave() =>
		!string.IsNullOrWhiteSpace(NewUsername) ||
		!string.IsNullOrWhiteSpace(NewLogin)	||
		!string.IsNullOrWhiteSpace(NewPassword) || 
		!string.IsNullOrWhiteSpace(ConfirmPassword);
}
