using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessTracker.Models;

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

        #endregion

        #region Brush
        public ObservableCollection<Brush>? CyclingColor { get; set; }
        public ObservableCollection<Brush>? SleepingColor { get; set; }
        public ObservableCollection<Brush>? WeightColor { get; set; }
        public ObservableCollection<Brush>? CaloriesColor { get; set; }
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

            #endregion
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
