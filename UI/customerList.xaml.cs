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
    /// Interaction logic for ListGuestreqest.xaml
    /// </summary>
    public partial class customerList : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        GuestRequest g;
        HostingUnit hu;
        string ID;
        public customerList(string id)
        {
            InitializeComponent();
            g = new GuestRequest();
            hu = new HostingUnit();
            hu.Owner = new Host();
            ID = id;
            hu.Owner.BankBranchDetails = new BankBranch();
            this.guestRequestDataGrid.ItemsSource = bl.listReqs();
           
        }

        private void guestRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void addClick(object sender, RoutedEventArgs e)
        {
            g = this.guestRequestDataGrid.SelectedItem as GuestRequest;
            new NumHostingUnit(ID, g).ShowDialog();
        
        }
    }
}
