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
            try
            {
                List<PaymentStatus> statuses = new List<PaymentStatus>() { new PaymentStatus() { Name = "Все" } };
                statuses.AddRange(Utils.db.PaymentStatus.ToList());
                statusCb.ItemsSource = statuses;
                statusCb.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FillDataGrid();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void FillDataGrid()
        {
            List<Booking> bookings =  Utils.db.Booking.ToList();
            if (statusCb.SelectedIndex != 0)
            {
                bookings = bookings.Where(b => b.StatusId == (statusCb.SelectedItem as PaymentStatus).Id).ToList();
            }
            bookingDataGrid.ItemsSource = bookings;
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

        private void statusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) FillDataGrid();
        }

        private void sellBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            Booking selected = bookingDataGrid.SelectedItem as Booking;
            if (selected.StatusId == 2)
            {
                MessageBox.Show("Этот заказ уже продан");
                return;
            }
            NavigationService.Navigate(new SellPage(booking: (bookingDataGrid.SelectedItem as Booking)));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            Booking selected = bookingDataGrid.SelectedItem as Booking;
            if (selected.StatusId == 2)
            {
                MessageBox.Show("Этот заказ уже отменён");
                return;
            }
            if (MessageBox.Show("Вы точно хотите отменить этот заказ?",
                "Подтвердите отмену", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    selected.StatusId = 2;
                    Utils.db.SaveChanges();
                    Page_Loaded(null, null);
                }
                catch (Exception ex)
                {
                    Utils.Error(ex.Message);
                }
            }
        }

        private void payBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            Booking selected = bookingDataGrid.SelectedItem as Booking;
            if (selected.StatusId == 2)
            {
                MessageBox.Show("Этот заказ уже отменён");
                return;
            }
            if (selected.Payment.Count > 0)
            {
                MessageBox.Show("Этот заказ уже оплачен");
                return;
            }
            NavigationService.Navigate(new PaymentPage(booking: (bookingDataGrid.SelectedItem as Booking)));
        }
    }
}
