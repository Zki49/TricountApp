﻿using prbd_2324_c02.Model;
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

        public int Value {  get; set; }

       public ICommand IncrementCommand { get; set; }
       public ICommand DecrementCommand { get; set; }
       
        public  NumericUpDownViewModel() {
            Value = 1;
            IncrementCommand = new RelayCommand(Increment);
            DecrementCommand = new RelayCommand(Decrement,()=>Value >0);


        }

        private void Increment() {
           Value++;
        }
        private void Decrement() {
            Value--;
        }    
    }
}
