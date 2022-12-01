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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tourDataGrid.ItemsSource = Utils.db.Tour.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tourDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите отель");
                return;
            }
            Tour selected = tourDataGrid.SelectedItem as Tour;
            NavigationService.Navigate(new TourPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tourDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите отель");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этот отель?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Tour selected = tourDataGrid.SelectedItem as Tour;
                    Utils.db.Tour.Remove(selected);
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
