using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;

namespace FitnessTracker
{
    public class FitnessViewModel : INotifyPropertyChanged
    {
        #region Fields

        int _stepCount;
        double _stepCalorie;
        double _walkDistance;
        double _walkDuration;
        int _heartRate;
        double _sleepHours;
        double _cyclingHours;
        double _runningHours;
        double _yogaDuration;
        double _swimmingDuration;
        double _totalCalories;
        int _selectedTabIndex;
        DateTime _minStartTime;
        DateTime _maxEndTime;
        DateTime _activityTabSelectedDate = DateTime.Today;
        DateTime _selectedDate = DateTime.Today;
        ObservableCollection<WeeklyStepData>? _weeklyStepData;
        MonthCellTemplateSelector? _monthTemplateSelector;
        int _totalSteps;
        string _selectedActivityType = "Walking";
        readonly INavigation _navigation;
        string? _yBindingProperty;
        ObservableCollection<DataPoint>? _cyclingData;
        ObservableCollection<DataPoint>? _sleepingData;
        ObservableCollection<DataPoint>? _weightData;
        ObservableCollection<DataPoint>? _caloriesData;
        ObservableCollection<FAQ>? _faqs;
        ObservableCollection<FitnessActivity>? _walkingList;
        ObservableCollection<FitnessActivity>? _walkingChartList;
        ObservableCollection<FitnessActivityGroup>? _journalData;
        ObservableCollection<Brush>? _chartColor;
        DateTime _journalSelectedDate = DateTime.Today;
        Grid? _selectedActivityGrid;
        bool _isBackIconVisible;

        #endregion

        #region Constructor

        public FitnessViewModel(INavigation navigation, Grid? selectedActivityGrid = null)
        {
            LoadSampleData();
            LoadData();
            LoadJournalData(_journalSelectedDate);
            LoadFAQs();
            SelectActivityCommand = new Command<string>(SelectedActivity);
            IsBackIconVisibleCommand = new Command(() =>
            {
                BackAction?.Invoke();
                IsBackIconVisible = false;
            });
            _navigation = navigation;
            _selectedActivityGrid = selectedActivityGrid;
        }

        #endregion

        #region Commands

        public ICommand SelectActivityCommand { get; }

        public ICommand IsBackIconVisibleCommand { get; }

        #endregion

