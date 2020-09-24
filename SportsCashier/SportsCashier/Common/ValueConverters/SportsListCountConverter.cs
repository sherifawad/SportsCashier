using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SportsCashier.ValueConverters
{
    public class SportsListCountConverter : BaseValueConverter<SportsListCountConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<Sport> sportssList)
            {
                var c = sportssList?.Count();

                if (c == null)
                    return 0;
                else
                    return c;

            }

            return 0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
