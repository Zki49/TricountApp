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
    /// Logique d'interaction pour TricountView.xaml
    /// </summary>
    public partial class TricountView : UserControlBase
    {
        public TricountView() {
            InitializeComponent();
        }

        private void ItemBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var border = sender as Border;
            if (border != null && border.DataContext != null) {
                var tricount = border.DataContext;
                Console.WriteLine("Item clicked: " + tricount);
            }
        }
    }
}
