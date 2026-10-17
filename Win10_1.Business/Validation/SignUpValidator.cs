using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Validation;

public class SignUpValidator
{
    private readonly IUserRepository _userRepo;

    public SignUpValidator(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> CanSignUpAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Login))
            return false;

        var dbUser = await _userRepo.GetByLoginAsync(user.Login);

        return dbUser is null;
    }
}
