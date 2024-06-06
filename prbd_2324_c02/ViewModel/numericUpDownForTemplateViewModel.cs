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
    class numericUpDownForTemplateViewModel : ViewModelBase<User, PridContext>
    {
 
       

            public Template_items Value { get; set; }

            public ICommand IncrementCommand { get; set; }
            public ICommand DecrementCommand { get; set; }
            public String Price { get; set; }

            public numericUpDownForTemplateViewModel(Template_items template_Items) {
                Value = template_Items;
                IncrementCommand = new RelayCommand(Increment);
                DecrementCommand = new RelayCommand(Decrement, () => Value.weight > 0);
               
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


