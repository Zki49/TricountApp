using prbd_2324_c02.Model;
using PRBD_Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prbd_2324_c02.ViewModel;

public class MainViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
{
    public string Title => "prbd_2324_c02";

    public ObservableCollection<Tricount> tricounts { get; set; } = new ();
    public MainViewModel() {
        OnRefreshData();
    }
    protected override void OnRefreshData() {
        tricounts.RefreshFromModel(Context.Tricounts.Where(t=> t.Creator.UserId == CurrentUser.UserId  ));   
    }
}