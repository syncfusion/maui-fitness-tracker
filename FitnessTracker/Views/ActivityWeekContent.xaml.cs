
namespace FitnessTracker
{
	public partial class ActivityWeekContent : ContentView
	{
		public ActivityWeekContent ()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            UpdateWeekLabel(calendar.SelectedDate.Value);
        }

        private void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        private void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                var startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek);
                calendar.SelectedDate = startOfWeek.AddDays(-7);
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                var startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek);
                var nextWeek = startOfWeek.AddDays(7);

                if (nextWeek <= DateTime.Today)
                {
                    calendar.SelectedDate = nextWeek;
                }
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedWeek = calendar.SelectedDate.Value;
                UpdateWeekLabel(calendar.SelectedDate.Value);
                calendar.IsOpen = false;
                nextIcon.IsEnabled = (viewModel.SelectedWeek.AddDays(7) <= DateTime.Today);
            }
        }

        private void UpdateWeekLabel(DateTime selectedDate)
        {
            DateTime startOfWeek = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (int)DayOfWeek.Sunday); // Start from Sunday
            DateTime endOfWeek = startOfWeek.AddDays(6);
            dayLabel.Text = $"{startOfWeek:dd MMMM} - {endOfWeek:dd MMMM}";
        }
    }
}