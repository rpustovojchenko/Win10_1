using Win10_1.Domain.Models;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Services.Interfaces;

public interface IAuthValidateService
{
    Task<bool> CheckSignInForm(object context, UserVM user, PasswordRequirementsVM password);
    Task<bool> CheckSignUpForm(object context, UserVM user, PasswordRequirementsVM password);
    Task<bool> CanSignIn(object context, UserVM user, PasswordRequirementsVM password);
    Task<bool> CanSignUp(object context, UserVM user);
}
