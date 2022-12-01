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
    /// Логика взаимодействия для ClientTypesPage.xaml
    /// </summary>
    public partial class ClientTypesPage : Page
    {
        public ClientTypesPage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientTypePage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (client_typeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип");
                return;
            }
            Client_type selected = client_typeDataGrid.SelectedItem as Client_type;
            NavigationService.Navigate(new ClientTypePage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (client_typeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этот тип?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Client_type selected = client_typeDataGrid.SelectedItem as Client_type;
                    Utils.db.Client_type.Remove(selected);
                    Utils.db.SaveChanges();
                    Page_Loaded(null, null);
                }
                catch (Exception ex)
                {
                    Utils.Error(ex.Message);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                client_typeDataGrid.ItemsSource = Utils.db.Client_type.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }
    }
}
