using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    partial class TricountViewModel : ViewModelBase<User,PridContext>
    {
        private Tricount tricount;

        public Tricount currentTricount { get => tricount; set => SetProperty(ref tricount, value,() => Console.WriteLine(value) ); }


        public string Title => "prbd_2324_c02";
        public string Color => "LightGray";
        private string _inputText;
        public string InputText {
            get => _inputText;
            set => SetProperty(ref _inputText, value, () => OnRefreshData());
        }

        public ICommand LogoutCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand openTricount { get; set; }


        public ObservableCollection<Tricount> tricounts { get; set; } = new();
        public ObservableCollection<detailTricountViewModel> cartes { get; set; } = new();
        public TricountViewModel() {
            OnRefreshData();
            LogoutCommand = new RelayCommand(logout);
            ClearCommand = new RelayCommand(ClearTextBox);
            AddCommand = new RelayCommand(Add);
            openTricount = new RelayCommand<detailTricountViewModel>(vm=> { NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, vm.tricount); });

            Register<Tricount>(App.Messages.MSG_TRICOUNT_CHANGED, tricount => OnRefreshData());

           // makeCarte();

        }
        protected override void OnRefreshData() {
            tricounts.Clear();
            if (!CurrentUser.Role) {
                if (!string.IsNullOrEmpty(InputText)) {
                    Console.WriteLine("rentre filtre !!!!");
                    tricounts.RefreshFromModel(Context.Tricounts.Where(t => t.Creator.UserId == CurrentUser.UserId && (EF.Functions.Like(t.Title, InputText + "%") ||
                                                                                                                      EF.Functions.Like(t.Description, InputText + "%") ||
                                                                                                                      EF.Functions.Like(t.Creator.FullName, InputText + "%"))).OrderByDescending(tricounts => tricounts.Operations.OrderByDescending(o => o.CreatAt)));
                    cartes.Clear();
                    makeCarte();
                } else {
                    tricounts.RefreshFromModel(Context.Tricounts.Where(t => t.Creator.UserId == CurrentUser.UserId || t.Subscriptions.Any(s => s.UserId == CurrentUser.UserId)));
                    cartes.Clear();
                    makeCarte();
                }
            } else {
                if (!string.IsNullOrEmpty(InputText)) {
                    tricounts.RefreshFromModel(Context.Tricounts.Where(t => (EF.Functions.Like(t.Title, InputText + "%") ||
                                                                             EF.Functions.Like(t.Description, InputText + "%") ||
                                                                             EF.Functions.Like(t.Creator.FullName, InputText + "%")
                                                                             
                        )).OrderByDescending(tricounts=>tricounts.Operations.OrderByDescending(o=>o.CreatAt)));
                    cartes.Clear();
                    makeCarte();

                } else {
                    tricounts.RefreshFromModel(Context.Tricounts);
                    cartes.Clear();
                    makeCarte();
                }
            }

        }
        private void logout() {
            NotifyColleagues(App.Messages.MSG_LOGOUT);
        }
        private void ClearTextBox() {
            InputText = string.Empty;
        }
        private void Add() {
            Tricount tricount = new Tricount();
            NotifyColleagues(App.Messages.MSG_ADD, tricount);

        }
        private void opentricount() {
            
            NotifyColleagues(App.Messages.MSG_OPEN_TRICOUNT, currentTricount);

        }

        private void makeCarte() {
            foreach(var tric in tricounts) {
                cartes.Add(new detailTricountViewModel(tric));
            }
        }
}
}
