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
using System.Windows.Forms.DataVisualization.Charting;
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
        Series bookingSeries;
        Series sellSeries;
        public SellsReportPage()
        {
            InitializeComponent();

            startDp.SelectedDate = new DateTime(2020, 1, 1);
            endDp.SelectedDate = DateTime.Now;

            bookingChart.ChartAreas.Add("Main");
            bookingSeries = bookingChart.Series.Add("Tours");
            bookingSeries.ChartType = SeriesChartType.Column;

            sellChart.ChartAreas.Add("Main");
            sellSeries = sellChart.Series.Add("Tours");
            sellSeries.ChartType = SeriesChartType.Column;
        }

        void makeReport()
        {
            if (!IsLoaded) return;
            try
            {
                bookingSeries.Points.Clear();
                sellSeries.Points.Clear();
                List<Tour> tours = Utils.db.Tour.ToList();
                foreach (Tour tour in tours)
                {
                    tour.SetPeriod(startDp.SelectedDate.Value, endDp.SelectedDate.Value);
                    bookingSeries.Points.AddXY(tour.Name, tour.periodBooking.Count);
                    sellSeries.Points.AddXY(tour.Name, tour.periodSell.Count);
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

        private void pdfBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                pdfBtn.Visibility = Visibility.Hidden;
                csvBtn.Visibility = Visibility.Hidden;
                print.PrintVisual(mainGrid, "");
                pdfBtn.Visibility = Visibility.Visible;
                csvBtn.Visibility = Visibility.Visible;
            }
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
