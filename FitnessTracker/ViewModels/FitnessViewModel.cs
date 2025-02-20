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
        ObservableCollection<WalkingData>? _walkingData;
        ObservableCollection<WalkingData>? _walkingChartData;

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
            FindTodayData(CyclingData!);
            FindTodayData(CaloriesData!);
            FindTodayData(SleepingData!);

            WeightData = new ObservableCollection<TrendData>()
            {
                // Weight Trend (Last 6 Months)
                new TrendData
                {
                    Name = "Weight",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Label = "Aug", Value = 61.5 },
                        new DataPoint { Label = "Sep", Value = 62.5 },
                        new DataPoint { Label = "Oct", Value = 67.8 },
                        new DataPoint { Label = "Nov", Value = 67.8 },
                        new DataPoint { Label = "Dec", Value = 57.7 },
                        new DataPoint { Label = "Jan", Value = 67.9 }
                    }
                }
            };

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

        void LoadCyclingData()
        {
            var rawData = new List<TrendData>()
            {
                new TrendData
                {
                    Name = "Cycling",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = new DateTime(2025, 1, 1), Value = 2.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 2), Value = 3.2 },
                        new DataPoint { Date = new DateTime(2025, 1, 3), Value = 2.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 4), Value = 4.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 5), Value = 3.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 6), Value = 2.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 7), Value = 3.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 8), Value = 3.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 9), Value = 2.7 },
                        new DataPoint { Date = new DateTime(2025, 1, 10), Value = 4.1 },
                        new DataPoint { Date = new DateTime(2025, 1, 11), Value = 3.4 },
                        new DataPoint { Date = new DateTime(2025, 1, 12), Value = 3.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 13), Value = 3.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 14), Value = 2.6 },
                        new DataPoint { Date = new DateTime(2025, 1, 15), Value = 3.3 },
                        new DataPoint { Date = new DateTime(2025, 1, 16), Value = 2.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 17), Value = 3.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 18), Value = 3.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 19), Value = 2.4 },
                        new DataPoint { Date = new DateTime(2025, 1, 20), Value = 3.7 },
                        new DataPoint { Date = new DateTime(2025, 1, 21), Value = 2.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 22), Value = 3.1 },
                        new DataPoint { Date = new DateTime(2025, 1, 23), Value = 4.2 },
                        new DataPoint { Date = new DateTime(2025, 1, 24), Value = 3.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 25), Value = 2.6 },
                        new DataPoint { Date = new DateTime(2025, 1, 26), Value = 3.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 27), Value = 3.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 28), Value = 4.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 29), Value = 2.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 30), Value = 3.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 31), Value = 2.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 1), Value = 3.4 },
                        new DataPoint { Date = new DateTime(2025, 2, 2), Value = 2.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 3), Value = 3.6 },
                        new DataPoint { Date = new DateTime(2025, 2, 4), Value = 3.1 },
                        new DataPoint { Date = new DateTime(2025, 2, 5), Value = 4.0 },
                        new DataPoint { Date = new DateTime(2025, 2, 6), Value = 3.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 7), Value = 2.8 },
                        new DataPoint { Date = new DateTime(2025, 2, 8), Value = 3.5 },
                        new DataPoint { Date = new DateTime(2025, 2, 9), Value = 2.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 10), Value = 4.3 },
                        new DataPoint { Date = new DateTime(2025, 2, 11), Value = 3.0 },
                        new DataPoint { Date = new DateTime(2025, 2, 12), Value = 2.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 13), Value = 3.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 14), Value = 3.2 },
                        new DataPoint { Date = new DateTime(2025, 2, 15), Value = 2.6 },
                        new DataPoint { Date = new DateTime(2025, 2, 16), Value = 3.8 },
                        new DataPoint { Date = new DateTime(2025, 2, 17), Value = 4.1 },
                        new DataPoint { Date = new DateTime(2025, 2, 18), Value = 3.3 },
                        new DataPoint { Date = new DateTime(2025, 2, 19), Value = 2.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 20), Value = 3.5 }
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
            var rawData = new List<TrendData>
            {
                new TrendData
                {
                    Name = "Calories Burned",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = new DateTime(2025, 1, 1), Value = 1000 },
                        new DataPoint { Date = new DateTime(2025, 1, 2), Value = 1050 },
                        new DataPoint { Date = new DateTime(2025, 1, 3), Value = 980 },
                        new DataPoint { Date = new DateTime(2025, 1, 4), Value = 1100 },
                        new DataPoint { Date = new DateTime(2025, 1, 5), Value = 1025 },
                        new DataPoint { Date = new DateTime(2025, 1, 6), Value = 975 },
                        new DataPoint { Date = new DateTime(2025, 1, 7), Value = 1075 },
                        new DataPoint { Date = new DateTime(2025, 1, 8), Value = 995 },
                        new DataPoint { Date = new DateTime(2025, 1, 9), Value = 960 },
                        new DataPoint { Date = new DateTime(2025, 1, 10), Value = 1125 },
                        new DataPoint { Date = new DateTime(2025, 1, 11), Value = 1010 },
                        new DataPoint { Date = new DateTime(2025, 1, 12), Value = 990 },
                        new DataPoint { Date = new DateTime(2025, 1, 13), Value = 1090 },
                        new DataPoint { Date = new DateTime(2025, 1, 14), Value = 950 },
                        new DataPoint { Date = new DateTime(2025, 1, 15), Value = 1025 },
                        new DataPoint { Date = new DateTime(2025, 1, 16), Value = 970 },
                        new DataPoint { Date = new DateTime(2025, 1, 17), Value = 1065 },
                        new DataPoint { Date = new DateTime(2025, 1, 18), Value = 1085 },
                        new DataPoint { Date = new DateTime(2025, 1, 19), Value = 940 },
                        new DataPoint { Date = new DateTime(2025, 1, 20), Value = 1050 },
                        new DataPoint { Date = new DateTime(2025, 1, 21), Value = 975 },
                        new DataPoint { Date = new DateTime(2025, 1, 22), Value = 1025 },
                        new DataPoint { Date = new DateTime(2025, 1, 23), Value = 1150 },
                        new DataPoint { Date = new DateTime(2025, 1, 24), Value = 990 },
                        new DataPoint { Date = new DateTime(2025, 1, 25), Value = 930 },
                        new DataPoint { Date = new DateTime(2025, 1, 26), Value = 1095 },
                        new DataPoint { Date = new DateTime(2025, 1, 27), Value = 985 },
                        new DataPoint { Date = new DateTime(2025, 1, 28), Value = 1200 },
                        new DataPoint { Date = new DateTime(2025, 1, 29), Value = 950 },
                        new DataPoint { Date = new DateTime(2025, 1, 30), Value = 1075 },
                        new DataPoint { Date = new DateTime(2025, 1, 31), Value = 960 },
                        new DataPoint { Date = new DateTime(2025, 2, 1), Value = 1015 },
                        new DataPoint { Date = new DateTime(2025, 2, 2), Value = 990 },
                        new DataPoint { Date = new DateTime(2025, 2, 3), Value = 1080 },
                        new DataPoint { Date = new DateTime(2025, 2, 4), Value = 1025 },
                        new DataPoint { Date = new DateTime(2025, 2, 5), Value = 1100 },
                        new DataPoint { Date = new DateTime(2025, 2, 6), Value = 1070 },
                        new DataPoint { Date = new DateTime(2025, 2, 7), Value = 975 },
                        new DataPoint { Date = new DateTime(2025, 2, 8), Value = 1060 },
                        new DataPoint { Date = new DateTime(2025, 2, 9), Value = 995 },
                        new DataPoint { Date = new DateTime(2025, 2, 10), Value = 1130 },
                        new DataPoint { Date = new DateTime(2025, 2, 11), Value = 1010 },
                        new DataPoint { Date = new DateTime(2025, 2, 12), Value = 960 },
                        new DataPoint { Date = new DateTime(2025, 2, 13), Value = 1095 },
                        new DataPoint { Date = new DateTime(2025, 2, 14), Value = 1030 },
                        new DataPoint { Date = new DateTime(2025, 2, 15), Value = 940 },
                        new DataPoint { Date = new DateTime(2025, 2, 16), Value = 1080 },
                        new DataPoint { Date = new DateTime(2025, 2, 17), Value = 1125 },
                        new DataPoint { Date = new DateTime(2025, 2, 18), Value = 1040 },
                        new DataPoint { Date = new DateTime(2025, 2, 19), Value = 970 },
                        new DataPoint { Date = new DateTime(2025, 2, 20), Value = 1065 }
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
            var rawData = new List<TrendData>
            {
                new TrendData
                {
                    Name = "Sleeping",
                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint { Date = new DateTime(2025, 1, 1), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 2), Value = 6.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 3), Value = 7.2 },
                        new DataPoint { Date = new DateTime(2025, 1, 4), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 5), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 6), Value = 7.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 7), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 8), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 9), Value = 7.3 },
                        new DataPoint { Date = new DateTime(2025, 1, 10), Value = 6.7 },
                        new DataPoint { Date = new DateTime(2025, 1, 11), Value = 7.4 },
                        new DataPoint { Date = new DateTime(2025, 1, 12), Value = 6.6 },
                        new DataPoint { Date = new DateTime(2025, 1, 13), Value = 7.1 },
                        new DataPoint { Date = new DateTime(2025, 1, 14), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 15), Value = 7.2 },
                        new DataPoint { Date = new DateTime(2025, 1, 16), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 17), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 18), Value = 7.3 },
                        new DataPoint { Date = new DateTime(2025, 1, 19), Value = 6.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 20), Value = 7.4 },
                        new DataPoint { Date = new DateTime(2025, 1, 21), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 22), Value = 7.1 },
                        new DataPoint { Date = new DateTime(2025, 1, 23), Value = 6.6 },
                        new DataPoint { Date = new DateTime(2025, 1, 24), Value = 7.5 },
                        new DataPoint { Date = new DateTime(2025, 1, 25), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 1, 26), Value = 7.2 },
                        new DataPoint { Date = new DateTime(2025, 1, 27), Value = 6.7 },
                        new DataPoint { Date = new DateTime(2025, 1, 28), Value = 7.4 },
                        new DataPoint { Date = new DateTime(2025, 1, 29), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 1, 30), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 1, 31), Value = 6.6 },
                        new DataPoint { Date = new DateTime(2025, 2, 1), Value = 7.3 },
                        new DataPoint { Date = new DateTime(2025, 2, 2), Value = 6.5 },
                        new DataPoint { Date = new DateTime(2025, 2, 3), Value = 7.2 },
                        new DataPoint { Date = new DateTime(2025, 2, 4), Value = 7.1 },
                        new DataPoint { Date = new DateTime(2025, 2, 5), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 6), Value = 7.4 },
                        new DataPoint { Date = new DateTime(2025, 2, 7), Value = 6.6 },
                        new DataPoint { Date = new DateTime(2025, 2, 8), Value = 7.0 },
                        new DataPoint { Date = new DateTime(2025, 2, 9), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 2, 10), Value = 7.5 },
                        new DataPoint { Date = new DateTime(2025, 2, 11), Value = 6.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 12), Value = 7.3 },
                        new DataPoint { Date = new DateTime(2025, 2, 13), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 14), Value = 7.2 },
                        new DataPoint { Date = new DateTime(2025, 2, 15), Value = 6.8 },
                        new DataPoint { Date = new DateTime(2025, 2, 16), Value = 7.4 },
                        new DataPoint { Date = new DateTime(2025, 2, 17), Value = 6.7 },
                        new DataPoint { Date = new DateTime(2025, 2, 18), Value = 7.1 },
                        new DataPoint { Date = new DateTime(2025, 2, 19), Value = 6.9 },
                        new DataPoint { Date = new DateTime(2025, 2, 20), Value = 7.3 }
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
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
