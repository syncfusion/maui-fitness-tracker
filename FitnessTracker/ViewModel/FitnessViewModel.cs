using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Templates;
using FitnessTracker.Views;
using Microsoft.Maui.Controls.Shapes;

namespace FitnessTracker
{
    public class FitnessViewModel : INotifyPropertyChanged
    {
        #region Current day data

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
        public double TotalCalories { get; set; }

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

        #endregion

        public List<FitnessActivity> Activities { get; set; }

        #region Dummy chart data
        ObservableCollection<DataPoint>? _cyclingData;
        ObservableCollection<DataPoint>? _sleepingData;
        ObservableCollection<DataPoint>? _weightData;
        ObservableCollection<DataPoint>? _caloriesData;
        ObservableCollection<FAQ> _faqs;
        ObservableCollection<FitnessActivity>? _walkingList;
        ObservableCollection<FitnessActivity>? _walkingChartList;

        public ObservableCollection<DataPoint>? CyclingData
        {
            get => _cyclingData;
            set
            {
                _cyclingData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint>? SleepingData
        {
            get => _sleepingData;
            set
            {
                _sleepingData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint>? WeightData
        {
            get => _weightData;
            set
            {
                _weightData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint>? CaloriesData
        {
            get => _caloriesData;
            set
            {
                _caloriesData = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<FAQ>? FAQs
        {
            get => _faqs;
            set
            {
                _faqs = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FitnessActivity>? WalkingList
        {
            get => _walkingList;
            set
            {
                _walkingList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FitnessActivity>? WalkingChartList
        {
            get => _walkingChartList;
            set
            {
                _walkingChartList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Brush
        public ObservableCollection<Brush>? CyclingColor { get; set; }
        public ObservableCollection<Brush>? SleepingColor { get; set; }
        public ObservableCollection<Brush>? WeightColor { get; set; }
        public ObservableCollection<Brush>? CaloriesColor { get; set; }
        public ObservableCollection<Brush>? WalkingColor { get; set; }
        #endregion

        #region For activity related

        int _selectedTabIndex;
        DateTime _minStartTime;
        DateTime _maxEndTime;
        DateTime _activityTabSelectedDate = DateTime.Today;
        DateTime _selectedDate = DateTime.Today;
        ObservableCollection<WeeklyStepData> _weeklyStepData;
        MonthCellTemplateSelector? _monthTemplateSelector;
        int _totalSteps;
        string _selectedActivityType = "Walking";
        readonly INavigation _navigation;
        string _yBindingProperty;

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

        
        public DateTime MinStartTime
        {
            get { return _minStartTime; }
            set
            {
                _minStartTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime MaxEndTime
        {
            get { return _maxEndTime; }
            set
            {
                _maxEndTime = value;
                OnPropertyChanged();
            }
        }

        
        public ObservableCollection<WeeklyStepData> WeeklyStepData
        {
            get => _weeklyStepData;
            set
            {
                _weeklyStepData = value;
                OnPropertyChanged();
            }
        }
        
        public MonthCellTemplateSelector? MonthTemplateSelector
        {
            get => _monthTemplateSelector;
            set
            {
                _monthTemplateSelector = value;
                OnPropertyChanged(nameof(MonthTemplateSelector));
            }
        }
        public Dictionary<DateTime, (int Steps, int Calories)> dailySteps = new Dictionary<DateTime, (int Steps, int Calories)>();

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

        public int TotalSteps
        {
            get => _totalSteps;
            set
            {
                _totalSteps = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand SelectActivityCommand { get; }

        public string YBindingProperty
        {
            get => _yBindingProperty;
            set
            {
                _yBindingProperty = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region For journal related

        ObservableCollection<FitnessActivityGroup>? _journalData;
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

        DateTime _journalSelectedDate = DateTime.Today;

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

        #endregion

        public FitnessViewModel(INavigation navigation)
        {
            LoadSampleData();
            LoadData();
            LoadJournalData(_journalSelectedDate);
 			LoadFAQs();
            SelectActivityCommand = new Command<string>(SelectedActivity);
            _navigation = navigation;
        }

        private void LoadSampleData()
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
                    CaloriesBurned = 0,
                    Distance = 0,
                    Steps = 0,
                    HeartRateAvg = random.Next(45, 65),
                    Title = "Night Sleep"
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
                    continue;

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
                        4 => 0, // Sleeping
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

        private string GenerateActivityLabel(string activityType, DateTime startTime)
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

        private string GenerateDescription(string activityType)
        {
            string remarks = activityType switch
            {
                "Walking" => "A refreshing walk to stay active.",
                "Running" => "A great way to boost endurance.",
                "Yoga" => "A peaceful session for mind and body.",
                "Cycling" => "A fun and effective cardio workout.",
                "Sleeping" => "A good night's sleep is essential for recovery.",
                "Swimming" => "A full-body workout to build strength and endurance.",
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
            var groupedActivities = Activities
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

            JournalData = new ObservableCollection<FitnessActivityGroup>(groupedActivities);
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

        private void UpdateWeekView()
        {
            var today = SelectedDate.Date;
            var currentWeekStart = today.AddDays(-(int)today.DayOfWeek); // Get Sunday of the current week
            var currentWeekEnd = currentWeekStart.AddDays(6); // Get Saturday of the current week

            var groupedData = Activities.Where(d => d.StartTime.Date >= currentWeekStart && d.EndTime.Date <= currentWeekEnd && d.ActivityType == SelectedActivityType)
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
                                           bool hasData = groupedData.ContainsKey(date);

                                           var totalDuration = hasData ? groupedData[date].TotalDuration : TimeSpan.Zero;
                                           var startTime = date;
                                           var endTime = startTime.Add(totalDuration); // EndTime = StartTime + Summed Duration

                                           return new FitnessActivity
                                           {
                                               StartTime = startTime,
                                               EndTime = endTime, // Corrected duration-based end time
                                               Steps = hasData ? groupedData[date].TotalSteps : 0,
                                               CaloriesBurned = hasData ? groupedData[date].TotalCalories : 0
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
        }

        private void UpdateDayView()
        {
            var today = SelectedDate;
            var dayData = Activities.Where(d => d.StartTime.Date == today && d.ActivityType == SelectedActivityType)
                .OrderByDescending(d => d.StartTime) // Sort by most recent activity first
                .ToList();

            var chartData = dayData
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

            WalkingList = new ObservableCollection<FitnessActivity>(dayData);
            WalkingChartList = new ObservableCollection<FitnessActivity>(chartData);
            if(WalkingChartList.Any())
            {
                MinStartTime = today.Date; // Ensures start from 12:00 AM
                MaxEndTime = today.Date.AddDays(1); // Ends at 11:59 PM
            }

            TotalSteps = WalkingList.Count > 0 ? WalkingList.Sum(a => a.Steps) : 0;
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
                for (DateTime d = startOfWeek; d <= endOfWeek; d = d.AddDays(1))
                {
                    if (dailySteps.ContainsKey(d))
                    {
                        weeklySteps += dailySteps[d].Steps;
                    }
                }

                // Add the week data
                WeeklyStepData.Add(new WeeklyStepData
                {
                    WeekRange = $"{startOfWeek:dd MMMM} - {endOfWeek:dd MMMM}",
                    TotalSteps = weeklySteps
                });

                startOfWeek = startOfWeek.AddDays(7);
            }

            TotalSteps = WeeklyStepData.Count > 0 ? WeeklyStepData.Sum(a => a.TotalSteps) : 0;
            UpdateMonthTemplate();
        }


        private void UpdateMonthData()
        {
            // Get the first and last day of the selected month
            var firstDay = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1); // Last day of the month

            // Filter data based on selected month and activities
            var monthlyData = Activities
                .Where(d => d.StartTime.Date >= firstDay && d.StartTime.Date <= lastDay && d.ActivityType == SelectedActivityType)
                .GroupBy(d => d.StartTime.Date) // Group by day
                .ToDictionary(g => g.Key, g => (
                    TotalSteps: g.Sum(d => d.Steps),
                    TotalCalories: (int)g.Sum(d => d.CaloriesBurned)
                ));

            dailySteps = new Dictionary<DateTime, (int Steps, int Calories)>(monthlyData);
        }

        private void UpdateMonthTemplate()
        {
            MonthTemplateSelector = new MonthCellTemplateSelector
            {
                ViewModel = this,
                IntenseStepCountTemplate = MonthTemplate_2(80),
                HighStepCountTemplate = MonthTemplate_2(60),
                MediumStepCountTemplate = MonthTemplate_2(45),
                LowStepCountTemplate = MonthTemplate_2(25),
                DefaultStepCountTemplate = MonthTemplate_2(15)
            };
        }

        private void SelectedActivity(string activityType)
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

            _navigation.PushAsync(new ActivityCustomViewPage(this));
        }

        private readonly Dictionary<string, string> ActivityColors = new()
        {
            { "Walking", "#116DF9" },
            { "Running", "#2196F3" },
            { "Cycling", "#F4890B" },
            { "Swimming", "#E2227E" },
            { "Yoga", "#00E190" },
            { "Sleeping", "#7633DA" },
        };

        private void UpdateChartColor()
        {
            if (ActivityColors.TryGetValue(SelectedActivityType, out string? selectedColor))
            {
                WalkingColor = new ObservableCollection<Brush>
                {
                    new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor)), new SolidColorBrush(Color.FromArgb(selectedColor))
                };
            }
        }

        private void LoadCyclingData()
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
                    Value = Activities
                        .Where(a => a.ActivityType == "Cycling" && a.StartTime.Date == day)
                        .Sum(a => a.Distance) // Sum up distances per day
                })
            );
        }

        private void LoadCaloriesData()
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
                    Value = Activities
                        .Where(a => a.StartTime.Date == day) // Get all activities for the day
                        .Sum(a => a.CaloriesBurned) // Sum up calories burned per day
                })
            );
        }

        private void LoadSleepingData()
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
                    Value = Activities
                        .Where(a => a.ActivityType == "Sleeping" && a.StartTime.Date == day.Date)
                        .Sum(a => a.DurationMinutes / 60) // Convert minutes to hours and sum up
                })
            );
        }

        private void LoadWeightData()
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

        private void LoadTodayData(DateTime date)
        {
            var today = date;

            // Summarizing today's data (e.g., total steps, calories, etc.)
            var todayActivities = Activities.Where(a => a.StartTime.Date == today).ToList();

            if (todayActivities.Any())
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

            private void LoadFAQs()
            {
                FAQs = new ObservableCollection<FAQ>
                 {
                     new FAQ { Question = "How does the app track my fitness progress?", Answer = "The app monitors various health and activity metrics, including steps taken, distance traveled, calories burned, and heart rate. By analyzing this data, it provides insights into your daily activity levels and overall fitness journey." },
                     new FAQ { Question = "What types of workouts and exercises does the app support?", Answer = "The app supports a wide range of workouts, such as walking, running, cycling, swimming, and strength training. It also allows you to log custom workouts to suit your personal fitness routine." },
                     new FAQ { Question = "Can I set personal fitness goals within the app?", Answer = "Yes, you can set personalized goals for steps, distance, calories burned, and active minutes. The app will track your progress and provide reminders to help you stay on target." },
                     new FAQ { Question = "Is my personal data secure within the app?", Answer = "We prioritize your privacy and security. The app uses encryption to protect your personal data and complies with data protection regulations. You can review our privacy policy within the app's settings for more details." },
                     new FAQ { Question = "How can I contact customer support?", Answer = "For assistance, you can reach our customer support team through the 'Help' section in the app, where you'll find options to chat with a representative or submit a support ticket." },
                     new FAQ { Question = "How do I log out my account?", Answer = "To log out your account, navigate to the profile settings, select 'Log out,' option." },
                 };
            }
            DataTemplate MonthTemplate_2(int opacity)
             {
            var template = new DataTemplate(() =>
            {
                Grid grid = new Grid();

                Border border = new Border();
                border.StrokeShape = new RoundRectangle()
                {
                    CornerRadius = new CornerRadius(25)
                };

                var color = ActivityColors.TryGetValue(SelectedActivityType, out string? selectedColor);
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


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
