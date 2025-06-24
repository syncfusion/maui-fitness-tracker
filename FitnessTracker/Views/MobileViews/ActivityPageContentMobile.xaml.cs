namespace FitnessTracker;

public partial class ActivityPageContentMobile : ContentView
{
	public ActivityPageContentMobile()
	{
		InitializeComponent();
		calendar.MaximumDate = DateTime.Today;
		calendar.SelectedDate = DateTime.Today;
        dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
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
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
		}
    }

    void NextIcon_Tapped(object sender, TappedEventArgs e)
    {
        if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
        {
            calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
            dayLabel.Text= calendar.SelectedDate.Value.ToString("ddd, d MMM");
        }
    }

    async void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
        {
            viewModel.ActivityTabSelectedDate = calendar.SelectedDate.Value;
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
            calendar.IsOpen= false;
            await Task.Delay(100);
            nextIcon.IsEnabled = (viewModel.ActivityTabSelectedDate != DateTime.Today);
            var color = (Application.Current!.RequestedTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
            nextIconLabel.TextColor = (viewModel.ActivityTabSelectedDate.Date == DateTime.Today.Date) ? Colors.LightGray : color;
        }
    }

    void StepCount_Tapped(object sender, TappedEventArgs e)
    {
        if(BindingContext is FitnessViewModel viewModel)
        {
            Navigation.PushAsync(new ActivityCustomViewPageMobile(viewModel));
        }
    }
}