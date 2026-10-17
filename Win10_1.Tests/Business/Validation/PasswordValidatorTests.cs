using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;

namespace Win10_1.Tests.Business.Validation;

public class PasswordValidatorTests
{
    public static TheoryData<string?, bool, bool, bool> PasswordCases =>
        new()
        {
            // password, isLatinOnly, isMinLength, hasDigitOrSpecial
            { null, false, false, false },
            { "", false, false, false },

            { "abc", true, false, false },
            { "abcdef", true, false, false },

            { new string('1', UserConstraints.PasswordMinLength), false, true, true },
            { "abc" + new string('1', UserConstraints.PasswordMinLength - 3), true, true, true },

            { "abc!", true, false, true },
            { "abc" + new string('1', UserConstraints.PasswordMinLength - 3) + "!", true, true, true },

            { "Абв", false, false, false },
            { "abcАбв", false, false, false },
            { "abc ", true, false, false }
        };

    [Theory]
    [MemberData(nameof(PasswordCases))]
    public void IsPasswordValid_ReturnsExpectedChecks(
        string? password,
        bool expectedIsLatinOnly,
        bool expectedIsMinLength,
        bool expectedHasDigitOrSpecial)
    {
        var result = PasswordValidator.IsPasswordValid(password);

        Assert.Equal(expectedIsLatinOnly, result.IsLatinOnly);
        Assert.Equal(expectedIsMinLength, result.IsMinLength);
        Assert.Equal(expectedHasDigitOrSpecial, result.HasDigitOrSpecial);
    }

    [Fact]
    public void IsPasswordValid_ReturnsIsMinLengthTrue_WhenLengthEqualsMinLength()
    {
        var password = new string('a', UserConstraints.PasswordMinLength);

        var result = PasswordValidator.IsPasswordValid(password);

        Assert.True(result.IsMinLength);
        Assert.True(result.IsLatinOnly);
        Assert.False(result.HasDigitOrSpecial);
    }

    [Fact]
    public void IsPasswordValid_ReturnsIsMinLengthFalse_WhenLengthIsLessThanMinLength()
    {
        var password = new string('a', UserConstraints.PasswordMinLength - 1);

        var result = PasswordValidator.IsPasswordValid(password);

        Assert.False(result.IsMinLength);
    }

    [Theory]
    [InlineData("abc1", true)]
    [InlineData("abc!", true)]
    [InlineData("abc@", true)]
    [InlineData("abc#", true)]
    [InlineData("abc$", true)]
    [InlineData("abc%", true)]
    [InlineData("abc-", true)]
    [InlineData("abc_", true)]
    [InlineData("abc", false)]
    [InlineData("abc ", false)]
    public void IsPasswordValid_ReturnsHasDigitOrSpecial(
        string password,
        bool expected)
    {
        var result = PasswordValidator.IsPasswordValid(password);

        Assert.Equal(expected, result.HasDigitOrSpecial);
    }

    [Theory]
    [InlineData(null, null, true)]
    [InlineData("", "", true)]
    [InlineData("Password123!", "Password123!", true)]
    [InlineData("Password123!", "password123!", false)]
    [InlineData("Password123!", "", false)]
    [InlineData("", "Password123!", false)]
    [InlineData(null, "Password123!", false)]
    [InlineData("Password123!", null, false)]
    public void IsConfirmPasswordValid_ReturnsExpectedResult(
        string? password,
        string? confirmPassword,
        bool expected)
    {
        var result = PasswordValidator.IsConfirmPasswordValid(
            password,
            confirmPassword);

        Assert.Equal(expected, result);
    }
}