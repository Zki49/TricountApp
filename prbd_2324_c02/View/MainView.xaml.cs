using prbd_2324_c02.Model;
using PRBD_Framework;
using System.Windows.Controls;
using System.Windows.Input;
using static prbd_2324_c02.App;

namespace prbd_2324_c02.View;

public partial class MainView : WindowBase
{
    public MainView() {
        InitializeComponent();

        Register<Tricount>(App.Messages.MSG_ADD , tricount => EditTricount(tricount, false));
        Register<Tricount>(App.Messages.MSG_OPEN_TRICOUNT, tricount => OpenTricount(tricount));
        Register<Tricount>(App.Messages.MSG_ADD_OPERATION, tricount => new AddOperationView(tricount, false, new Operations()).Show());

    }

    private void EditTricount(Tricount tricount, bool isEdit) {
        if (tricount != null) {
            OpenTab(!isEdit ? "<New Tricount>" : tricount.Title, tricount.Title, () => new EditTricountView(tricount, isEdit));
        }
        
    }
    private void OpenTricount(Tricount tricount) {
        if (tricount != null) {
            OpenTab(tricount.Title,tricount.Title, () => new OpenTricountView(tricount));
        }

    }

    private void OpenTab(string header, string tag, Func<UserControlBase> createView) {
        var tab = tabControl.FindByTag(tag);
        if (tab == null)
            tabControl.Add(createView(), header, tag);
        else
            tabControl.SetFocus(tab);
    }

}