using prbd_2324_c02.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            DeleteCommand = new RelayCommand(() => {  NotifyColleagues(App.Messages.MSG_USER_DELETE, this.user); }) ;
        }

       /* public override bool Equals(Object other) {
            
            if (other.GetType() is DeleteViewModel) {
                var Other = (DeleteViewModel)other;
                if (user.Equals(Other.user))
                    return true;
            }else { return false; }
            return false;
           
        }*/
    }
}
