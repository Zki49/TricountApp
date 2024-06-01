using prbd_2324_c02.Model;
using PRBD_Framework;
using System.Windows.Controls;
using System.Windows.Input;

namespace prbd_2324_c02.View;

public partial class MainView : WindowBase
{
    public MainView() {
        InitializeComponent();
        Register<Tricount>(App.Messages.MSG_EDIT, tricount => EditTricount(tricount, true));
        Register<Tricount>(App.Messages.MSG_ADD , tricount => EditTricount(tricount, false));
        Register<Tricount>(App.Messages.MSG_OPEN_TRICOUNT, tricount => OpenTricount(tricount));
        Register<Tricount>(App.Messages.MSG_CLOSE_TAB,
           member => DoCloseTab(member));
       
    }

   private void EditTricount(Tricount tricount, bool isEdit) {
        if (tricount != null) {
            Console.Write(tricount +"   is_edit  = "+isEdit );
           OpenTab(!isEdit ? "<New Tricount>" : tricount.Title, tricount.Title, () => new EditTricountView(tricount, isEdit));
        }
        
    }
    private void DoCloseTab(Tricount member) {
        tabControl.CloseByTag(string.IsNullOrEmpty(member.Title) ? "<New Member>" : member.Title);
    }
    private void OpenTricount(Tricount tricount) {
        if (tricount != null) {
            OpenTab(tricount.Title,tricount.Title+"  ", () => new OpenTricountView(tricount));
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