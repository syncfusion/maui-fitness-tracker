namespace FitnessTracker
{
	public partial class ActivityCustomViewPageMobile : ContentPage
	{
		public ActivityCustomViewPageMobile(FitnessViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
        }

        void BackIcon_Tapped(object sender, TappedEventArgs e)
        {
            tabview.SelectedIndex = 0;
            Navigation.PopAsync();
        }
    }
}