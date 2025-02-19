
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

        private void EditIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new EditActivityPage());
        }

        private void DeleteIcon_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}