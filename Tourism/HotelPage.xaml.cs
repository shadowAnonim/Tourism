using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tourism
{
    /// <summary>
    /// Interaction logic for HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        Hotel hotel;
        bool edit = false;
        public HotelPage(Hotel hotel = null)
        {
            InitializeComponent();
            try
            {
                if (hotel == null)
                {
                    this.hotel = new Hotel();
                }
                else
                {
                    this.hotel = hotel;
                    edit = true;
                }
                hotelGrid.DataContext = this.hotel;
                managerIdTextBox.ItemsSource = Utils.db.Manager.ToList();
                regionIdTextBox.ItemsSource = Utils.db.Region.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.Match(phoneTextBox.Text, @"/^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$/").Success)
            {
                Utils.Error("Некоректный формат телефона");
                return;
            }
            try
            {
                if (!edit)
                    Utils.db.Hotel.Add(hotel);
                Utils.db.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!edit)
            {
                managerIdTextBox.SelectedIndex = 0;
                regionIdTextBox.SelectedIndex = 0;
            }
        }
    }
}
