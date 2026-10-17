using System.Windows.Input;
using Win10_1.Business.Commands;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.Base;
using Win10_1.UI.ViewModels.ModelsVM;
using Win10_1.UI.ViewModels.Session;
using Win10_1.UI.Views.Windows;

namespace Win10_1.UI.ViewModels;

public class AuthorizationVM : BaseVM
{
    public UserVM User { get; }
    public PasswordRequirementsVM Password { get; }

    private readonly AppSession             _session;
    private readonly IAuthValidateService   _avs;
    private readonly IPasswordHasher        _passwordHasher;
    private readonly IUserRepository        _userRepo;
    private readonly INavigationService     _navService;
    
    public ICommand SignInCommand { get; }
    public ICommand SignUpCommand { get; }


    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            Set(ref _selectedTabIndex, value);
            OnTabChanged(value);
        }
    }

    public AuthorizationVM(
        AppSession              session,
        IAuthValidateService    avs, 
        IPasswordHasher         passwordHasher,
        IUserRepository         userRepo,
        INavigationService      navService)
    {
        _session            = session;
        _avs                = avs;
        _passwordHasher     = passwordHasher;
        _userRepo           = userRepo;
        _navService         = navService;

        User        = _session.User;
        Password    = _session.Password;

        SignInCommand = new AsyncRelayCommand(OnSignInCommandExecuted, CanSignInCommandExecute);
        SignUpCommand = new AsyncRelayCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);
    }

    private bool CanSignInCommandExecute() => true;
    private bool CanSignUpCommandExecute() => true;

    private async Task OnSignInCommandExecuted()
    {
        if (!await _avs.CheckSignInForm(this, User, Password) ||
            !await _avs.CanSignIn(this, User, Password)) return;

        User.HashPassword = _passwordHasher.HashPassword(Password.Password
            ?? throw new ArgumentNullException());

        var userFromDb = await _userRepo.GetByLoginAsync(User.Login!);
        _session.User = new UserVM(userFromDb!);

        OpenTaskManager();
    }

    private async Task OnSignUpCommandExecuted()
    {
        if (!await _avs.CheckSignUpForm(this, User, Password) ||
            !await _avs.CanSignUp(this, User)) return;

        User.HashPassword = _passwordHasher.HashPassword(Password.Password
            ?? throw new ArgumentNullException());

        await _userRepo.AddAsync(User.User);

        var userFromDb = await _userRepo.GetByLoginAsync(User.Login!);
        _session.User = new UserVM(userFromDb!);

        OpenTaskManager();
    }

    private void OnTabChanged(int index)
    {
        Password.Password           = string.Empty;
        Password.ConfirmPassword    = string.Empty;
    }

    private void OpenTaskManager()
    {
        _navService.OpenWindow<TaskManagerWindow>();
        _navService.SetMainWindow<TaskManagerWindow>();
        _navService.CloseWindow<AuthorizationWindow>();
    }
}
