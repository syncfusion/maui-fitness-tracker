﻿
namespace FitnessTracker.Views
{
    public partial class TrackActivity : ContentPage
    {
        List<string> activityList = new List<string>{ "Walking", "Running", "Cycling", "Swimming", "Hiking", "Aerobics", "Elliptical Training", "Strength Training", "Stair Climbing", "Yoga", "Dancing", "Martial Arts", "Pilates", "Meditation", "Rowing", "CrossFit" };
        public TrackActivity()
        {
            InitializeComponent();
            activityBox.ItemsSource = activityList;
            activityBox.SelectedIndex = 0;
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            beforeClick.IsVisible = false;
            afterClick.IsVisible = true;
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            beforeClick.IsVisible = true;
            afterClick.IsVisible = false;
        }

        private void CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}