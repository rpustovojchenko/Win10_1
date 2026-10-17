using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Validation;

public class ProfileValidator
{
    private readonly IUserRepository _userRepo;

    public ProfileValidator(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> IsUsernameValid(string username) => 
        UserValidator.IsUsernameValid(username);

    public async Task<bool> IsUsernameLengthValid(string username) =>
        UserValidator.IsUsernameLengthValid(username);

    public async Task<bool> IsLoginValid(string login) => 
        UserValidator.IsLoginValid(login);

    public async Task<bool> IsLoginLengthValid(string login) =>
        UserValidator.IsLoginLengthValid(login);

    public async Task<bool> IsLoginExists(string login) => 
        (await _userRepo.GetByLoginAsync(login)) is not null;

    public async Task<bool> IsPasswordValid(string password)
    {
        var pwd = password ?? string.Empty;

        bool hasLetters = pwd.Any(char.IsLetter);

        bool allLettersLatin = pwd.Where(char.IsLetter)
            .All(c =>
                ('a' <= c && c <= 'z') ||
                ('A' <= c && c <= 'Z'));

        bool isLatinOnly = hasLetters && allLettersLatin;

        bool isMinLength = pwd.Length >= UserConstraints.PasswordMinLength;

        bool hasDigitOrSpecial = pwd.Any(c => char.IsDigit(c) ||
                                              char.IsPunctuation(c) ||
                                              char.IsSymbol(c));

        return isLatinOnly && isMinLength && hasDigitOrSpecial;
    }

    public async Task<bool> IsConfirmPasswordValid(string? password, string? confirmPassword) => 
        PasswordValidator.IsConfirmPasswordValid(password, confirmPassword);
}
