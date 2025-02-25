namespace FitnessTracker
{
    /// <summary>
    /// 
    /// </summary>
    using System;
    using System.ComponentModel;

    public class FitnessActivity : INotifyPropertyChanged
    {
        private string _activityType;
        private DateTime _startTime;
        private DateTime _endTime;
        private double _caloriesBurned;
        private double _distance;
        private int _steps;
        private int _heartRateAvg;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
