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
                    "Walking" => "\ue79f",
                    "Running" => "\ue778",
                    "Cycling" => "\ue7dc",
                    "Yoga" => "\ue7fe",
                    "Swimming" => "\ue7da",
                    "Sleeping" => "\ue7f7",
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
