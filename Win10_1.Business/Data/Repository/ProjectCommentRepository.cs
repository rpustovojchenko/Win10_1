using Microsoft.EntityFrameworkCore;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository;

public class ProjectCommentRepository : IProjectCommentRepository
{
    private readonly AppDbContext _db;

    public ProjectCommentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ProjectComment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
    }

    public async Task<ProjectComment> UpdateAsync(ProjectComment comment)
    {
        comment.UpdatedAt = DateTime.UtcNow;

        _db.Comments.Update(comment);
        await _db.SaveChangesAsync();

        return comment;
    }

    public async Task DeleteAsync(int commentId)
    {
        var comment = await _db.Comments.FindAsync(commentId);
        if (comment is null) return;

        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ProjectComment>> GetByProjectIdAsync(int projectId) =>
        await _db.Comments.Where(c => c.ProjectId == projectId)
                          .ToListAsync();

    public async Task<ProjectComment?> GetByIdAsync(int id) =>
        await _db.Comments.FindAsync(id);
}
