using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.ViewModel
{
    class detailTricountViewModel : ViewModelBase<User, PridContext>
    {
        public string Title {  get; set; }
        public string description {  get; set; }
        public string CreatedAt {  get; set; }
        public string balance {  get; set; }
        public string UserFullname {  get; set; }
        public string LastOperation {  get; set; }
        public string MyExpense { get; set; }
        public string nbFriends { get; set; }
        public string nbOperation { get; set; }
        public string TotalExpenses { get; set; }
        public string Color { get; set; }
        public string ColorText { get; set; }
        public Tricount tricount { get; set; }


        public detailTricountViewModel() {

        }
        public detailTricountViewModel(  Tricount tricount) {
            makeCarte(tricount);
            this.tricount = tricount;
        }

        private void makeCarte(Tricount tricount) {
            Title = tricount.Title; 
            description = tricount.Description;
            CreatedAt = "on " + tricount.CreatedAt.ToString();
            UserFullname = "Created by: " + tricount.Creator.FullName;

            nbFriends = "With " + getNbFriends(tricount) + "friend";
            if(getNbFriends(tricount) > 1) {
                nbFriends+="s";
            } ;

            nbOperation = tricount.Operations.Count > 0 ? tricount.Operations.Count + "operations"
                 : "No operation";

            if (tricount.Operations.Count > 0) {
                LastOperation = "Last operation on " + getLastOperation(tricount);

                TotalExpenses = "Total Expenses: " + tricount.totalExpenses();
                MyExpense = "My Expenses: " + tricount.myExpense(CurrentUser);
                balance = "My Balance: " + tricount.balance(CurrentUser);
            }

            if (tricount.balance(CurrentUser) > 0) {
                Color = "#A0D858";
                ColorText = "Green";
            }
            if(tricount.balance(CurrentUser) < 0) {
                Color = "Red";
                ColorText = Color;
            }
            if(tricount.balance(CurrentUser) ==0) {
                Color = "Gray";
            }

        }

        private string getLastOperation(Tricount tricount) {
            var op = tricount.Operations;
            var res = "";
            for(int i = 0; i < op.Count - 1; ++i) {
                if (op[i].CreatAt.CompareTo(op[i+1].CreatAt) <=0) {
                    res = op[i + 1].CreatAt.ToString();
                }
            }
            return res;
        }
        private int getNbFriends(Tricount tricount) {
            var res = tricount.Subscriptions.Count;
            return res-1;
        }
    }
}
