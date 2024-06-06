using prbd_2324_c02.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    public class DeleteViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
    {
        public String FullName { get; set; }
        public User user { get; set; }
        public ICommand DeleteCommand { get; set; }

        public DeleteViewModel(User user) {
            this.user = user;
            this.FullName = user.FullName;
            DeleteCommand = new RelayCommand(() => { Console.WriteLine("del")/* NotifyColleagues(App.Messages.MSG_USER_DELETE, this.user)*/; }) ;
        }
    }
}
