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
    /// Логика взаимодействия для TourPage.xaml
    /// </summary>
    public partial class TourPage : Page
    {
        Tour tour;
        bool edit = false;
        public TourPage(Tour tour = null)
        {
            InitializeComponent();
            try
            {
                if (tour == null)
                {
                    this.tour = new Tour();
                }
                else
                {
                    this.tour = tour;
                    edit = true;
                }
                tourGrid.DataContext = this.tour;
                feeding_idTextBox.ItemsSource = Utils.db.Feeding.ToList();
                hotel_idTextBox.ItemsSource = Utils.db.Hotel.ToList();
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (priceTextBox.Text == "0")
            {
                Utils.Error("Цена не может равняться нулю");
                return;
            }
            if (arrival_dateDatePicker.SelectedDate.Value == departure_dateDatePicker.SelectedDate.Value)
            {
                Utils.Error("Дата прибытия и дата выезда не могут быть одним днём");
                return;
            }
            if (arrival_dateDatePicker.SelectedDate.Value < DateTime.Now)
            {
                Utils.Error("Неверно выбрана дата прибытия");
                return;
            }
            if (departure_dateDatePicker.SelectedDate.Value < arrival_dateDatePicker.SelectedDate.Value)
            {
                Utils.Error("Дата выезда не может быть раньше даты прибытия");
                return;
            }
            try
            {
                if (!edit)
                    Utils.db.Tour.Add(tour);
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
                feeding_idTextBox.SelectedIndex = 0;
                hotel_idTextBox.SelectedIndex = 0;
            }
        }
        }
    }
