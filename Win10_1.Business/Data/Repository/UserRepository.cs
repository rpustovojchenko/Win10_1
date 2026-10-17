using Microsoft.EntityFrameworkCore;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetByLoginAsync(string login) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
}
