using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using prbd_2324_c02.Model;
using System.Windows;
using System.Text.RegularExpressions;

namespace prbd_2324_c02.ViewModel;

    public class SignupViewModel : ViewModelBase<User, PridContext>
{
    private string _mail;
    private string _password;
    private string _confirmPassword;
    private string _fullName;
    public ICommand ExitCommand { get; set; }
    public ICommand SignupCommand { get; set; }


        public SignupViewModel() {
            ExitCommand = new RelayCommand(Exit);
            SignupCommand = new RelayCommand(SignupAction,
                () => _mail != null && _fullName != null && _password != null &&!HasErrors);
    }
        private void Exit() {
            NotifyColleagues(App.Messages.MSG_LOGOUT);
        }

    private void SignupAction() {
        if (ValidatePassword() && ValidateFullName() && ValidateMail()) {
            User.AddUser(FullName,Mail, Password);

            
            var member = User.GetUserByMail(Mail);
            
            NotifyColleagues(App.Messages.MSG_LOGIN, member);
        }
    }

    public string Mail {
        get => _mail;
        set => SetProperty(ref _mail, value, () => ValidateMail());
    }
    public string Password {
        get => _password;
        set => SetProperty(ref _password, value, () => ValidatePassword());
    }
    public string ConfirmPassword {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value, () => ValidatePassword());
    }
    public string FullName {
        get => _fullName;
        set => SetProperty(ref _fullName, value, () => ValidateFullName());
    }

    public bool ValidateMail() {

        ClearErrors();
        var member = User.GetUserByMail(Mail);

        if (string.IsNullOrEmpty(Mail))
            AddError(nameof(Mail), "required");
        else if (member != null) {
            AddError(nameof(Mail), "mail already exist");
        }

        return !HasErrors;
    }

    public bool ValidatePassword() {
        ClearErrors();
        string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";

        if (string.IsNullOrEmpty(Password))
            AddError(nameof(Password), "required");
        else if (string.IsNullOrEmpty(ConfirmPassword))
            AddError(nameof(ConfirmPassword), "required");
        else if (!Regex.IsMatch(Password, pattern)) {
            AddError(nameof(Password), "min lenght 8 and 1 maj and 1 signe spécial et 1 chiffre");
        }
        else if (!Password.Equals(ConfirmPassword)) {
            AddError(nameof(ConfirmPassword), "must be the same");
        }

        return !HasErrors;
    }

    public bool ValidateFullName() {
        ClearErrors();
        
        if (FullName.Length<3)
            AddError(nameof(FullName), "min 3 character");
        
        return !HasErrors;
    }


}

