using Microsoft.EntityFrameworkCore;
using prbd_2324_c02.Model;
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
        public ObservableCollectionFast<double> balance { get; set; } = new();


        public Tricount Tricount {
            get => _tricount;
            private init => SetProperty(ref _tricount, value);

        }

        public ICommand Delete { get; set; }
        public ICommand EditCommand { get; set; }



        public OpenTricountViewModel(Tricount tricount) {
            Console.WriteLine("$$$" + tricount);
            Tricount = tricount;
            OnRefreshData();
            Delete = new RelayCommand(deleteTricount);
            EditCommand = new RelayCommand(EditTricount);

        }
        private void EditTricount() {
            Console.WriteLine("cc" + Tricount);
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
                var balanceForUser = Tricount.balance(user);
                balance.Add(balanceForUser);
            }
        }
       
    }
}
