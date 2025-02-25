namespace FitnessTracker.Views;

public partial class AIAssistViewPage : ContentPage
{
	public AIAssistViewPage()
	{
		InitializeComponent();
	}

    void CloseButton_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopAsync();
    }
}