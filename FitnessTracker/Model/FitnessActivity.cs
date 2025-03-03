namespace FitnessTracker
{
    /// <summary>
    /// 
    /// </summary>
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class FitnessActivity : INotifyPropertyChanged
    {
        private string _activityType = string.Empty;
        private DateTime _startTime;
        private DateTime _endTime;
        private double _caloriesBurned;
        private double _distance;
        private int _steps;
        private int _heartRateAvg;
        private string _activityTitle = string.Empty;

        public string ActivityType
        {
            get => _activityType;
            set
            {
                if (_activityType != value)
                {
                    _activityType = value;
                    OnPropertyChanged(nameof(ActivityType));
                }
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    OnPropertyChanged(nameof(StartTime));
                    OnPropertyChanged(nameof(DurationMinutes)); // Update duration if StartTime changes
                }
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    OnPropertyChanged(nameof(EndTime));
                    OnPropertyChanged(nameof(DurationMinutes)); // Update duration if EndTime changes
                }
            }
        }

        public double DurationMinutes => (EndTime - StartTime).TotalMinutes;

        public double CaloriesBurned
        {
            get => _caloriesBurned;
            set
            {
                if (_caloriesBurned != value)
                {
                    _caloriesBurned = value;
                    OnPropertyChanged(nameof(CaloriesBurned));
                }
            }
        }

        public double Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        public int Steps
        {
            get => _steps;
            set
            {
                if (_steps != value)
                {
                    _steps = value;
                    OnPropertyChanged(nameof(Steps));
                }
            }
        }

        public int HeartRateAvg
        {
            get => _heartRateAvg;
            set
            {
                if (_heartRateAvg != value)
                {
                    _heartRateAvg = value;
                    OnPropertyChanged(nameof(HeartRateAvg));
                }
            }
        }

        public string ActivityTitle
        {
            get => _activityTitle;
            set
            {
                if (_activityTitle != value)
                {
                    _activityTitle = value;
                    OnPropertyChanged(nameof(ActivityTitle));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Represents a grouped collection of fitness activities for a specific date.
    /// </summary>
    public class FitnessActivityGroup : ObservableCollection<FitnessActivity>
    {
        public string GroupTitle { get; set; } // Today, Yesterday, or Date
        public int TotalSteps { get; set; }
        public int TotalCalories { get; set; }

        public FitnessActivityGroup(string title, int totalSteps, int totalCalories, IEnumerable<FitnessActivity> activities) : base(activities)
        {
            GroupTitle = title;
            TotalSteps = totalSteps;
            TotalCalories = totalCalories;
        }
    }
}
