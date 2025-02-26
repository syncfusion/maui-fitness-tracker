using FitnessTracker.ViewModels;
using FitnessTracker.Models;
using System.ComponentModel;
namespace FitnessTracker.Views;


public partial class EditProfilePage : ContentPage
{
    private ProfileViewModel _viewModel = new ProfileViewModel();

    public ProfileViewModel ViewModel
    {
        get => _viewModel;
        set
        {
            if (_viewModel != value)
            {
                _viewModel = value;
            }
        }
    }

    public EditProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
        genderBox.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        weightBox.ItemsSource = Enum.GetValues(typeof(WeightUnit)).Cast<WeightUnit>().ToList();
        heightBox.ItemsSource = Enum.GetValues(typeof(HeightUnit)).Cast<HeightUnit>().ToList();
        bodyFatBox.ItemsSource = Enum.GetValues(typeof(BodyFat)).Cast<BodyFat>().ToList();
        activeStatusBox.ItemsSource = Enum.GetValues(typeof(ActiveStatus)).Cast<ActiveStatus>().ToList();
        measurementUnitBox.ItemsSource = ViewModel.UserProfile.MeasurementUnits;
    }

    void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

    void OnSaveButtonClicked(object sender, EventArgs args)
    {
        DateTime _dob= DateTime.ParseExact(dob.Text,"dd/mm/yyyy",null);
        ViewModel.UserProfile = new ProfileData()
        {
            FirstName = firstname.Text,
            LastName= lastName.Text,
            EmailID = "davidwilliam@gmail.com",
            ImageSource = "alexuser.png",
            DOB = _dob,
            DateOnly = _dob.Date.ToString("dd/MM/yyyy"),
            Weight = int.Parse(userweight.Text),
            Height = int.Parse(userheight.Text),
            Gender = (Gender)Enum.Parse(typeof(Gender), genderBox.Text),
            WeightUnit = (WeightUnit)Enum.Parse(typeof(WeightUnit), weightBox.Text),
            HeightUnit = (HeightUnit)Enum.Parse(typeof(HeightUnit), heightBox.Text),
            BodyFat = (BodyFat)Enum.Parse(typeof(BodyFat), bodyFatBox.Text),
            ActiveStatus = (ActiveStatus)Enum.Parse(typeof(ActiveStatus), activeStatusBox.Text),
            SelectedMeasurementUnit = measurementUnitBox.Text,
        };
        Navigation.PopAsync();
    }
}