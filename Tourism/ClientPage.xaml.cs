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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        Client client;
        bool edit = false;
        public ClientPage(Client client = null)
        {
            InitializeComponent();
            try
            {
                if (client == null)
                {
                    this.client = new Client();
                }
                else
                {
                    this.client = client;
                    edit = true;
                }
                clientGrid.DataContext = this.client;
                type_idTextBox.ItemsSource = Utils.db.Client_type.ToList();
                manager_idTextBox.ItemsSource = Utils.db.Manager.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!edit)
                    Utils.db.Client.Add(client);
                Utils.db.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!edit)
            {
                type_idTextBox.SelectedIndex = 0;
                manager_idTextBox.SelectedIndex = 0;
            }
        }
    }
}
