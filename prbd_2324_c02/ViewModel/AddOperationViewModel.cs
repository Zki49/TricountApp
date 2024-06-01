﻿using System;
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
        public ICommand deletCommand { get; set; }
        public ICommand AddCommand { get; set; }


        public ObservableCollection<Repartitions> Repartitions { get; set; } = new();

        public AddOperationViewModel(Tricount tricount, Operations curent, bool isedit) {
            OnRefreshData();
            Tricount = tricount;
            Curent = curent;
            Title = curent.title;
            Amout = curent.Amount;
            Date = curent.CreatAt;
            BoutonaddorSave = isedit ? "Save" : "Add";
            this.isedit = isedit;
            MakeCommand();
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
            if (isedit) {
                //edition de l'operation 
                Curent.title = Title;
                Curent.Amount = Amout;
                //etc ...
                //Curent.save();
            } else {
                //creation de l'operation 
                Curent.Tricount = Tricount;
                Curent.title = Title;
                Curent.Amount = Amout;
                //etc ...
                //Curent.save();
            }
           // NotifyColleagues(App.Messages.MSG_CLOSE_TAB);
        }

        protected override void OnRefreshData() {
            if (!isedit) {
                var Participants = Tricount.Subscriptions;
                foreach(var participant in Participants) {
                    Repartitions rep = new Repartitions();
                    rep.weight = 1;
                    rep.userId = participant.UserId;
                    rep.operations = Curent;
                    Repartitions.Add(rep);
                }
            } else {
                Repartitions.RefreshFromModel(Context.Repartitions.Where(r => r.operationsID == Curent.OperationsId));
            }
            
        }
    }
}
