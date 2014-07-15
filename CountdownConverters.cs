using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace QuickTimer
{
    public class PercentTimeLeft : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        //TODO Need better error handling.
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double percent = 0;

            try
            {

                double duration = ((TimeSpan)values[0]).Ticks;
                double remaining = ((TimeSpan)values[1]).Ticks;
                percent = 100 * remaining / duration;
            }
            catch
            {
            }

            if (percent >= 0)
            {
                return percent;
            }
            else
            {
                return (double)0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class PercentTimeLeftProgress : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        //TODO Need better error handling.
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double percent = 0;

            try
            {

                double duration = ((TimeSpan)values[0]).Ticks;
                double remaining = ((TimeSpan)values[1]).Ticks;
                percent = remaining / duration;
            }
            catch
            {
            }

            if (percent >= 0)
            {
                return percent;
            }
            else
            {
                return (double)0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
