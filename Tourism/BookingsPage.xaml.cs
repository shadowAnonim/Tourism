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
    /// Логика взаимодействия для BookingsPage.xaml
    /// </summary>
    public partial class BookingsPage : Page
    {
        public BookingsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bookingDataGrid.ItemsSource = Utils.db.Booking.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookingPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            Booking selected = bookingDataGrid.SelectedItem as Booking;
            NavigationService.Navigate(new BookingPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этот заказ?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Booking selected = bookingDataGrid.SelectedItem as Booking;
                    Utils.db.Booking.Remove(selected);
                    Utils.db.SaveChanges();
                    Page_Loaded(null, null);
                }
                catch (Exception ex)
                {
                    Utils.Error(ex.Message);
                }
            }
        }
    }
}
