namespace FitnessTracker
{
	public partial class ActivityWeekContent : ContentView
	{
		public ActivityWeekContent ()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
            nextIconLabel.TextColor = (calendar.SelectedDate.Value.AddDays(7) <= DateTime.Today.Date) ? color : Colors.LightGray;
        }

        void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                var startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek);
                calendar.SelectedDate = startOfWeek.AddDays(-7);
            }
        }

        void NextIcon_Tapped(object sender, TappedEventArgs e)
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

        async void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate.Value;
                calendar.IsOpen = false;
                await Task.Delay(100);
                nextIcon.IsEnabled = (viewModel.SelectedDate.AddDays(7) <= DateTime.Today.Date);
                var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
                nextIconLabel.TextColor = (viewModel.SelectedDate.AddDays(7) <= DateTime.Today.Date) ? color : Colors.LightGray;
            }
        }

        void OnDaySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is FitnessActivity activity)
            {
                if (BindingContext is FitnessViewModel viewModel)
                {
                    viewModel.SelectedDate = activity.StartTime.Date;  // Update the selected date
                    viewModel.SelectedTabIndex = 0;       // Navigate to Day tab
                }
            }
        }
    }
}