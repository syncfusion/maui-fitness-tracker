
namespace FitnessTracker.Views
{
	public partial class EditActivityPage : ContentPage
	{
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Hiking", "Aerobics", "Elliptical Training", "Strength Training", "Stair Climbing", "Yoga", "Dancing", "Martial Arts", "Pilates", "Meditation", "Rowing", "CrossFit" };
        List<string> timeSlots = new List<string>();
        public EditActivityPage ()
		{
			InitializeComponent ();
            LoadStartEndTime ();
            activityBox.ItemsSource = activityList;
            activityBox.SelectedIndex = 0;
		}

        void LoadStartEndTime()
        {
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