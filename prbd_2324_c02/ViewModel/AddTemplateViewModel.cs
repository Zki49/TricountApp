﻿using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace prbd_2324_c02.ViewModel
{
    internal class AddTemplateViewModel : ViewModelBase<User, PridContext>
    {
        public Tricount Tricount { get; set; }
        public string Title { get; set; }
        public Template Curent { get; set; }
        public string BoutonaddorSave { get; set; }
        public bool isedit { get; set; }
        public ICommand AddCommand { get; set; }
        public ObservableCollectionFast<numericUpDownForTemplateViewModel> tempateItemviewmodel { get; set; } = new();
        public AddTemplateViewModel(Tricount tricount, Template curent, bool isedit) {
            Tricount = tricount;
            Curent = curent;
            Title = curent.Title;
            BoutonaddorSave = isedit ? "Save" : "Add";
            this.isedit = isedit;
            MakeCommand();
            MakeViewModel();
        }
        private void MakeViewModel() {
            foreach(var tempIt in Curent.TemplateItems) {
                tempateItemviewmodel.Add(new numericUpDownForTemplateViewModel(tempIt));
              
            }
            Console.WriteLine(tempateItemviewmodel.Count());


        }

        public override bool Validate() {
            ClearErrors();

            if (string.IsNullOrEmpty(Title))
                AddError(nameof(Title), "required");
            else if (Title.Length < 3) {
                AddError(nameof(Title), "Minimum 3 char");
            }
            
            return !HasErrors;
        }

        private void MakeCommand() {
            
            AddCommand = new RelayCommand(Addtemplate);

        }

        private void Addtemplate() {
            if (isedit) {

                //edition de la template
                Curent.Title = Title;
                Context.SaveChanges();
                RaisePropertyChanged();
                NotifyColleagues(App.Messages.MSG_TEMPLATE_CHANGE);
            } else {
                Curent.Title = Title;
                Curent.AddTemplate();
                RaisePropertyChanged();
                NotifyColleagues(App.Messages.MSG_ADD_NEW_TEMPLATE, Curent);
            }
            NotifyColleagues(App.Messages.MSG_CLOSE_WINDOWS);
            


        }


    }
}
