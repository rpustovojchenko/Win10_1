using Win10_1.Domain.Constraints;

namespace Win10_1.Business.Validation;

public static class PasswordValidator
{
    public static (bool IsLatinOnly, bool IsMinLength, bool HasDigitOrSpecial) IsPasswordValid(string? password)
    {
        var pwd = password ?? string.Empty;

        bool hasLetters = pwd.Any(char.IsLetter);

        bool allLettersLatin = pwd.Where(char.IsLetter)
            .All(c =>
                ('a' <= c && c <= 'z') ||
                ('A' <= c && c <= 'Z'));

        bool isLatinOnly = hasLetters && allLettersLatin;

        bool isMinLength = pwd.Length >= UserConstraints.PasswordMinLength;

        bool hasDigitOrSpecial = pwd.Any(c => char.IsDigit(c)       ||
                                              char.IsPunctuation(c) ||
                                              char.IsSymbol(c));

        return (isLatinOnly, isMinLength, hasDigitOrSpecial);
    }

    public static bool IsConfirmPasswordValid(string? password, string? confirmPassword) =>
        password == confirmPassword;
}
