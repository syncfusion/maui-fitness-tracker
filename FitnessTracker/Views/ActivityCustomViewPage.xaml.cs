﻿namespace FitnessTracker
{
	public partial class ActivityCustomViewPage : ContentPage
	{
		public ActivityCustomViewPage (FitnessViewModel viewModel)
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