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
    /// Interaction logic for ListHostingUnitArea.xaml
    /// </summary>
    public partial class ListHostingUnitArea : Window
    {
        HostingUnit hu;
        BL.IBL bl = BL.BLFactory.getBL();
        public ListHostingUnitArea()
        {
            InitializeComponent();
            hu = new HostingUnit();
            hu.Owner = new Host();
            hu.Owner.BankBranchDetails = new BankBranch();
            this.areacomboBox.ItemsSource = Enum.GetValues(typeof(BE.Area));

        }

        private void listOrderDateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area a = (Area)Enum.Parse(typeof(Area), areacomboBox.SelectedItem.ToString());
            this.ListHostingUnitAreaDateGrid.ItemsSource = bl.groupHostingUnitByArea(a);
        }
        private void ownerClick(object sender, RoutedEventArgs e)
        {
            hu = this.ListHostingUnitAreaDateGrid.SelectedItem as HostingUnit;
            string ownerDetails = "Host key: " + hu.Owner.HostKey + "\nPrivate name: " + hu.Owner.PrivateName + "\nFamily name: " + hu.Owner.FamilyName + "\nPhone number: " + hu.Owner.FhoneNumber + "\nMail address: " + hu.Owner.MailAddress + "\nBank account number: " + hu.Owner.BankAccountNumber + "\nCollection clearance: " + hu.Owner.CollectionClearance;
            MessageBox.Show(ownerDetails, "OWNER DETAILS:", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        private void bankBranchClick(object sender, RoutedEventArgs e)
        {
            hu = this.ListHostingUnitAreaDateGrid.SelectedItem as HostingUnit;
            string banckBranchDetails = "Bank numner: " + hu.Owner.BankBranchDetails.BankNumber + "\nBank name: " + hu.Owner.BankBranchDetails.BankName + "\nBranch number: " + hu.Owner.BankBranchDetails.BranchNumber + "\nBranch adress: " + hu.Owner.BankBranchDetails.BranchAddress + "\nBranch city: " + hu.Owner.BankBranchDetails.BranchCity;
            MessageBox.Show(banckBranchDetails, "BANK BRANCH DETAILS:", MessageBoxButton.OK, MessageBoxImage.Information);


        }
    }
}
