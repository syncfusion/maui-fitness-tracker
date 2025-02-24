using FitnessTracker.ViewModels;
using FitnessTracker.Models;
using System;
namespace FitnessTracker.Views;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        genderBox.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        weightBox.ItemsSource = Enum.GetValues(typeof(WeightUnit)).Cast<WeightUnit>().ToList();
        heightBox.ItemsSource = Enum.GetValues(typeof(HeightUnit)).Cast<HeightUnit>().ToList();
        bodyFatBox.ItemsSource = Enum.GetValues(typeof(BodyFat)).Cast<BodyFat>().ToList();
        activeStatusBox.ItemsSource = Enum.GetValues(typeof(ActiveStatus)).Cast<ActiveStatus>().ToList();
        measurementUnitBox.ItemsSource = viewModel.UserProfile.MeasurementUnits;
    }

    private  void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

}