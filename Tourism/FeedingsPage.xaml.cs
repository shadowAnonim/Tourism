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
    /// Логика взаимодействия для FeedingsPage.xaml
    /// </summary>
    public partial class FeedingsPage : Page
    {
        public FeedingsPage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FeedingPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                feedingDataGrid.ItemsSource = Utils.db.Feeding.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (feedingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите отель");
                return;
            }
            Feeding selected = feedingDataGrid.SelectedItem as Feeding;
            NavigationService.Navigate(new FeedingPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (feedingDataGrid.SelectedItem == null)
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
                    Feeding selected = feedingDataGrid.SelectedItem as Feeding;
                    Utils.db.Feeding.Remove(selected);
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
