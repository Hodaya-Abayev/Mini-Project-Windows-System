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

namespace UI
{
    /// <summary>
    /// Interaction logic for SiteManager.xaml
    /// </summary>
    public partial class SiteManager : Window
    {
        public SiteManager()
        {
            InitializeComponent();
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            new ListGuestreqest().ShowDialog();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            new ListOrder().ShowDialog();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            new listHostingUnits().ShowDialog();
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            new ListGuestReqestArea().ShowDialog();
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            new ListguestrequestCostumers().ShowDialog();
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
           new ListHostByHostingUnits().ShowDialog();
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            new ListHostingUnitArea().ShowDialog();
        }
    }
}
