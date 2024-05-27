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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Query.Internal;
using prbd_2324_c02.Model;
using prbd_2324_c02.ViewModel;
using PRBD_Framework;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour AddOperationView.xaml
    /// </summary>
    public partial class AddOperationView : WindowBase
    {
        public AddOperationView(Tricount tricount ,bool isedit , Operations curent) {
            InitializeComponent();
           DataContext = new AddOperationViewModel(tricount,curent,isedit);


        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
