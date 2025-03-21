namespace FitnessTracker;

public partial class ProfileSetupPage : ContentPage
{
    #region List

    List<string> GendersList = new List<string>{"Male", "Female", "Non-Binary", "Other" };

    List<string> BodyFatLevelsList = new List<string>{ "Low", "Medium", "High"};

    List<string> ActiveStatusesList = new List<string>{"Sedentary", "Lightly Active", "Moderately Active", "Very Active"};

    List<string> MeasurementUnitsList = new List<string>{"Cm", "Inches"};
    List<string> weightList = new List<string> { "Kg" };
    List<string> heightList = new List<string> { "Cm" };

    #endregion
    PhysicalInfo viewModel;
    PersonalInfo personalInfo;

    public ProfileSetupPage(PersonalInfo personalInfoviewmodel,PhysicalInfo physicalInfoviewmodel)
	{
		InitializeComponent();
        Gender.ItemsSource = GendersList;
        ActiveStatus.ItemsSource = ActiveStatusesList;
        BodyFat.ItemsSource = BodyFatLevelsList;
        MeasurementUnits.ItemsSource = MeasurementUnitsList;
        weightcombo.ItemsSource = weightList;
        heightcombo.ItemsSource = heightList;
        viewModel =physicalInfoviewmodel;
        personalInfo = personalInfoviewmodel;
        BindingContext = viewModel;
    }
    void FinishsetupButton_Clicked(object sender, EventArgs e)
    {
        viewModel.Height = heightentry.Text;
        viewModel.Weight= weightentry.Text;
        personalInfo.DateOfBirth= HiddenDatePicker.SelectedDate;
        viewModel.Gender = (string?)Gender.SelectedItem;
        viewModel.ActiveStatus = (string?)ActiveStatus.SelectedItem;
        viewModel.BodyFat = (string?)BodyFat.SelectedItem;
        viewModel.MeasurementUnit= (string?)MeasurementUnits.SelectedItem;

        Navigation.PushAsync(new MainPage(viewModel,personalInfo));
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
    }
}