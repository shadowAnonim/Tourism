using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tourism
{
    public static class Utils
    {
        public static mainEntities db = new mainEntities();

        public static void Error(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
