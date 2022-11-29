﻿using System;
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
    public partial class ManagerPage : Page
    {

        Manager manager;
        bool edit = false;
        public ManagerPage(Manager manager = null)
        {
            InitializeComponent();
            try
            {
                if (manager == null)
                {
                    this.manager = new Manager();
                }
                else
                {
                    this.manager = manager;
                    edit = true;
                }
                managerGrid.DataContext = this.manager;
                roleIdTextBox.ItemsSource = Utils.db.Role.ToList();
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
                    Utils.db.Manager.Add(manager);
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
            if(!edit)
                roleIdTextBox.SelectedIndex = 0;
        }
    }
}
