namespace FitnessTracker;

public partial class JournalPageContent : ContentView
{
	public JournalPageContent()
	{
		InitializeComponent();
        calendar.MaximumDate = DateTime.Today;
        calendar.SelectedDate = DateTime.Today;
        dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
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
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
        }
    }

    void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        if (calendar.SelectedDate is not null)
        {
            dayLabel.Text = calendar.SelectedDate.Value.ToString("ddd, d MMM");
            calendar.IsOpen = false;
        }
    }
}