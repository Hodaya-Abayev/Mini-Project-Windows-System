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
    /// Interaction logic for ListHostByHostingUnits.xaml
    /// </summary>
    public partial class ListHostByHostingUnits : Window
    {
        Host h;
        BL.IBL bl = BL.BLFactory.getBL();
        public ListHostByHostingUnits()
        {
            InitializeComponent();
            h = new Host();
            h.BankBranchDetails = new BankBranch();
        }
        private void numGotFocus(object sender, RoutedEventArgs e)
        {
            NumtextBox.BorderBrush = Brushes.Black;
            label1.Content = null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (NumtextBox.Text == "")
                    throw new Exception("this field must contain a number!");
                foreach (var item in NumtextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain a number!");

                }
                this.ListHostByHostingUnitsDataGrid.ItemsSource = bl.groupHostByNumOfHostingUnit(Convert.ToInt32(NumtextBox.Text));

            }
            catch (Exception ex)
            {
                NumtextBox.BorderBrush = Brushes.Red;
                label1.Content = ex.Message;

            }



        }
        private void NumtextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void bankClick(object sender, RoutedEventArgs e)
        {
            h = this.ListHostByHostingUnitsDataGrid.SelectedItem as Host;
            string banckBranchDetails = "Bank numner: " + h.BankBranchDetails.BankNumber + "\nBank name: " + h.BankBranchDetails.BankName + "\nBranch number: " + h.BankBranchDetails.BranchNumber + "\nBranch adress: " + h.BankBranchDetails.BranchAddress + "\nBranch city: " + h.BankBranchDetails.BranchCity;
            MessageBox.Show(banckBranchDetails, "BANK BRANCH DETAILS:", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
