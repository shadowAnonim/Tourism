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
    /// Логика взаимодействия для PaymentsPage.xaml
    /// </summary>
    public partial class PaymentsPage : Page
    {
        public PaymentsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                paymentDataGrid.ItemsSource = Utils.db.Payment.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PaymentPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (paymentDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            Payment paymentected = paymentDataGrid.SelectedItem as Payment;
            NavigationService.Navigate(new PaymentPage(paymentected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (paymentDataGrid.SelectedItem == null)
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
                    Payment paymentected = paymentDataGrid.SelectedItem as Payment;
                    Utils.db.Payment.Remove(paymentected);
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

