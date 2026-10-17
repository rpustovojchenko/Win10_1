using Win10_1.Business.Services.Interfaces;

namespace Win10_1.Business.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyPassword(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
}
