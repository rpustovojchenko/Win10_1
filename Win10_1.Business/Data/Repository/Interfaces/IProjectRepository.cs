using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Repository.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(int projectId);
    Task<List<Project>> GetByUserIdAsync(int userId);
    Task<Project?> GetByIdAsync(int id);
}
