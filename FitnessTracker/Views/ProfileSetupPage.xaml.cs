
using FitnessTracker.Models;

namespace FitnessTracker.Views;

public partial class ProfileSetupPage : ContentPage
{
    ProfileSetupViewModel profileSetupViewModel;

    public ProfileSetupPage(ProfileSetupViewModel viewModel)
	{
		InitializeComponent();
        profileSetupViewModel = viewModel ;
        BindingContext = profileSetupViewModel;
    }
    void FinishsetupButton_Clicked(object sender, EventArgs e)
    {
        profileSetupViewModel.Height = heightentry.Text;
        profileSetupViewModel.Weight= weightentry.Text;
        profileSetupViewModel.DateOfBirthString = DateEntry.Text;
        profileSetupViewModel.SelectedGender = (string?)Gender.SelectedItem;
        profileSetupViewModel.SelectedActiveStatus = (string?)ActiveStatus.SelectedItem;
        profileSetupViewModel.SelectedBodyFat = (string?)BodyFat.SelectedItem;
        profileSetupViewModel.SelectedMeasurementUnit= (string?)MeasurementUnits.SelectedItem;

        Navigation.PushAsync(new HomePage(profileSetupViewModel));
    }
    void DatePicker_Tapped(object sender, TappedEventArgs e)
    {
        HiddenDatePicker.IsVisible = true;
    }

    private void HiddenDatePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
    {
        if (e.NewValue is DateTime dateValue)
        {
            string formattedDate = dateValue.Date.ToString("dd/MM/yyyy");
            DateEntry.Text = formattedDate;
            HiddenDatePicker.IsVisible = false;
        }

        //if (!string.IsNullOrWhiteSpace(DateEntry.Text))
        //{
        //    if (DateTime.TryParse(DateEntry.Text, out DateTime parsedDate))
        //    {
        //        viewModel.DateOfBirth = parsedDate;
        //    }
        //}
    }
}