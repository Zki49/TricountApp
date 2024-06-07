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
        /// bricolage bricolage 
        private Operations _operations;
        ///on doit faire des truc bizard mais on a pas le choix sinon le framwork fait de chose bizard sans quon lui demande 

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
        public ICommand SaveTemplate { get; set; }


        public ObservableCollection<Repartitions> Repartitions { get; set; } = new();
        public ObservableCollection<Template> templates { get; set; } = new();
        public ObservableCollection<User> users { get; set; } = new();
        private Repartitions _partitions;
        public ObservableCollection<Repartitions> temp { get; set; } = new();
        private bool templateAply= false;

        public AddOperationViewModel(Tricount tricount, Operations curent, bool isedit) {
           
            Tricount = tricount;
            Curent = curent;
            _operations = new Operations(curent);
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
            Register<Repartitions>(App.Messages.MSG_REP_CHANGE,rep => { 
                _partitions=rep;
                OnRefreshData();
                
            });  

           

           
        }

        public override bool Validate() {
            ClearErrors();


            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");

            else if (Repartitions.Sum(r => r.weight) == 0) {
                AddError(nameof(Reparttionsviewmodel), "You must check at least one participant !");
            }
            else if (Amout <= 0)
                AddError(nameof(Amout), "minimum 1 cent ");


            return !HasErrors;
        }

        private void MakeCommand() {
            deletCommand = new RelayCommand(deleteOperation);
            AddCommand = new RelayCommand(Addoperation, () => !HasErrors);
            Apply = new RelayCommand(ApplyTemplate);
            SaveTemplate = new RelayCommand(SaveTemplateAction, () => !HasErrors);

        }
        private void ApplyTemplate() {
            Reparttionsviewmodel.Clear();
            Curent.repartitions.Clear();
            Repartitions.Clear();
            templateAply=true;
            foreach (var item in TemplateSelected.TemplateItems) {
              
                Repartitions rep = new Repartitions();
                rep.weight = item.weight;
                rep.user = item.User;
                rep.operations = Curent;
                rep.operations.Amount=Amout;

                Repartitions.Add(rep);
                temp.Add(rep);
              //  Curent.repartitions.Add(rep);
                //users.Add(participant.User);

                //Curent.Amount = Amout;
                Console.WriteLine(Curent.repartitions.Count());
                //Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep));
                //Context.SaveChanges();

                

            }
            foreach(var user in users) {
                bool isIn = false;
                foreach (var rep in Repartitions) {
                    if(user.Equals(rep.user)) { 
                        isIn = true;
                    }
                }
                if(!isIn) {
                    Repartitions rep = new Repartitions();
                    rep.weight = 0;
                    rep.user = user;
                    rep.operations = Curent;
                    rep.operations.Amount = Amout;

                    Repartitions.Add(rep);
                    temp.Add(rep);
                }
            }
            foreach(var rep in Repartitions) {
                rep.operations.Amount = Amout;
                var tot = Repartitions.Sum(r => r.weight);
                Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep , Amout ,tot));
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
            bool prem = temp.Count()==0;
            templates.Clear();
            Reparttionsviewmodel.Clear();
            var Temp = Tricount.Templates;

            foreach (var template in Temp) {
                templates.Add(template);
            }
            if (!templateAply) {
                if (!isedit) {
                    var Participants = Tricount.Subscriptions;
                    foreach (var participant in Participants) {
                        Repartitions rep = new Repartitions();
                        rep.weight = 1;
                        rep.user = participant.User;
                        rep.operations = Curent;
                        rep.operations.Amount = Amout;
                        Repartitions.Add(rep);
                        //Curent.repartitions.Add(rep);
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
                            if (prem) {

                                Repartitions rep = new Repartitions();
                                rep.weight = 0;
                                rep.user = user;
                                rep.operations = Curent;
                                rep.operations.Amount = Amout;
                                Repartitions.Add(rep);
                                temp.Add(rep);
                            }



                        }
                    }
                }
                if (!prem) {
                    foreach (var rep in temp) {
                        Repartitions.Add(rep);

                    }
                }
                
            }
            
            
           

            Reparttionsviewmodel.Clear();
            var tot= Repartitions.Sum(r => r.weight);   
            foreach (var rep in Repartitions) {
                Reparttionsviewmodel.Add(new NumericUpDownViewModel(rep , Amout , tot));
            }
            //Context.SaveChanges();
            // RaisePropertyChanged();
            Validate();

        }
        private void SaveTemplateAction() {

            var temp = new Template();
            temp.Tricount = Tricount;
            var ti = new List<Template_items>();
            foreach(var rep in Repartitions) {
                var templateI = new Template_items();
                templateI.weight = rep.weight;
                templateI.User = rep.user;
                templateI.template = temp;

                ti.Add(templateI);

            }

        temp.TemplateItems = ti;
            NotifyColleagues(App.Messages.MSG_ADD_TEMPLATE, temp);

        }

        public void Reload() {
            //Curent.delete();
            //_operations.save();
            //Tricount.Operations.Add(_operations);
          //  Context.SaveChanges();

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
