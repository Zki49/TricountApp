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
using prbd_2324_c02.ViewModel;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour DialoView.xaml
    /// </summary>
    public partial class DialogView 
    {
        public DialogView(string objectname) {
            InitializeComponent();
            DataContext = new DialogViewModel( objectname);
        }
       
    }
}
