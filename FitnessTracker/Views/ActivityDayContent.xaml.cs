using FitnessTracker.Views;

namespace FitnessTracker
{
	public partial class ActivityDayContent : ContentView
	{
		public ActivityDayContent ()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = new DateTime(2025, 02, 01);
            dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
        }

        private void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        private void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(-1);
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
                calendar.IsOpen = false;
            }
        }

        private void ActivityItem_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count == 0)
            {
                return;
            }

            Navigation.PushAsync(new ActivityItemDetailPage());
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}