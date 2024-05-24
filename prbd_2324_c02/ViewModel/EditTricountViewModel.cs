using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.ViewModel
{
   public  class EditTricountViewModel: ViewModelBase<User, PridContext>
    {
        private Tricount curent;
        private bool mode;
        public EditTricountViewModel(Tricount curent, bool isEdit) {
               mode = isEdit;
               this.curent = curent;
        }
     

    }
}
