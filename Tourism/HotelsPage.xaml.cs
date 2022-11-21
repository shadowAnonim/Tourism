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
    /// Interaction logic for HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        public HotelsPage()
        {
            InitializeComponent();
            hotelDataGrid.ItemsSource = Utils.db.Hotel.ToList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HotelPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            hotelDataGrid.ItemsSource = Utils.db.Hotel.ToList();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (hotelDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите отель");
                return;
            }
            Hotel selected = hotelDataGrid.SelectedItem as Hotel;
            NavigationService.Navigate(new HotelPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (hotelDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите отель");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этот отель?", 
                "Подтвердите удаление", MessageBoxButton.YesNo) == 
                MessageBoxResult.Yes)
            {
                Hotel selected = hotelDataGrid.SelectedItem as Hotel;
                Utils.db.Hotel.Remove(selected);
                Utils.db.SaveChanges();
                Page_Loaded(null, null);
            }
        }
    }
}
