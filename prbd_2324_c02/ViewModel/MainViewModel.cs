using prbd_2324_c02.Model;
using PRBD_Framework;

namespace prbd_2324_c02.ViewModel;

public class MainViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
{
    public User CurrentUser => ViewModelBase<User, PridContext>.CurrentUser;
    public string Title => CurrentUser.FullName;
}