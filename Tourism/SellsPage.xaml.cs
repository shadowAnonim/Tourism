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
    /// Логика взаимодействия для SellPage.xaml
    /// </summary>
    public partial class SellsPage : Page
    {
        public SellsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sellDataGrid.ItemsSource = Utils.db.Sell.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sellDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите продажу");
                return;
            }
            Sell selected = sellDataGrid.SelectedItem as Sell;
            NavigationService.Navigate(new SellPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sellDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите продажу");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить эту продажу?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Sell selected = sellDataGrid.SelectedItem as Sell;
                    List<TourSell> tours = new List<TourSell>();
                    foreach (TourSell tour in selected.TourSell)
                        tours.Add(tour);
                    foreach (TourSell tour in tours)
                        Utils.db.TourSell.Remove(tour);
                    Utils.db.Sell.Remove(selected);
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
