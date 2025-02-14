
namespace FitnessTracker.Views
{
	public partial class ActivityCustomViewPage : ContentPage
	{
		public ActivityCustomViewPage ()
		{
			InitializeComponent ();
        }

        private void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}