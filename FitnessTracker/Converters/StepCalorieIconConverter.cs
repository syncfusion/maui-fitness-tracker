using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker
{
    public class StepCalorieIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Determine if the activity type is Running/Walking
            string? activityType = (value as FitnessActivity)?.ActivityType ?? (value as FitnessViewModel)?.SelectedActivityType;
            bool isRunningOrWalking = activityType == "Running" || activityType == "Walking";

            // Determine if the app is in Dark or Light mode
            bool isDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark ||
                               (Application.Current.UserAppTheme == AppTheme.Unspecified &&
                                Application.Current.RequestedTheme == AppTheme.Dark);

            // Select icon and color key based on activity type and theme
            string icon = isRunningOrWalking ? "\ue723" : "\ue720";
            string colorKey = isRunningOrWalking ? (isDarkTheme ? "series-5Dark" : "series-5Light")
                                                 : (isDarkTheme ? "series-3Dark" : "series-3Light");

            // Retrieve color from resources
            if (Application.Current.Resources.TryGetValue(colorKey, out var colorResource) && colorResource is Color iconColor)
            {
                return (parameter as string) == "Color" ? iconColor : icon;
            }

            // Default return values if the color key is not found
            return (parameter as string) == "Color" ? Colors.Transparent : "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
