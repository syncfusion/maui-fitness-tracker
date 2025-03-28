using Syncfusion.Maui.Popup;
using Syncfusion.Maui.Toolkit.EffectsView;

namespace FitnessTracker
{
	public partial class ActivityDayContentDesktop : ContentView
	{
		public ActivityDayContentDesktop ()
		{
			InitializeComponent ();
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
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(-1);
            }
        }

        void NextIcon_Tapped(object sender, TappedEventArgs e)
        {
            if (calendar.SelectedDate is not null && calendar.SelectedDate != DateTime.Today)
            {
                calendar.SelectedDate = calendar.SelectedDate.Value.AddDays(1);
            }
        }

        void Calendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate is not null && BindingContext is FitnessViewModel viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate.Value;
                calendar.IsOpen = false;
                nextIcon.IsEnabled = (viewModel.SelectedDate != DateTime.Today);
            }
        }

        void ActivityItem_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
            {
                return;
            }

            var selectedActivity = e.CurrentSelection.FirstOrDefault() as FitnessActivity;
            if (selectedActivity is not null)
            {
                Navigation.PushAsync(new ActivityItemDetailPage(selectedActivity));
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        void OnDetailsTapped(object sender, TappedEventArgs e)
        {
            detailspopup.ShowRelativeToView(details, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottomLeft);
        }

        Border details;
        //SfPopup detailspopup;
        void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if(e.Element is View child)
            {
                if(child.StyleId == "details")
                {
                    details = (Border)child;
                }
                else if(child.StyleId == "detailspopup")
                {
                    //detailspopup = (SfPopup)child;
                }
            }
        }
    }
}