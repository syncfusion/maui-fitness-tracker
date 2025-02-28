using FitnessTracker.Models;

namespace FitnessTracker.Views;

public partial class EditProfilePage : ContentPage
{
    PersonalInfo _personalInfoviewmodel;
    PhysicalInfo _physicalInfoviewmodel;
    #region List

    List<string> GendersList = new List<string> { "Male", "Female", "Non-Binary", "Other" };

    List<string> BodyFatLevelsList = new List<string> { "Low", "Medium", "High" };

    List<string> ActiveStatusesList = new List<string> { "Sedentary", "Lightly Active", "Moderately Active", "Very Active" };

    List<string> MeasurementUnitsList = new List<string> { "Cm", "Inches" };
    List<string> weightList = new List<string> { "Kg" };
    List<string> heightList = new List<string> { "Cm" };

    #endregion
    public EditProfilePage(PhysicalInfo physicalviewmodel,PersonalInfo personalviewmodel)
	{
		InitializeComponent();
        _physicalInfoviewmodel=physicalviewmodel;
        _personalInfoviewmodel = personalviewmodel;
        if (!string.IsNullOrWhiteSpace(personalviewmodel.Name))
        {
            var parts = personalviewmodel.Name.Split(' ', 2);
            personalviewmodel.FirstName = parts[0];
            personalviewmodel.LastName = parts.Length > 1 ? parts[1] : string.Empty;
        }
        else
        {
            personalviewmodel.FirstName = string.Empty;
            personalviewmodel.LastName = string.Empty;
        }
        this.personinforlayout.BindingContext = _personalInfoviewmodel;
        this.physicalinfolayout.BindingContext = _physicalInfoviewmodel;
        genderBox.ItemsSource = GendersList;
        bodyFatBox.ItemsSource = BodyFatLevelsList;
        activeStatusBox.ItemsSource = ActiveStatusesList;
        measurementUnitBox.ItemsSource = MeasurementUnitsList;
        weightBox.ItemsSource = weightList;
        heightBox.ItemsSource = heightList;
        weightBox.SelectedIndex = 0;
        heightBox.SelectedIndex = 0;
    }

    void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

    void Savebutton_Clicked(object sender, EventArgs e)
    {
        _personalInfoviewmodel.Name = $"{firstnameentry.Text?.Trim()} {lastnameentry.Text?.Trim()}".Trim();
        _personalInfoviewmodel.FirstName = firstnameentry.Text;
        _personalInfoviewmodel.LastName= lastnameentry.Text;
        _personalInfoviewmodel.DateOfBirth = HiddenDatePicker.SelectedDate;
        _physicalInfoviewmodel.Gender = (string?)genderBox.SelectedItem;
        _physicalInfoviewmodel.ActiveStatus = (string?)activeStatusBox.SelectedItem;
        _physicalInfoviewmodel.BodyFat = (string?)bodyFatBox.SelectedItem;
        _physicalInfoviewmodel.MeasurementUnit = (string?)measurementUnitBox.SelectedItem;
        Navigation.PushAsync(new MainPage(_physicalInfoviewmodel,_personalInfoviewmodel));
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