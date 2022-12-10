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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void hotelsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HotelsPage());
        }

        private void regionsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegionsPage());
        }

        private void personalBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagersPage());
        }

        private void rolesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolesPage());
        }

        private void toursBtn_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ToursPage());
        }

        private void feedingBtn_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FeedingsPage());
        }

        private void clientsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }

        private void clientTypesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientTypesPage());
        }

        private void bookingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookingsPage());
        }

        private void paymentsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PaymentsPage());
        }
    }
}
