using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByLoginAsync(string login);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
