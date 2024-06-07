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
        private String GrandTitle;
        private String _description;
        private DateTime _date;
        public ObservableCollection<User> users { get; set; } = new();
        public ObservableCollection<User> participants { get; set; } = new();
        public ObservableCollection<DeleteViewModel> userDelete { get; set; } = new();
        public ObservableCollection<templateViewModel> templates { get; set; } = new();
        public ICommand AddUserCommand { get; set; }
        public ICommand AddAllUserCommand { get; set; }
        public ICommand AddMySelfCommand { get; set; }
        public ICommand AddTricountCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Newtemplate {  get; set; }
        public ICommand AddMySelf {  get; set; }
        
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
        public String Creator { get; set; }


        public EditTricountViewModel(Tricount curent, bool isEdit) {
            mode = isEdit;
            this.curent = curent;
            Creator = curent.Creator.FullName;
            if (isEdit) {
                Title= curent.Title;
                GrandTitle = curent.Title;
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
            Register<Template>(App.Messages.MSG_TEMPLATE_DELETE, template => {
                Console.WriteLine("delete demande");
                DeleteTemplate(template);
            });
            Register(App.Messages.MSG_TEMPLATE_CHANGE, () => OnRefreshData());
            Register<Template>(App.Messages.MSG_ADD_NEW_TEMPLATE, template => templates.Add(new templateViewModel(template)) ); 
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
            userDelete.Clear();
            templates.Clear();
            users.Clear();
            participants.Clear();
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
            foreach(var temp in curent.Templates) {
                templates.Add(new templateViewModel(temp));
            }

          

        }
        private void makeCommand() {
            AddUserCommand = new RelayCommand(AddUser ,()=>UserSelected!=null);
            AddAllUserCommand = new RelayCommand(AddAllUser , ()=>users.Count!=0);
            AddTricountCommand = new RelayCommand(AddTricount,()=> !HasErrors );
            CancelCommand = new RelayCommand(cancel);
            Newtemplate = new RelayCommand(NewTemplate ,() => mode);
            AddMySelf = new RelayCommand(AddMyself);

        }

        private void AddMyself() {
            if (users.Contains(CurrentUser)) {
                
                userDelete.Add(new DeleteViewModel(CurrentUser));
                users.Remove(CurrentUser);

            }
        }

        private void NewTemplate() {

            if (mode) {
                Template template = new Template();
                template.Tricount = curent;
                foreach (var subscription in curent.Subscriptions) {
                    var templateItem = new Template_items();
                    templateItem.weight = 1;
                    templateItem.template = template;
                    templateItem.User = subscription.User;
                    template.TemplateItems.Add(templateItem);
                    
                }
                
                Console.WriteLine(template.Title);
                NotifyColleagues(App.Messages.MSG_ADD_TEMPLATE, template);
                
            }
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
        }

        private void DeleteTemplate(Template temp) {

            templates.Remove(new templateViewModel(temp));
            temp.RemoveTemplate();
            OnRefreshData();
            RaisePropertyChanged(nameof(templates));
        }

    }
}
