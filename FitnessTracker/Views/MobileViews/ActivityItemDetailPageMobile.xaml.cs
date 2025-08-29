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
            var activity = BindingContext as FitnessActivity;
            if (activity != null)
            {
                Navigation.PushAsync(new EditActivityPageMobile(activity));
            }
        }

        void DeleteIcon_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}