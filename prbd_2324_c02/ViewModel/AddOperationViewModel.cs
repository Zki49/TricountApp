using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using prbd_2324_c02.Model;
using PRBD_Framework;

namespace prbd_2324_c02.ViewModel
{
    public class AddOperationViewModel : ViewModelBase<User, PridContext>
    {
        public Tricount Tricount { get; set; }
        public Operations Curent { get; set; }
        public String Title { get; set; }
        public double Amout { get; set; }
        public DateTime Date { get; set; }
        public string BoutonaddorSave { get; set; }
        public bool isedit { get; set; }
        public string VisibleDelete {  get; set; }
        public ICommand deletCommand { get; set; }
        public ICommand AddCommand { get; set; }


        public ObservableCollection<Repartitions> Repartitions { get; set; } = new();

        public AddOperationViewModel(Tricount tricount, Operations curent, bool isedit) {
           
            Tricount = tricount;
            Curent = curent;
            Title = curent.title;
            Amout = curent.Amount;
            Date = curent.CreatAt;
            this.Curent.Tricount = tricount;
            BoutonaddorSave = isedit ? "Save" : "Add";
            VisibleDelete = isedit ? "" : "Hidden";
            this.isedit = isedit;
            MakeCommand();
            OnRefreshData();
        }

        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Amout < 0)
                AddError(nameof(Amout), "minimum 1 cent ");


            return !HasErrors;
        }

        private void MakeCommand() {
            deletCommand = new RelayCommand(deleteOperation);
            AddCommand = new RelayCommand(Addoperation);

        }
        private void deleteOperation() {
            if (isedit) {
                if (App.ShowDialog<DialogViewModel, User, PridContext>("Operation ").Equals(true)) {
                    Curent.delete();
                }
            }
           



        }
        private void Addoperation() {
            NotifyColleagues(App.Messages.MSG_CLOSE_WINDOWS);
            var rep = new List<Repartitions>(Repartitions);
            if (isedit) {
                
                //edition de l'operation 
                Curent.title = Title;
                Curent.Amount = Amout;
                Curent.repartitions = rep;
                RaisePropertyChanged();
                Context.SaveChanges();
            } else {
                //creation de l'operation 
                Curent.Tricount = Tricount;
                Curent.title = Title;
                Curent.Amount = Amout;
                Curent.repartitions = rep;
                Curent.CreatAt = Date;
                Curent.save();
                
            }

            
        }

        protected override void OnRefreshData() {
            if (!isedit) {
                var Participants = Tricount.Subscriptions;
                foreach(var participant in Participants) {
                    Repartitions rep = new Repartitions();
                    rep.weight = 1;
                    rep.user = participant.User;
                    rep.operations = Curent;
                    Repartitions.Add(rep);
                }
            } else {
                Repartitions.RefreshFromModel(Context.Repartitions.Where(r => r.operationsID == Curent.OperationsId));
            }
            
        }
    }
}
