using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    class OpenTricountViewModel : ViewModelBase<User, PridContext>
    { 
        private readonly Tricount _tricount;

        public Tricount Tricount {  
            get => _tricount;
            private init => SetProperty(ref _tricount, value);

        }
        public ICommand Delete { get; set; }


        public OpenTricountViewModel(Tricount tricount) {
            Console.WriteLine("$$$" + tricount);
            Tricount = tricount;
            Delete = new RelayCommand(deleteTricount);

        }
        private void deleteTricount() {
            
                if (App.ShowDialog<DialogViewModel, User, PridContext>(" Tricount ").Equals(true)) {
                   
                    Tricount.delete();
                NotifyColleagues(App.Messages.MSG_CLOSE_TAB, Tricount);
                NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED, Tricount);

                
            }

        }
    }
}
