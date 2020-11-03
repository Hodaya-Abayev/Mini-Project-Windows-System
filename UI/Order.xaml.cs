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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class order : Window
    {
        string ID;
        public order(string id)
        {
            InitializeComponent();
            ID = id;
        }

    

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new UpdateOrder().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new customerList(ID).ShowDialog();
        }
    }
}
