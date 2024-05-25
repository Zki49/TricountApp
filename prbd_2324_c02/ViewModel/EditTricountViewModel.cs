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
        public ICommand AddUserCommand { get; set; }
        public ICommand AddAllUserCommand { get; set; }
        public ICommand AddMySelfCommand { get; set; }
        public ICommand AddTricountCommand { get; set; }

        public User UserSelected { get; set; }
        public String Title {
            get => _title;
            set => SetProperty(ref _title, value, () => Validate());
        }
        public String Description { get; set; }
        public DateTime Date { get => _date; set => SetProperty(ref _date, value, () => Console.Write(value)); }
        public String DatetoText { get; set; }


        public EditTricountViewModel(Tricount curent, bool isEdit) {
            OnRefreshData();
            mode = isEdit;
            this.curent = curent;
            Date = curent.CreatedAt.Equals(new DateTime()) ? DateTime.Now : curent.CreatedAt;
            DatetoText = Date.ToString();
            makeCommand();

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
            users.RefreshFromModel(Context.Users.Where(user => user.UserId != CurrentUser.UserId));
            participants.Add(CurrentUser);

        }
        private void makeCommand() {
            AddUserCommand = new RelayCommand(AddUser ,()=>UserSelected!=null);
            AddAllUserCommand = new RelayCommand(AddAllUser , ()=>users.Count!=0);
            AddTricountCommand = new RelayCommand(AddTricount,()=> !HasErrors );

        }
        private void AddUser() {
            if (UserSelected != null) {
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
                }
                users.Clear();
                
            }
        }

        private void AddTricount() {
            List<User> list = new List<User>(participants);

            Tricount.AddTricount(Title, Description, Date, list, CurrentUser);
        }
    }
}
