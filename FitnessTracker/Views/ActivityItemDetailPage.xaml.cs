namespace FitnessTracker
{
	public partial class ActivityItemDetailPage : ContentPage
	{
		public ActivityItemDetailPage (FitnessActivity activity)
		{
			InitializeComponent ();
            BindingContext = activity;
        }

        void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync ();
        }

        void EditIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new EditActivityPage(BindingContext as FitnessActivity));
        }

        void DeleteIcon_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}