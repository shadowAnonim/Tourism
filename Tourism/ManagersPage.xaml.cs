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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagersPage : Page
    {
        public ManagersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                managerDataGrid.ItemsSource = Utils.db.Manager.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (managerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите управляющего");
                return;
            }
            Manager selected = managerDataGrid.SelectedItem as Manager;
            NavigationService.Navigate(new ManagerPage(selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (managerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите управляющего");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этого управляющего?",
                "Подтвердите удаление", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager selected = managerDataGrid.SelectedItem as Manager;
                    Utils.db.Manager.Remove(selected);
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
