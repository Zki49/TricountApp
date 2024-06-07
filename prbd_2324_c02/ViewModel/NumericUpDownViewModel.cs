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
        
        public Repartitions  Value {  get; set; }

       public ICommand IncrementCommand { get; set; }
       public ICommand DecrementCommand { get; set; }
        public String Price { get; set; }
        public double Amount { get; set; }
        public int Nb_ope { get; set; }
        private Operations op {  get; set; }

        public  NumericUpDownViewModel(Repartitions rep , double amount , int nb_ope) {
            Value = rep;
            Amount = amount;
            Nb_ope = nb_ope;
            // faire ca car sinon avec la navihgation ca plant pk impossible de savoir ///
            op = rep.operations;
            IncrementCommand = new RelayCommand(Increment);
            DecrementCommand = new RelayCommand(Decrement,()=>Value.weight >0);
            Price = "" + getPrice();

            Register<Repartitions>(App.Messages.MSG_REP_CHANGE, rep => {
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
            NotifyColleagues(App.Messages.MSG_REP_CHANGE,Value);
        }
        private void Decrement() {
            Value.weight--;
            Price = "" + getPrice();
            RaisePropertyChanged(nameof(Value));
            RaisePropertyChanged(nameof(Price));
            
            NotifyColleagues(App.Messages.MSG_REP_CHANGE, Value);
        }

        private double getPrice() {
            if (Value.weight != 0) {
                return (op.Amount * ((double)Value.weight / (double)Nb_ope));
            } else {
                return 0;
            }
        }


    }
}
