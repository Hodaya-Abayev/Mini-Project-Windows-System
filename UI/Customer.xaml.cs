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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
       
        GuestRequest g;
        BL.IBL bl = BL.BLFactory.getBL();
        public Customer()
        {
            InitializeComponent();
           
            g = new GuestRequest();
            this.guestRequest.DataContext = g;
            
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Area));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(BE.Types));
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(BE.Pool));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(BE.Jacuzzi));
            this.gardenComboBox.ItemsSource = Enum.GetValues(typeof(BE.Garden));
            this.tvComboBox.ItemsSource = Enum.GetValues(typeof(BE.TV));
            this.mealsComboBox.ItemsSource = Enum.GetValues(typeof(BE.Meals));
            this.ChildrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(BE.ChildrensAttractions));

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if ((privateNameTextBox.Text == "") || (familyNameTextBox.Text == "") || (mailAddressTextBox.Text == "") || (entryDatePicker.Text == "") || (releaseDatePicker.Text == "") || (areaComboBox.Text == "") || (subAreaTextBox.Text == "") || (adultsTextBox.Text == "") || (childrenTextBox.Text == "") || (poolComboBox.Text == "") || (jacuzziComboBox.Text == "") || (gardenComboBox.Text == "") || (tvComboBox.Text == "") || (ChildrensAttractionsComboBox.Text == "") || (mealsComboBox.Text == "")
                    ||(adultsTextBox.BorderBrush == Brushes.Red)||(childrenTextBox.BorderBrush == Brushes.Red)||(mailAddressTextBox.BorderBrush == Brushes.Red))

                { 
                    MessageBox.Show("you have to fill in all the details!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning );
                    return;
                }
                g.GuestRequestKey = ++Configuration.guestRequestKeySeq;
                g.PrivateName = privateNameTextBox.Text;
                g.FamilyName = familyNameTextBox.Text;
                g.MailAddress = mailAddressTextBox.Text;
                g.EntryDate = entryDatePicker.SelectedDate.Value;
                g.ReleaseDate = releaseDatePicker.SelectedDate.Value;
                g.RegistrationDate = DateTime.Today;
                g.SubArea = subAreaTextBox.Text;
                g.area = (Area)Enum.Parse(typeof(Area), (areaComboBox.Text));
                g.Adults = Convert.ToInt16(adultsTextBox.Text);
                g.Children = Convert.ToInt16(childrenTextBox.Text);
                g.statusRequest = StatusRequest.Active;
                g.types= (Types)Enum.Parse(typeof(Types), (typeComboBox.Text));
                g.pool= (Pool)Enum.Parse(typeof(Pool), (poolComboBox.Text));
                g.jacuzzi= (Jacuzzi)Enum.Parse(typeof(Jacuzzi), (jacuzziComboBox.Text));
                g.garden= (Garden)Enum.Parse(typeof(Garden), (gardenComboBox.Text));
                g.tv= (TV)Enum.Parse(typeof(TV), (tvComboBox.Text));
                g.meals= (Meals)Enum.Parse(typeof(Meals), (mealsComboBox.Text));
                g.childrensAttractions= (ChildrensAttractions)Enum.Parse(typeof(ChildrensAttractions), (ChildrensAttractionsComboBox.Text));
                bl.addClientRequest(g);
                MessageBox.Show("the guest request was added successfully!\n your guest request number: "+Configuration.guestRequestKeySeq);
              

                this.Close();


            }
            catch (Exceptions exp)
            {
                MessageBox.Show(exp.message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
       
        private void adultNameTextBox_LostFocus(object sender, RoutedEventArgs e) 
       
        {
            try
            {
                foreach (var item in adultsTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain a number!");
                }
            }
            catch(Exception ex)
            {
                adultsTextBox.BorderBrush = Brushes.Red;
                adultLabel.Content = ex.Message;
            }
        }
        private void adultNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            adultsTextBox.BorderBrush = Brushes.Black;
            adultLabel.Content = null;
        }
        private void childrenTextBox_LostFocus(object sender, RoutedEventArgs e)

        {
            try
            {
                foreach (var item in childrenTextBox.Text)
                {
                    if (item < 47 || item > 57)
                        throw new Exception("this field must contain a number!");
                }
            }
            catch (Exception ex)
            {
                childrenTextBox.BorderBrush = Brushes.Red;
                childrenLabel.Content = ex.Message;
            }
        }
        private void childrenTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
           childrenTextBox.BorderBrush = Brushes.Black;
            childrenLabel.Content = null;
        }

        
        private void mailAdress_LostFocus(object sender, RoutedEventArgs e)

        {
            try
            {
                if(!mailAddressTextBox.Text.EndsWith("@gmail.com"))
                     throw new Exception("this mail adress is not current");
                
            }
            catch (Exception ex)
            {
                mailAddressTextBox.BorderBrush = Brushes.Red;
                mailAdressLabel.Content = ex.Message;
            }
        }
        private void mailAdress_GotFocus(object sender, RoutedEventArgs e)
        {
            mailAddressTextBox.BorderBrush = Brushes.Black;
            mailAdressLabel.Content = null;
        }







    }



}