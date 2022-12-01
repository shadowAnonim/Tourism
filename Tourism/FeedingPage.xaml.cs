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
    /// Логика взаимодействия для FeedingPage.xaml
    /// </summary>
    public partial class FeedingPage : Page
    {
        Feeding feeding;
        bool edit = false;
        public FeedingPage(Feeding feeding = null)
        {
            InitializeComponent();
            try
            {
                if (feeding == null)
                {
                    this.feeding = new Feeding();
                }
                else
                {
                    this.feeding = feeding;
                    edit = true;
                }
                grid1.DataContext = this.feeding;
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
                    Utils.db.Feeding.Add(feeding);
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
            }
        }
    }
}
