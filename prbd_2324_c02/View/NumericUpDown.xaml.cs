using PRBD_Framework;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using prbd_2324_c02.ViewModel;
using prbd_2324_c02.Model;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControlBase
    {
        public Repartitions Value {  get; set; }
        public NumericUpDown(/*passer la repartion et la passe au vm*/) {
            InitializeComponent();
           // DataContext = new NumericUpDownViewModel(Content);
           // DataContext = new NumericUpDownViewModel(repartion);
        }
        public static readonly DependencyProperty NumberProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
    }
}
