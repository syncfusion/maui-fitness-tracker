using System.Reflection;

namespace FitnessTracker;

public partial class ProfilesetupPageDesktop : ContentPage
{
    #region List

    List<string> GendersList = new List<string> { "Male", "Female", "Non-Binary", "Other" };

    List<string> BodyFatLevelsList = new List<string> { "Low", "Medium", "High" };

    List<string> ActiveStatusesList = new List<string> { "Sedentary", "Lightly Active", "Moderately Active", "Very Active" };

    List<string> MeasurementUnitsList = new List<string> { "Cm", "Inches" };
    List<string> weightList = new List<string> { "Kg", "lb"};
    List<string> heightList = new List<string> { "Cm" , "m","in","ft"};

    #endregion
    PhysicalInfo physicalInfo;
    PersonalInfo personalInfo;
    public ProfilesetupPageDesktop(PersonalInfo personalInfoviewmodel, PhysicalInfo physicalInfoviewmodel)
	{
		InitializeComponent();
        Gender.ItemsSource = GendersList;
        ActiveStatus.ItemsSource = ActiveStatusesList;
        BodyFat.ItemsSource = BodyFatLevelsList;
        MeasurementUnits.ItemsSource = MeasurementUnitsList;
        weightcombo.ItemsSource = weightList;
        heightcombo.ItemsSource = heightList;
        physicalInfo = physicalInfoviewmodel;
        personalInfo = personalInfoviewmodel;
        BindingContext = physicalInfo;
    }

    void FinishsetupButton_Clicked(object sender, EventArgs e)
    {
        physicalInfo.Height = heightentry.Text;
        physicalInfo.Weight = weightentry.Text;
        personalInfo.DateOfBirth = HiddenDatePicker.SelectedDate;
        physicalInfo.Gender = (string?)Gender.SelectedItem;
        physicalInfo.ActiveStatus = (string?)ActiveStatus.SelectedItem;
        physicalInfo.BodyFat = (string?)BodyFat.SelectedItem;
        physicalInfo.MeasurementUnit = (string?)MeasurementUnits.SelectedItem;

        Navigation.PushAsync(new MainPageDesktop(personalInfo,physicalInfo));
    }
    void DatePicker_Tapped(object sender, TappedEventArgs e)
    {
        HiddenDatePicker.IsVisible = true;
        HiddenDatePicker.IsOpen = true;
    }

    void HiddenDatePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
    {
        if (e.NewValue is DateTime dateValue)
        {
            string formattedDate = dateValue.Date.ToString("dd/MM/yyyy");
            DateEntry.Text = formattedDate;
            HiddenDatePicker.IsVisible = false;
        }
    }
}