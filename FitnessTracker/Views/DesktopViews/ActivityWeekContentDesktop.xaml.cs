namespace FitnessTracker
{
    public partial class ActivityWeekContentDesktop : ContentView
    {
        public ActivityWeekContentDesktop()
        {
            InitializeComponent();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
        }

        void DayLabel_Tapped(object sender, TappedEventArgs e)
        {
            calendar.IsOpen = true;
        }

        void PreviousIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                var startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek);
                calendar.SelectedDate = startOfWeek.AddDays(-7);
            }
        }

        void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null)
            {
                var startOfWeek = calendar.SelectedDate.Value.AddDays(-(int)calendar.SelectedDate.Value.DayOfWeek);
                var nextWeek = startOfWeek.AddDays(7);

                if (nextWeek <= DateTime.Today)
                {
                    calendar.SelectedDate = nextWeek;
                }
            }
        }

        void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate.Value;
                calendar.IsOpen = false;
                nextIcon.IsEnabled = (viewModel.SelectedDate.AddDays(7) <= DateTime.Today);
            }
        }
    }
}