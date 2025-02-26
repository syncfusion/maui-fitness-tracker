using FitnessTracker.Models;

namespace FitnessTracker.Views;

public partial class EditProfilePage : ContentPage
{
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
        this.personinforlayout.BindingContext = personalviewmodel;
        this.physicalinfolayout.BindingContext = physicalviewmodel;
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
}