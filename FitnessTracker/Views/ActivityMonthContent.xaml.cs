
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.Calendar;
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
            if (BindingContext is FitnessViewModel vm && calendarDialog.SelectedDate is not null)
            {
                calendarLayout.Backward();
                vm.SelectedMonth = vm.SelectedMonth.AddMonths(-1);
                monthLabel.Text = vm.SelectedMonth.ToString("MMMM yyyy");
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendarDialog.SelectedDate is not null)
            { 
                calendarLayout.Forward();
                vm.SelectedMonth = vm.SelectedMonth.AddMonths(1);
                monthLabel.Text = vm.SelectedMonth.ToString("MMMM yyyy");
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendarDialog.SelectedDate is not null)
            {
                monthLabel.Text = calendarDialog.SelectedDate.Value.ToString("MMMM yyyy");
                vm.SelectedMonth = (DateTime)calendarDialog.SelectedDate;
                calendarLayout.SelectedDate = vm.SelectedMonth;
                calendarDialog.IsOpen = false;
            }
        }
    }
}