using System;
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

            double duration = ((TimeSpan)values[0]).Ticks;
            double remaining = ((TimeSpan)values[1]).Ticks;
            percent = duration > 0 ? 100 * remaining / duration : 0.0;

            return percent >= 0 ? percent : 0.0;
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

            double duration = ((TimeSpan)values[0]).Ticks;
            double remaining = ((TimeSpan)values[1]).Ticks;
            percent = duration > 0 ? remaining / duration : 0.0;

            return percent > 0 ? percent : 1.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
