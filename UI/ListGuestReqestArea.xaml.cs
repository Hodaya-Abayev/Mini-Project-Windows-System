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
using BL;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for ListGuestReqestArea.xaml
    /// </summary>
    public partial class ListGuestReqestArea : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        public ListGuestReqestArea()
        {
            InitializeComponent();
            this.areacomboBox.ItemsSource = Enum.GetValues(typeof(BE.Area));
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Area a = (Area)Enum.Parse(typeof(Area), areacomboBox.SelectedItem.ToString());
            this.guestRequestByAreaDataGrid.ItemsSource = bl.groupRequestByArea(a);
        }
    }
}
