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
    /// Логика взаимодействия для RegionsPage.xaml
    /// </summary>
    public partial class RegionsPage : Page
    {
        public RegionsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                regionDataGrid.ItemsSource = Utils.db.Region.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegionPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (regionDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите регион ");
                return;
            }
            Region selected = regionDataGrid.SelectedItem as Region;
            NavigationService.Navigate(new RegionPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (regionDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите регион");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этот Регион?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Region selected = regionDataGrid.SelectedItem as Region;
                    Utils.db.Region.Remove(selected);
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
