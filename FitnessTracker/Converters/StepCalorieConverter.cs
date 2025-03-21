using System.Globalization;

namespace FitnessTracker
{
    public class StepCalorieConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 || values[0] == null || values[1] == null || values[2] == null)
            {
                return "";
            }

            string? selectedActivity = values[0]?.ToString();
            int totalSteps = values[1] is int steps ? steps : 0;
            double totalCalories = values[2] is double calories ? calories : 0;
            bool isWalkingOrRunning = selectedActivity == "Walking" || selectedActivity == "Running";
            if (parameter is string header && header == "Header")
            {
                return isWalkingOrRunning ? "Steps" : "Calories";
            }

            return isWalkingOrRunning ? $"{totalSteps} Steps" : $"{totalCalories} Calories";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
