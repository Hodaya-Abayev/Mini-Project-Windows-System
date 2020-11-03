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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnitDetails : Window
    {
        HostingUnit hu;
    
        BL.IBL bl = BL.BLFactory.getBL();
        public HostingUnitDetails()
        {
            InitializeComponent();
            hu = new HostingUnit();
            hu.Owner = new Host();
            hu.Owner.BankBranchDetails = new BankBranch();
   

       
            this.hostingUnit.DataContext = hu;
            this.owner.DataContext = hu.Owner;
            this.bank.DataContext = hu.Owner.BankBranchDetails;
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Area));
            this.bankNumberComboBox.ItemsSource = bl.bankNum();
            this.bankNameComboBox.ItemsSource = bl.bankName();
            this.branchNumberComboBox.ItemsSource = bl.branchNum();
            this.branchAdressComboBox.ItemsSource = bl.branchAdress();
            this.branchCityComboBox.ItemsSource = bl.branchCity();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bankNumberComboBox.Text == "" || bankNameComboBox.Text == "" || branchNumberComboBox.Text == "" || branchAdressComboBox.Text == "" || branchCityComboBox.Text == "" || hostKeyTextBox.Text == "" || privateNameTextBox.Text == "" || familyNameTextBox.Text == "" || phoneNumberTextBox.Text == "" || mailAdressTextBox.Text == "" || BankAccountNumberTextBox.Text == "" || hostingUnitNameTextBox.Text == ""
                    || hostKeyTextBox.BorderBrush == Brushes.Red|| phoneNumberTextBox.BorderBrush == Brushes.Red|| BankAccountNumberTextBox.BorderBrush == Brushes.Red|| mailAdressTextBox.BorderBrush == Brushes.Red)
                {
                    MessageBox.Show("you have to fill in all the details!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                hu.HostingUnitKey = ++Configuration.hostingUnitKeySeq;
                hu.HostingUnitName = hostingUnitNameTextBox.Text;
                hu.Owner.HostKey = Convert.ToInt64(hostKeyTextBox.Text);
                hu.Owner.PrivateName = privateNameTextBox.Text;
                hu.Owner.FamilyName = familyNameTextBox.Text;
                hu.Owner.FhoneNumber = Convert.ToInt64(phoneNumberTextBox.Text);
                hu.Owner.MailAddress = mailAdressTextBox.Text;
                hu.Owner.BankAccountNumber = Convert.ToInt64(BankAccountNumberTextBox.Text);
                hu.area = (Area)Enum.Parse(typeof(Area), (areaComboBox.Text));
                hu.Owner.BankBranchDetails.BankNumber = Convert.ToInt32(bankNumberComboBox.Text);
                hu.Owner.BankBranchDetails.BankName = bankNameComboBox.Text;
                hu.Owner.BankBranchDetails.BranchNumber = Convert.ToInt16(branchNumberComboBox.Text);
                hu.Owner.BankBranchDetails.BranchCity = branchCityComboBox.Text;
                hu.Owner.BankBranchDetails.BranchAddress = branchAdressComboBox.Text;
                bl.addHostingUnit(hu);



                MessageBox.Show("the hosting unit was added successfully!\n your hosting unit number: " + Configuration.hostingUnitKeySeq);
                this.Close();
            }
            catch(Exceptions exp)
            {
                MessageBox.Show(exp.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

  
        
        private void hostKeyLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if(hostKeyTextBox.Text.Count()!=9)
                    throw new Exception("this field must contain 9 digits!");
                foreach (var item in hostKeyTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain 9 digits!");
                }
            }
            catch (Exception ex)
            {
                hostKeyTextBox.BorderBrush = Brushes.Red;
                hostKeyLabel.Content = ex.Message;
            }
        }
        private void hostKeyGotFocus(object sender, RoutedEventArgs e)
        {
            hostKeyTextBox.BorderBrush = Brushes.Black;
            hostKeyLabel.Content = null;
        }
        private void phoneNumberLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (phoneNumberTextBox.Text.Count() != 10)
                    throw new Exception("this field must contain 10 digits!");
                foreach (var item in phoneNumberTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain 10 digits!");
                }
            }
            catch (Exception ex)
            {
                phoneNumberTextBox.BorderBrush = Brushes.Red;
                phoneNumberLabel.Content = ex.Message;
            }
        }
        private void phoneNumberGotFocus(object sender, RoutedEventArgs e)
        {
            phoneNumberTextBox.BorderBrush = Brushes.Black;
            phoneNumberLabel.Content = null;
        }
        private void BankAccountNumberLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in BankAccountNumberTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain a number!");
                }
            }
            catch (Exception ex)
            {
                BankAccountNumberTextBox.BorderBrush = Brushes.Red;
                bankAccountNumberLabel.Content = ex.Message;
            }
        }
        private void BankAccountNumberGotFocus(object sender, RoutedEventArgs e)
        {
            BankAccountNumberTextBox.BorderBrush = Brushes.Black;
            bankAccountNumberLabel.Content = null;
        }
        private void mailAdress_LostFocus(object sender, RoutedEventArgs e)

        {
            try
            {
                if (!mailAdressTextBox.Text.EndsWith("@gmail.com"))
                    throw new Exception("this mail adress is not current");

            }
            catch (Exception ex)
            {
                mailAdressTextBox.BorderBrush = Brushes.Red;
                mailAdressLabel.Content = ex.Message;
            }
        }
        private void mailAdress_GotFocus(object sender, RoutedEventArgs e)
        {
            mailAdressTextBox.BorderBrush = Brushes.Black;
            mailAdressLabel.Content = null;
        }

        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
