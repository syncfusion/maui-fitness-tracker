using System.Globalization;

namespace FitnessTracker.Converters
{
    public class DurationToFormattedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is FitnessActivity activity)
            {
                DateTime startTime = activity.StartTime;
                DateTime endTime = activity.EndTime;

                var duration = endTime - startTime;
                if (duration.TotalSeconds <= 0)
                    return "0m";

                int hours = duration.Hours;
                int minutes = duration.Minutes;
                int seconds = duration.Seconds;

                return hours > 0 ? $"{hours}h {minutes}m" : $"{minutes}m {seconds}s";
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
