using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.IdentityModel.Tokens;
using prbd_2324_c02.Model;
using PRBD_Framework;

namespace prbd_2324_c02.ViewModel
{
    public class AddOperationViewModel : ViewModelBase<User, PridContext>
    {
        public Tricount Tricount { get; set; }
        public Operations Curent { get; set; }
        private String _title;


        public User UserSelected { get; set; }
        public String Title {
            get => _title;
            set => SetProperty(ref _title, value, () => Validate());
        }

        public ObservableCollection<NumericUpDownViewModel> Reparttionsviewmodel { get; set; } = new ObservableCollection<NumericUpDownViewModel>();
        private double _amout;
        public double Amout {
            get => _amout;
            set => SetProperty(ref _amout, value, () => Validate());
                }
        public DateTime Date { get; set; }
        public string BoutonaddorSave { get; set; }
        public bool isedit { get; set; }
        public string VisibleDelete {  get; set; }
        public Template TemplateSelected {  get; set; }


        public ICommand deletCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand Apply { get; set; }


        public ObservableCollection<Repartitions> Repartitions { get; set; } = new();
        public ObservableCollection<Template> templates { get; set; } = new();
        public ObservableCollection<User> users { get; set; } = new();

        public AddOperationViewModel(Tricount tricount, Operations curent, bool isedit) {
           
            Tricount = tricount;
            Curent = curent;
            Title = curent.title;
            Amout = curent.Amount;
            Date = curent.CreatAt;
            if (!isedit) {
                this.Curent.Tricount = tricount;
            }
            BoutonaddorSave = isedit ? "Save" : "Add";
            VisibleDelete = isedit ? "" : "Hidden";
            this.isedit = isedit;
            OnRefreshData();
            MakeCommand();

           

           
        }

        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Amout <= 0)
                AddError(nameof(Amout), "minimum 1 cent ");


            return !HasErrors;
        }

        private void MakeCommand() {
            deletCommand = new RelayCommand(deleteOperation);
            AddCommand = new RelayCommand(Addoperation, () => !HasErrors);
            Apply = new RelayCommand(ApplyTemplate);

        }
        private void ApplyTemplate() {
            Reparttionsviewmodel.Clear();
            Curent.repartitions.Clear();
           
            //Curent.repartitions = new();
            Context.SaveChanges();
            RaisePropertyChanged();
            foreach (var item in TemplateSelected.TemplateItems) {
                /*var rep = new Repartitions(item);
                Curent.repartitions.Add(rep);
                rep.operations = Curent;*/


                //rep.operationsID = Curent.OperationsId;
                //rep.userId = Curent.userId;
                Repartitions rep = new Repartitions();
                rep.weight = item.weight;
                rep.user = item.User;
                //rep.operations = Curent;
                Repartitions.Add(rep);
                Curent.repartitions.Add(rep);
                //users.Add(participant.User);

                //Curent.Amount = Amout;
                Console.WriteLine(Curent.repartitions.Count());
                //Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep));
                Context.SaveChanges();
                

            }
            foreach(var rep in Curent.repartitions) {
                rep.operations.Amount = Amout;
                Console.WriteLine(rep.operations.Amount);
                Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep , Amout , Curent.repartitions.Count()));
            }


        }
        private void deleteOperation() {
            if (isedit) {
                if (App.ShowDialog<DialogViewModel, User, PridContext>("Operation ").Equals(true)) {
                    Curent.delete();
                    NotifyColleagues(App.Messages.MSG_CLOSE_WINDOWS);
                    NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED, Tricount );
                    NotifyColleagues(App.Messages.MSG_CLOSE_TAB, Tricount);
                    NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, Tricount);
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
                Curent.user = UserSelected;
                Curent.save();
                Context.SaveChanges();
                RaisePropertyChanged();

            }
            NotifyColleagues(App.Messages.MSG_TRICOUNT_CHANGED, Tricount);
            NotifyColleagues(App.Messages.MSG_CLOSE_TAB, Tricount);
            NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, Tricount);
            RaisePropertyChanged();
        }

        protected override void OnRefreshData() {
            var Temp = Tricount.Templates;

            foreach (var template in Temp) {
                templates.Add(template);
            }
            if (!isedit) {
                var Participants = Tricount.Subscriptions;
                foreach (var participant in Participants) {
                    Repartitions rep = new Repartitions();
                    rep.weight = 1;
                    rep.user = participant.User;
                    rep.operations = Curent;
                    Repartitions.Add(rep);
                    Curent.repartitions.Add(rep);
                    users.Add(participant.User);
                }
            } else {
                Repartitions.RefreshFromModel(Context.Repartitions.Where(r => r.operationsID == Curent.OperationsId));
                var Participants = Tricount.Subscriptions;
                foreach (var participant in Participants) {

                    users.Add(participant.User);
                }
                foreach (var user in users) {
                    bool isIn = false;
                    foreach (var rep in Repartitions) {
                        if (user.Equals(rep.user)) {
                            isIn = true;
                        }
                    }
                    if (!isIn) {
                        Repartitions rep = new Repartitions();
                        rep.weight = 0;
                        rep.user = user;
                        rep.operations = Curent;
                        Repartitions.Add(rep);
                        Curent.repartitions.Add(rep);
                    }
                }
            }
            foreach (var rep in Repartitions) {
                Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep , Amout , Curent.repartitions.Count()));
            }
            Context.SaveChanges();
            RaisePropertyChanged();
            
        }

        public void Reload() {
            /*App.ClearContext();
            Repartitions.Clear();
            templates.Clear();
            users.Clear();
            Reparttionsviewmodel.Clear();
            //var reparCur = Context.Operations.Find(Curent.OperationsId).repartitions;
            /*foreach (var user in users) {
                bool isIn = false;
                foreach (var rep in reparCur) {
                    if (user.Equals(rep.user)) {
                        isIn = true;
                    }
                }
                if (!isIn) {
                    Repartitions rep = new Repartitions();
                    rep.weight = 0;
                    rep.userId = user.UserId;
                    rep.operationsID = Curent.OperationsId;
                    Repartitions.Remove(rep);
                    Curent.repartitions.Remove(rep);
                }
            }*/
           
            /*Curent.Reload();    
            RaisePropertyChanged();*/
            
        }
    }
}
