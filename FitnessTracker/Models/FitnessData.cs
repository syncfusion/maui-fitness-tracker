using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class FitnessData
    {
        public StepsData? Steps { get; set; }
        public HeartRateData? HeartRate { get; set; }
        public SleepData? Sleep { get; set; }
        public CaloriesData? Calories { get; set; }
        public List<TrendData>? Trends { get; set; }
    }

    public class StepsData
    {
        public int Count { get; set; }
        public double DistanceKm { get; set; }
        public int Calories { get; set; }
        public int MoveMinutes { get; set; }
    }

    public class HeartRateData
    {
        public int BPM { get; set; }
    }

    public class SleepData
    {
        public double Hours { get; set; }
    }

    public class CaloriesData
    {
        public int TotalCalories { get; set; }
        public int ActiveCalories { get; set; }
        public int RestingCalories { get; set; }
    }

    public class TrendData
    {
        public string Name { get; set; } = string.Empty;
        public List<DataPoint>? DataPoints { get; set; }
    }

    public class DataPoint
    {
        public string Label { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class WalkingData
    {
        public DateTime Date { get; set; }
        public string DayPrefix => Date.ToString("ddd");
        public int Steps { get; set; }

        public DateTime StartTime { get; set; } // User-provided later
        public DateTime EndTime { get; set; }   // User-provided later

        public string Duration => CalculateDuration(); // Auto-calculated

        public int WeekNumber => CultureInfo.CurrentCulture.Calendar
            .GetWeekOfYear(Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        public int Year => Date.Year;
        public string Label { get; set; } = string.Empty;
        public string Time => StartTime.ToString("hh:mm tt").ToLower();

        private string CalculateDuration()
        {
            if(EndTime > StartTime)
            {
                TimeSpan duration = EndTime - StartTime;
                int hours = (int)duration.TotalHours;
                int minutes = duration.Minutes;
                int seconds = duration.Seconds;

                if (hours > 0)
                {
                    return $"{hours}h {minutes}m"; // Display hours and minutes, omit seconds
                }

                return $"{minutes}m {seconds}s"; // Display only minutes and seconds if no hours
            }

            return "0h 0m 0s"; // Default value if times are invalid
        }
    }

    public class JournalData
    {
        public DateTime Date { get; set; }
        public string Time => Date.ToString("hh:mm tt").ToLower();
        public string Name { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Calories { get; set; }
        public int Steps { get; set; }
        public string CaloriesDisplay => Calories > 0 ? $"{Calories} Cal" : "";
    }

    public class JournalGroup : ObservableCollection<JournalData>
    {
        public string GroupTitle { get; set; } // Today, Yesterday, or Date
        public int TotalSteps { get; set; }
        public int TotalCalories { get; set; }

        public JournalGroup(string title, int totalSteps, int totalCalories, IEnumerable<JournalData> activities) : base(activities)
        {
            GroupTitle = title;
            TotalSteps = totalSteps;
            TotalCalories = totalCalories;
        }
    }

    public class FAQ
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public bool IsExpanded { get; set; } = false;
    }

    public class StepData : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }
        private int _steps;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WeeklyStepData
    {
        public string WeekRange { get; set; } = string.Empty;
        public int TotalSteps { get; set; }

    }
    public class ProfileSetupViewModel : INotifyPropertyChanged
    {
        string? _name;
        string? _firstName;
        string? _lastName;
        string? _email;
        string? _newEmail;
        string? _password;
        string? _confirmPassword;
        string? _newPassword;
        string? _height;
        string? _weight;
        DateTime? _dateOfBirth = DateTime.Now;
        private string? _dateOfBirthString;
        string? _selectedGender;
        string? _selectedBodyFat;
        string? _selectedActiveStatus;
        string? _selectedMeasurementUnit;
        string _otp = string.Empty;
        #region List
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> BodyFatLevels { get; set; }
        public ObservableCollection<string> ActiveStatuses { get; set; }
        public ObservableCollection<string> MeasurementUnits { get; set; }
        #endregion

        public ProfileSetupViewModel()
        {
            #region List
            // Initialize data lists
            Genders = new ObservableCollection<string>
            {
                "Male", "Female", "Non-Binary", "Other"
            };

            BodyFatLevels = new ObservableCollection<string>
            {
                "Low", "Medium", "High"
            };

            ActiveStatuses = new ObservableCollection<string>
            {
                "Sedentary", "Lightly Active", "Moderately Active", "Very Active"
            };

            MeasurementUnits = new ObservableCollection<string>
            {
                "Cm", "Inches"
            };
            #endregion
        }
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    UpdateNameParts(value);
                }
            }
        }

        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                }
            }
        }

        public string? LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                }
            }
        }

        private void UpdateNameParts(string? fullName)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var parts = fullName.Split(' ', 2); 
                FirstName = parts[0];
                LastName = parts.Length > 1 ? parts[1] : string.Empty;
            }
            else
            {
                FirstName = string.Empty;
                LastName = string.Empty;
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                }
            }
        }
        public string? NewEmail
        {
            get => _newEmail;
            set
            {
                if (_newEmail != value)
                {
                    _newEmail = value;
                }
            }
        }
        public string VerificationMessage => $"We have sent a verification code to {NewEmail}";
        public string? Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
            }
        }

        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                }
            }
        }

        public string? NewPassword
        {
            get => _newPassword;
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    //OnPropertyChanged(nameof(NewPassword));
                }
            }
        }
        public string? Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                }
            }
        }

        public string? Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    DateOfBirthString = _dateOfBirth?.ToString("dd/MM/yyyy") ?? string.Empty;
                }
            }
        }

        public string? DateOfBirthString
        {
            get => _dateOfBirthString;
            set
            {
                if (_dateOfBirthString != value)
                {
                    _dateOfBirthString = value;

                    // Convert string to DateTime only if it is in the correct format
                    if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        DateOfBirth = parsedDate;
                    }
                    else
                    {
                        DateOfBirth = null; 
                    }
                }
            }
        }

        public string? SelectedGender
        {
            get => _selectedGender;
            set
            {
                if (_selectedGender != value)
                {
                    _selectedGender = value;
                }
            }
        }

        public string? SelectedBodyFat
        {
            get => _selectedBodyFat;
            set
            {
                if (_selectedBodyFat != value)
                {
                    _selectedBodyFat = value;
                }
            }
        }

        public string? SelectedActiveStatus
        {
            get => _selectedActiveStatus;
            set
            {
                if (_selectedActiveStatus != value)
                {
                    _selectedActiveStatus = value;
                }
            }
        }

        public string? SelectedMeasurementUnit
        {
            get => _selectedMeasurementUnit;
            set
            {
                if (_selectedMeasurementUnit != value)
                {
                    _selectedMeasurementUnit = value;
                }
            }
        }
        public string OTP
        {
            get => _otp;
            set
            {
                _otp = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

