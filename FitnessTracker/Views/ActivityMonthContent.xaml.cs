
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
            calendarDialog.MaximumDate = DateTime.Today;
            calendarDialog.SelectedDate = DateTime.Today;
            monthLabel.Text = calendarDialog.SelectedDate.Value.ToString("MMMM yyyy");
        }

        private void MonthLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendarDialog.IsOpen = true;
        }

        private void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null)
            {
                calendarDialog.SelectedDate = calendarDialog.SelectedDate.Value.AddMonths(-1);
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null && calendarDialog.SelectedDate.Value.Month != DateTime.Today.Month)
            {
                calendarDialog.SelectedDate = calendarDialog.SelectedDate.Value.AddMonths(1);
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedMonth = calendarDialog.SelectedDate.Value;
                calendarLayout.DisplayDate = viewModel.SelectedMonth.Date;
                monthLabel.Text = calendarDialog.SelectedDate.Value.ToString("MMMM yyyy");
                calendarDialog.IsOpen = false;
                nextIcon.IsEnabled = (calendarDialog.SelectedDate.Value.Month != DateTime.Today.Month);
            }
        }
    }
}