        #region Public Properties
        bool _isVisible = false;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible)); // Notify UI about the property change
            }
        }
        /// <summary>
        /// Gets or sets the total number of steps taken.
        /// </summary>
        public int StepCount
        {
            get => _stepCount;
            set
            {
                _stepCount = value;
                OnPropertyChanged(nameof(StepCount));
            }
        }

        /// <summary>
        /// Gets or sets the calories burned from steps taken.
        /// </summary>
        public double StepCalorie
        {
            get => _stepCalorie;
            set
            {
                _stepCalorie = value;
                OnPropertyChanged(nameof(StepCalorie));
            }
        }

        /// <summary>
        /// Gets or sets the total distance covered by walking.
        /// </summary>
        public double WalkDistance
        {
            get => _walkDistance;
            set
            {
                _walkDistance = value;
                OnPropertyChanged(nameof(WalkDistance));
            }
        }

        /// <summary>
        /// Gets or sets the total walk duration.
        /// </summary>
        public double WalkDuration
        {
            get => _walkDuration;
            set
            {
                _walkDuration = value;
                OnPropertyChanged(nameof(WalkDuration));
            }
        }

        /// <summary>
        /// Gets or sets the average heart rate in beats per minute (BPM).
        /// </summary>
        public int HeartRate
        {
            get => _heartRate;
            set
            {
                _heartRate = value;
                OnPropertyChanged(nameof(HeartRate));
            }
        }

        /// <summary>
        /// Gets or sets the total sleep duration in hours.
        /// </summary>
        public double SleepHours
        {
            get => _sleepHours;
            set
            {
                _sleepHours = value;
                OnPropertyChanged(nameof(SleepHours));
            }
        }

        /// <summary>
        /// Gets or sets the total number of calories burned.
        /// </summary>
        public double TotalCalories
        {
            get => _totalCalories;
            set
            {
                _totalCalories = value;
                OnPropertyChanged(nameof(TotalCalories));
            }
        }

        /// <summary>
        /// Gets or sets the calories burned from active physical activities.
        /// </summary>
        public double ActiveCalories { get; set; }

        /// <summary>
        /// Gets or sets the calories burned while resting.
        /// </summary>
        public double RestingCalories { get; set; }

        /// <summary>
        /// Gets or sets the cycling duration in hours.
        /// </summary>
        public double CyclingHours
        {
            get => _cyclingHours;
            set
            {
                _cyclingHours = value;
                OnPropertyChanged(nameof(CyclingHours));
            }
        }

        /// <summary>
        /// Gets or sets the running duration in hours.
        /// </summary>
        public double RunningHours
        {
            get => _runningHours;
            set
            {
                _runningHours = value;
                OnPropertyChanged(nameof(RunningHours));
            }
        }

        /// <summary>
        /// Gets or sets the yoga duration in minutes.
        /// </summary>
        public double YogaDuration
        {
            get => _yogaDuration;
            set
            {
                _yogaDuration = value;
                OnPropertyChanged(nameof(YogaDuration));
            }
        }

        /// <summary>
        /// Gets or sets the swimming duration in minutes.
        /// </summary>
        public double SwimmingDuration
        {
            get => _swimmingDuration;
            set
            {
                _swimmingDuration = value;
                OnPropertyChanged(nameof(SwimmingDuration));
            }
        }
        /// <summary>
        /// Gets or sets the body weight in kilograms.
        /// </summary>
        public double CurrentWeight { get; set; }

        /// <summary>
        /// Gets or sets the current date.
        /// </summary>
        public DateTime CurrentDate { get; set; }

        /// <summary>
        /// Gets or sets the total calories burned for all activities.
        /// </summary>
        public double CaloriesBurned { get; set; }

        /// <summary>
        /// Gets or sets the cycling data points.
        /// </summary>
        public ObservableCollection<DataPoint>? CyclingData
        {
            get => _cyclingData;
            set
            {
                _cyclingData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the sleeping data points.
        /// </summary>
        public ObservableCollection<DataPoint>? SleepingData
        {
            get => _sleepingData;
            set
            {
                _sleepingData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the weight data points.
        /// </summary>
        public ObservableCollection<DataPoint>? WeightData
        {
            get => _weightData;
            set
            {
                _weightData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the calories data points.
        /// </summary>
        public ObservableCollection<DataPoint>? CaloriesData
        {
            get => _caloriesData;
            set
            {
                _caloriesData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the FAQ list.
        /// </summary>
        public ObservableCollection<FAQ>? FAQs
        {
            get => _faqs;
            set
            {
                _faqs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the walking list data.
        /// </summary>
        public ObservableCollection<FitnessActivity>? WalkingList
        {
            get => _walkingList;
            set
            {
                _walkingList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the walking chart list data.
        /// </summary>
        public ObservableCollection<FitnessActivity>? WalkingChartList
        {
            get => _walkingChartList;
            set
            {
                _walkingChartList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected tab index.
        /// </summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged();
                    UpdateView();
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum start time for filtering data.
        /// </summary>
        public DateTime MinStartTime
        {
            get { return _minStartTime; }
            set
            {
                _minStartTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum end time for filtering data.
        /// </summary>
        public DateTime MaxEndTime
        {
            get { return _maxEndTime; }
            set
            {
                _maxEndTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the weekly step data.
        /// </summary>
        public ObservableCollection<WeeklyStepData>? WeeklyStepData
        {
            get => _weeklyStepData;
            set
            {
                _weeklyStepData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the template selector for customizing month cell appearance in the calendar.
        /// </summary>
        public MonthCellTemplateSelector? MonthTemplateSelector
        {
            get => _monthTemplateSelector;
            set
            {
                _monthTemplateSelector = value;
                OnPropertyChanged(nameof(MonthTemplateSelector));
            }
        }

        /// <summary>
        /// Gets or sets the selected date in the activity tab.
        /// </summary>
        public DateTime ActivityTabSelectedDate
        {
            get => _activityTabSelectedDate;
            set
            {
                _activityTabSelectedDate = value;
                LoadTodayData(_activityTabSelectedDate);
                OnPropertyChanged(nameof(ActivityTabSelectedDate));
            }
        }

        /// <summary>
        /// Gets or sets the currently selected date.
        /// </summary>
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    UpdateView();
                }
            }
        }

        /// <summary>
        /// Gets or sets the total number of steps taken.
        /// </summary>
        public int TotalSteps
        {
            get => _totalSteps;
            set
            {
                _totalSteps = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected activity type.
        /// </summary>
        public string SelectedActivityType
        {
            get => _selectedActivityType;
            set
            {
                if (_selectedActivityType != value)
                {
                    _selectedActivityType = value;
                    OnPropertyChanged(nameof(SelectedActivityType));
                    UpdateView();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Y-axis binding property used in charts.
        /// </summary>
        public string? YBindingProperty
        {
            get => _yBindingProperty;
            set
            {
                _yBindingProperty = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected journal data.
        /// </summary>
        public ObservableCollection<FitnessActivityGroup>? JournalData
        {
            get => _journalData;
            set
            {
                if (_journalData != value)
                {
                    _journalData = value;
                    OnPropertyChanged(nameof(JournalData));
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected journal date.
        /// </summary>
        public DateTime JournalSelectedDate
        {
            get => _journalSelectedDate;
            set
            {
                _journalSelectedDate = value;
                LoadJournalData(_journalSelectedDate);
                OnPropertyChanged(nameof(JournalSelectedDate));
            }
        }

        /// <summary>
        /// Gets or sets the collection of brushes used for cycling activity.
        /// </summary>
        public ObservableCollection<Brush>? CyclingColor { get; set; }

        /// <summary>
        /// Gets or sets the collection of brushes used for sleep tracking.
        /// </summary>
        public ObservableCollection<Brush>? SleepingColor { get; set; }

        /// <summary>
        /// Gets or sets the collection of brushes used for weight tracking.
        /// </summary>
        public ObservableCollection<Brush>? WeightColor { get; set; }

        /// <summary>
        /// Gets or sets the collection of brushes used for calorie tracking.
        /// </summary>
        public ObservableCollection<Brush>? CaloriesColor { get; set; }

        /// <summary>
        /// Gets or sets the chart colors.
        /// </summary>
        public ObservableCollection<Brush>? ChartColor
        {
            get => _chartColor;
            set
            {
                if(_chartColor != value)
                {
                    _chartColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBackIconVisible
        {
            get => _isBackIconVisible;
            set
            {
                if (_isBackIconVisible != value)
                {
                    _isBackIconVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public Action<View>? SetDesktopContent { get; set; }

        public Action? BackAction { get; set; }

        #endregion

        #region Helper Collections

        /// <summary>
        /// Stores the raw data for all activity types.
        /// </summary>
        public List<FitnessActivity>? Activities { get; set; }

        /// <summary>
        /// Stores daily step count and calories burned, mapped by date.
        /// </summary>
        public Dictionary<DateTime, (int Steps, int Calories)> DailySteps = new Dictionary<DateTime, (int Steps, int Calories)>();

        /// <summary>
        /// Stores activity colors for both light and dark themes, mapped by activity type.
        /// </summary>
        readonly Dictionary<string, (string Light, string Dark)> ActivityColors = new()
        {
            { "Walking", ("#116DF9", "#BF3B49") },
            { "Running", ("#2196F3", "#0D71C1") },
            { "Cycling", ("#F4890B", "#DA9646") },
            { "Swimming", ("#E2227E", "#C9588E") },
            { "Yoga", ("#00E190", "#588249") },
            { "Sleeping", ("#9215F3", "#8B40C6") }
        };

        #endregion

        #region Private Methods

        void LoadSampleData()
        {
            // Sample Data
            var random = new Random();
            Activities = new List<FitnessActivity>();
            string[] activityTypes = { "Walking", "Running", "Yoga", "Cycling", "Sleeping", "Swimming" };
            const int numberOfEntries = 500;

            // Track days where sleep is already added
            var sleepDays = new HashSet<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                var sleepDate = DateTime.Today.AddDays(-i);
                var sleepStart = sleepDate.AddHours(random.Next(22, 23));
                var sleepEnd = sleepStart.AddHours(random.Next(5, 8));
                Activities.Add(new FitnessActivity
                {
                    ActivityType = "Sleeping",
                    StartTime = sleepStart,
                    EndTime = sleepEnd,
                    CaloriesBurned = random.Next(250, 450),
                    Distance = 0,
                    Steps = 0,
                    HeartRateAvg = random.Next(45, 65),
                    Title = GenerateActivityLabel("Sleeping", sleepStart),
                    Remarks = GenerateDescription("Sleeping")
                });

                sleepDays.Add(sleepDate.Date);
            }

            for (int i = 0; i < numberOfEntries; i++)
            {
                var randomIndex = random.Next(activityTypes.Length);
                var activityType = activityTypes[randomIndex];

                // Generate random start and end times within the last 24 hours
                var day = -random.Next(0, 30);
                var date = DateTime.Today.AddDays(day);
                var endTime = DateTime.Now.AddDays(day).AddMinutes(-random.Next(0, 1440));
                var startTime = endTime.AddMinutes(-random.Next(30, 120)); // Duration between 30 mins and 2 hours

                // Ensure sleep entry is added only once per day
                if (activityType == "Sleeping" && sleepDays.Contains(date.Date))
                {
                    continue;
                }

                if (activityType == "Sleeping")
                {
                    sleepDays.Add(date.Date);
                    endTime = date.AddHours(random.Next(22, 30)); // Sleep typically happens at night
                    startTime = endTime.AddHours(-random.Next(5, 8)); // 5 to 8 hours duration
                }

                if (activityType == "Swimming")
                {
                    endTime = startTime.AddMinutes(random.Next(20, 31)); // Swimming time between 20 to 60 minutes
                }

                var activityTitleLabel = GenerateActivityLabel(activityType, startTime);
                var remarks = GenerateDescription(activityType);
                Activities.Add(new FitnessActivity
                {
                    ActivityType = activityType,
                    StartTime = startTime,
                    EndTime = endTime,
                    CaloriesBurned = randomIndex switch
                    {
                        0 => random.Next(100, 200), // Walking
                        1 => random.Next(300, 500), // Running
                        2 => random.Next(50, 150),  // Yoga
                        3 => random.Next(250, 600), // Cycling
                        4 => random.Next(250, 450), // Sleeping
                        5 => random.Next(100, 300), // Swimming
                        _ => 0
                    },
                    Distance = activityType switch
                    {
                        "Yoga" or "Sleeping" => 0,
                        "Swimming" => random.NextDouble() * (2.0 - 0.5) + 0.5, // Swimming distance in km (0.5 - 2.0 km)
                        _ => random.NextDouble() * (10.0 - 1.0) + 1.0 // General distance for other activities
                    },
                    Steps = activityType switch
                    {
                        "Yoga" or "Cycling" or "Swimming" => 0, // No steps for Yoga, Cycling, or Swimming
                        _ => random.Next(1000, 10000)
                    },
                    HeartRateAvg = activityType switch
                    {
                        "Walking" => random.Next(80, 100),
                        "Running" => random.Next(90, 110),
                        "Yoga" => random.Next(60, 90),
                        "Cycling" => random.Next(90, 110),
                        "Sleeping" => random.Next(45, 65),
                        "Swimming" => random.Next(85, 110),
                        _ => 0
                    },
                    Title = activityTitleLabel,
                    Remarks = remarks
                });
            }
        }

        string GenerateActivityLabel(string activityType, DateTime startTime)
        {
            string timeOfDay = startTime.Hour switch
            {
                >= 5 and < 12 => "Morning",
                >= 12 and < 17 => "Afternoon",
                >= 17 and < 21 => "Evening",
                _ => "Night"
            };

            return timeOfDay + " " + activityType;
        }

        string GenerateDescription(string activityType)
        {
            string remarks = activityType switch
            {
                "Walking" => "A refreshing way to stay active.",
                "Running" => "A great way to boost endurance.",
                "Yoga" => "A peaceful session for mind and body.",
                "Cycling" => "A fun and effective cardio workout.",
                "Sleeping" => "A good night's sleep is essential for recovery.",
                "Swimming" => "A full-body workout that builds strength and endurance.",
                _ => ""
            };

            return remarks;
        }

        void LoadData()
        {
            #region Brush collections

            CyclingColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5"))
            };

            SleepingColor = new ObservableCollection<Brush>()
            {
                new LinearGradientBrush
                {
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop { Color = Color.FromArgb("#8618FC").WithAlpha(0.5f), Offset = 0.0f }, // Top color
                        new GradientStop { Color = Color.FromArgb("#8618FC").WithAlpha(0.0f), Offset = 1.0f } // Faded bottom
                    },
                    StartPoint = new Point(0, 0), // Start at the top
                    EndPoint = new Point(0, 1)   // End at the bottom
                }
            };

            WeightColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739"))
            };

            CaloriesColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA"))
            };

            #endregion

            #region Dummay chart data

            LoadCyclingData();
            LoadSleepingData();
            LoadWeightData();
            LoadCaloriesData();
            LoadWalkingData();
            LoadTodayData(DateTime.Today);

            #endregion
        }

        void LoadWalkingData()
        {
            UpdateView();
        }
        void LoadJournalData(DateTime date)
        {
            var groupedActivities = Activities?
                                    .Where(a => a.StartTime.Date <= date.Date)
                                    .GroupBy(a => a.StartTime.Date)
                                    .OrderByDescending(g => g.Key)
                                    .Select(g =>
                                    {
                                        string title = g.Key == date.Date ? "Today" :
                                               g.Key ==date.Date.AddDays(-1) ? "Yesterday" :
                                               g.Key.ToString("ddd, dd MMM");

                                        int totalSteps = g.Sum(a => a.Steps);
                                        int totalCalories = (int)g.Sum(a => a.CaloriesBurned);

                                        // Sort activities within each group in descending order by StartTime
                                        var sortedActivities = g.OrderByDescending(a => a.StartTime);

                                        return new FitnessActivityGroup(title, totalSteps, totalCalories, sortedActivities);
                                    });

            JournalData = new ObservableCollection<FitnessActivityGroup>(groupedActivities!);
        }

        void UpdateView()
        {
            UpdateChartColor();
            if (SelectedTabIndex == 0)
            {
                UpdateDayView();
            }
            else if (SelectedTabIndex == 1)
            {
                UpdateWeekView();
            }
            else if(SelectedTabIndex == 2)
            {
                UpdateMonthView();
            }
        }

        void UpdateWeekView()
        {
            var today = SelectedDate.Date;
            var currentWeekStart = today.AddDays(-(int)today.DayOfWeek); // Get Sunday of the current week
            var currentWeekEnd = currentWeekStart.AddDays(6); // Get Saturday of the current week
            var groupedData = Activities?.Where(d => d.StartTime.Date >= currentWeekStart && d.EndTime.Date <= currentWeekEnd && d.ActivityType == SelectedActivityType)
                                .GroupBy(d => d.StartTime.Date)
                                .ToDictionary(g => g.Key, g => new
                                {
                                    TotalSteps = g.Sum(d => d.Steps),
                                    TotalDuration = TimeSpan.FromMinutes(g.Sum(d => (d.EndTime - d.StartTime).TotalMinutes)), // Sum up duration
                                    TotalCalories = g.Sum(d => d.CaloriesBurned)
                                });

            // Generate complete week data, filling missing days with Steps = 0
            var weeklyData = Enumerable.Range(0, 7)
                                       .Select(offset =>
                                       {
                                           var date = currentWeekStart.AddDays(offset);
                                           bool hasData = groupedData!.ContainsKey(date);

                                           var totalDuration = hasData ? groupedData[date].TotalDuration : TimeSpan.Zero;
                                           var startTime = date;
                                           var endTime = startTime.Add(totalDuration); // EndTime = StartTime + Summed Duration

                                           return new FitnessActivity
                                           {
                                               StartTime = startTime,
                                               EndTime = endTime, // Corrected duration-based end time
                                               Steps = hasData ? groupedData[date].TotalSteps : 0,
                                               CaloriesBurned = hasData ? groupedData[date].TotalCalories : 0,
                                               ActivityType = SelectedActivityType
                                           };
                                       })
                                       .OrderBy(d => d.StartTime) // Ensure ordering from Sunday to Saturday
                                       .ToList();

            var weeklyCollectionData = (currentWeekEnd <= DateTime.Today)
                                        ? weeklyData.OrderByDescending(a => a.StartTime.Date).ToList() // Show full week if it’s in the past
                                        : weeklyData.Where(d => d.StartTime.Date <= DateTime.Today) // Limit current week data up to today
                                                    .OrderByDescending(a => a.StartTime.Date)
                                                    .ToList();

            WalkingList = new ObservableCollection<FitnessActivity>(weeklyCollectionData);
            WalkingChartList = new ObservableCollection<FitnessActivity>(weeklyData);
            TotalSteps = WalkingList.Count > 0 ? WalkingList.Sum(a => a.Steps) : 0;
            TotalCalories = WalkingList.Count > 0 ? WalkingList.Sum(a => a.CaloriesBurned) : 0;
        }

        void UpdateDayView()
        {
            var today = SelectedDate;
            var dayData = Activities?.Where(d => d.StartTime.Date == today && d.ActivityType == SelectedActivityType)
                .OrderByDescending(d => d.StartTime) // Sort by most recent activity first
                .ToList();

            var chartData = dayData?
                .GroupBy(d => d.StartTime.Date) // Group by hours instead of full date
                .SelectMany(g => g.Select(d => new FitnessActivity
                {
                    StartTime = d.StartTime, // Retain one representative time
                    EndTime = d.EndTime, // Get max end time in the group
                    Steps =  d.Steps, // Sum steps for that hour
                    CaloriesBurned = d.CaloriesBurned
                }))
                .OrderBy(d => d.StartTime)
                .ToList();

            WalkingList = new ObservableCollection<FitnessActivity>(dayData!);
            WalkingChartList = new ObservableCollection<FitnessActivity>(chartData!);
            if(WalkingChartList.Any())
            {
                MinStartTime = today.Date; // Ensures start from 12:00 AM
                MaxEndTime = today.Date.AddDays(1); // Ends at 11:59 PM
            }

            TotalSteps = WalkingList.Count > 0 ? WalkingList.Sum(a => a.Steps) : 0;
            TotalCalories = WalkingList.Count > 0 ? WalkingList.Sum(a => a.CaloriesBurned) : 0;
        }

        void UpdateMonthView()
        {
            UpdateMonthData();
            DateTime date = SelectedDate.Date;
            WeeklyStepData = new ObservableCollection<WeeklyStepData>();
            int totalDays = DateTime.DaysInMonth(date.Year, date.Month);

            // Determine the first and last day of the month
            DateTime firstDay = new DateTime(date.Year, date.Month, 1);
            DateTime lastDay = new DateTime(date.Year, date.Month, totalDays);

            // Find the first Sunday before or on the first day of the month
            DateTime startOfWeek = firstDay.AddDays(-(int)firstDay.DayOfWeek);

            // Iterate through full weeks in the month
            while (startOfWeek <= lastDay)
            {
                DateTime endOfWeek = startOfWeek.AddDays(6); // End of the week (Saturday)

                // Sum steps only for the days within this month
                int weeklySteps = 0;
                int weeklyCalories = 0;
                for (DateTime d = startOfWeek; d <= endOfWeek; d = d.AddDays(1))
                {
                    if (DailySteps.ContainsKey(d))
                    {
                        weeklySteps += DailySteps[d].Steps;
                        weeklyCalories += DailySteps[d].Calories;
                    }
                }

                // Add the week data
                WeeklyStepData.Add(new WeeklyStepData
                {
                    WeekRange = $"{startOfWeek:dd MMMM} - {endOfWeek:dd MMMM}",
                    TotalSteps = weeklySteps,
                    TotalCalories = weeklyCalories,
                    ActivityType = SelectedActivityType
                });

                startOfWeek = startOfWeek.AddDays(7);
            }

            TotalSteps = WeeklyStepData.Count > 0 ? WeeklyStepData.Sum(a => a.TotalSteps) : 0;
            TotalCalories = WeeklyStepData.Count > 0 ? WeeklyStepData.Sum(a => a.TotalCalories) : 0;
            UpdateMonthTemplate();
        }


        void UpdateMonthData()
        {
            // Get the first and last day of the selected month
            var firstDay = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1); // Last day of the month

            // Filter data based on selected month and activities
            var monthlyData = Activities?
                .Where(d => d.StartTime.Date >= firstDay && d.StartTime.Date <= lastDay && d.ActivityType == SelectedActivityType)
                .GroupBy(d => d.StartTime.Date) // Group by day
                .ToDictionary(g => g.Key, g => (
                    TotalSteps: g.Sum(d => d.Steps),
                    TotalCalories: (int)g.Sum(d => d.CaloriesBurned)
                ));

            DailySteps = new Dictionary<DateTime, (int Steps, int Calories)>(monthlyData!);
        }

        void UpdateMonthTemplate()
        {
            MonthTemplateSelector = new MonthCellTemplateSelector
            {
                ViewModel = this,
                IntenseStepCountTemplate = MonthTemplate(80),
                HighStepCountTemplate = MonthTemplate(60),
                MediumStepCountTemplate = MonthTemplate(45),
                LowStepCountTemplate = MonthTemplate(25),
                DefaultStepCountTemplate = MonthTemplate(15)
            };
        }

        void SelectedActivity(string activityType)
        {
            SelectedActivityType = activityType;
            switch (activityType)
            {
                case "Walking":
                case "Running":
                    YBindingProperty = "Steps";      // Bind to Steps
                    break;

                case "Swimming":
                case "Cycling":
                case "Yoga":
                case "Sleeping":
                    YBindingProperty = "CaloriesBurned"; // Bind to Calories Burned
                    break;

                default:
                    YBindingProperty = "Steps";
                    break;
            }

#if IOS || ANDROID
            _navigation.PushAsync(new ActivityCustomViewPageMobile(this));
#elif MACCATALYST || WINDOWS
            _selectedActivityGrid?.Children.Add(new ActivityCustomViewContentDesktop());
            SetDesktopContent?.Invoke(new ActivityCustomViewContentDesktop());
            IsBackIconVisible = true;
            BackAction = () =>
            {
                HideBackIcon();
                SetDesktopContent?.Invoke(new ActivityPageContentDesktop());
            };
#endif
        }

        void HideBackIcon()
        {
            IsBackIconVisible = false;
            _selectedActivityGrid?.Children.Clear();
            _selectedActivityGrid?.Children.Add(new ActivityPageContentDesktop());
        }

        public void UpdateChartColor()
        {
            if (ActivityColors.TryGetValue(SelectedActivityType, out var colorPair))
            {
                string selectedColor = (Application.Current.RequestedTheme == AppTheme.Dark) ? colorPair.Dark : colorPair.Light;
                ChartColor = new ObservableCollection<Brush>
                {
                    new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor))
                };
            }
        }

        void LoadCyclingData()
        {
            var sevenDaysAgo = DateTime.Today.AddDays(-6); // Start from 6 days before today
            var orderedDays = Enumerable.Range(0, 7) // Generate last 7 days
                                        .Select(i => sevenDaysAgo.AddDays(i))
                                        .ToList();

            CyclingData = new ObservableCollection<DataPoint>(
                orderedDays
                .Select(day => new DataPoint
                {
                    Label = day.ToString("ddd"), // Format as "Wed", "Thu", etc.
                    Value = Activities!
                        .Where(a => a.ActivityType == "Cycling" && a.StartTime.Date == day.Date)
                        .Sum(a => a.DurationMinutes / 60) // Sum up distances per day
                })
            );
        }

        void LoadCaloriesData()
        {
            var sevenDaysAgo = DateTime.Today.AddDays(-6); // Start from 6 days before today
            var orderedDays = Enumerable.Range(0, 7) // Generate last 7 days
                                        .Select(i => sevenDaysAgo.AddDays(i))
                                        .ToList();

            CaloriesData = new ObservableCollection<DataPoint>(
                orderedDays
                .Select(day => new DataPoint
                {
                    Label = day.ToString("ddd"), // Format as "Wed", "Thu", etc.
                    Value = Activities!
                        .Where(a => a.StartTime.Date == day) // Get all activities for the day
                        .Sum(a => a.CaloriesBurned) // Sum up calories burned per day
                })
            );
        }

        void LoadSleepingData()
        {
            var sevenDaysAgo = DateTime.Today.AddDays(-6); // Start from 6 days before today
            var orderedDays = Enumerable.Range(0, 7) // Generate last 7 days
                                        .Select(i => sevenDaysAgo.AddDays(i))
                                        .ToList();

            SleepingData = new ObservableCollection<DataPoint>(
                orderedDays
                .Select(day => new DataPoint
                {
                    Label = day.ToString("ddd"), // Format as "Mon", "Tue", etc.
                    Value = Activities!
                        .Where(a => a.ActivityType == "Sleeping" && a.StartTime.Date == day.Date)
                        .Sum(a => a.DurationMinutes / 60) // Convert minutes to hours and sum up
                })
            );
        }

        void LoadWeightData()
        {
            var sixMonthsAgo = DateTime.Today.AddMonths(-5); // Start from 5 months ago
            var orderedMonths = Enumerable.Range(0, 6) // Generate last 6 months
                                          .Select(i => sixMonthsAgo.AddMonths(i))
                                          .ToList();

            var random = new Random();
            double initialWeight = random.Next(55, 65); // Start with a realistic weight

            WeightData = new ObservableCollection<DataPoint>(
                orderedMonths.Select(month =>
                {
                    // Fluctuate weight slightly (+/- 1 kg per month)
                    initialWeight += random.NextDouble() * 6 - 3; // Variance between -1kg to +1kg

                    return new DataPoint
                    {
                        Label = month.ToString("MMM"), // Format as "Jan", "Feb", etc.
                        Value = Math.Round(initialWeight, 1) // Keep 1 decimal place
                    };
                })
            );
        }

        void LoadTodayData(DateTime date)
        {
            // Summarizing today's data (e.g., total steps, calories, etc.)
            var todayActivities = Activities?.Where(a => a.StartTime.Date == date).ToList();

            if (todayActivities is not null && todayActivities.Any())
            {
                var walkingActivities = todayActivities.Where(a => a.ActivityType == "Walking").ToList();
                var runningActivities = todayActivities.Where(a => a.ActivityType == "Running").ToList();
                var cyclingActivities = todayActivities.Where(a => a.ActivityType == "Cycling").ToList();
                var sleepingActivity = todayActivities.FirstOrDefault(a => a.ActivityType == "Sleeping");
                var yogaActivities = todayActivities.Where(a => a.ActivityType == "Yoga").ToList();
                var swimmingActivities = todayActivities.Where(a => a.ActivityType == "Swimming").ToList();

                StepCount = walkingActivities.Sum(a => a.Steps);
                StepCalorie = walkingActivities.Sum(a => a.CaloriesBurned);
                WalkDistance = walkingActivities.Sum(a => a.Distance);
                WalkDuration = walkingActivities.Any() ? walkingActivities.Sum(a => (a.EndTime - a.StartTime).TotalMinutes) : 0;
                HeartRate = todayActivities.Any() ? (int)todayActivities.Average(a => a.HeartRateAvg) : 0;
                SleepHours = sleepingActivity != null ? (sleepingActivity.EndTime - sleepingActivity.StartTime).TotalHours : 0;
                CyclingHours = cyclingActivities.Any() ? cyclingActivities.Sum(a => (a.EndTime - a.StartTime).TotalHours) : 0;
                CaloriesBurned = todayActivities.Sum(a => a.CaloriesBurned);
                TotalCalories = CaloriesData!.Sum(a => a.Value);
                ActiveCalories = (int)(TotalCalories * 0.35);
                RestingCalories = TotalCalories - ActiveCalories;
                CurrentDate = date;
                CurrentWeight = WeightData!.FirstOrDefault(w => w.Label == DateTime.Today.ToString("MMM"))?.Value ?? 0;
                RunningHours = runningActivities.Any() ? runningActivities.Sum(a => (a.EndTime - a.StartTime).TotalHours) : 0;
                YogaDuration = yogaActivities.Any() ? yogaActivities.Sum(a => (a.EndTime - a.StartTime).TotalMinutes) : 0;
                SwimmingDuration = swimmingActivities.Any() ? swimmingActivities.Sum(a => (a.EndTime - a.StartTime).TotalMinutes) : 0;
            }
        }

        void LoadFAQs()
        {
            FAQs = new ObservableCollection<FAQ>
            {
                new FAQ { Question = "How do I reset my step goal?", Answer = "Open the fitness app. Navigate to Goals and select Step Goal. Enter a new goal and save your changes." },
                new FAQ { Question = "Why is my calorie count inaccurate?", Answer = "Inaccuracies may arise from incorrect personal data, device calibration issues, or estimation errors. Double-check your profile settings and recalibrate the device if needed." },
                new FAQ { Question = "How can I track my sleep manually?", Answer = "In the app, go to the Sleep section. Select Add Sleep and enter your sleep and wake times to log your rest." },
                new FAQ { Question = "How do I sync my fitness tracker with Google Fit?", Answer = "Open the app settings, find the Connected Apps option, and select Google Fit. Follow the instructions to link your account and enable synchronization." },
            };
        }

        DataTemplate MonthTemplate(int opacity)
        {
            var template = new DataTemplate(() =>
            {
                Grid grid = new Grid();

                Border border = new Border();
                border.StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(25)
                };

                var color = ActivityColors.TryGetValue(SelectedActivityType, out var colorPair);
                string? selectedColor = (Application.Current.RequestedTheme == AppTheme.Dark) ? colorPair.Dark : colorPair.Light;
                selectedColor = selectedColor?.Substring(1);
                string opacityColor = "#" + opacity + selectedColor;
                border.Background = Color.FromArgb(opacityColor);
                border.StrokeThickness = 0;

                Label label = new Label();
                label.SetBinding(Label.TextProperty, "Date.Day");
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;
                label.Padding = new Thickness(2);
                border.Content = label;

                grid.Add(border);
                grid.Padding = new Thickness(1);

                return grid;
            });

            return template;
        }

#endregion

        #region PropertyChanged

        /// <summary>
        /// Notifies when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
