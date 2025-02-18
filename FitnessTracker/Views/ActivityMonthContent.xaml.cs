
using Microsoft.Maui.Controls.Shapes;
using System.Globalization;

namespace FitnessTracker
{
	public partial class ActivityMonthContent : ContentView
	{
        FitnessViewModel viewModel;
		public ActivityMonthContent ()
		{
			InitializeComponent ();
            //viewModel = new FitnessViewModel ();
            //BindingContext = viewModel;
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            monthLabel.Text = calendar.SelectedDate.Value.ToString("MMMM yyyy");
        }

        private void MonthLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        private void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddMonths(-1);
                monthLabel.Text = calendar.SelectedDate.Value.ToString("MMMM yyyy");
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddMonths(1);
                monthLabel.Text = calendar.SelectedDate.Value.ToString("MMMM yyyy");
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                monthLabel.Text = calendar.SelectedDate.Value.ToString("MMMM yyyy");
                calendar.IsOpen = false;
            }
        }
    }
}