namespace FitnessTracker
{
	public partial class ActivityItemDetailPage : ContentPage
	{
		public ActivityItemDetailPage (FitnessActivity activity)
		{
			InitializeComponent ();
            BindingContext = activity;
        }

        private void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync ();
        }

        private void EditIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new EditActivityPage(BindingContext as FitnessActivity));
        }

        private void DeleteIcon_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}