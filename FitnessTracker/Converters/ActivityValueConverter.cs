using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker
{
    public class ActivityValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is FitnessActivity activity)
            {
                if (activity.ActivityType == "Walking" || activity.ActivityType == "Running")
                {
                    return $"{activity.Steps} Steps";
                }
                else
                {
                    return $"{activity.CaloriesBurned} Calories";
                }
            }
            else if (value is WeeklyStepData weeklyData && parameter is string selectedActivity)
            {
                if (selectedActivity == "Walking" || selectedActivity == "Running")
                    return $"{weeklyData.TotalSteps} steps";
                else
                    return $"{weeklyData.TotalCalories} Calories";
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
