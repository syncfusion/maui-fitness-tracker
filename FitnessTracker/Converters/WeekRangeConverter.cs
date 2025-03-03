using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Converters
{
    public class WeekRangeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime selectedDate)
            {
                DateTime startOfWeek = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); // Sunday start
                DateTime endOfWeek = startOfWeek.AddDays(6); // Saturday end

                return $"{startOfWeek:dd MMMM} - {endOfWeek:dd MMMM}";
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
