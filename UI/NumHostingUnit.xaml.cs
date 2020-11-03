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
using System.Net.Mail;
using System.Windows.Shapes;
using BE;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for NumHostingUnit.xaml
    /// </summary>
    public partial class NumHostingUnit : Window
    {
        BL.IBL bl = BL.BLFactory.getBL();
        HostingUnit hu;
        GuestRequest gr;
        Order o;
        string ID;
        public NumHostingUnit(string id, GuestRequest g)
        {
            InitializeComponent();
            ID = id;
            this.NUComboBox.ItemsSource = bl.unitByHosts(Convert.ToInt64(id));
            hu = new HostingUnit();
            o = new Order();
            gr = g;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void updateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                hu = bl.listHosts().FirstOrDefault(item => item.HostingUnitKey.ToString() == NUComboBox.SelectedValue.ToString());
                o.HostingUnitKey = Convert.ToInt64(NUComboBox.SelectedValue.ToString());
                o.GuestRequestKey = gr.GuestRequestKey;
                o.statusOrder = StatusOrder.SentMail;
                o.MailDate = DateTime.Today;
                o.CreateDate = DateTime.Today;
                o.OrderKey = ++Configuration.OrderKeyseq;
                bl.addOrder(hu, gr, o);

                try
                {
                    bl.mailSent(o, hu, gr);
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("the order was added successfully!\n order number: " + Configuration.OrderKeyseq);
                this.Close();
            }
            catch(Exceptions ex)
            {
                MessageBox.Show(ex.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
