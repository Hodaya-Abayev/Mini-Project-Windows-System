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
    /// Interaction logic for ID.xaml
    /// </summary>
    public partial class ID : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        public ID()
        {
            InitializeComponent();
        }
   
        private void IDgotFocus(object sender, RoutedEventArgs e)
        {
            IDTextBox.BorderBrush = Brushes.Black;
            IDLabel.Content = null;
        }
        private void next_Butten_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDTextBox.Text.Count() != 9)
                    throw new Exception("this field must contain 9 digits!");
                foreach (var item in IDTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain 9 digits!");

                }

            }
            catch (Exception ex)
            {
                IDTextBox.BorderBrush = Brushes.Red;
                IDLabel.Content = ex.Message;
                return;
            }
            if (bl.listHosts().Exists(item => item.Owner.HostKey == Convert.ToInt64(IDTextBox.Text) == true))
            {
                this.Close();
                new HostOption(IDTextBox.Text).ShowDialog();
                
            }
            else
                MessageBox.Show("this ID is not exist!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
