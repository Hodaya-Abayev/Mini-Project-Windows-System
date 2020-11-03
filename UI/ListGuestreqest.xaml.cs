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
    public partial class ListGuestreqest : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        public ListGuestreqest()
        {
            InitializeComponent();
            this.guestRequestDataGrid.ItemsSource = bl.listReqs();
        }

        private void guestRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
