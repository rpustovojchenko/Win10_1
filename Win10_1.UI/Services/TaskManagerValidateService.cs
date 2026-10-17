using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.Domain.Models;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class TaskManagerValidateService : ITaskManagerValidateService
{
    private readonly IMessageService    _ms;
    private readonly ProjectValidator   _pv;

    public TaskManagerValidateService(IMessageService ms, ProjectValidator pv)
    {
        _ms = ms;
        _pv = pv;
    }

    public async Task<bool> CheckNewProjectForm(object context, Project project) =>
        await CheckProjectName(context, project.ProjectName)                    &&
        await CheckDescription(context, project.Description ?? string.Empty)    &&
        await CheckTheme(context, project.Theme ?? string.Empty);

    private async Task<bool> CheckProjectName(object context, string projectName)
    {
        if (!await _pv.IsProjectNameValid(projectName))
        {
            await _ms.ShowWarningAsync(
                context, 
                LocalizationService.GetString(LocalizationKeys.ProjectNameRequired));

            return false;
        }

        if (!await _pv.IsProjectNameLengthValid(projectName))
        {
            await _ms.ShowErrorAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProjectNameMaxLength), ProjectConstraints.ProjectNameMaxLength),
                LocalizationService.GetString(LocalizationKeys.InvalidProjectNameTitle));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckDescription(object context, string description)
    {
        if (!await _pv.IsDescriptionLengthValid(description))
        {
            await _ms.ShowErrorAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProjectDescriptionMaxLength), ProjectConstraints.DescriptionMaxLength),
                LocalizationService.GetString(LocalizationKeys.InvalidProjectDescriptionTitle));

            return false;
        }

        return true;
    }

    private async Task<bool> CheckTheme(object context, string description)
    {
        if (!await _pv.IsThemeLengthValid(description))
        {
            await _ms.ShowErrorAsync(
                context,
                string.Format(LocalizationService.GetString(LocalizationKeys.ProjectThemeMaxLength), ProjectConstraints.ThemeMaxLength),
                LocalizationService.GetString(LocalizationKeys.InvalidProjectThemeTitle));

            return false;
        }

        return true;
    }
}
