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


        public OpenTricountViewModel(Tricount tricount) {
            
            _tricount = tricount;
            OnRefreshData();

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
