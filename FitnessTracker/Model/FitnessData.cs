using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class DataPoint
    {
        public string Label { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class WalkingData
    {
        public DateTime Date { get; set; }
        public string DayPrefix => Date.ToString("ddd");
        public int Steps { get; set; }

        public DateTime StartTime { get; set; } // User-provided later
        public DateTime EndTime { get; set; }   // User-provided later

        public string Duration => CalculateDuration(); // Auto-calculated

        public int WeekNumber => CultureInfo.CurrentCulture.Calendar
            .GetWeekOfYear(Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        public int Year => Date.Year;
        public string Label { get; set; } = string.Empty;
        public string Time => StartTime.ToString("hh:mm tt").ToLower();

        private string CalculateDuration()
        {
            if(EndTime > StartTime)
            {
                TimeSpan duration = EndTime - StartTime;
                int hours = (int)duration.TotalHours;
                int minutes = duration.Minutes;
                int seconds = duration.Seconds;

                if (hours > 0)
                {
                    return $"{hours}h {minutes}m"; // Display hours and minutes, omit seconds
                }

                return $"{minutes}m {seconds}s"; // Display only minutes and seconds if no hours
            }

            return "0h 0m 0s"; // Default value if times are invalid
        }
    }

    public class JournalData
    {
        public DateTime Date { get; set; }
        public string Time => Date.ToString("hh:mm tt").ToLower();
        public string Name { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Calories { get; set; }
        public int Steps { get; set; }
        public string CaloriesDisplay => Calories > 0 ? $"{Calories} Cal" : "";
    }

    public class JournalGroup : ObservableCollection<JournalData>
    {
        public string GroupTitle { get; set; } // Today, Yesterday, or Date
        public int TotalSteps { get; set; }
        public int TotalCalories { get; set; }

        public JournalGroup(string title, int totalSteps, int totalCalories, IEnumerable<JournalData> activities) : base(activities)
        {
            GroupTitle = title;
            TotalSteps = totalSteps;
            TotalCalories = totalCalories;
        }
    }

    public class FAQ
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public bool IsExpanded { get; set; } = false;
    }

    public class WeeklyStepData
    {
        public string WeekRange { get; set; } = string.Empty;
        public int TotalSteps { get; set; }

    }
}

