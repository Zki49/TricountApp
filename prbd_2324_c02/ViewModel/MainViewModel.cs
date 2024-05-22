using Microsoft.EntityFrameworkCore;
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
    private string _inputText;
    public string InputText {
        get => _inputText;
        set => SetProperty(ref _inputText, value, () => OnRefreshData());
    }

    public ICommand LogoutCommand {  get; set; }
    public ICommand ClearCommand { get; set; }

    public ObservableCollection<Tricount> tricounts { get; set; } = new ();
    public MainViewModel() {
        OnRefreshData();
        LogoutCommand =  new RelayCommand(logout);
        ClearCommand = new RelayCommand(ClearTextBox);
    }
    protected override void OnRefreshData() {
        if (!CurrentUser.Role) {
            if (!string.IsNullOrEmpty(InputText)) { 
                tricounts.RefreshFromModel(Context.Tricounts.Where(t => t.Creator.UserId == CurrentUser.UserId &&(EF.Functions.Like(t.Title,InputText + "%" ) ||
                                                                                                                  EF.Functions.Like(t.Description, InputText + "%") ||
                                                                                                                  EF.Functions.Like(t.Creator.FullName, InputText + "%"))));
            } else {
                tricounts.RefreshFromModel(Context.Tricounts.Where(t => t.Creator.UserId == CurrentUser.UserId));
            }
        } else {
            if (!string.IsNullOrEmpty(InputText)) {
                tricounts.RefreshFromModel(Context.Tricounts.Where(t => (EF.Functions.Like(t.Title, InputText + "%") ||
                                                                         EF.Functions.Like(t.Description, InputText + "%") ||
                                                                         EF.Functions.Like(t.Creator.FullName, InputText + "%"))));
            } else {
                tricounts.RefreshFromModel(Context.Tricounts);
            }
        }
        
    }
    private void logout() {
        NotifyColleagues(App.Messages.MSG_LOGOUT);
    }
    private void ClearTextBox() {
        InputText = string.Empty;
    }
}