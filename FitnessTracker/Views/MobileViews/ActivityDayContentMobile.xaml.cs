namespace FitnessTracker
{
	public partial class ActivityDayContentMobile : ContentView
	{
		public ActivityDayContentMobile()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            var color = (Application.Current!.RequestedTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
            nextIconLabel.TextColor = (calendar.SelectedDate.Value.Date == DateTime.Today.Date) ? Colors.LightGray : color;
        }

        void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(-1);
            }
        }

        void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
            }
        }

        async void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate.Value;
                calendar.IsOpen = false;
                await Task.Delay(100);
                nextIcon.IsEnabled = (viewModel.SelectedDate.Date != DateTime.Today.Date);
                var color = (Application.Current!.RequestedTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
                nextIconLabel.TextColor = (viewModel.SelectedDate.Date == DateTime.Today.Date) ? Colors.LightGray : color;
            }
        }

        void ActivityItem_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count == 0)
            {
                return;
            }

            var selectedActivity = e.CurrentSelection.FirstOrDefault() as FitnessActivity;
            if (selectedActivity is not null)
            {
                Navigation.PushAsync(new ActivityItemDetailPageMobile(selectedActivity));
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}