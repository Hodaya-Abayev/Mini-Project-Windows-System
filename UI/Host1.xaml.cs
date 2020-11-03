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
    /// Interaction logic for Host1.xaml
    /// </summary>
    public partial class Host1 : Window
    {
        public Host1()
        {
            InitializeComponent();
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            new HostingUnitDetails().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ID().ShowDialog();
        }
    }
}
