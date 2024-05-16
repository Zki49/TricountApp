using prbd_2324_c02.Model;
using PRBD_Framework;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel;

public class MainViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
{

    public string Title => "prbd_2324_c02";
    public string Color => "LightGray";
    public ICommand LogoutCommand {  get; set; }

    public ObservableCollection<Tricount> tricounts { get; set; } = new ();
    public MainViewModel() {
        OnRefreshData();
        LogoutCommand =  new RelayCommand(logout);
    }
    protected override void OnRefreshData() {
        if (!CurrentUser.Role) {
            tricounts.RefreshFromModel(Context.Tricounts.Where(t => t.Creator.UserId == CurrentUser.UserId));
        } else {
            tricounts.RefreshFromModel(Context.Tricounts);
        }
    }
    private void logout() {
        NotifyColleagues(App.Messages.MSG_LOGOUT);
    }
}