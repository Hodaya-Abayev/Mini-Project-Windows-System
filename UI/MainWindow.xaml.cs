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
using BE;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL bl = BLFactory.getBL();
        public MainWindow()
        {
            InitializeComponent();
        }

    
        private void customer_Button_Click(object sender, RoutedEventArgs e)
        {
            new Customer().ShowDialog();
        }

        private void host_Button_Click(object sender, RoutedEventArgs e)
        {
            new Host1().ShowDialog();
        }

        private void site_manager_Button_Click(object sender, RoutedEventArgs e)
        {
            new SiteManager().ShowDialog();
        }
    }
}
