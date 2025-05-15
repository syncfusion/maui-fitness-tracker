using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FitnessTracker
{
    /// <summary>
    /// Represents a single fitness activity with various metrics like type, start time, end time, and more.
    /// </summary>
    public class FitnessActivity : INotifyPropertyChanged
    {
        private string _activityType = string.Empty;
        private DateTime _startTime;
        private DateTime _endTime;
        private double _caloriesBurned;
        private double _distance;
        private int _steps;
        private int _heartRateAvg;
        private string _title = string.Empty;
        private string _remarks = string.Empty;

        /// <summary>
        /// Gets or sets the type of activity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the start time of the activity.
        /// </summary>
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

        /// <summary>
        /// gets or sets the end time of the activity.
        /// </summary>
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

        /// <summary>
        /// Calculates the duration of the activity in minutes.
        /// </summary>
        public double DurationMinutes => (EndTime - StartTime).TotalMinutes;

        /// <summary>
        /// Gets or sets the number of calories burned during the activity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the distance covered during the activity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the number of steps taken during the activity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the average heart rate during the activity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the title of the activity.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        /// <summary>
        /// Gets or sets the remarks or notes associated with the activity.
        /// </summary>
        public string Remarks
        {
            get => _remarks;
            set
            {
                if (_remarks != value)
                {
                    _remarks = value;
                    OnPropertyChanged(nameof(Remarks));
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Represents a grouped collection of fitness activities for a specific date.
    /// </summary>
    public class FitnessActivityGroup : ObservableCollection<FitnessActivity>
    {
        /// <summary>
        /// Gets or sets the title of the group (e.g., Today, Yesterday, or a specific date).
        /// </summary>
        public string GroupTitle { get; set; }

        /// <summary>
        /// Gets or sets the total steps counted in the group.
        /// </summary>
        public int TotalSteps { get; set; }

        /// <summary>
        /// Gets or sets the total calories burned in the group.
        /// </summary>
        public int TotalCalories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessActivityGroup"/> class with specified group details and activities.
        /// </summary>
        /// <param name="title">The title of the group.</param>
        /// <param name="totalSteps">The total steps in the group.</param>
        /// <param name="totalCalories">The total calories burned in the group.</param>
        /// <param name="activities">The collection of fitness activities.</param>
        public FitnessActivityGroup(string title, int totalSteps, int totalCalories, IEnumerable<FitnessActivity> activities) : base(activities)
        {
            GroupTitle = title;
            TotalSteps = totalSteps;
            TotalCalories = totalCalories;
        }
    }
}
