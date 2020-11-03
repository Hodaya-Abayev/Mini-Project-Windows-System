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
    /// Interaction logic for ListHostingUnitCostumers.xaml
    /// </summary>
    public partial class ListguestrequestCostumers : Window
    {
        HostingUnit hu;
        BL.IBL bl = BL.BLFactory.getBL();
        public ListguestrequestCostumers()
        {
            InitializeComponent();
        }
        private void ownerClick(object sender, RoutedEventArgs e)
        {
            hu = this.listRequestByCustomersDateGrid.SelectedItem as HostingUnit;
            string ownerDetails = "Host key: " + hu.Owner.HostKey + "\nPrivate name: " + hu.Owner.PrivateName + "\nFamily name: " + hu.Owner.FamilyName + "\nPhone number: " + hu.Owner.FhoneNumber + "\nMail address: " + hu.Owner.MailAddress + "\nBank account number: " + hu.Owner.BankAccountNumber + "\nCollection clearance: " + hu.Owner.CollectionClearance;
            MessageBox.Show(ownerDetails, "OWNER DETAILS:", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        private void bankBranchClick(object sender, RoutedEventArgs e)
        {
            hu = this.listRequestByCustomersDateGrid.SelectedItem as HostingUnit;
            string banckBranchDetails = "Bank numner: " + hu.Owner.BankBranchDetails.BankNumber + "\nBank name: " + hu.Owner.BankBranchDetails.BankName + "\nBranch number: " + hu.Owner.BankBranchDetails.BranchNumber + "\nBranch adress: " + hu.Owner.BankBranchDetails.BranchAddress + "\nBranch city: " + hu.Owner.BankBranchDetails.BranchCity;
            MessageBox.Show(banckBranchDetails, "BANK BRANCH DETAILS:", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void numLostFocus(object sender, RoutedEventArgs e)
        {
           
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
                this.listRequestByCustomersDateGrid.ItemsSource = bl.groupRequestByNumOfCustomers(Convert.ToInt32(NumtextBox.Text));

            }
            catch (Exception ex)
            {
                NumtextBox.BorderBrush = Brushes.Red;
                label1.Content = ex.Message;

            }
           
            
   
        }
    }
}
