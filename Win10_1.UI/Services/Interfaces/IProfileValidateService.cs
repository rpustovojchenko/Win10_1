namespace Win10_1.UI.Services.Interfaces;

public interface IProfileValidateService
{
    Task<bool> CheckUsername(object context, string? username, string oldUsername);
    Task<bool> CheckLogin(object context, string? login, string oldLogin);
    Task<bool> CheckPassword(object context, string? password, string oldPassword);
    Task<bool> CheckConfirmPassword(object context, string? password, string? confirmPassword);
}
