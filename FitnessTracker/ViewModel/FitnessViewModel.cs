using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessTracker.Models;
using FitnessTracker.Templates;
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
        ObservableCollection<WalkingData>? _walkingData;
        ObservableCollection<WalkingData>? _walkingChartData;

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



        public ObservableCollection<WalkingData>? WalkingData
        {
            get => _walkingData;
            set
            {
                _walkingData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WalkingData>? WalkingChartData
        {
            get => _walkingChartData;
            set
            {
                _walkingChartData = value;
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

        DateTime _minStartTime;
        DateTime _maxEndTime;
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

        private List<WalkingData> rawData = new()
        {
            new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1200, StartTime = new DateTime(2025, 2, 1, 8, 0, 0), EndTime = new DateTime(2025, 2, 1, 8, 50, 10), Label = "Evening walk" },
            new WalkingData { Date = new DateTime(2025, 1, 31), Steps = 1800, StartTime = new DateTime(2025, 1, 31, 7, 0, 0), EndTime = new DateTime(2025, 1, 31, 8, 2, 0), Label = "Morning walk" },
            new WalkingData { Date = new DateTime(2025, 1, 30), Steps = 1512, StartTime = new DateTime(2025, 1, 30, 6, 0, 0), EndTime = new DateTime(2025, 1, 30, 7, 8, 0), Label = "Lunch walk" },
            new WalkingData { Date = new DateTime(2025, 1, 29), Steps = 336, StartTime = new DateTime(2025, 1, 29, 5, 30, 0), EndTime = new DateTime(2025, 1, 29, 5, 56, 9), Label = "Night walk" },
            new WalkingData { Date = new DateTime(2025, 1, 28), Steps = 258, StartTime = new DateTime(2025, 1, 28, 5, 10, 0), EndTime = new DateTime(2025, 1, 28, 5, 32, 42), Label = "Evening walk" },
            new WalkingData { Date = new DateTime(2025, 1, 27), Steps = 353, StartTime = new DateTime(2025, 1, 27, 4, 50, 0), EndTime = new DateTime(2025, 1, 27, 5, 20, 22), Label = "Office walk" },
            new WalkingData { Date = new DateTime(2025, 1, 26), Steps = 3126, StartTime = new DateTime(2025, 1, 26, 4, 0, 0), EndTime = new DateTime(2025, 1, 26, 6, 2, 0), Label = "Morning run" }
        };

        public ObservableCollection<WeeklyStepData>? WeeklyStepData { get; set; }
        private MonthCellTemplateSelector? _monthTemplateSelector;
        public MonthCellTemplateSelector? MonthTemplateSelector
        {
            get => _monthTemplateSelector;
            set
            {
                _monthTemplateSelector = value;
                OnPropertyChanged(nameof(MonthTemplateSelector));
            }
        }
        public Dictionary<DateTime, int> dailySteps = new Dictionary<DateTime, int>();

        DateTime _activityTabSelectedDate = DateTime.Today;

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

        #endregion

        #region For journal related

        ObservableCollection<JournalGroup>? _journalDatas;
        public ObservableCollection<JournalGroup>? JournalDatas
        {
            get => _journalDatas;
            set
            {
                if (_journalDatas != value)
                {
                    _journalDatas = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public FitnessViewModel()
        {
            LoadSampleData();
            //GenerateWeeklyStepDataPoints();
            LoadData();
            LoadJournalData();
 			LoadFAQs();
			GenerateStepDataCollection(DateTime.Now);
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
                    HeartRateAvg = random.Next(45, 65)
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
                var startTime = endTime.AddDays(day).AddMinutes(-random.Next(30, 120)); // Duration between 30 mins and 2 hours

                // Ensure sleep entry is added only once per day
                if (activityType == "Sleeping" && sleepDays.Contains(date.Date))
                    continue;

                if (activityType == "Sleeping")
                {
                    sleepDays.Add(date.Date);
                    endTime = date.AddDays(day).AddHours(random.Next(22, 30)); // Sleep typically happens at night
                    startTime = endTime.AddHours(day).AddHours(-random.Next(5, 8)); // 5 to 8 hours duration
                }

                if (activityType == "Swimming")
                {
                    endTime = startTime.AddMinutes(random.Next(20, 31)); // Swimming time between 20 to 60 minutes
                }

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
                    }
                });
            }
        }

        private List<DataPoint> GenerateWeeklyStepDataPoints()
        {
            // Get today's date and the date one week ago
            DateTime today = DateTime.Today;
            DateTime oneWeekAgo = today.AddDays(-6); // Includes today

            // Prepare a dictionary to hold the step count for each of the last 7 days
            var stepDataByDate = Enumerable.Range(0, 7)
                                           .Select(offset => oneWeekAgo.AddDays(offset))
                                           .ToDictionary(date => date, date => 0);

            // Filter and group activities by date, then sum the steps for each date
            var stepsByDate = Activities
                .Where(activity => activity.StartTime.Date >= oneWeekAgo && activity.StartTime.Date <= today)
                .Where(activity => activity.ActivityType != "Yoga" && activity.ActivityType != "Cycling") // Exclude activities without steps
                .GroupBy(activity => activity.StartTime.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    TotalSteps = group.Sum(activity => activity.Steps)
                });

            // Fill the dictionary with computed step counts
            foreach (var entry in stepsByDate)
            {
                stepDataByDate[entry.Date] = entry.TotalSteps;
            }

            // Create data points for charting
            var chartDataPoints = stepDataByDate.Select(entry => new DataPoint
            {
                Label = entry.Key.ToString("ddd"), // Short weekday name (e.g., "Mon")
                Value = entry.Value
            }).ToList();

            return chartDataPoints;
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

            WalkingColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#116DF9")),
                new SolidColorBrush(Color.FromArgb("#116DF9")),
                new SolidColorBrush(Color.FromArgb("#116DF9")),
                new SolidColorBrush(Color.FromArgb("#116DF9")),
                new SolidColorBrush(Color.FromArgb("#116DF9"))
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
        void LoadJournalData()
        {
            var activities = new List<JournalData>
            {
                new JournalData { Date = DateTime.Now.AddHours(-2), Name = "Evening Walk", Duration = "1h 30m", Icon = "walking.png", Calories = 1127, Steps = 6500 },
                new JournalData { Date = DateTime.Now.AddHours(-3), Name = "Yoga", Duration = "1h 30m", Icon = "yoga_bw.png", Calories = 500, Steps = 200 },
                new JournalData { Date = DateTime.Now.AddHours(-4), Name = "Morning Run", Duration = "1h 30m", Icon = "running_bw.png", Calories = 850, Steps = 4000 },
                new JournalData { Date = DateTime.Now.AddDays(-1).AddHours(-2), Name = "Evening Walk", Duration = "1h 30m", Icon = "walking.png", Calories = 1127, Steps = 6500 },
                new JournalData { Date = DateTime.Now.AddDays(-1).AddHours(-4), Name = "Swimming", Duration = "1h 30m", Icon = "swimming_bw", Calories = 700, Steps = 3000 },
                new JournalData { Date = new DateTime(2025, 1, 4, 14, 0, 0), Name = "Evening Walk", Duration = "1h 30m", Icon = "walking.png", Calories = 1127, Steps = 6500 },
                new JournalData { Date = new DateTime(2025, 1, 4, 16, 0, 0), Name = "Evening Football", Duration = "2 hours", Icon = "football.png", Calories = 600, Steps = 2500 },
                new JournalData { Date = new DateTime(2025, 1, 4, 18, 0, 0), Name = "Afternoon Bike Ride", Duration = "1h 30m", Icon = "bikeride.png", Calories = 900, Steps = 1500 }
            };

            var grouped = activities
                    .GroupBy(a => a.Date.Date)
                    .OrderByDescending(g => g.Key) // Sort by date in descending order
                    .Select(g =>
                    {
                        string title = g.Key == DateTime.Today ? "Today" :
                                       g.Key == DateTime.Today.AddDays(-1) ? "Yesterday" :
                                       g.Key.ToString("ddd, dd MMM");

                        int totalSteps = g.Sum(a => a.Steps);
                        int totalCalories = g.Sum(a => a.Calories);

                        return new JournalGroup(title, totalSteps, totalCalories, g);
                    });

            JournalDatas = new ObservableCollection<JournalGroup>(grouped);
        }

        void UpdateView()
        {
            if (SelectedTabIndex == 1) // Week View
            {
                WalkingData = new ObservableCollection<WalkingData>(
                    rawData.OrderByDescending(a => a.Year)
                           .ThenByDescending(a => a.WeekNumber)
                           .ThenByDescending(a => a.Date.DayOfWeek)
                );
                WalkingChartData = new ObservableCollection<WalkingData>(
                    rawData.OrderBy(d => d.Date)
                );
            }
            else if (SelectedTabIndex == 0) // Day View
            {
                //WalkingData = new ObservableCollection<WalkingData>(
                //    rawData.OrderByDescending(a => a.Date)
                //);

                var rawData = new List<WalkingData>
                {
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1200, StartTime = new DateTime(2025, 2, 1, 7, 0, 0), EndTime = new DateTime(2025, 2, 1, 8, 2, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 891, StartTime = new DateTime(2025, 2, 1, 5, 30, 0), EndTime = new DateTime(2025, 2, 1, 6, 10, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 251, StartTime = new DateTime(2025, 2, 1, 2, 11, 0), EndTime = new DateTime(2025, 2, 1, 2, 26, 0), Label = "Night walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 400, StartTime = new DateTime(2025, 2, 1, 8, 0, 0), EndTime = new DateTime(2025, 2, 1, 8, 30, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1800, StartTime = new DateTime(2025, 2, 1, 5, 30, 0), EndTime = new DateTime(2025, 2, 1, 7, 0, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 650, StartTime = new DateTime(2025, 2, 1, 9, 15, 0), EndTime = new DateTime(2025, 2, 1, 9, 50, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1050, StartTime = new DateTime(2025, 2, 1, 10, 30, 0), EndTime = new DateTime(2025, 2, 1, 11, 10, 0), Label = "Morning walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1250, StartTime = new DateTime(2025, 2, 1, 12, 0, 0), EndTime = new DateTime(2025, 2, 1, 12, 45, 0), Label = "Afternoon walk" },
                    new WalkingData { Date = new DateTime(2025, 2, 1), Steps = 1600, StartTime = new DateTime(2025, 2, 1, 15, 0, 0), EndTime = new DateTime(2025, 2, 1, 15, 40, 0), Label = "Afternoon walk" }
                };

                var today = new DateTime(2025, 02, 01);
                var dayData = rawData.Where(d => d.Date.Date == today)
                    .OrderByDescending(d => d.StartTime) // Sort by most recent activity first
                    .ToList();

                var chartData = dayData
                    .GroupBy(d => d.Date) // Group by Date
                    .SelectMany(g => g.Select(d => new WalkingData
                    {
                        Date = d.Date,
                        StartTime = d.StartTime, // Retain actual start time
                        Steps = d.Steps
                    }))
                    .OrderBy(d => d.StartTime)
                    .ToList();

                WalkingData = new ObservableCollection<WalkingData>(dayData);
                WalkingChartData = new ObservableCollection<WalkingData>(chartData);

                MinStartTime = WalkingChartData.Min(w => w.StartTime).Date; // Start of the earliest day
                MaxEndTime = WalkingChartData.Max(w => w.StartTime).Date.AddDays(1).AddSeconds(-1); // End of the latest day
            }

            // Update chart in ascending order
            //WalkingChartData = new ObservableCollection<WalkingData>(
            //    rawData.OrderBy(d => d.Date)
            //);

            OnPropertyChanged(nameof(WalkingData));
            OnPropertyChanged(nameof(WalkingChartData));
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

        private void GenerateStepDataCollection(DateTime date)
        {
            WeeklyStepData = new ObservableCollection<WeeklyStepData>();
            Random rnd = new Random();

            // Generate random steps for each day of the month
            int totalDays = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= totalDays; i++)
            {
                DateTime day = new DateTime(date.Year, date.Month, i);
                dailySteps[day] = rnd.Next(0, 2000); // Random step count between 0 and 2,000
            }

            // Determine the first and last day of the month
            DateTime firstDay = new DateTime(date.Year, date.Month, 1);
            DateTime lastDay = new DateTime(date.Year, date.Month, totalDays);

            // Find the first Sunday before or on the first day of the month
            DateTime startOfWeek = firstDay.AddDays(-(int)firstDay.DayOfWeek);

            // Iterate through full weeks in the month
            while (startOfWeek <= lastDay)
            {
                DateTime endOfWeek = startOfWeek.AddDays(6); // End of the week (Saturday)

                // Ensure the range stays within the month's valid dates
                DateTime rangeStart = startOfWeek;
                DateTime rangeEnd = endOfWeek;

                // Sum steps only for the days within this month
                int weeklySteps = 0;
                for (DateTime d = rangeStart; d <= rangeEnd; d = d.AddDays(1))
                {
                    if (dailySteps.ContainsKey(d))
                        weeklySteps += dailySteps[d];
                }

                // Add the week data
                WeeklyStepData.Add(new WeeklyStepData
                {
                    WeekRange = $"{rangeStart:dd MMMM} - {rangeEnd:dd MMMM}",
                    TotalSteps = weeklySteps
                });

                // Move to the next week
                startOfWeek = startOfWeek.AddDays(7);
            }

            OnPropertyChanged(nameof(WeeklyStepData));
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

                string opacityColor = "#" + opacity + "116DF9";
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
