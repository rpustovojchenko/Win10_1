using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class TaskValidateService : ITaskValidateService
{
    private readonly TaskValidator _tv;

    public TaskValidateService(TaskValidator tv)
    {
        _tv = tv;
    }

    public async Task<string?> CheckTitle(string title)
    {
        if (!await _tv.IsTitleValid(title))
            return LocalizationService.GetString(LocalizationKeys.TaskTitleRequired);

        if (!await _tv.IsTitleLengthValid(title))
            return string.Format(
                LocalizationService.GetString(LocalizationKeys.TaskTitleMaxLength), 
                TaskConstraints.TitleMaxLength);

        return null;
    }

    public async Task<string?> CheckDescription(string description)
    {
        if (!await _tv.IsDescriptionValid(description))
            return LocalizationService.GetString(LocalizationKeys.TaskDescriptionRequired);

        if (!await _tv.IsDescriptionLengthValid(description))
            return string.Format(
                LocalizationService.GetString(LocalizationKeys.TaskDescriptionMaxLength), 
                TaskConstraints.DescriptionMaxLength);

        return null;
    }
}