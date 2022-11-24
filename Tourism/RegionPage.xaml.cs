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
    /// Логика взаимодействия для RegionPage.xaml
    /// </summary>
    public partial class RegionPage : Page
    {
        Region region;
        bool edit = false;
        public RegionPage(Region region = null)
        {
            InitializeComponent();
            try
            {
                if (region == null)
                {
                    this.region = new Region();
                }
                else
                {
                    this.region = region;
                    edit = true;
                }
                regionGrid.DataContext = this.region;
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
                    Utils.db.Region.Add(region);
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
