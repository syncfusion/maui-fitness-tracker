using FitnessTracker.Views;
using System.Globalization;

namespace FitnessTracker
{
	public partial class ActivityDayContent : ContentView
	{
        FitnessViewModel _viewModel;
		public ActivityDayContent ()
		{
			InitializeComponent ();
            _viewModel = (FitnessViewModel)BindingContext;
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
        }

        private void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        private void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(-1);
                vm.SelectedDate = calendar.SelectedDate;
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
            }
        }

        private void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null && vm.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
                vm.SelectedDate = calendar.SelectedDate;
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
            }
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (BindingContext is FitnessViewModel vm && calendar.SelectedDate is not null)
            {
                vm.SelectedDate = calendar.SelectedDate;
                dayLabel.Text = calendar.SelectedDate.Value.ToString("dddd, d MMMM");
                calendar.IsOpen = false;
            }
        }

        private void ActivityItem_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count == 0)
            {
                return;
            }

            Navigation.PushAsync(new ActivityItemDetailPage());
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}