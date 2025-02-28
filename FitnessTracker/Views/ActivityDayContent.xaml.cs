using FitnessTracker.Views;

namespace FitnessTracker
{
	public partial class ActivityDayContent : ContentView
	{
		public ActivityDayContent ()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
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
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate.Value;
                calendar.IsOpen = false;
                nextIcon.IsEnabled = (viewModel.SelectedDate != DateTime.Today);
            }
        }

        private void ActivityItem_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count == 0)
            {
                return;
            }

            var selectedActivity = e.CurrentSelection.FirstOrDefault() as FitnessActivity;
            if (selectedActivity is not null)
            {
                Navigation.PushAsync(new ActivityItemDetailPage(selectedActivity));
            }
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}