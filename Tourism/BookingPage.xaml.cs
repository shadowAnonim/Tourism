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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tourism
{
    /// <summary>
    /// Логика взаимодействия для BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        Booking booking;
        bool edit = false;
        List<Tour_booking> tours;
        public BookingPage(Booking booking = null)
        {
            InitializeComponent();
            try
            {
                if (booking == null)
                {
                    this.booking = new Booking();
                }
                else
                {
                    this.booking = booking;
                    edit = true;
                }
                bookingGrid.DataContext = this.booking;
                client_idTextBox.ItemsSource = Utils.db.Client.ToList();
                payment_type_idTextBox.ItemsSource = Utils.db.Payment_type.ToList();
                toursCb.ItemsSource = Utils.db.Tour.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!edit)
                    Utils.db.Booking.Add(booking);
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
                client_idTextBox.SelectedIndex = 0;
                payment_type_idTextBox.SelectedIndex = 0;
            }
            toursCb.SelectedIndex = 0;
        }

        private void toursCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour selected = toursCb.SelectedItem as Tour;
            priceTb.Text = selected.Price.ToString();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Tour selected = toursCb.SelectedItem as Tour;
            if (!int.TryParse(countTb.Text, out int count) || !int.TryParse(priceTb.Text, out int price))
            {
                Utils.Error("Поля должны иметь числовое значение");
                return;
            }
            booking.Tour_booking.Add(new Tour_booking() { Booking = booking, People_count = count, Price = price, Tour = selected });
            bookingGrid.DataContext = null;
            bookingGrid.DataContext = booking;
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tour_bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур");
                return;
            }
            try
            {
                Tour_booking selected = tour_bookingDataGrid.SelectedItem as Tour_booking;
                Utils.db.Tour_booking.Remove(selected);
                booking.Tour_booking.Remove(selected);
                bookingGrid.DataContext = null;
                bookingGrid.DataContext = booking;
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
    }
    }
}
