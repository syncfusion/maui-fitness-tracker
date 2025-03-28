using Syncfusion.Maui.Picker;
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
            if(sender is SfEffectsView effectsview && effectsview.BindingContext is FitnessActivity activity)
            {
                detailspopup.BindingContext = activity;
                detailspopup.ShowRelativeToView(details, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottomLeft);
            }
            
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

        void OnViewTapped(object sender, TappedEventArgs e)
        {
            if(detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "View Activity";
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
            }
        }

        void OnEditTapped(object sender, TappedEventArgs e)
        {
            if (detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "Edit Activity";
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
            }
        }

        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            //datePicker.IsOpen = true;
        }

        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            //datePicker.IsOpen = true;
        }

        void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            //if (datePicker.SelectedDate != null)
            //{
            //    datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            //}
        }

        void startTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            //startTimePicker.IsOpen = true;
        }

        void StartTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            //startTimePicker.IsOpen = true;
        }

        void startTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            //if (startTimePicker.SelectedTime != null)
            //{
            //    startTimePickerEntry.Text = DateTime.Today.Add(startTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            //}
        }

        void endTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            //endTimePicker.IsOpen = true;
        }

        void EndTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            //endTimePicker.IsOpen = true;
        }

        void endTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            //if (endTimePicker.SelectedTime != null)
            //{
            //    endTimePickerEntry.Text = DateTime.Today.Add(endTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            //}
        }

        private void OnDeleteTapped(object sender, TappedEventArgs e)
        {
            if (detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "Delete Activity";
                editpopup.HeightRequest = 238;
                editpopup.WidthRequest = 354;
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
            }
        }
    }
}