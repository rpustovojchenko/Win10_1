using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Validation;

public class SignInValidator
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;

    public SignInValidator(IUserRepository userRepo, IPasswordHasher passwordHasher)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> CanSignInAsync(User user, string password)
    {
        var dbUser = await _userRepo.GetByLoginAsync(user.Login);

        if (dbUser is null) return false;

        return _passwordHasher.VerifyPassword(password, dbUser.HashPassword);
    }
}
