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
        private String _title;
        private String _description;
        private DateTime _date;

        public String Title {  
            get=> _title;
            set => SetProperty(ref _title, value, () => Validate() ) ; }
        public String Description { get; set; }
        public DateTime Date { get=>_date; set => SetProperty(ref _date, value, () => Console.Write(value)); }
        public String DatetoText {  get; set; }


        public EditTricountViewModel(Tricount curent, bool isEdit) {
               mode = isEdit;
               this.curent = curent;
            Date = curent.CreatedAt.Equals(new DateTime()) ? DateTime.Now : curent.CreatedAt; 
            DatetoText=Date.ToString();

        }
        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Title.Length<3)
                AddError(nameof(Title), "lengh must be 3 nchar");
            


            return !HasErrors;
        }


    }
}
