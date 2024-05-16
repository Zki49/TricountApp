using PRBD_Framework;
using prbd_2324_c02.Model;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel;

public class LoginViewModel : CommonViewModel { 
    private string _mail;
    private string _password;
    public ICommand LoginCommand { get; set; }
    public ICommand CancelCommand { get; set; }

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
        Console.WriteLine(Password);

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
        //CancelCommand = new RelayCommand();
    }
    
    private void LoginAction() {
        if (Validate()) {
            var member = User.GetUserByMail(Mail);
            NotifyColleagues(App.Messages.MSG_LOGIN, member);
        }
    }

    protected override void OnRefreshData() {
    }
}
