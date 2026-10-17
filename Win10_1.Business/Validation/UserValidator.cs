using Win10_1.Domain.Constraints;

namespace Win10_1.Business.Validation;

public static class UserValidator
{
    public static bool IsUsernameValid(string? username) =>
        !string.IsNullOrWhiteSpace(username) &&
        username.All(c => char.IsLetter(c) && c != ' ');

    public static bool IsUsernameLengthValid(string? username) =>
        username is not null &&
        username.Length <= UserConstraints.UsernameMaxLength;

    public static bool IsLoginValid(string? login) =>
        !string.IsNullOrWhiteSpace(login) &&
        login.All(c => 
            ('a' <= c && c <= 'z') || 
            ('A' <= c && c <= 'Z') || 
            char.IsDigit(c));

    public static bool IsLoginLengthValid(string? login) =>
        login is not null &&
        login.Length <= UserConstraints.LoginMaxLength;
}
