using Win10_1.Domain.Constraints;

namespace Win10_1.Business.Validation;

public class ProjectValidator
{
    public Task<bool> IsProjectNameValid(string? projectName) =>
        Task.FromResult(!string.IsNullOrWhiteSpace(projectName));

    public Task<bool> IsProjectNameLengthValid(string? projectName) =>
        Task.FromResult(
            projectName is not null && 
            projectName.Length <= ProjectConstraints.ProjectNameMaxLength);

    public Task<bool> IsThemeLengthValid(string? theme) =>
        Task.FromResult(
            theme is not null && 
            theme.Length <= ProjectConstraints.ThemeMaxLength);

    public Task<bool> IsDescriptionLengthValid(string? description) =>
        Task.FromResult(
            description is not null && 
            description.Length <= ProjectConstraints.DescriptionMaxLength);
}
