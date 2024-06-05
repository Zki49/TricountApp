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
       
        public  NumericUpDownViewModel( Repartitions rep) {
            Value = rep;
            IncrementCommand = new RelayCommand(Increment);
            DecrementCommand = new RelayCommand(Decrement,()=>Value.weight >0);


        }
       
        private void Increment() {
           Value.weight++;
           RaisePropertyChanged(nameof(Value));
        }
        private void Decrement() {
            Value.weight--;
            
            
            RaisePropertyChanged(nameof(Value));
        }
    }
}
