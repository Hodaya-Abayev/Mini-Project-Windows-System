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
    /// Interaction logic for Host.xaml
    /// </summary>
    public partial class HostOption : Window
    {
        string ID;
        public HostOption(string id)
        {
            InitializeComponent();
            ID = id;

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                new order(ID).ShowDialog();
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new DelateHU(ID).ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new UpdeteHU(ID).ShowDialog();

            
        }


    }
}
