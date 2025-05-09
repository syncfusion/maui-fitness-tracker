using System.Globalization;

namespace FitnessTracker
{
    public class StepCalorieIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? activityType = (value as FitnessActivity)?.ActivityType ?? (value as FitnessViewModel)?.SelectedActivityType;
            bool isRunningOrWalking = activityType == "Running" || activityType == "Walking";
            bool isDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark ||
                               (Application.Current.UserAppTheme == AppTheme.Unspecified &&
                                Application.Current.RequestedTheme == AppTheme.Dark);
            string icon = isRunningOrWalking ? "\ue723" : "\ue720";
            string colorKey = isRunningOrWalking ? (isDarkTheme ? "FitnessTrackerSeries5Dark" : "FitnessTrackerSeries5Light")
                                                 : (isDarkTheme ? "FitnessTrackerSeries3Dark" : "FitnessTrackerSeries3Light");
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
