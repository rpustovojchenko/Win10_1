using Win10_1.Business.Services;

namespace Win10_1.Tests.Business.Services;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Theory]
    [InlineData("Password123!")]
    [InlineData("P@ssw0rd_with_long_value_1234567890")]
    [InlineData("пароль")]
    public void HashPassword_ReturnsHash_DifferentFromPassword(string password)
    {
        var hash = _passwordHasher.HashPassword(password);

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual(password, hash);
    }

    [Fact]
    public void HashPassword_ReturnsDifferentHashes_ForSamePassword()
    {
        const string password = "Password123!";

        var firstHash = _passwordHasher.HashPassword(password);
        var secondHash = _passwordHasher.HashPassword(password);

        Assert.NotEqual(firstHash, secondHash);
    }

    [Fact]
    public void VerifyPassword_ReturnsTrue_ForCorrectPassword()
    {
        const string password = "Password123!";

        var hash = _passwordHasher.HashPassword(password);

        Assert.True(_passwordHasher.VerifyPassword(password, hash));
    }

    [Theory]
    [InlineData("WrongPassword123!")]
    [InlineData("password123!")]
    [InlineData("PASSWORD123!")]
    [InlineData("")]
    public void VerifyPassword_ReturnsFalse_ForIncorrectPassword(string incorrectPassword)
    {
        const string password = "Password123!";

        var hash = _passwordHasher.HashPassword(password);

        Assert.False(_passwordHasher.VerifyPassword(incorrectPassword, hash));
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse_ForValidButNonMatchingHash()
    {
        const string password = "Password123!";
        const string otherPassword = "DifferentPassword123!";

        var hash = _passwordHasher.HashPassword(otherPassword);

        Assert.False(_passwordHasher.VerifyPassword(password, hash));
    }

    [Fact]
    public void VerifyPassword_ThrowsSaltParseException_ForInvalidHash()
    {
        const string password = "Password123!";
        const string invalidHash = "not-a-bcrypt-hash";

        Assert.ThrowsAny<BCrypt.Net.SaltParseException>(
            () => _passwordHasher.VerifyPassword(password, invalidHash));
    }
}
