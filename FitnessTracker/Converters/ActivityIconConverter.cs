using System.Globalization;

namespace FitnessTracker
{
    public class ActivityIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is FitnessActivity activity)
            {
                return activity.ActivityType switch
                {
                    "Walking" => "\ue718",
                    "Running" => "\ue71c",
                    "Cycling" => "\ue719",
                    "Yoga" => "\ue728",
                    "Swimming" => "\ue71a",
                    "Sleeping" => "\ue721",
                    _ => ""
                };
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
