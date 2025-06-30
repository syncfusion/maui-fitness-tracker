using System.Globalization;

namespace FitnessTracker
{
    public class StepCalorieIconConverter : IValueConverter
    {
        /// <summary>
        /// Converts a fitness activity type into an icon and color for UI display, considering the app's theme and activity.
        /// </summary>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? activityType = (value as FitnessActivity)?.ActivityType ?? (value as FitnessViewModel)?.SelectedActivityType;
            bool isRunningOrWalking = activityType == "Running" || activityType == "Walking";
            bool isDarkTheme = Application.Current.RequestedTheme == AppTheme.Dark ||
                               (Application.Current.RequestedTheme == AppTheme.Unspecified &&
                                Application.Current.RequestedTheme == AppTheme.Dark);
            string icon = isRunningOrWalking ? "\ue7f9" : "\ue7f6";
            string colorKey = isRunningOrWalking ? (isDarkTheme ? "series-5Dark" : "series-5Light")
                                                 : (isDarkTheme ? "series-3Dark" : "series-3Light");
            if (Application.Current.Resources.TryGetValue(colorKey, out var colorResource) && colorResource is Color iconColor)
            {
                return (parameter as string) == "Color" ? iconColor : icon;
            }

            return (parameter as string) == "Color" ? Colors.Transparent : "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
