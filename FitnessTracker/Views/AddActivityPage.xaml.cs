﻿
namespace FitnessTracker.Views
{
    public partial class AddActivityPage : ContentPage
    {
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Hiking", "Aerobics", "Elliptical Training", "Strength Training", "Stair Climbing", "Yoga", "Dancing", "Martial Arts", "Pilates", "Meditation", "Rowing", "CrossFit" };
        public AddActivityPage()
        {
            InitializeComponent();
            activityBox.ItemsSource = activityList;
            LoadStartEndTime();
        }

        void LoadStartEndTime()
        {
            List<string> timeSlots = new List<string>();

            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute += 15)
                {
                    string period = hour < 12 ? "am" : "pm";
                    int displayHour = (hour % 12 == 0) ? 12 : hour % 12;
                    timeSlots.Add($"{displayHour}:{minute:00} {period}");
                }
            }

            startTimeBox.ItemsSource = timeSlots;
            endTimeBox.ItemsSource = timeSlots;
        }
    }
}