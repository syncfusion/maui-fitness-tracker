using FitnessTracker.Views;

namespace FitnessTracker;

public partial class ActivityPageContent : ContentView
{
	public ActivityPageContent()
	{
		InitializeComponent();
		calendar.MaximumDate = DateTime.Today;
		calendar.SelectedDate = DateTime.Today;
        dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
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
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
		}
    }

    private void NextIcon_Tapped(object sender, TappedEventArgs e)
    {
        if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
        {
            calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
            dayLabel.Text= calendar.SelectedDate.Value.ToString("ddd, d MMM");
        }
    }

    private async void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
        {
            viewModel.ActivityTabSelectedDate = calendar.SelectedDate.Value;
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
            calendar.IsOpen= false;
            await Task.Delay(100);
            nextIcon.IsEnabled = (viewModel.ActivityTabSelectedDate != DateTime.Today);
        }
    }

    private void StepCount_Tapped(object sender, TappedEventArgs e)
    {
        if(BindingContext is FitnessViewModel viewModel)
        {
            Navigation.PushAsync(new ActivityCustomViewPage(viewModel));
        }
    }
}