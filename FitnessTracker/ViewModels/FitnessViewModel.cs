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

            CyclingData = new ObservableCollection<TrendData>()
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
                }
            };

            SleepingData = new ObservableCollection<TrendData>()
            {
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
                }
            };

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

            CaloriesData = new ObservableCollection<TrendData>()
            {
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
