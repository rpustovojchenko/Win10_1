using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Win10_1.Business.Data;
using Win10_1.Business.Data.Repository;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Enums;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels;
using Win10_1.UI.ViewModels.DialogsVM.Services;
using Win10_1.UI.ViewModels.DialogsVM.Services.Interfaces;
using Win10_1.UI.ViewModels.Session;
using Win10_1.UI.Views.Windows;

namespace Win10_1.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public ResourceDictionary ThemeDictionary       { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);

        Services = services.BuildServiceProvider();

        var db = Services.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        ChangeTheme(AppTheme.Dark);

        LocalizationService.SetLanguage(AppLanguage.Russian);

        var authWindow = Services.GetRequiredService<AuthorizationWindow>();

        MainWindow = authWindow;
        authWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Db Context
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Config.GetConnectionString()));

        // Repositories
        services.AddScoped<IUserRepository          , UserRepository>();
        services.AddScoped<IProjectRepository       , ProjectRepository>();
        services.AddScoped<IProjectTaskRepository   ,  ProjectTaskRepository>();
        services.AddScoped<IProjectCommentRepository, ProjectCommentRepository>();

        // Validation
        services.AddScoped<SignInValidator>();
        services.AddScoped<SignUpValidator>();
        services.AddScoped<ProjectValidator>();
        services.AddScoped<TaskValidator>();
        services.AddScoped<CommentValidator>();
        services.AddScoped<ProfileValidator>();

        // Business Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // UI Services
        services.AddSingleton<IDialogCoordinator        , DialogCoordinator>();
        services.AddSingleton<INavigationService        , NavigationService>();
        services.AddScoped<IMessageService              , MahAppsMessageService>();
        services.AddScoped<IAuthValidateService         , AuthValidateService>();
        services.AddScoped<ITaskManagerValidateService  , TaskManagerValidateService>();
        services.AddScoped<IDialogVMService             , DialogVMService>();
        services.AddScoped<ITaskValidateService         , TaskValidateService>();
        services.AddScoped<ICommentValidateService      , CommentValidateService>();
        services.AddScoped<IProjectValidateService      , ProjectValidateService>();
        services.AddScoped<IFilterService               , FilterService>();
        services.AddScoped<IProfileValidateService      , ProfileValidateService>();

        // Session
        services.AddSingleton<AppSession>();

        // View Models
        services.AddScoped<AuthorizationVM>();
        services.AddScoped<TaskManagerVM>();

        // Windows
        services.AddScoped<AuthorizationWindow>();
        services.AddScoped<TaskManagerWindow>();
    }

    public void ChangeTheme(AppTheme theme)
    {
        switch (theme)
        {
            case AppTheme.Light:
                ThemeManager.Current.ChangeTheme(Current, "Light.Blue");
                ReplaceThemeDictionary(new Uri("Resources/Themes/LightTheme.xaml", UriKind.Relative));
                break;
            case AppTheme.Dark:
                ThemeManager.Current.ChangeTheme(Current, "Dark.Blue");
                ReplaceThemeDictionary(new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative));
                break;
        }
    }

    private void ReplaceThemeDictionary(Uri source)
    {
        Resources.MergedDictionaries.Remove(ThemeDictionary);

        ThemeDictionary = new ResourceDictionary
        {
            Source = source
        };

        Resources.MergedDictionaries.Add(ThemeDictionary);
    }
}
