using System.ComponentModel;

namespace FitnessTracker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonalInfo : INotifyPropertyChanged
    {
        private string? _name;
        private string? _firstName;
        private string? _lastName;
        private DateTime? _dateOfBirth;
        private string? _email;
        private string? _password;

        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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
                    OnPropertyChanged(nameof(FirstName));
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
                    OnPropertyChanged(nameof(LastName));
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
                    OnPropertyChanged(nameof(DateOfBirth));
                }
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
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhysicalInfo : INotifyPropertyChanged
    {
        private string? _height;
        private string? _weight;
        private string? _gender;
        private string? _bodyFat;
        private string? _activeStatus;
        private string? _measurementUnit;

        public string? Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
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
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public string? Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        public string? BodyFat
        {
            get => _bodyFat;
            set
            {
                if (_bodyFat != value)
                {
                    _bodyFat = value;
                    OnPropertyChanged(nameof(BodyFat));
                }
            }
        }

        public string? ActiveStatus
        {
            get => _activeStatus;
            set
            {
                if (_activeStatus != value)
                {
                    _activeStatus = value;
                    OnPropertyChanged(nameof(ActiveStatus));
                }
            }
        }

        public string? MeasurementUnit
        {
            get => _measurementUnit;
            set
            {
                if (_measurementUnit != value)
                {
                    _measurementUnit = value;
                    OnPropertyChanged(nameof(MeasurementUnit));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
