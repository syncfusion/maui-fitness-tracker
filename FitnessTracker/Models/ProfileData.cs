using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessTracker.Models;

public enum Gender { Male, Female }
public enum WeightUnit { Kg }
public enum HeightUnit { Cm }
public enum BodyFat { High, Medium, Low }
public enum ActiveStatus { High, Moderately, Low }
public class ProfileData : INotifyPropertyChanged
{
    private string _firstName = string.Empty;

    public string FirstName
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

    private string _lastName = string.Empty;

    public string LastName
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

    private string _emailID = string.Empty;

    public string EmailID
    {
        get => _emailID;
        set
        {
            if (_emailID != value)
            {
                _emailID = value;
                OnPropertyChanged(nameof(EmailID));
            }
        }
    }

    private string _imageSource = string.Empty;

    public string ImageSource
    {
        get => _imageSource;
        set
        {
            if (_imageSource != value)
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
    }

    private DateTime _dob;

    public DateTime DOB
    {
        get => _dob;
        set
        {
            if (_dob != value)
            {
                _dob = value;
                OnPropertyChanged(nameof(DOB));
            }
        }
    }

    private string _dateOnly = string.Empty;

    public string DateOnly
    {
        get => _dateOnly;
        set
        {
            if (_dateOnly != value)
            {
                _dateOnly = value;
                OnPropertyChanged(nameof(DateOnly));
            }
        }
    }

    private double _weight;

    public double Weight
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

    private double _height;

    public double Height
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

    private Gender _gender;

    public Gender Gender
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

    private WeightUnit _weightUnit;

    public WeightUnit WeightUnit
    {
        get => _weightUnit;
        set
        {
            if (_weightUnit != value)
            {
                _weightUnit = value;
                OnPropertyChanged(nameof(WeightUnit));
            }
        }
    }

    private HeightUnit _heightUnit;

    public HeightUnit HeightUnit
    {
        get => _heightUnit;
        set
        {
            if (_heightUnit != value)
            {
                _heightUnit = value;
                OnPropertyChanged(nameof(HeightUnit));
            }
        }
    }

    private BodyFat _bodyFat;

    public BodyFat BodyFat
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

    private ActiveStatus _activeStatus;

    public ActiveStatus ActiveStatus
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

    public ObservableCollection<string> MeasurementUnits { get; set; }

    private string _selectedMeasurementUnit = string.Empty;

    public string SelectedMeasurementUnit
    {
        get => _selectedMeasurementUnit;
        set
        {
            if (_selectedMeasurementUnit != value)
            {
                _selectedMeasurementUnit = value;
                OnPropertyChanged(nameof(SelectedMeasurementUnit));
            }
        }
    }

    public ProfileData()
    {
        MeasurementUnits = new ObservableCollection<string> { "Metric (Kg/Km)", "Imperial (lbs/miles)" };
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}