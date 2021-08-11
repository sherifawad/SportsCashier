using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.ValueConverters
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class AnyTrueMultiConverter : IMultiValueConverter
    {
        public string Name { get; set; } = "Keep";
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || !targetType.IsAssignableFrom(typeof(bool)))
                return false;

            if (((Button)parameter).Text == Name && values[0] != null)
            {
                if ((bool)values[0])
                    return true;
                else
                    return false;
                //foreach (var value in values)
                //{
                //    if (!(value is bool b))
                //    {
                //        return false;
                //        // Alternatively, return BindableProperty.UnsetValue to use the binding FallbackValue
                //    }
                //    else if (b)
                //    {
                //        return true;
                //    }
                //}
            }
                
            
            return true;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
