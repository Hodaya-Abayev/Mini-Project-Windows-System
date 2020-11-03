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
    /// Interaction logic for UpdeteHU.xaml
    /// </summary>
    public partial class UpdeteHU : Window
    {
        HostingUnit hu;
        BL.IBL bl = BL.BLFactory.getBL();
        string ID;
        public UpdeteHU(string id)
        {
            InitializeComponent();
            ID = id;
            this.keyComboBox.ItemsSource = bl.unitByHosts(Convert.ToInt64(id));
            this.hostKeyTextBox.Content = id;
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Area));
            this.bankNumberComboBox.ItemsSource = bl.bankNum();
            this.bankNameComboBox.ItemsSource = bl.bankName();
            this.branchNumberComboBox.ItemsSource = bl.branchNum();
            this.branchAdressComboBox.ItemsSource = bl.branchAdress();
            this.branchCityComboBox.ItemsSource = bl.branchCity();
            hu = new HostingUnit();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hu = bl.listHosts().FirstOrDefault(item => item.HostingUnitKey.ToString() == keyComboBox.SelectedValue.ToString());
            bankNumberComboBox.Text = Convert.ToString(hu.Owner.BankBranchDetails.BankNumber);
            bankNameComboBox.Text = hu.Owner.BankBranchDetails.BankName;
            branchNumberComboBox.Text = Convert.ToString(hu.Owner.BankBranchDetails.BranchNumber);
            branchAdressComboBox.Text = hu.Owner.BankBranchDetails.BranchAddress;
            branchCityComboBox.Text = hu.Owner.BankBranchDetails.BranchCity;
            privateNameTextBox.Text = hu.Owner.PrivateName;
            familyNameTextBox.Text = hu.Owner.FamilyName;
            phoneNumberTextBox.Text = hu.Owner.FhoneNumber.ToString();
            mailAdressTextBox.Text = hu.Owner.MailAddress;
            bankAccountNumberTextBox.Text = hu.Owner.BankAccountNumber.ToString();
            collectionClearanceCheckBox.IsChecked = hu.Owner.CollectionClearance == true ? true : false;
            HostingUnitNameTextBox.Text = hu.HostingUnitName;
            areaComboBox.Text = Convert.ToString(hu.area);

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
                foreach (var item in bankAccountNumberTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain a number!");
                }
            }
            catch (Exception ex)
            {
                bankAccountNumberTextBox.BorderBrush = Brushes.Red;
                bankAccountNumberLabel.Content = ex.Message;
            }
        }
        private void BankAccountNumberGotFocus(object sender, RoutedEventArgs e)
        {
            bankAccountNumberTextBox.BorderBrush = Brushes.Black;
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bankNumberComboBox.Text == "" || bankNameComboBox.Text == "" || branchNumberComboBox.Text == "" || branchAdressComboBox.Text == "" || branchCityComboBox.Text == "" || privateNameTextBox.Text == "" || familyNameTextBox.Text == "" || phoneNumberTextBox.Text == "" || mailAdressTextBox.Text == "" || bankAccountNumberTextBox.Text == "" || HostingUnitNameTextBox.Text == ""
                        || hostKeyTextBox.BorderBrush == Brushes.Red || phoneNumberTextBox.BorderBrush == Brushes.Red || bankAccountNumberTextBox.BorderBrush == Brushes.Red || mailAdressTextBox.BorderBrush == Brushes.Red)
                {
                    MessageBox.Show("you have to fill in all the details!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
               
                hu.HostingUnitName = HostingUnitNameTextBox.Text;

                hu.Owner.PrivateName = privateNameTextBox.Text;
                hu.Owner.FamilyName = familyNameTextBox.Text;
                hu.Owner.FhoneNumber = Convert.ToInt64(phoneNumberTextBox.Text);
                hu.Owner.MailAddress = mailAdressTextBox.Text;
                hu.Owner.BankAccountNumber = Convert.ToInt64(bankAccountNumberTextBox.Text);
                hu.area = (Area)Enum.Parse(typeof(Area), (areaComboBox.Text));
                hu.Owner.BankBranchDetails.BankNumber = Convert.ToInt32(bankNumberComboBox.Text);
                hu.Owner.BankBranchDetails.BankName = bankNameComboBox.Text;
                hu.Owner.BankBranchDetails.BranchNumber = Convert.ToInt16(branchNumberComboBox.Text);
                hu.Owner.BankBranchDetails.BranchCity = branchCityComboBox.Text;
                hu.Owner.BankBranchDetails.BranchAddress = branchAdressComboBox.Text;
                bl.updateHostingUnit(hu);


                MessageBox.Show("Your hosting unit was updated successfully!", "UPDATE", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

            catch (Exceptions exp)
            {
                MessageBox.Show(exp.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

   


}
