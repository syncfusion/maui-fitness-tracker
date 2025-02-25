
using System.Globalization;

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
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(-7);
                DateTime startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek + (int)DayOfWeek.Sunday); // Start from Sunday
                DateTime endOfWeek = startOfWeek.AddDays(6);

                // Ensuring end date does not exceed today
                if (endOfWeek > DateTime.Today)
                    endOfWeek = DateTime.Today;
                vm.SelectedWeek = endOfWeek;
                UpdateWeekLabel(vm.SelectedWeek.Value);
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null && vm.SelectedWeek!.Value.AddDays(7) <= DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(7);
                DateTime startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek + (int)DayOfWeek.Sunday); // Start from Sunday
                DateTime endOfWeek = startOfWeek.AddDays(6);

                // Ensuring end date does not exceed today
                if (endOfWeek > DateTime.Today)
                    endOfWeek = DateTime.Today;
                vm.SelectedWeek = endOfWeek;
                UpdateWeekLabel(vm.SelectedWeek.Value);
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null)
            {
                vm.SelectedWeek = calendar.SelectedDate;
                UpdateWeekLabel(vm.SelectedWeek.Value);
                calendar.IsOpen = false;
            }
        }

        private void UpdateWeekLabel(DateTime selectedDate)
        {
            DateTime startOfWeek = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (int)DayOfWeek.Sunday); // Start from Sunday
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // Ensuring end date does not exceed today
            if (endOfWeek > DateTime.Today)
                endOfWeek = DateTime.Today;

            dayLabel.Text = $"{startOfWeek:dd MMMM} - {endOfWeek:dd MMMM}";
        }
    }
}