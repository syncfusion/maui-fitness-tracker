using Syncfusion.Maui.Toolkit.EffectsView;

namespace FitnessTracker
{
    public partial class ActivityWeekContentDesktop : ContentView
    {
        Border? details;

        public ActivityWeekContentDesktop()
        {
            InitializeComponent();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
            nextIconLabel.TextColor = (calendar.SelectedDate.Value.AddDays(7) <= DateTime.Today.Date) ? color : Colors.LightGray;
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
                nextIcon.IsEnabled = (viewModel.SelectedDate.AddDays(7) <= DateTime.Today.Date);
                var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
                nextIconLabel.TextColor = (viewModel.SelectedDate.AddDays(7) <= DateTime.Today.Date) ? color : Colors.LightGray;
            }
        }

        void OnDetailsTapped(object sender, TappedEventArgs e)
        {
            if (sender is SfEffectsView effectsview && effectsview.BindingContext is FitnessActivity activity)
            {
                var parentBorder = GetParentOfType<Border>(effectsview);

                if (parentBorder != null)
                {
                    detailspopup.BindingContext = activity;
                    detailspopup.ShowRelativeToView(parentBorder, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottomLeft, 50, 0);
                }
            }
        }

        T? GetParentOfType<T>(Element? element) where T : Element
        {
            while (element != null)
            {
                if (element is T typedElement)
                {
                    return typedElement;
                }

                element = element.Parent;
            }

            return null;
        }

        void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is View child)
            {
                if (child.StyleId == "details")
                {
                    details = (Border)child;
                }
            }
        }

        void OnViewTapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel viewModel && detailspopup.BindingContext is FitnessActivity activity)
            {
                viewModel.SelectedDate = activity.StartTime.Date;
                viewModel.SelectedTabIndex = 0;
                detailspopup.IsOpen = false;
            }
        }
    }
}