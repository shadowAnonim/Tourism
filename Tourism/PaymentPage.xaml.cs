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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {

        Payment payment;
        bool edit = false;
        public PaymentPage(Payment payment = null, Booking booking = null)
        {
            InitializeComponent();
            try
            {
                if (payment == null)
                {
                    this.payment = new Payment();
                    this.payment.Booking = booking;
                }
                else
                {
                    this.payment = payment;
                    edit = true;
                }
                paymentGrid.DataContext = this.payment;
                amountTextBox.Text = this.payment.Booking.Total.ToString();
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
                {
                    payment.Date = DateTime.Now;
                    Utils.db.Payment.Add(payment);
                }                  
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

        }
    }
}
