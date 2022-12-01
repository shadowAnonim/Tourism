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
    /// Логика взаимодействия для RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolePage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roleDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }
            Role selected = roleDataGrid.SelectedItem as Role;
            NavigationService.Navigate(new RolePage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roleDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить эту роль?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Role selected = roleDataGrid.SelectedItem as Role;
                    Utils.db.Role.Remove(selected);
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
                roleDataGrid.ItemsSource = Utils.db.Role.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }
    }
}
