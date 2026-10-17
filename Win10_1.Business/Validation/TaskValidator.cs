using Win10_1.Domain.Constraints;

namespace Win10_1.Business.Validation;

public class TaskValidator
{
    public Task<bool> IsTitleValid(string? title) =>
        Task.FromResult(!string.IsNullOrWhiteSpace(title));

    public Task<bool> IsTitleLengthValid(string? title) =>
        Task.FromResult(
            title is not null && 
            title.Length <= TaskConstraints.TitleMaxLength);

    public Task<bool> IsDescriptionValid(string? description) => 
        Task.FromResult(!string.IsNullOrWhiteSpace(description));

    public Task<bool> IsDescriptionLengthValid(string? description) =>
        Task.FromResult(
            description is not null && 
            description.Length <= TaskConstraints.DescriptionMaxLength);
}
