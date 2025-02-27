using System.ComponentModel;

namespace FitnessTracker
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonalInfo 
    {
        private string? _name = "David William";
        private string? _firstName = "David";
        private string? _lastName = "William";
        private DateTime? _dateOfBirth= new DateTime(2000, 1, 1);
        private string? _email = "davidwilliam@gmail.com";
        private string? _password = "1234";

        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
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

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
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
                }
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhysicalInfo 
    {
        private string? _height = "162";
        private string? _weight ="63";
        private string? _gender ="Female";
        private string? _bodyFat = "Medium";
        private string? _activeStatus = "Moderately Active";
        private string? _measurementUnit ="Cm";

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

        public string? Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
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
                }
            }
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
