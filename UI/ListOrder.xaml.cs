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
using BE;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for ListOrder.xaml
    /// </summary>
    public partial class ListOrder : Window
    {
        Order o;
        GuestRequest g;
        BL.IBL bl = BL.BLFactory.getBL();
        public ListOrder()
        {
            InitializeComponent();
            this.listOrderDateGrid.ItemsSource = bl.listOrs();
            o = new Order();
            g = new GuestRequest();

        }

        private void listOrderDateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
        private void feeClick(object sender, RoutedEventArgs e)
        {
            o = this.listOrderDateGrid.SelectedItem as Order;
            if (o.statusOrder == StatusOrder.CloseOfResponsiveness)
            {
                g = bl.listReqs().FirstOrDefault(item => item.GuestRequestKey == o.GuestRequestKey);
                MessageBox.Show("the total fee payment is: " + bl.orderFee(o, g) + " Shekels", "FEE", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("thf fee was not calculated yet, because the deal is not closed yet!", "FEE", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
        
}
