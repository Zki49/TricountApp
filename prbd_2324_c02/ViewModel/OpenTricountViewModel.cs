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
        public ICommand AddOperationCommand { get; set; }

        public Tricount Tricount {  
            get => _tricount;
            private init => SetProperty(ref _tricount, value);

        }
      

        public OpenTricountViewModel(Tricount tricount) {
            Console.WriteLine("$$$" + tricount);
            _tricount = tricount;

            AddOperationCommand = new RelayCommand(AddOperationAction);

        }

        private void AddOperationAction() {
            NotifyColleagues(App.Messages.MSG_ADD_OPERATION, Tricount);
        }
    }
}
