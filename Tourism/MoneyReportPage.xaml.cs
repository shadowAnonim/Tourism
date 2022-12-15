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
    /// Логика взаимодействия для MoneyReportPage.xaml
    /// </summary>
    public partial class MoneyReportPage : Page
    {
        public MoneyReportPage()
        {
            InitializeComponent();
            try
            {
                moneyLbl.Content = Utils.db.Sell.Sum(s => s.TourSell.Sum(t => t.Price));
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }
    }
}
