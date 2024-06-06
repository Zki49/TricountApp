using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.ViewModel
{
    class balanceViewModel : ViewModelBase<User, PridContext>
    {
        public String username{ get; set; }
        public string balance{ get; set; }
        public double with{ get; set; }
        public String color{ get; set; }
        public String colorRight { get; set; }


        public balanceViewModel(String UserName , double balance , String color) {
            if (CurrentUser.FullName.Equals(UserName)) {
                UserName += "   (me)";
            }
            this.username = UserName;
            this.balance = Math.Round(balance,2).ToString();
            this.color = color;
            if( balance < 0) {
                this.username= Math.Round(balance, 2).ToString();
                this.balance = UserName;
                colorRight=this.color;
                this.color = "Transparent";

                this.with = -balance;
            } else {
                this.with = balance;

            }
            if( balance ==0) {
              this.balance = "0";
                this.color = "Transparent";
            }
           
        }

    }
}
