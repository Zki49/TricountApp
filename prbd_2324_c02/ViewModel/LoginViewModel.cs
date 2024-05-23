using PRBD_Framework;
using prbd_2324_c02.Model;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel;

public class LoginViewModel : CommonViewModel { 
    private string _mail;
    private string _password;
    public ICommand LoginCommand { get; set; }

    public ICommand LoginCommandBoris { get; set; }
    public ICommand LoginCommandBenoit { get; set; }
    public ICommand LoginCommandXavier { get; set; }
    public ICommand LoginCommandAdmin { get; set; }
    public ICommand SignupCommand { get; set; }

    public string Mail {
        get => _mail;
        set => SetProperty(ref _mail,  value ,()=> Validate());
    }
    public string Password {
        get => _password;
        set => SetProperty(ref _password, value, () => Validate());
    }

    public override bool Validate() {
        ClearErrors();

        var member = User.GetUserByMail(Mail);

        if (string.IsNullOrEmpty(Mail))
            AddError(nameof(Mail), "required");
        else if (member == null)
            AddError(nameof(Mail), "does not exist");
        else if (!member.checkPassword(Password)) {
            AddError(nameof(Password), "wrong password");
        }
        

        return !HasErrors;
    }
    public LoginViewModel() {
        LoginCommand = new RelayCommand(LoginAction,
            () => _mail != null && _password != null && !HasErrors);

        LoginCommandBoris = new RelayCommand(LoginActionBoris);
        LoginCommandXavier= new RelayCommand(LoginActionXavier);
        LoginCommandBenoit= new RelayCommand(LoginActionBenoit);
        LoginCommandAdmin= new RelayCommand(LoginActionAdmin);
        SignupCommand = new RelayCommand(SignupAction);
    }
    
    private void LoginAction() {
        if (Validate()) {
            var member = User.GetUserByMail(Mail);
            NotifyColleagues(App.Messages.MSG_LOGIN, member);
        }
    }
    private void LoginActionBoris() {
        var member = Context.Users.Find(1);
        NotifyColleagues(App.Messages.MSG_LOGIN, member);

    }
    private void LoginActionBenoit() {
        var member = Context.Users.Find(2);
        NotifyColleagues(App.Messages.MSG_LOGIN, member);

    }
    private void LoginActionXavier() {
        var member = Context.Users.Find(4);
        NotifyColleagues(App.Messages.MSG_LOGIN, member);

    }
    private void LoginActionAdmin() {
        var member = Context.Users.Find(6);
        NotifyColleagues(App.Messages.MSG_LOGIN, member);

    }
    private void SignupAction() {
        NotifyColleagues(App.Messages.MSG_SIGNUP);
    }
    protected override void OnRefreshData() {
    }
}
