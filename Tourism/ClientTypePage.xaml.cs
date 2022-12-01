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
    /// Логика взаимодействия для ClientTypePage.xaml
    /// </summary>
    public partial class ClientTypePage : Page
    {
        Client_type client_Type;
        bool edit = false;
        public ClientTypePage(Client_type client_Type = null)
        {
            InitializeComponent();
            try
            {
                if (client_Type == null)
                {
                    this.client_Type = new Client_type();
                }
                else
                {
                    this.client_Type = client_Type;
                    edit = true;
                }
                grid1.DataContext = this.client_Type;
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
                    Utils.db.Client_type.Add(client_Type);
                Utils.db.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }
    }
}
