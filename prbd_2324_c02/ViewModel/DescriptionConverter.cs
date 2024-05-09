using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace prbd_2324_c02.ViewModel
{
    public class DescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            // Vérifiez si la valeur est null
            if (value == null) {
                // Si oui, retournez un message alternatif
                return "no description";
            }
            // Sinon, retournez la valeur d'origine
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
