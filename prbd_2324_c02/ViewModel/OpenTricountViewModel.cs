using Azure;
using Microsoft.EntityFrameworkCore;
using prbd_2324_c02.Model;
using prbd_2324_c02.View;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    class OpenTricountViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
    {
        private readonly Tricount _tricount;
        public ObservableCollectionFast<User> participant { get; set; } = new();
        public ObservableCollectionFast<balanceViewModel> balance { get; set; } = new();


        public Tricount Tricount {
            get => _tricount;
            private init => SetProperty(ref _tricount, value);

        }
        public Operations CurrentOpe {
            get;
            set;
        }

        public ICommand Delete { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddOperationCommand {  get; set; }
        public ICommand openOperation {  get; set; }



        public OpenTricountViewModel(Tricount tricount) {
            Tricount = tricount;
            OnRefreshData();
            Delete = new RelayCommand(deleteTricount);
            EditCommand = new RelayCommand(EditTricount);
            AddOperationCommand = new RelayCommand(addOperation);
            openOperation = new RelayCommand(openOperationCommand);
            Register(App.Messages.MSG_OPE_CHANGED,()=>OnRefreshData());
            //  Register<Operations>(App.Messages.MSG_EDITOPERATION, operation => {
            //    new AddOperationView(tricount, true, operation).Show();
            //});

            


        }
        private void addOperation() {
            
            NotifyColleagues(App.Messages.MSG_ADD_OPE,Tricount);
        }
        private void EditTricount() {
            NotifyColleagues(App.Messages.MSG_CLOSE_TAB, Tricount);
            NotifyColleagues(App.Messages.MSG_EDIT, Tricount);
        }
        private void deleteTricount() {
            
                if (App.ShowDialog<DialogViewModel, User, PridContext>(" Tricount ").Equals(true)) {
                    Tricount.delete();
                    NotifyColleagues(App.Messages.MSG_CLOSE_TAB, Tricount);
                    NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED, Tricount);
                }

        }

        protected override void OnRefreshData() {

            participant.RefreshFromModel(Context.Users.Where(u => u.Role.Equals(false) && u.Subscriptions.Any(s => s.TricountId == Tricount.Id) ));
            foreach (User user in participant) {
                double balanceForUser = Tricount.balance(user);
                var color = "";
                if (balanceForUser > 0) {
                    color = "green";
                }if (balanceForUser < 0) {
                    color = "red";
                }

                balance.Add(new balanceViewModel(user.FullName,balanceForUser,color));

            }
            
        }

        private void openOperationCommand() {
           Console.WriteLine(CurrentOpe);
            new AddOperationView(Tricount, true, CurrentOpe).Show();
            // NotifyColleagues(App.Messages.MSG_EDITOPERATION, CurrentOpe);
        }

    }
}
