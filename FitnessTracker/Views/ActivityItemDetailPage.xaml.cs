
namespace FitnessTracker.Views
{
	public partial class ActivityItemDetailPage : ContentPage
	{
		public ActivityItemDetailPage ()
		{
			InitializeComponent ();
        }

        private void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync ();
        }
    }
}