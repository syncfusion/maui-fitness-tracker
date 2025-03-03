
namespace FitnessTracker.Views
{
	public partial class ActivityCustomViewPage : ContentPage
	{
		public ActivityCustomViewPage (FitnessViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
        }

        private void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}