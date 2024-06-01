using prbd_2324_c02.Model;
using prbd_2324_c02.ViewModel;
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

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour OpenTricountView.xaml
    /// </summary>
    public partial class OpenTricountView : UserControlBase
    {
        private readonly OpenTricountViewModel _vm;
        public OpenTricountView(Tricount tricount) {
            Console.WriteLine("test");
            InitializeComponent();
            DataContext = _vm = new OpenTricountViewModel(tricount);
            
        }
        }
}
