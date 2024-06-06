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

        public  NumericUpDownViewModel(Repartitions rep , double amount , int nb_ope) {
            Console.WriteLine("je rentre");
            Value = rep;
            Amount = amount;
            Nb_ope = nb_ope;
            Console.WriteLine(Value.operations.repartitions);
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
            //Context.SaveChanges();
            NotifyColleagues(App.Messages.MSG_REP_CHANGE);
        }
        private void Decrement() {
            Value.weight--;
            Price = "" + getPrice();
            RaisePropertyChanged(nameof(Value));
            RaisePropertyChanged(nameof(Price));
            //Context.SaveChanges();
            
            NotifyColleagues(App.Messages.MSG_REP_CHANGE);
        }

        private double getPrice() {
            //Console.WriteLine("@@@@@@"+Value.operations.repartitions);
            if (Value.weight != 0) {
                Console.WriteLine(Value.operations.toString());
                return (Value.operations.Amount * ((double)Value.weight / (double)Value.operations.repartitions.Sum(rep => rep.weight)));
            } else {
                return 0;
            }
           // Console.WriteLine();
            //return 0.0;
        }


    }
}
