using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository.Interfaces;

public interface IProjectCommentRepository
{
    Task AddAsync(ProjectComment comment);
    Task<ProjectComment> UpdateAsync(ProjectComment comment);
    Task DeleteAsync(int commentId);
    Task<List<ProjectComment>> GetByProjectIdAsync(int projectId);
    Task<ProjectComment?> GetByIdAsync(int id);
}
