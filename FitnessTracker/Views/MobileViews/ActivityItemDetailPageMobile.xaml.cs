namespace FitnessTracker
{
	public partial class ActivityItemDetailPageMobile : ContentPage
	{
		public ActivityItemDetailPageMobile(FitnessActivity activity)
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
            Navigation.PushAsync(new EditActivityPageMobile(BindingContext as FitnessActivity));
        }

        void DeleteIcon_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}