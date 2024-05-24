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
using PRBD_Framework;

namespace prbd_2324_c02.View
{
    /// <summary>
    /// Logique d'interaction pour EditTricountxaml.xaml
    /// </summary>
    public partial class EditTricountView : UserControlBase
    {
        public EditTricountView()
        {
            InitializeComponent();
        }
        private void CloseTab_Click(object sender, RoutedEventArgs e) {
            var tabItem = ((sender as Button).Parent as StackPanel).Parent as TabItem;
            var tabControl = tabItem.Parent as TabControl;
            tabControl.Items.Remove(tabItem);
        }
    }
}
