using Microsoft.EntityFrameworkCore;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository;

public class ProjectTaskRepository : IProjectTaskRepository
{
    private readonly AppDbContext _db;

    public ProjectTaskRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ProjectTask task)
    {
        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();
    }

    public async Task<ProjectTask> UpdateAsync(ProjectTask task)
    {
        task.UpdatedAt = DateTime.UtcNow;

        _db.Tasks.Update(task);
        await _db.SaveChangesAsync();

        return task;
    }

    public async Task DeleteAsync(int taskId)
    {
        var task = await _db.Tasks.FindAsync(taskId);
        if (task is null) return;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ProjectTask>> GetByProjectIdAsync(int projectId) =>
        await _db.Tasks.Where(t => t.ProjectId == projectId)
                       .ToListAsync();

    public async Task<ProjectTask?> GetByIdAsync(int id) =>
        await _db.Tasks.FindAsync(id);
}
