using Win10_1.Business.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;

    public ProjectRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Project project)
    {
        await _db.Projects.AddAsync(project);
        await _db.SaveChangesAsync();
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        project.UpdatedAt = DateTime.UtcNow;

        _db.Projects.Update(project);
        await _db.SaveChangesAsync();

        return project;
    }

    public async Task DeleteAsync(int projectId)
    {
        var proj = await _db.Projects.FindAsync(projectId);
        if (proj is null) return;

        _db.Projects.Remove(proj);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Project>> GetByUserIdAsync(int userId) =>
        await _db.Projects.Where(p => p.UserId == userId)
                          .ToListAsync();

    public async Task<Project?> GetByIdAsync(int id) =>
        await _db.Projects.FindAsync(id);
}
