using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.ViewModel
{
    class balanceViewModel
    {
        public String username{ get; set; }
        public double balance{ get; set; }
        public double with{ get; set; }
        public String color{ get; set; }

        public balanceViewModel(String UserName , double balance , String color) { 
            this.username = UserName;
            this.balance = Math.Round(balance,2);
            this.color = color;
            if( balance < 0) {
                this.with = -balance;
            } else {
                this.with = balance;

            }
        }

    }
}
