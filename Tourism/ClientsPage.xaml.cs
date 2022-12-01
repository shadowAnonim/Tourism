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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            Client selected = clientDataGrid.SelectedItem as Client;
            NavigationService.Navigate(new ClientPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этого клиента?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Client selected = clientDataGrid.SelectedItem as Client;
                    Utils.db.Client.Remove(selected);
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
                clientDataGrid.ItemsSource = Utils.db.Client.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }
    }
}
