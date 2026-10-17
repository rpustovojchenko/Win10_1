using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class CommentValidateService : ICommentValidateService
{
    private readonly CommentValidator _cv;

    public CommentValidateService(CommentValidator cv)
    {
        _cv = cv;
    }

    public async Task<string?> CheckComment(string comment)
    {
        if (!await _cv.IsCommentValid(comment))
            return LocalizationService.GetString(LocalizationKeys.CommentRequired);

        if (!await _cv.IsCommentLengthValid(comment))
            return string.Format(LocalizationService.GetString(LocalizationKeys.CommentMaxLength), CommentConstraints.CommentMaxLength); ;

        return null;
    }
}
