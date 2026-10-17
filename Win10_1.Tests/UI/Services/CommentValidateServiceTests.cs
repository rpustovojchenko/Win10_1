using System.Windows;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services;

namespace Win10_1.Tests.UI.Services;

[Collection("WpfApplication")]
public class CommentValidateServiceTests
{
    private readonly CommentValidator _commentValidator = new();
    private readonly CommentValidateService _service;

    public CommentValidateServiceTests()
    {
        if (Application.Current is null)
            _ = new Application();

        _service = new CommentValidateService(_commentValidator);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public async Task CheckComment_ReturnsRequiredMessage_WhenCommentIsInvalid(
        string? comment)
    {
        var result = await _service.CheckComment(comment!);

        var expected = LocalizationService.GetString(
            LocalizationKeys.CommentRequired);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckComment_ReturnsMaxLengthMessage_WhenCommentIsTooLong()
    {
        var tooLongComment = new string(
            'a',
            CommentConstraints.CommentMaxLength + 1);

        var result = await _service.CheckComment(tooLongComment);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.CommentMaxLength),
            CommentConstraints.CommentMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckComment_ReturnsMaxLengthMessage_WhenCommentLengthEqualsLimitPlusOne()
    {
        var comment = new string(
            'a',
            CommentConstraints.CommentMaxLength + 1);

        var result = await _service.CheckComment(comment);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("Valid comment")]
    [InlineData("a")]
    [InlineData("Комментарий на русском")]
    public async Task CheckComment_ReturnsNull_WhenCommentIsValid(string comment)
    {
        var result = await _service.CheckComment(comment);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckComment_ReturnsNull_WhenCommentLengthEqualsMaxLength()
    {
        var comment = new string(
            'a',
            CommentConstraints.CommentMaxLength);

        var result = await _service.CheckComment(comment);

        Assert.Null(result);
    }
}