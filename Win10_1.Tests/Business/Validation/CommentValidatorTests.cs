using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;

namespace Win10_1.Tests.Business.Validation;

public class CommentValidatorTests
{
    private readonly CommentValidator _validator = new();

    [Theory]
    [InlineData("Комментарий")]
    [InlineData(" Comment ")]
    [InlineData("a")]
    public async Task IsCommentValid_ReturnsTrue_ForNonEmptyComment(string comment)
    {
        var result = await _validator.IsCommentValid(comment);

        Assert.True(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public async Task IsCommentValid_ReturnsFalse_ForNullOrWhitespaceComment(string? comment)
    {
        var result = await _validator.IsCommentValid(comment!);

        Assert.False(result);
    }

    [Fact]
    public async Task IsCommentLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var comment = new string('a', CommentConstraints.CommentMaxLength);

        var result = await _validator.IsCommentLengthValid(comment);

        Assert.True(result);
    }

    [Fact]
    public async Task IsCommentLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var comment = new string('a', CommentConstraints.CommentMaxLength + 1);

        var result = await _validator.IsCommentLengthValid(comment);

        Assert.False(result);
    }

    [Fact]
    public async Task IsCommentLengthValid_ReturnsTrue_ForEmptyComment()
    {
        var result = await _validator.IsCommentLengthValid(string.Empty);

        Assert.True(result);
    }

    [Fact]
    public async Task IsCommentLengthValid_ReturnsFalse_ForNullComment()
    {
        var result = await _validator.IsCommentLengthValid(null);

        Assert.False(result);
    }
}
