using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessTracker.Models;

namespace FitnessTracker.ViewModels
{
    public class FitnessViewModel : INotifyPropertyChanged
    {
        ObservableCollection<FitnessData>? _fitnessData;
        public ObservableCollection<FitnessData>? FitnessData
        {
            get => _fitnessData;
            set
            {
                _fitnessData = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<TrendData>? _cyclingData;
        public ObservableCollection<TrendData>? CyclingData
        {
            get => _cyclingData;
            set
            {
                _cyclingData = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<TrendData>? _sleepingData;
        public ObservableCollection<TrendData>? SleepingData
        {
            get => _sleepingData;
            set
            {
                _sleepingData = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<TrendData>? _weightData;
        public ObservableCollection<TrendData>? WeightData
        {
            get => _weightData;
            set
            {
                _weightData = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<TrendData>? _caloriesData;
        public ObservableCollection<TrendData>? CaloriesData
        {
            get => _caloriesData;
            set
            {
                _caloriesData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Brush>? CyclingColor { get; set; }
        public ObservableCollection<Brush>? SleepingColor { get; set; }
        public ObservableCollection<Brush>? WeightColor { get; set; }
        public ObservableCollection<Brush>? CaloriesColor { get; set; }

        public FitnessViewModel()
        {
            LoadData();
        }

        void LoadData()
        {
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
            CyclingColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5")),
                new SolidColorBrush(Color.FromArgb("#0086E5"))
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
            WeightColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739")),
                new SolidColorBrush(Color.FromArgb("#E23739"))
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
            CaloriesColor = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA")),
                new SolidColorBrush(Color.FromArgb("#736BEA"))
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
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
