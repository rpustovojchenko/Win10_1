using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository.Interfaces;

public interface IProjectTaskRepository
{
    Task AddAsync(ProjectTask task);
    Task<ProjectTask> UpdateAsync(ProjectTask task);
    Task DeleteAsync(int taskId);
    Task<List<ProjectTask>> GetByProjectIdAsync(int projectId);
    Task<ProjectTask?> GetByIdAsync(int id);
}
