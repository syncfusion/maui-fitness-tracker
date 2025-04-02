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
        }

        void MonthLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendarDialog.IsOpen = true;
        }

        void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null)
            {
                calendarDialog.SelectedDate = calendarDialog.SelectedDate.Value.AddMonths(-1);
            }
        }

        void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null && calendarDialog.SelectedDate.Value.Month != DateTime.Today.Month)
            {
                calendarDialog.SelectedDate = calendarDialog.SelectedDate.Value.AddMonths(1);
            }
        }

        void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendarDialog.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendarDialog.SelectedDate.Value;
                calendarLayout.DisplayDate = viewModel.SelectedDate.Date;
                calendarDialog.IsOpen = false;
                nextIcon.IsEnabled = (calendarDialog.SelectedDate.Value.Month != DateTime.Today.Month);
            }
        }

        void OnWeekSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is WeeklyStepData weekrange)
            {
                if (BindingContext is FitnessViewModel viewModel)
                {
                    DateTime startdate = ParseWeekRange(weekrange.WeekRange);
                    viewModel.SelectedDate = startdate;  // Update the selected date
                    viewModel.SelectedTabIndex = 1;       // Navigate to Day tab
                }
            }
        }

        DateTime ParseWeekRange(string weekRange)
        {
            var parts = weekRange.Split(" - ");
            if (parts.Length != 2)
            {
                return DateTime.Now;
            }

            string startDateString = parts[0]; // Example: "30 Dec"
            string endDateString = parts[1];   // Example: "5 Jan"

            int currentYear = DateTime.Now.Year;

            // Get the numeric month of start and end dates
            if (DateTime.TryParseExact(startDateString + " " + currentYear, "d MMMM yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                DateTime.TryParseExact(endDateString + " " + currentYear, "d MMMM yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                // If the start month is December and the end month is January, adjust the year
                if (startDate.Month == 12 && endDate.Month == 1)
                {
                    startDate = startDate.AddYears(-1); // Move start date to previous year
                }
            }

            return startDate;
        }
    }
}