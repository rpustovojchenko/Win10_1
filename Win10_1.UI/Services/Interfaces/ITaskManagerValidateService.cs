using Win10_1.Domain.Models;

namespace Win10_1.UI.Services.Interfaces;

public interface ITaskManagerValidateService
{
    Task<bool> CheckNewProjectForm(object context, Project project);
}
