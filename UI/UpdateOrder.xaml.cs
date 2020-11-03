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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        HostingUnit hu;
        GuestRequest gr;
        Order o;
        
        public UpdateOrder()
        {
            InitializeComponent();
            o = new Order();
            hu = new HostingUnit();
            hu.Owner = new Host();
            hu.Owner.BankBranchDetails = new BankBranch();
            gr = new GuestRequest();



            this.updateOrderDataGrid.ItemsSource = bl.listOrs();
           
        }

 
        private void statusClick(object sender, RoutedEventArgs e)
        {
            o = this.updateOrderDataGrid.SelectedItem as Order;
            hu = bl.listHosts().FirstOrDefault(item => item.HostingUnitKey == o.HostingUnitKey );
            gr = bl.listReqs().FirstOrDefault(item => item.GuestRequestKey == o.GuestRequestKey);
            if (hu == null || gr == null)
            {
                MessageBox.Show("Not exist!");
                return;
            }
            new UpdateStatus(o, hu, gr).ShowDialog();
                this.Close();
           
        }


    }
}
