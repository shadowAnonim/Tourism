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
    /// Логика взаимодействия для SellPage.xaml
    /// </summary>
    public partial class SellPage : Page
    {

        Sell sell;
        bool edit = false;
        public SellPage(Sell sell = null)
        {
            InitializeComponent();
            try
            {
                if (sell == null)
                {
                    this.sell = new Sell();
                }
                else
                {
                    this.sell = sell;
                    edit = true;
                }
                sellGrid.DataContext = this.sell;
                bookingIdTextBox.ItemsSource = Utils.db.Booking.ToList();
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
                    Utils.db.Sell.Add(sell);
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
                bookingIdTextBox.SelectedIndex = 0;
        }
    }
}
