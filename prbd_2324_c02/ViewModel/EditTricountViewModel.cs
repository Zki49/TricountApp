using Microsoft.IdentityModel.Tokens;
using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prbd_2324_c02.ViewModel
{
    public class EditTricountViewModel : ViewModelBase<User, PridContext>
    {
        private Tricount curent;
        private bool mode;
        private String _title;
        private String _description;
        private DateTime _date;
        public ObservableCollection<User> users { get; set; } = new();
        public ObservableCollection<User> participants { get; set; } = new();
        public ObservableCollection<DeleteViewModel> userDelete { get; set; } = new();
        public ICommand AddUserCommand { get; set; }
        public ICommand AddAllUserCommand { get; set; }
        public ICommand AddMySelfCommand { get; set; }
        public ICommand AddTricountCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private User _user;

        public User UserSelected {
            get;
            set;
        }
        public String Title {
            get => _title;
            set => SetProperty(ref _title, value, () => Validate());
        }
        public String Description { get; set; }
        public DateTime Date { get => _date; set => SetProperty(ref _date, value, () => Console.Write(value)); }
        public String DatetoText { get; set; }


        public EditTricountViewModel(Tricount curent, bool isEdit) {
            mode = isEdit;
            this.curent = curent;
           if (isEdit) {
                Title= curent.Title;
            }
            Date = curent.CreatedAt.Equals(new DateTime()) ? DateTime.Now : curent.CreatedAt;
            DatetoText = Date.ToString();
            Description= string.IsNullOrEmpty(curent.Description) ? " " : curent.Description;
            makeCommand();
            OnRefreshData();
            Register<User>(App.Messages.MSG_USER_DELETE, user => {
                Console.WriteLine("delete demande");
                DeleteAction(user);
            });
        }
        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Title.Length < 3)
                AddError(nameof(Title), "lengh must be 3 nchar");



            return !HasErrors;
        }
        protected override void OnRefreshData() {
            if (mode) {
                var sub = Context.Subscriptions.Where(S => S.TricountId == curent.Id);
                users.RefreshFromModel(Context.Users);
                foreach (var s in sub) {
                    participants.Add(s.User);
                    users.Remove(s.User);
                }
                

            }else {
                users.RefreshFromModel(Context.Users.Where(user => user.UserId != CurrentUser.UserId));
                participants.Add(CurrentUser);
            }

            foreach(var par in participants) {
                userDelete.Add(new DeleteViewModel(par));
            }

          

        }
        private void makeCommand() {
            AddUserCommand = new RelayCommand(AddUser ,()=>UserSelected!=null);
            AddAllUserCommand = new RelayCommand(AddAllUser , ()=>users.Count!=0);
            AddTricountCommand = new RelayCommand(AddTricount,()=> !HasErrors );
            CancelCommand = new RelayCommand(cancel);
            DeleteCommand = new RelayCommand(()=>Console.Write("texstf"));

        }
        private void cancel() {
           
            NotifyColleagues(App.Messages.MSG_CLOSE_TAB, curent);
            if(mode) {
                NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, curent);
            }
                
            
        }
        private void AddUser() {
              
            if (UserSelected != null) {
                //!!!!!!!!!!!!apres le remove le user selected devien null !!!!!!!!
                userDelete.Add(new DeleteViewModel(UserSelected));
                participants.Add(UserSelected);
                users.Remove(UserSelected);
               
              
                UserSelected = null;
            }
        }
        private void AddAllUser() {
            if (users.Count!=0) {
                List<User> list = new List<User>(users);
                foreach (var user in list) {
                    participants.Add(user);
                    userDelete.Add(new DeleteViewModel(user));
                }
                users.Clear();
                
            }
        }

        private void AddTricount() {
            List<User> list = new List<User>(participants);

            if (!mode) {
                NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED, Tricount.AddTricount(Title, Description, Date, list, CurrentUser));
                NotifyColleagues(App.Messages.MSG_CLOSE_TAB, new Tricount());
            } else {
                NotifyColleagues(App.Messages.MSG_CLOSE_TAB, curent);
                curent.Title=Title; 
                curent.Description=Description;
                //continuer
                RaisePropertyChanged();
                Context.SaveChanges();  
                NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, curent);
                NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED,curent);

            }
          

            

        }

        private void DeleteAction(User user){
           
            users.Add(user);
            Console.Write("delte");
            var list = new List<DeleteViewModel>(userDelete);
            foreach (var item in list) {
                if(item.user.Equals(user)) {
                    userDelete.Remove(item);

                }
            }
            //participants.Remove(user);
        }

    }
}
