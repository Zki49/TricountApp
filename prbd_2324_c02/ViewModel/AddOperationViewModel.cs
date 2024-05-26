using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prbd_2324_c02.Model;
using PRBD_Framework;

namespace prbd_2324_c02.ViewModel
{
    public  class AddOperationViewModel : ViewModelBase<User, PridContext>
    {
        public Tricount Tricount { get; set; }
        public Operations Curent { get; set; }
        public String Title {  get; set; }
        public double Amout { get; set; }
        public DateTime Date { get; set; }
        public string BoutonaddorSave {  get; set; }
        public bool isedit { get; set; }
        

        public AddOperationViewModel(Tricount tricount, Operations curent,bool isedit) {
            Tricount = tricount;
            Curent = curent;
            Title = curent.title;
            Amout = curent.Amount;
            Date = curent.CreatAt;
            BoutonaddorSave = isedit ? "Save" : "Add";
            this.isedit = isedit;
        }

        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Amout<0)
                AddError(nameof(Amout), "minimum 1 cent ");


            return !HasErrors;
        }
    }
}
