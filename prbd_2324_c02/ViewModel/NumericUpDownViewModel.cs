using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    public class NumericUpDownViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
    {

        public Repartitions Value {  get; set; }

       public ICommand IncrementCommand { get; set; }
       public ICommand DecrementCommand { get; set; }
        public String Price { get; set; }

        public  NumericUpDownViewModel(Repartitions rep) {
            Value = rep;
            IncrementCommand = new RelayCommand(Increment);
            DecrementCommand = new RelayCommand(Decrement,()=>Value.weight >0);
            Price = "" + getPrice();

            Register(App.Messages.MSG_REP_CHANGE, () => {
                Price = "" + getPrice();
                RaisePropertyChanged(nameof(Price));
            });

            Register(App.Messages.MSG_MAJ, () => {
                Value.Reload();
                RaisePropertyChanged();
                Console.Write("dddd");
            });
        }
       
        private void Increment() {
           Value.weight++;
            Price = "" + getPrice();
            RaisePropertyChanged(nameof(Value));
           RaisePropertyChanged(nameof(Price));
            NotifyColleagues(App.Messages.MSG_REP_CHANGE);
        }
        private void Decrement() {
            Value.weight--;
            Price = "" + getPrice();
            RaisePropertyChanged(nameof(Value));
            RaisePropertyChanged(nameof(Price));
            NotifyColleagues(App.Messages.MSG_REP_CHANGE);
        }

        private double getPrice() {
            if (Value.weight != 0) {
                return (Value.operations.Amount * ((double)Value.weight / (double)Value.operations.repartitions.Sum(rep => rep.weight)));
            } else {
                return 0;
            }
        }


    }
}
