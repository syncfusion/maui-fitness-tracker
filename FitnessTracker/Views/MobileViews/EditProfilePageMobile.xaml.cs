namespace FitnessTracker;

public partial class EditProfilePageMobile : ContentPage
{
    PersonalInfo _personalInfoviewModel;
    PhysicalInfo _physicalInfoviewModel;
    #region List

    List<string> GendersList = new List<string> { "Male", "Female", "Non-Binary", "Other" };

    List<string> BodyFatLevelsList = new List<string> { "Low", "Medium", "High" };

    List<string> ActiveStatusesList = new List<string> { "Sedentary", "Lightly Active", "Moderately Active", "Very Active" };

    List<string> MeasurementUnitsList = new List<string> { "Cm", "Inches" };
    List<string> weightList = new List<string> { "Kg" };
    List<string> heightList = new List<string> { "Cm" };

    #endregion
    public EditProfilePageMobile(PhysicalInfo physicalviewmodel,PersonalInfo personalviewmodel)
	{
		InitializeComponent();
        _physicalInfoviewModel=physicalviewmodel;
        _personalInfoviewModel = personalviewmodel;
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

        personinforlayout.BindingContext = _personalInfoviewModel;
        physicalinfolayout.BindingContext = _physicalInfoviewModel;
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
        _personalInfoviewModel.Name = $"{firstnameentry.Text?.Trim()} {lastnameentry.Text?.Trim()}".Trim();
        _personalInfoviewModel.FirstName = firstnameentry.Text;
        _personalInfoviewModel.LastName= lastnameentry.Text;
        _personalInfoviewModel.DateOfBirth = HiddenDatePicker.SelectedDate;
        _physicalInfoviewModel.Gender = (string?)genderBox.SelectedItem;
        _physicalInfoviewModel.ActiveStatus = (string?)activeStatusBox.SelectedItem;
        _physicalInfoviewModel.BodyFat = (string?)bodyFatBox.SelectedItem;
        _physicalInfoviewModel.MeasurementUnit = (string?)measurementUnitBox.SelectedItem;
        Navigation.PushAsync(new MainPageMobile(_physicalInfoviewModel,_personalInfoviewModel));
    }

    void DatePicker_Tapped(object sender, TappedEventArgs e)
    {
        HiddenDatePicker.IsVisible = true;
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