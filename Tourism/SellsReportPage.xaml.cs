using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Tourism
{
    /// <summary>
    /// Логика взаимодействия для SellsReportPage.xaml
    /// </summary>
    public partial class SellsReportPage : Page
    {
        public SellsReportPage()
        {
            InitializeComponent();
            startDp.SelectedDate = new DateTime(2020, 1, 1);
            endDp.SelectedDate = DateTime.Now;
        }

        void makeReport()
        {
            if (!IsLoaded) return;
            try
            {
                List<Tour> tours = Utils.db.Tour.ToList();
                foreach (Tour tour in tours)
                {
                    tour.Tour_booking = tour.Tour_booking.
                        Where(t => t.Booking.Date >= startDp.SelectedDate.Value
                        && t.Booking.Date <= endDp.SelectedDate.Value).ToList();
                    tour.TourSell = tour.TourSell.
                        Where(t => t.Sell.Date >= startDp.SelectedDate.Value
                        && t.Sell.Date <= endDp.SelectedDate.Value).ToList();
                }
                tourDataGrid.ItemsSource = tours;
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            makeReport();
        }

        private void startDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            makeReport();
        }

        private void csvBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog path = new SaveFileDialog();
            path.Filter = "csv files(*.csv) | *.csv";
            if (path.ShowDialog() != true)
            {
                return;
            }
            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.Append(string.Join(strSeperator, "Название тура", "Количество заказов", "Количество продаж", "\n"));
            foreach (Tour tour in Utils.db.Tour.ToList())
            {
                sbOutput.Append(string.Join(strSeperator, tour.Name, tour.Tour_booking.Count, tour.TourSell.Count, "\n"));
            }
            File.WriteAllText(path.FileName, sbOutput.ToString(), Encoding.UTF8);
        }
    }
}
