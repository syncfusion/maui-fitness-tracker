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

        #region Dummy chart data
        ObservableCollection<FitnessData>? _fitnessData;
        ObservableCollection<TrendData>? _cyclingData;
        ObservableCollection<TrendData>? _sleepingData;
        ObservableCollection<TrendData>? _weightData;
        ObservableCollection<TrendData>? _caloriesData;
        ObservableCollection<FAQ> _faqs;
        ObservableCollection<WalkingData>? _walkingData;
        ObservableCollection<WalkingData>? _walkingChartData;
        ObservableCollection<WeeklyStepData>? _weeklyStepData;

        public ObservableCollection<FitnessData>? FitnessData
        {
            get => _fitnessData;
            set
            {
                _fitnessData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TrendData>? CyclingData
        {
            get => _cyclingData;
            set
            {
                _cyclingData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TrendData>? SleepingData
        {
            get => _sleepingData;
            set
            {
                _sleepingData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TrendData>? WeightData
        {
            get => _weightData;
            set
            {
                _weightData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TrendData>? CaloriesData
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

        public ObservableCollection<WeeklyStepData>? WeeklyStepData
        {
            get => _weeklyStepData;
            set
            {
                if (_weeklyStepData != value)
                {
                    _weeklyStepData = value;
                    OnPropertyChanged();
                }
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

        private DateTime? _selectedDate = DateTime.Today;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    UpdateDayView(); // Refresh data when date changes
                }
            }
        }

        private DateTime? _selectedWeek = DateTime.Today;
        public DateTime? SelectedWeek
        {
            get => _selectedWeek;
            set
            {
                if (_selectedWeek != value)
                {
                    _selectedWeek = value;
                    OnPropertyChanged();
                    UpdateWeekView(); // Refresh data when date changes
                }
            }
        }

        private DateTime _selectedMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth.Year != value.Year || _selectedMonth.Month != value.Month)
                {
                    _selectedMonth = value;
                    
                    UpdateMonthView(); // Refresh data when date changes
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }

        private List<WalkingData>? rawData;
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
            LoadData();
            LoadJournalData();
            UpdateMonthTemplate();
 			LoadFAQs();
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
            LoadCaloriesData();
            LoadSleepingData();
            LoadWeightData();
            FindTodayData(CyclingData!);
            FindTodayData(CaloriesData!);
            FindTodayData(SleepingData!);
            FindTodayData(WeightData!);

            FitnessData = new ObservableCollection<FitnessData>
            {
                new FitnessData {
                Steps = new StepsData { Count = 6500, DistanceKm = 5.07, Calories = 213, MoveMinutes = 61 },
                HeartRate = new HeartRateData { BPM = 80 },
                Sleep = new SleepData { Hours = 7.5 },
                Calories = new CaloriesData { TotalCalories = 2150, ActiveCalories = 850, RestingCalories = 1300 },
                Trends = new List<TrendData>
        {
            // Cycling Trend
            new TrendData
            {
                Name = "Cycling",
                DataPoints = new List<DataPoint>
                {
                    new DataPoint { Label = "Sun", Value = 2.0 },
                    new DataPoint { Label = "Mon", Value = 3.5 },
                    new DataPoint { Label = "Tue", Value = 2.8 },
                    new DataPoint { Label = "Wed", Value = 4.0 },
                    new DataPoint { Label = "Thu", Value = 3.0 },
                    new DataPoint { Label = "Fri", Value = 2.5 },
                    new DataPoint { Label = "Sat", Value = 3.8 }
                }
            },

            // Sleeping Trend (Last 7 Days)
            new TrendData
            {
                Name = "Sleeping",
                DataPoints = new List<DataPoint>
                {
                    new DataPoint { Label = "Sun", Value = 7.0 },
                    new DataPoint { Label = "Mon", Value = 6.5 },
                    new DataPoint { Label = "Tue", Value = 7.2 },
                    new DataPoint { Label = "Wed", Value = 7.0 },
                    new DataPoint { Label = "Thu", Value = 6.8 },
                    new DataPoint { Label = "Fri", Value = 7.5 },
                    new DataPoint { Label = "Sat", Value = 7.0 }
                }
            },

            // Weight Trend (Last 6 Months)
            new TrendData
            {
                Name = "Weight",
                DataPoints = new List<DataPoint>
                {
                    new DataPoint { Label = "Aug", Value = 61.5 },
                    new DataPoint { Label = "Sep", Value = 62.0 },
                    new DataPoint { Label = "Oct", Value = 61.8 },
                    new DataPoint { Label = "Nov", Value = 62.5 },
                    new DataPoint { Label = "Dec", Value = 61.7 },
                    new DataPoint { Label = "Jan", Value = 62.5 }
                }
            },

            // Calories Burned Trend (Last 7 Days)
            new TrendData
            {
                Name = "Calories Burned",
                DataPoints = new List<DataPoint>
                {
                    new DataPoint { Label = "Sun", Value = 1000 },
                    new DataPoint { Label = "Mon", Value = 1100 },
                    new DataPoint { Label = "Tue", Value = 1050 },
                    new DataPoint { Label = "Wed", Value = 1150 },
                    new DataPoint { Label = "Thu", Value = 1075 },
                    new DataPoint { Label = "Fri", Value = 1125 },
                    new DataPoint { Label = "Sat", Value = 1200 }
                }
            }
        }
            }
            };

            LoadWalkingData();

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
            GenerateStepRawData();

            if (SelectedTabIndex == 1) // Week View
            {
                UpdateWeekView();
            }
            else if (SelectedTabIndex == 0) // Day View
            {
                UpdateDayView();
            }
            else if(SelectedTabIndex == 2) // Month view
            {
                UpdateMonthView();
            }
        }

        void GenerateStepRawData()
        {
            var startDate = DateTime.Today.AddDays(-99); // Last 7 days including today
            rawData = new List<WalkingData>();
            var random = new Random();

            for (int i = 0; i < 100; i++) // Loop for each day
            {
                var date = startDate.AddDays(i);
                rawData.Add(new WalkingData { Date = date, Steps = random.Next(1, 501), StartTime = date.AddHours(6), EndTime = date.AddHours(7), Label = "Morning walk" });
                rawData.Add(new WalkingData { Date = date, Steps = random.Next(1, 501), StartTime = date.AddHours(12), EndTime = date.AddHours(12.5), Label = "Afternoon walk" });
                rawData.Add(new WalkingData { Date = date, Steps = random.Next(1, 501), StartTime = date.AddHours(16.5), EndTime = date.AddHours(17.5), Label = "Evening walk" });
                rawData.Add(new WalkingData { Date = date, Steps = random.Next(1, 501), StartTime = date.AddHours(21.5), EndTime = date.AddHours(22.5), Label = "Night walk" });
            }
        }

        void UpdateDayView()
        {
            var today = SelectedDate;
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

        void UpdateWeekView()
        {
            var today = SelectedWeek!.Value;
            var currentWeekStart = today.AddDays(-(int)today.DayOfWeek); // Get Sunday of the current week
            var currentWeekEnd = currentWeekStart.AddDays(6); // Get Saturday of the current week

            // Filter and group raw data for the current week
            var groupedData = rawData.Where(d => d.Date.Date >= currentWeekStart && d.Date.Date <= currentWeekEnd)
                                     .GroupBy(d => d.Date.Date)
                                     .ToDictionary(g => g.Key, g => new
                                     {
                                         TotalSteps = g.Sum(d => d.Steps),
                                         TotalDuration = TimeSpan.FromMinutes(g.Sum(d => (d.EndTime - d.StartTime).TotalMinutes)) // Sum durations
                                     });

            // Generate complete week data, filling missing days with Steps = 0
            var weeklyData = Enumerable.Range(0, 7)
                                       .Select(offset =>
                                       {
                                           var date = currentWeekStart.AddDays(offset);
                                           bool hasData = groupedData.ContainsKey(date);
                                           TimeSpan totalDuration = hasData ? groupedData[date].TotalDuration : TimeSpan.Zero;

                                           return new WalkingData
                                           {
                                               Date = date,
                                               Steps = hasData ? groupedData[date].TotalSteps : 0,
                                               TotalDuration = FormatDuration(totalDuration) // Assign formatted string
                                           };
                                       })
                                       .OrderBy(d => d.Date) // Ensure ordering from Sunday to Saturday
                                       .ToList();

            var weeklyCollectionData = weeklyData.Where(d => d.Date <= today)
                                                 .OrderByDescending(d => d.Date) // Order from today -> Sunday
                                                 .ToList();

            WalkingData = new ObservableCollection<WalkingData>(weeklyCollectionData);
            WalkingChartData = new ObservableCollection<WalkingData>(weeklyData);
        }

        void UpdateMonthView()
        {
            UpdateMonthData();
            DateTime date = SelectedMonth.Date;
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

                startOfWeek = startOfWeek.AddDays(7);
            }

            UpdateMonthTemplate();
        }

        void UpdateMonthData()
        {
            // Get the first and last day of the selected month
            var firstDay = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1); // Last day of the month

            // Filter data based on selected month
            var monthlyData = rawData!
                .Where(d => d.Date >= firstDay && d.Date <= lastDay)
                .GroupBy(d => d.Date.Date) // Group by day
                .ToDictionary(
                    g => g.Key,  // Date
                    g => g.Sum(d => d.Steps) // Total steps for the day
                );

            dailySteps = new Dictionary<DateTime, int>(monthlyData);
        }

        void UpdateMonthTemplate()
        {
            MonthTemplateSelector = new MonthCellTemplateSelector
            {
                ViewModel = this,
                IntenseStepCountTemplate = MonthTemplate_2(95),
                HighStepCountTemplate = MonthTemplate_2(60),
                MediumStepCountTemplate = MonthTemplate_2(40),
                LowStepCountTemplate = MonthTemplate_2(25),
                DefaultStepCountTemplate = MonthTemplate_2(10)
            };
        }

        private string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalMinutes == 0)
                return "0h 0m";

            int hours = (int)duration.TotalHours;
            int minutes = duration.Minutes;

            return hours > 0 ? $"{hours}h {minutes}m" : $"{minutes}m";
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
        void LoadCyclingData()
        {
            DateTime date = DateTime.Now.Date;
            var rawData = new List<TrendData>()
            {
                new TrendData
                {
                    Name = "Cycling",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = date.AddDays(-50), Value = 2.5 },
                        new DataPoint { Date = date.AddDays(-49), Value = 3.2 },
                        new DataPoint { Date = date.AddDays(-48), Value = 2.8 },
                        new DataPoint { Date = date.AddDays(-47), Value = 4.0 },
                        new DataPoint { Date = date.AddDays(-46), Value = 3.5 },
                        new DataPoint { Date = date.AddDays(-45), Value = 2.9 },
                        new DataPoint { Date = date.AddDays(-44), Value = 3.8 },
                        new DataPoint { Date = date.AddDays(-43), Value = 3.0 },
                        new DataPoint { Date = date.AddDays(-42), Value = 2.7 },
                        new DataPoint { Date = date.AddDays(-41), Value = 4.1 },
                        new DataPoint { Date = date.AddDays(-40), Value = 3.4 },
                        new DataPoint { Date = date.AddDays(-39), Value = 3.0 },
                        new DataPoint { Date = date.AddDays(-38), Value = 3.9 },
                        new DataPoint { Date = date.AddDays(-37), Value = 2.6 },
                        new DataPoint { Date = date.AddDays(-36), Value = 3.3 },
                        new DataPoint { Date = date.AddDays(-35), Value = 2.9 },
                        new DataPoint { Date = date.AddDays(-34), Value = 3.5 },
                        new DataPoint { Date = date.AddDays(-33), Value = 3.8 },
                        new DataPoint { Date = date.AddDays(-32), Value = 2.4 },
                        new DataPoint { Date = date.AddDays(-31), Value = 3.7 },
                        new DataPoint { Date = date.AddDays(-30), Value = 2.8 },
                        new DataPoint { Date = date.AddDays(-29), Value = 3.1 },
                        new DataPoint { Date = date.AddDays(-28), Value = 4.2 },
                        new DataPoint { Date = date.AddDays(-27), Value = 3.0 },
                        new DataPoint { Date = date.AddDays(-26), Value = 2.6 },
                        new DataPoint { Date = date.AddDays(-25), Value = 3.9 },
                        new DataPoint { Date = date.AddDays(-24), Value = 3.0 },
                        new DataPoint { Date = date.AddDays(-23), Value = 4.5 },
                        new DataPoint { Date = date.AddDays(-22), Value = 2.5 },
                        new DataPoint { Date = date.AddDays(-21), Value = 3.8 },
                        new DataPoint { Date = date.AddDays(-20), Value = 2.7 },
                        new DataPoint { Date = date.AddDays(-19), Value = 3.4 },
                        new DataPoint { Date = date.AddDays(-18), Value = 2.9 },
                        new DataPoint { Date = date.AddDays(-17), Value = 3.6 },
                        new DataPoint { Date = date.AddDays(-16), Value = 3.1 },
                        new DataPoint { Date = date.AddDays(-15), Value = 4.0 },
                        new DataPoint { Date = date.AddDays(-14), Value = 3.7 },
                        new DataPoint { Date = date.AddDays(-13), Value = 2.8 },
                        new DataPoint { Date = date.AddDays(-12), Value = 3.5 },
                        new DataPoint { Date = date.AddDays(-11), Value = 2.9 },
                        new DataPoint { Date = date.AddDays(-10), Value = 4.3 },
                        new DataPoint { Date = date.AddDays(-9), Value = 3.0 },
                        new DataPoint { Date = date.AddDays(-8), Value = 2.7 },
                        new DataPoint { Date = date.AddDays(-7), Value = 3.9 },
                        new DataPoint { Date = date.AddDays(-6), Value = 3.2 },
                        new DataPoint { Date = date.AddDays(-5), Value = 2.6 },
                        new DataPoint { Date = date.AddDays(-4), Value = 3.8 },
                        new DataPoint { Date = date.AddDays(-3), Value = 4.1 },
                        new DataPoint { Date = date.AddDays(-2), Value = 3.3 },
                        new DataPoint { Date = date.AddDays(-1), Value = 2.7 },
                        new DataPoint { Date = date, Value = 3.5 }
                    }
                }
            };

            CyclingData = new ObservableCollection<TrendData>
            {
                new TrendData
                {
                    Name = "Cycling",
                    DataPoints = SortLastSevenDaysData(rawData)
                }
            };
        }

        void LoadCaloriesData()
        {
            DateTime date = DateTime.Now.Date;
            var rawData = new List<TrendData>
            {
                new TrendData
                {
                    Name = "Calories Burned",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = date.AddDays(-50), Value = 1000 },
                        new DataPoint { Date = date.AddDays(-49), Value = 1050 },
                        new DataPoint { Date = date.AddDays(-48), Value = 980 },
                        new DataPoint { Date = date.AddDays(-47), Value = 1100 },
                        new DataPoint { Date = date.AddDays(-46), Value = 1025 },
                        new DataPoint { Date = date.AddDays(-45), Value = 975 },
                        new DataPoint { Date = date.AddDays(-44), Value = 1075 },
                        new DataPoint { Date = date.AddDays(-43), Value = 995 },
                        new DataPoint { Date = date.AddDays(-42), Value = 960 },
                        new DataPoint { Date = date.AddDays(-41), Value = 1125 },
                        new DataPoint { Date = date.AddDays(-40), Value = 1010 },
                        new DataPoint { Date = date.AddDays(-39), Value = 990 },
                        new DataPoint { Date = date.AddDays(-38), Value = 1090 },
                        new DataPoint { Date = date.AddDays(-37), Value = 950 },
                        new DataPoint { Date = date.AddDays(-36), Value = 1025 },
                        new DataPoint { Date = date.AddDays(-35), Value = 970 },
                        new DataPoint { Date = date.AddDays(-34), Value = 1065 },
                        new DataPoint { Date = date.AddDays(-33), Value = 1085 },
                        new DataPoint { Date = date.AddDays(-32), Value = 940 },
                        new DataPoint { Date = date.AddDays(-31), Value = 1050 },
                        new DataPoint { Date = date.AddDays(-30), Value = 975 },
                        new DataPoint { Date = date.AddDays(-29), Value = 1025 },
                        new DataPoint { Date = date.AddDays(-28), Value = 1150 },
                        new DataPoint { Date = date.AddDays(-27), Value = 990 },
                        new DataPoint { Date = date.AddDays(-26), Value = 930 },
                        new DataPoint { Date = date.AddDays(-25), Value = 1095 },
                        new DataPoint { Date = date.AddDays(-24), Value = 985 },
                        new DataPoint { Date = date.AddDays(-23), Value = 1200 },
                        new DataPoint { Date = date.AddDays(-22), Value = 950 },
                        new DataPoint { Date = date.AddDays(-21), Value = 1075 },
                        new DataPoint { Date = date.AddDays(-20), Value = 960 },
                        new DataPoint { Date = date.AddDays(-19), Value = 1015 },
                        new DataPoint { Date = date.AddDays(-18), Value = 990 },
                        new DataPoint { Date = date.AddDays(-17), Value = 1080 },
                        new DataPoint { Date = date.AddDays(-16), Value = 1025 },
                        new DataPoint { Date = date.AddDays(-15), Value = 1100 },
                        new DataPoint { Date = date.AddDays(-14), Value = 1070 },
                        new DataPoint { Date = date.AddDays(-13), Value = 975 },
                        new DataPoint { Date = date.AddDays(-12), Value = 1060 },
                        new DataPoint { Date = date.AddDays(-11), Value = 995 },
                        new DataPoint { Date = date.AddDays(-10), Value = 1130 },
                        new DataPoint { Date = date.AddDays(-9), Value = 1010 },
                        new DataPoint { Date = date.AddDays(-8), Value = 960 },
                        new DataPoint { Date = date.AddDays(-7), Value = 1095 },
                        new DataPoint { Date = date.AddDays(-6), Value = 1030 },
                        new DataPoint { Date = date.AddDays(-5), Value = 940 },
                        new DataPoint { Date = date.AddDays(-4), Value = 1080 },
                        new DataPoint { Date = date.AddDays(-3), Value = 1125 },
                        new DataPoint { Date = date.AddDays(-2), Value = 1040 },
                        new DataPoint { Date = date.AddDays(-1), Value = 970 },
                        new DataPoint { Date = date, Value = 1065 }
                    }
                }
            };

            CaloriesData = new ObservableCollection<TrendData>
            {
                new TrendData
                {
                    Name = "Calories Burned",
                    DataPoints = SortLastSevenDaysData(rawData)
                }
            };
        }

        void LoadSleepingData()
        {
            DateTime date = DateTime.Now.Date;
            var rawData = new List<TrendData>
            {
                new TrendData
                {
                    Name = "Sleeping",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = date.AddDays(-50), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-49), Value = 6.5 },
                        new DataPoint { Date = date.AddDays(-48), Value = 7.2 },
                        new DataPoint { Date = date.AddDays(-47), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-46), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-45), Value = 7.5 },
                        new DataPoint { Date = date.AddDays(-44), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-43), Value = 6.9 },
                        new DataPoint { Date = date.AddDays(-42), Value = 7.3 },
                        new DataPoint { Date = date.AddDays(-41), Value = 6.7 },
                        new DataPoint { Date = date.AddDays(-40), Value = 7.4 },
                        new DataPoint { Date = date.AddDays(-39), Value = 6.6 },
                        new DataPoint { Date = date.AddDays(-38), Value = 7.1 },
                        new DataPoint { Date = date.AddDays(-37), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-36), Value = 7.2 },
                        new DataPoint { Date = date.AddDays(-35), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-34), Value = 6.9 },
                        new DataPoint { Date = date.AddDays(-33), Value = 7.3 },
                        new DataPoint { Date = date.AddDays(-32), Value = 6.5 },
                        new DataPoint { Date = date.AddDays(-31), Value = 7.4 },
                        new DataPoint { Date = date.AddDays(-30), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-29), Value = 7.1 },
                        new DataPoint { Date = date.AddDays(-28), Value = 6.6 },
                        new DataPoint { Date = date.AddDays(-27), Value = 7.5 },
                        new DataPoint { Date = date.AddDays(-26), Value = 6.9 },
                        new DataPoint { Date = date.AddDays(-25), Value = 7.2 },
                        new DataPoint { Date = date.AddDays(-24), Value = 6.7 },
                        new DataPoint { Date = date.AddDays(-23), Value = 7.4 },
                        new DataPoint { Date = date.AddDays(-22), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-21), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-20), Value = 6.6 },
                        new DataPoint { Date = date.AddDays(-19), Value = 7.3 },
                        new DataPoint { Date = date.AddDays(-18), Value = 6.5 },
                        new DataPoint { Date = date.AddDays(-17), Value = 7.2 },
                        new DataPoint { Date = date.AddDays(-16), Value = 7.1 },
                        new DataPoint { Date = date.AddDays(-15), Value = 6.9 },
                        new DataPoint { Date = date.AddDays(-14), Value = 7.4 },
                        new DataPoint { Date = date.AddDays(-13), Value = 6.6 },
                        new DataPoint { Date = date.AddDays(-12), Value = 7.0 },
                        new DataPoint { Date = date.AddDays(-11), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-10), Value = 7.5 },
                        new DataPoint { Date = date.AddDays(-9), Value = 6.7 },
                        new DataPoint { Date = date.AddDays(-8), Value = 7.3 },
                        new DataPoint { Date = date.AddDays(-7), Value = 6.9 },
                        new DataPoint { Date = date.AddDays(-6), Value = 7.2 },
                        new DataPoint { Date = date.AddDays(-5), Value = 6.8 },
                        new DataPoint { Date = date.AddDays(-4), Value = 7.4 },
                        new DataPoint { Date = date.AddDays(-3), Value = 6.7 },
                        new DataPoint { Date = date.AddDays(-2), Value = 7.1 },
                        new DataPoint { Date = date.AddDays(-1), Value = 6.9 },
                        new DataPoint { Date = date, Value = 7.3 }
                    }
                }
            };

            SleepingData = new ObservableCollection<TrendData>
            {
                new TrendData
                {
                    Name = "Sleeping",
                    DataPoints = SortLastSevenDaysData(rawData)
                }
            };
        }

        void LoadWeightData()
        {
            DateTime date = DateTime.Now.Date;
            var rawData = new List<TrendData>
            {
                new TrendData
                {
                    Name = "Weight",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = date.AddMonths(-12), Value = 60.0 },
                        new DataPoint { Date = date.AddMonths(-11), Value = 60.5 },
                        new DataPoint { Date = date.AddMonths(-10), Value = 61.0 },
                        new DataPoint { Date = date.AddMonths(-9), Value = 61.8 },
                        new DataPoint { Date = date.AddMonths(-8), Value = 62.0 },
                        new DataPoint { Date = date.AddMonths(-7), Value = 62.8 },
                        new DataPoint { Date = date.AddMonths(-6), Value = 61.5 },
                        new DataPoint { Date = date.AddMonths(-5), Value = 62.5 },
                        new DataPoint { Date = date.AddMonths(-4), Value = 67.8 },
                        new DataPoint { Date = date.AddMonths(-3), Value = 67.8 },
                        new DataPoint { Date = date.AddMonths(-2), Value = 57.7 },
                        new DataPoint { Date = date.AddMonths(-1), Value = 67.9 }
                    }
                }
            };

            WeightData = new ObservableCollection<TrendData>
            {
                new TrendData
                {
                    Name = "Weight",
                    DataPoints = GetLastSixMonthsData(rawData)
                }
            };
        }

        List<DataPoint> GetLastSixMonthsData(List<TrendData> rawData)
        {
            DateTime sixMonthsAgo = DateTime.Today.AddMonths(-6);

            var filteredDataPoints = rawData
                .SelectMany(trend => trend.DataPoints) // Flatten all DataPoints
                .Where(dp => dp.Date >= sixMonthsAgo) // Filter last 6 months
                .OrderBy(dp => dp.Date) // Ensure it's sorted by date
                .ToList();

            return filteredDataPoints;
        }

        List<DataPoint> SortLastSevenDaysData(List<TrendData> rawData)
        {
            // Get all data points from all trend data
            var allDataPoints = rawData.SelectMany(trend => trend.DataPoints).ToList();

            // Get today's date
            DateTime today = DateTime.Today;

            // Find the current week's Sunday
            DateTime currentWeekSunday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Sunday);

            // Find the current week's Saturday
            DateTime currentWeekSaturday = currentWeekSunday.AddDays(6);

            // Filter data for the current week (Sunday to Saturday)
            var currentWeekData = allDataPoints
                .Where(dp => dp.Date >= currentWeekSunday && dp.Date <= currentWeekSaturday)
                .OrderBy(dp => dp.Date)
                .ToList();

            // Fill missing days with empty data points (if required)
            var fullWeekData = Enumerable.Range(0, 7)
                .Select(offset => currentWeekSunday.AddDays(offset))
                .Select(date => currentWeekData.FirstOrDefault(dp => dp.Date == date) ?? new DataPoint { Date = date, Value = 0 })
                .ToList();

            return fullWeekData;
        }

        void FindTodayData(ObservableCollection<TrendData> rawData)
        {
            DateTime today = DateTime.Today;
            DateTime lastCompletedMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            // Store today's data in TodayData for each TrendData item
            foreach (var trend in rawData)
            {
                var todayDataPoint = trend.DataPoints.FirstOrDefault(dp => dp.Date == today);
                if (trend.Name == "Cycling")
                {
                    CyclingData[0].TodayData = todayDataPoint?.Value ?? 0;
                }
                else if (trend.Name == "Calories Burned")
                {
                    CaloriesData[0].TodayData = todayDataPoint?.Value ?? 0;
                }
                else if (trend.Name == "Sleeping")
                {
                    SleepingData[0].TodayData = todayDataPoint?.Value ?? 0;
                }
                else if(trend.Name == "Weight")
                {
                    var monthDataPoint = trend.DataPoints
                        .FirstOrDefault(dp => dp.Date.Month == lastCompletedMonth.Month && dp.Date.Year == lastCompletedMonth.Year);

                    WeightData[0].TodayData = monthDataPoint?.Value ?? 0;
                 }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
