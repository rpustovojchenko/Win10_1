using Win10_1.Domain.Constraints;

namespace Win10_1.Business.Validation;

public class CommentValidator
{
    public Task<bool> IsCommentValid(string? comment) =>
        Task.FromResult(!string.IsNullOrWhiteSpace(comment));

    public Task<bool> IsCommentLengthValid(string? comment) =>
        Task.FromResult(
            comment is not null && 
            comment.Length <= CommentConstraints.CommentMaxLength);
}
