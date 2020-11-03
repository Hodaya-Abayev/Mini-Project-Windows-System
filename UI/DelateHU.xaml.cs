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
    /// Interaction logic for DelateHU.xaml
    /// </summary>
    public partial class DelateHU : Window
    {
        HostingUnit hu;
        BL.IBL bl = BL.BLFactory.getBL();
        string ID;
        public DelateHU(string id)
        {
            InitializeComponent();
            ID = id;
            this.IDcomboBox.ItemsSource = bl.unitByHosts(Convert.ToInt64(id));
            hu = new HostingUnit();

        }

        private void delateHU_Button_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult ans= MessageBox.Show("Are you sure you want to delete this gosting unit?", "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes)
            {
                try
                {
                    bl.deleteHostingUnit(hu);
                    MessageBox.Show("your hosting unit was deleted successfully!", "DELETE", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                catch (Exceptions ex)
                {
                    MessageBox.Show(ex.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            hu = bl.listHosts().FirstOrDefault(item => item.HostingUnitKey.ToString() == IDcomboBox.SelectedValue.ToString());
        }
    }
}
