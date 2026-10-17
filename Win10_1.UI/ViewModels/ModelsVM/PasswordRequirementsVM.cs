using Win10_1.Business.Validation;
using Win10_1.Domain.Enums;
using Win10_1.UI.ViewModels.Base;

namespace Win10_1.UI.ViewModels.ModelsVM;

public class PasswordRequirementsVM : BaseVM
{
    private string? _password;
    public string? Password
    {
        get => _password;
        set
        {
            Set(ref _password, value?.Trim() ?? "");
            UpdatePasswordRequirements();
            UpdateConfirmation(_password, _confirmPassword);
        }
    }

    private string? _confirmPassword;
    public string? ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            Set(ref _confirmPassword, value?.Trim() ?? "");
            UpdateConfirmation(_password, _confirmPassword);
        }
    }

    private PasswordRequirementState _latinState = PasswordRequirementState.NotChecked;
    public PasswordRequirementState LatinState
    {
        get => _latinState;
        set => Set(ref _latinState, value);
    }

    private PasswordRequirementState _lengthState = PasswordRequirementState.NotChecked;
    public PasswordRequirementState LengthState
    {
        get => _lengthState;
        set => Set(ref _lengthState, value);
    }

    private PasswordRequirementState _specialState = PasswordRequirementState.NotChecked;
    public PasswordRequirementState SpecialState
    {
        get => _specialState;
        set => Set(ref _specialState, value);
    }

    private PasswordRequirementState _confirmState;
    public PasswordRequirementState ConfirmState
    {
        get => _confirmState;
        set => Set(ref _confirmState, value);
    }

    private void UpdatePasswordRequirements()
    {
        var pwd = Password ?? string.Empty;

        if (pwd.Length == 0)
        {
            (LatinState, LengthState, SpecialState) =
                (PasswordRequirementState.NotChecked, PasswordRequirementState.NotChecked, PasswordRequirementState.NotChecked);
            return;
        }

        var r = PasswordValidator.IsPasswordValid(Password);

        LatinState      = r.IsLatinOnly         ? PasswordRequirementState.Passed : PasswordRequirementState.Failed;
        LengthState     = r.IsMinLength         ? PasswordRequirementState.Passed : PasswordRequirementState.Failed;
        SpecialState    = r.HasDigitOrSpecial   ? PasswordRequirementState.Passed : PasswordRequirementState.Failed;
    }

    private void UpdateConfirmation(string? password, string? confirmPassword)
    {
        if (string.IsNullOrEmpty(password) && string.IsNullOrEmpty(confirmPassword))
        {
            ConfirmState = PasswordRequirementState.NotChecked;
            return;
        }

        ConfirmState = string.Equals(password, confirmPassword)
            ? PasswordRequirementState.Passed
            : PasswordRequirementState.Failed;
    }
}
