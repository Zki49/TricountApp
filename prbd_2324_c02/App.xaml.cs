using prbd_2324_c02.Model;
using prbd_2324_c02.ViewModel;
using System.Windows;
using System.Globalization;
using PRBD_Framework;
using prbd_2324_c02.View;

namespace prbd_2324_c02;

public partial class App : ApplicationBase<User, PridContext> {
    public enum Messages {
        MSG_LOGIN,
        MSG_LOGOUT,
        MSG_SIGNUP,
        MSG_ADD,
        MSG_TRICOUNT_CHANGED,
        MSG_OPEN_TRICOUNT,

        MSG_ADD_OPERATION,
          
        MSG_CLOSE_TAB,
        MSG_EDIT,
        MSG_RESET,
        MSG_ADD_OPE,
        MSG_CLOSE_WINDOWS,
        MSG_OPE_CHANGED,

        MSG_EDITOPERATION,
        MSG_REP_CHANGE,
        MSG_CANCEL_OPE,
        MSG_MAJ,
        MSG_USER_DELETE

    }
    public App() {
        var ci = new CultureInfo("fr-BE") {
            DateTimeFormat = {
                ShortDatePattern = "dd/MM/yyyy",
                DateSeparator = "/"
            }
        };
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
        
    }

    protected override void OnStartup(StartupEventArgs e) {
        PrepareDatabase();
        TestQueries();

        //NavigateTo<LoginViewModel, User, PridContext>();

        Register<User>(this, Messages.MSG_LOGIN, member => {
            Login(member);
            NavigateTo<MainViewModel, User, PridContext>();
        });
        Register(this, Messages.MSG_LOGOUT, () => {
            Logout();
            NavigateTo<LoginViewModel, User, PridContext>();
        });
        Register(this, Messages.MSG_SIGNUP, () => {
            NavigateTo<SignupViewModel, User, PridContext>();
        });

    
        Register(this, Messages.MSG_RESET, () => {
            Reset();
        });

    }

    private void Reset() {
        ClearContext();
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }


    private static void PrepareDatabase() {
        // Clear database and seed data
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();

       // Context.seedData();

        // Cold start
        Console.Write("Cold starting database... ");
        Context.Users.Find(1);
        var tricount1 = Context.Tricounts.Find(1);
        var user = Context.Users.Find(4);
        Console.WriteLine(user.FullName);
        Console.WriteLine(tricount1.Title);

        Console.WriteLine(tricount1.balance(user));

    }

    protected override void OnRefreshData() {
        // TODO
    }

    private static void TestQueries() {
        // Un endroit pour tester vos requêtes LINQ
       
    }
}