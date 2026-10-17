using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.ViewModels.Session;

public class AppSession
{
    public UserVM User { get; set; }
    public PasswordRequirementsVM Password { get; set; }

    public AppSession()
    {
        User = new UserVM();
        Password = new PasswordRequirementsVM();
    }
}
