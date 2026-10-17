using System.Collections.ObjectModel;
using Win10_1.Domain.Enums;
using Win10_1.UI.Services;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.Tests.UI.Services;

public class FilterServiceTests
{
    private readonly FilterService _service = new();

    private static ProjectVM CreateProject(
        string projectName,
        string? theme = null,
        Status status = Status.None,
        Priority priority = Priority.None) =>
        new()
        {
            ProjectName = projectName,
            Theme = theme,
            Status = status,
            Priority = priority
        };

    private static ObservableCollection<ProjectVM> CreateProjects() =>
        new()
        {
            CreateProject(
                "Alpha Project",
                "Dark",
                Status.InProgress,
                Priority.High),

            CreateProject(
                "Beta Application",
                "Light",
                Status.Completed,
                Priority.Medium),

            CreateProject(
                "Gamma System",
                "Dark Blue",
                Status.OnHold,
                Priority.Low),

            CreateProject(
                "Delta Report",
                null,
                Status.InProgress,
                Priority.High)
        };

    [Fact]
    public void Filter_ReturnsAllProjects_WhenAllFiltersAreEmpty()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Equal(4, result.Count);
        Assert.Equal(
            projects.Select(p => p.ProjectName),
            result.Select(p => p.ProjectName));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Filter_ReturnsAllProjects_WhenNameFilterIsNullOrWhitespace(
        string? projectNameFilter)
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            projectNameFilter!,
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Filter_FiltersByName_CaseInsensitively()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            "ALPHA",
            string.Empty,
            Status.None,
            Priority.None);

        var project = Assert.Single(result);

        Assert.Equal("Alpha Project", project.ProjectName);
    }

    [Fact]
    public void Filter_FiltersByName_ByPartialMatch()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            "project",
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Equal(
            new[] { "Alpha Project" },
            result.Select(p => p.ProjectName));
    }

    [Fact]
    public void Filter_ReturnsEmptyCollection_WhenNameDoesNotMatchAnyProject()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            "Unknown",
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Empty(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Filter_ReturnsAllProjects_WhenThemeFilterIsNullOrWhitespace(
        string? themeFilter)
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            themeFilter!,
            Status.None,
            Priority.None);

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Filter_ExcludesProjectsWithNullTheme_WhenThemeFilterIsSet()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            "Dark",
            Status.None,
            Priority.None);

        Assert.DoesNotContain(
            result,
            p => p.ProjectName == "Delta Report");
    }

    [Fact]
    public void Filter_ReturnsEmptyCollection_WhenThemeDoesNotMatchAnyProject()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            "Unknown Theme",
            Status.None,
            Priority.None);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_ReturnsAllProjects_WhenStatusFilterIsNone()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Filter_FiltersByStatus()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.InProgress,
            Priority.None);

        Assert.Equal(
            new[] { "Alpha Project", "Delta Report" },
            result.Select(p => p.ProjectName));
    }

    [Fact]
    public void Filter_ReturnsEmptyCollection_WhenStatusDoesNotMatchAnyProject()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.Cancelled,
            Priority.None);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_ReturnsAllProjects_WhenPriorityFilterIsNone()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.None,
            Priority.None);

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Filter_FiltersByPriority()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.None,
            Priority.High);

        Assert.Equal(
            new[] { "Alpha Project", "Delta Report" },
            result.Select(p => p.ProjectName));
    }

    [Fact]
    public void Filter_ReturnsEmptyCollection_WhenPriorityDoesNotMatchAnyProject()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            string.Empty,
            string.Empty,
            Status.None,
            Priority.Critical);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_AppliesAllFiltersTogether()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            "project",
            "dark",
            Status.InProgress,
            Priority.High);

        var project = Assert.Single(result);

        Assert.Equal("Alpha Project", project.ProjectName);
    }

    [Fact]
    public void Filter_ReturnsEmptyCollection_WhenCombinedFiltersDoNotMatch()
    {
        var projects = CreateProjects();

        var result = _service.Filter(
            projects,
            "Alpha",
            "Light",
            Status.InProgress,
            Priority.High);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_DoesNotModifyOriginalCollection()
    {
        var projects = CreateProjects();
        var originalCount = projects.Count;

        _service.Filter(
            projects,
            "Alpha",
            "Dark",
            Status.InProgress,
            Priority.High);

        Assert.Equal(originalCount, projects.Count);
    }
}