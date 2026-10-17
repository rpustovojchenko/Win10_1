using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.UI.Services;

public class ProjectValidateService : IProjectValidateService
{
    private readonly ProjectValidator _pv;

    public ProjectValidateService(ProjectValidator pv)
    {
        _pv = pv;
    }

    public async Task<string?> CheckProjectName(string name)
    {
        if (!await _pv.IsProjectNameValid(name))
            return LocalizationService.GetString(LocalizationKeys.ProjectNameRequired);

        if (!await _pv.IsProjectNameLengthValid(name))
            return string.Format(
                LocalizationService.GetString(LocalizationKeys.ProjectNameMaxLength), 
                ProjectConstraints.ProjectNameMaxLength);

        return null;
    }

    public async Task<string?> CheckTheme(string? theme)
    {
        if (theme is null)
            return null;

        if (!await _pv.IsThemeLengthValid(theme))
            return string.Format(
                LocalizationService.GetString(LocalizationKeys.ProjectThemeMaxLength), 
                ProjectConstraints.ThemeMaxLength);

        return null;
    }

    public async Task<string?> CheckDescription(string? description)
    {
        if (description is null)
            return null;

        if (!await _pv.IsDescriptionLengthValid(description))
            return string.Format(
                LocalizationService.GetString(LocalizationKeys.ProjectDescriptionMaxLength), 
                ProjectConstraints.DescriptionMaxLength);

        return null;
    }
}