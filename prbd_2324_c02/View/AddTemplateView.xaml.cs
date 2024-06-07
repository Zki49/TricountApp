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
using prbd_2324_c02.Model;
using prbd_2324_c02.ViewModel;
using PRBD_Framework;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour AddTemplateView.xaml
    /// </summary>
    public partial class AddTemplateView : WindowBase
    {
        public AddTemplateView(Tricount tricount, bool isedit, Template curent) {
            InitializeComponent();
            DataContext = new AddTemplateViewModel(tricount, curent, isedit);
            Register(App.Messages.MSG_CLOSE_WINDOWS, () => Close());
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
