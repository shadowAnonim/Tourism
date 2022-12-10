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
        List<TourSell> tours;
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

        private void toursCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour selected = toursCb.SelectedItem as Tour;
            priceTb.Text = selected.Price.ToString();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Tour selected = toursCb.SelectedItem as Tour;
            if (!int.TryParse(countTb.Text, out int count) || !decimal.TryParse(priceTb.Text, out decimal price))
            {
                Utils.Error("Поля должны иметь числовое значение");
                return;
            }
            sell.TourSell.Add(new TourSell() { Sell = sell, People_count = count, Price = price, Tour = selected });
            sellGrid.DataContext = null;
            sellGrid.DataContext = sell;
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tour_sellDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур");
                return;
            }
            try
            {
                TourSell selected = tour_sellDataGrid.SelectedItem as TourSell;
                Utils.db.TourSell.Remove(selected);
                sell.TourSell.Remove(selected);
                sellGrid.DataContext = null;
                sellGrid.DataContext = sell;
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            toursCb.SelectedIndex = 0;
        }
    }
}
