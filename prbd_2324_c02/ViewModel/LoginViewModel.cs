using PRBD_Framework;
using prbd_2324_c02.Model;

namespace prbd_2324_c02.ViewModel;

public class LoginViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
{
    private string _mail;

    public string Mail {
        get => _mail;
        set => SetProperty(ref _mail,  value ,()=> Validate());
    }

    public override bool Validate() {
        ClearErrors();

        var member = User.GetUserByMail(Mail);

        if (string.IsNullOrEmpty(Mail))
            AddError(nameof(Mail), "required");
        else if (Mail.Length < 3)
            AddError(nameof(Mail), "length must be >= 3");
        else if (member == null)
            AddError(nameof(Mail), "does not exist");

        return !HasErrors;
    }

    protected override void OnRefreshData() {
    }
}
