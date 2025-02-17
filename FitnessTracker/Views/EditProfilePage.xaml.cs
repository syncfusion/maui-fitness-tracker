namespace FitnessTracker.Views;

public partial class EditProfilePage : ContentPage
{
    List<string> weightList = new List<string> { "Kg" };
    List<string> heightList = new List<string> { "Cm" };
    public EditProfilePage()
	{
		InitializeComponent();
        weightBox.ItemsSource = weightList;
        heightBox.ItemsSource = heightList;
        weightBox.SelectedIndex = 0;
        heightBox.SelectedIndex = 0;
    }

    private void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }
}