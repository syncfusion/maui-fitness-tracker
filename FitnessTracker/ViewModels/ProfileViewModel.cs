using FitnessTracker.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessTracker.ViewModels;

public class ProfileViewModel:INotifyPropertyChanged
{

    private ProfileData _userProfile;

    public ProfileData UserProfile
    {
        get => _userProfile;
        set
        {
            if (_userProfile != value)
            {
                _userProfile = value;
                OnPropertyChanged(nameof(UserProfile));
            }
        }
    }
    
    public ProfileViewModel()
    {
         _userProfile=new ProfileData();
        UserProfile.FirstName = "David";
        UserProfile.LastName = "William";
        UserProfile.EmailID = "davidwilliam@gmail.com";
        UserProfile.ImageSource = "alexuser.png";
        UserProfile.DOB = DateTime.Parse("01/01/2000");
        UserProfile.DateOnly = UserProfile.DOB.Date.ToString("dd/MM/yyyy");
        UserProfile.Weight = 63;
        UserProfile.Height = 162;
        UserProfile.Gender = Gender.Male;
        UserProfile.WeightUnit = WeightUnit.Kg;
        UserProfile.HeightUnit = HeightUnit.Cm;
        UserProfile.BodyFat = BodyFat.Medium;
        UserProfile.ActiveStatus = ActiveStatus.Moderately;
        UserProfile.SelectedMeasurementUnit = UserProfile.MeasurementUnits[0];
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}