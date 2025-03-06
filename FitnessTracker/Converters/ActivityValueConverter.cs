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
            else if (value is WeeklyStepData weeklyData)
            {
                if (weeklyData.ActivityType == "Walking" || weeklyData.ActivityType == "Running")
                {
                    return $"{weeklyData.TotalSteps} steps";
                }
                else
                {
                    return $"{weeklyData.TotalCalories} Calories";
                }
            }
            else if(value is FitnessViewModel viewModel)
            {
                if(viewModel.SelectedActivityType == "Walking" || viewModel.SelectedActivityType == "Running")
                {
                    return "Steps";
                }
                else
                {
                    return "Calories";
                }
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
