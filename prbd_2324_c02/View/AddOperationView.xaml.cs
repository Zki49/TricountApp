using System;
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
        public Repartitions Value { get; set; }
        private AddOperationViewModel _vm;
        public AddOperationView(Tricount tricount ,bool isedit , Operations curent) {
            InitializeComponent();
           DataContext = _vm = new AddOperationViewModel(tricount,curent,isedit);
            Register(App.Messages.MSG_CLOSE_WINDOWS, () => close());

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) {

            NotifyColleagues(App.Messages.MSG_MAJ);
            _vm.Reload();
            Close();
            NotifyColleagues(App.Messages.MSG_CANCEL_OPE);
        }


        private void close() {
            this.Close();
        }
    }
}
