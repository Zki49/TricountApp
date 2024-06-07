using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using prbd_2324_c02.Model;
using PRBD_Framework;

namespace prbd_2324_c02.ViewModel
{
    class DialogViewModel : DialogViewModelBase<User,PridContext>
    {   
        public  string Message {  get; set; }
        public ICommand YesCommand {  get; set; }
        public ICommand NoCommand { get; set; } 
        public DialogViewModel(string objectsuprim) {
            Message = "You 're about delete this " + objectsuprim + ".\n Do you comfirm ?";
            makeCommand();
        }

        private void makeCommand() {
            YesCommand = new RelayCommand(() => {
                 DialogResult = true;
            });
            NoCommand = new RelayCommand(() => {
               DialogResult= false;
            });    
        }
    }
}
