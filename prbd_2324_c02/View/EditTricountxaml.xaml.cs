﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Metadata;
using prbd_2324_c02.Model;
using prbd_2324_c02.ViewModel;
using PRBD_Framework;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour EditTricountxaml.xaml
    /// </summary>
    public partial class EditTricountView :UserControlBase 
    {
        private readonly EditTricountViewModel _vm;
        public EditTricountView(Tricount tricount ,bool mode )
        {
            InitializeComponent();
            DataContext = _vm = new EditTricountViewModel(tricount,mode);

        }
        private void CloseTab_Click(object sender, RoutedEventArgs e) {
          
        }
        /*private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }*/
    }
}
