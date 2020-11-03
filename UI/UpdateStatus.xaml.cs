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
using System.Net.Mail;
using BE;
using BL;


namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateStatus.xaml
    /// </summary>
    public partial class UpdateStatus : Window
    {
        Order O;
        HostingUnit HU;
        GuestRequest G;

        
        BL.IBL bl = BL.BLFactory.getBL();
        public UpdateStatus(Order o,  HostingUnit h, GuestRequest g)
        {
            
            InitializeComponent();
            O = o;
            HU = h;
            G = g;
            this.StatusComboBox.ItemsSource = Enum.GetValues(typeof(BE.StatusOrder));
          
            
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void updateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusOrder s=(StatusOrder)Enum.Parse(typeof(StatusOrder), StatusComboBox.SelectedItem.ToString());
                
               
                bl.updateOrder(HU, O, s, G);
                if (s == StatusOrder.SentMail)
                {
                    try
                    {
                        bl.mailSent(O, HU, G);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                MessageBox.Show("This order was updated successfully!", "UPDATE", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exceptions ex)
            {
                MessageBox.Show(ex.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
