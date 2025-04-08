using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Popup;
using Syncfusion.Maui.Toolkit.EffectsView;

namespace FitnessTracker
{
	public partial class ActivityDayContentDesktop : ContentView
	{
        #region Fields
        Border details;
        SfDatePicker datePicker;
        SfTimePicker startTimePicker;
        SfTimePicker endTimePicker;
        Entry datePickerEntry;
        Entry startTimePickerEntry;
        Entry endTimePickerEntry;
        VerticalStackLayout viewActivityContent;
        ScrollView editActivityContent;
        Label deleteActivityContent;
        Grid viewActivityFooter;
        Grid editActivityFooter;
        Grid deleteActivityFooter;
        SfComboBox activityBox;
        Entry activityTitle;
        Entry energyExpended;
        Editor remarks;
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        #endregion

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

        void OnDetailsTapped(object sender, TappedEventArgs e)
        {
            if(sender is SfEffectsView effectsview && effectsview.BindingContext is FitnessActivity activity)
            {
                detailspopup.BindingContext = activity;
                detailspopup.ShowRelativeToView(details, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottomLeft);
            }
            
        }

        void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if(e.Element is View child)
            {
                if(child.StyleId == "details")
                {
                    details = (Border)child;
                }
                else if (child.StyleId == "datePicker")
                {
                    datePicker = (SfDatePicker)child;
                }
                else if (child.StyleId == "datePickerEntry")
                {
                    datePickerEntry = (Entry)child;
                }
                else if (child.StyleId == "startTimePicker")
                {
                    startTimePicker = (SfTimePicker)child;
                }
                else if (child.StyleId == "startTimePickerEntry")
                {
                    startTimePickerEntry = (Entry)child;
                }
                else if (child.StyleId == "endTimePicker")
                {
                    endTimePicker = (SfTimePicker)child;
                }
                else if (child.StyleId == "endTimePickerEntry")
                {
                    endTimePickerEntry = (Entry)child;
                }
                else if(child.StyleId == "viewActivityContent")
                {
                    viewActivityContent = (VerticalStackLayout)child;
                }
                else if(child.StyleId == "editActivityContent")
                {
                    editActivityContent = (ScrollView)child;
                }
                else if(child.StyleId == "deleteActivityContent")
                {
                    deleteActivityContent = (Label)child;
                }
                else if(child.StyleId == "viewActivityFooter")
                {
                    viewActivityFooter = (Grid)child;
                }
                else if(child.StyleId == "editActivityFooter")
                {
                    editActivityFooter = (Grid)child;
                }
                else if(child.StyleId == "deleteActivityFooter")
                {
                    deleteActivityFooter = (Grid)child; 
                }
                else if(child.StyleId == "activityBox")
                {
                    activityBox = (SfComboBox)child;
                }
                else if(child.StyleId == "activityTitle")
                {
                    activityTitle = (Entry)child;
                }
                else if(child.StyleId == "energyExpended")
                {
                    energyExpended = (Entry)child;
                }
                else if(child.StyleId == "remarks")
                {
                    remarks = (Editor)child;
                }
            }
        }

        void OnViewTapped(object sender, TappedEventArgs e)
        {
            if(detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "View Activity";
                editpopup.HeightRequest = 624;
                editpopup.WidthRequest = 480;
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
                viewActivityContent.IsVisible = true;
                viewActivityFooter.IsVisible = true;
            }
        }

        void OnEditTapped(object sender, TappedEventArgs e)
        {
            if (detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "Edit Activity";
                editpopup.HeightRequest = 624;
                editpopup.WidthRequest = 480;
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
                editActivityContent.IsVisible = true;
                editActivityFooter.IsVisible = true;
                activityBox.ItemsSource = activityList;
                activityBox.SelectedItem = activity.ActivityType;
            }
        }

        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
        }

        void startTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        void StartTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        void startTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (startTimePicker.SelectedTime != null)
            {
                startTimePickerEntry.Text = DateTime.Today.Add(startTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        void endTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        void EndTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        void endTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (endTimePicker.SelectedTime != null)
            {
                endTimePickerEntry.Text = DateTime.Today.Add(endTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        void OnDeleteTapped(object sender, TappedEventArgs e)
        {
            if (detailspopup.BindingContext is FitnessActivity activity)
            {
                editpopup.BindingContext = activity;
                editpopup.HeaderTitle = "Delete Activity";
                editpopup.HeightRequest = 238;
                editpopup.WidthRequest = 354;
                editpopup.ShowFooter = true;
                editpopup.IsOpen = true;
                deleteActivityContent.IsVisible = true;
                deleteActivityFooter.IsVisible = true;
            }
        }

        void editpopup_Closed(object sender, EventArgs e)
        {
            viewActivityContent.IsVisible = false;
            editActivityContent.IsVisible = false;
            deleteActivityContent.IsVisible = false;
            viewActivityFooter.IsVisible = false;
            editActivityFooter.IsVisible = false;
            deleteActivityFooter.IsVisible = false;
        }

        void OnEditButtonTapped(object sender, TappedEventArgs e)
        {
            viewActivityContent.IsVisible = false;
            viewActivityFooter.IsVisible = false;
            OnEditTapped(sender, e);
        }

        void OnDeleteButtonTapped(object sender, TappedEventArgs e)
        {
            viewActivityContent.IsVisible = false;
            viewActivityFooter.IsVisible = false;
            OnDeleteTapped(sender, e);
        }

        void OnCloseButtonTapped(object sender, TappedEventArgs e)
        {
            editpopup.IsOpen = false;
        }

        void OnDeleteActivityTapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is FitnessViewModel viewModel && editpopup.BindingContext is FitnessActivity activity)
            {
                viewModel?.Activities?.Remove(activity);
            }

            OnCloseButtonTapped(sender, e);
        }

        void OnSaveTapped(object sender, TappedEventArgs e)
        {
            if (editpopup.BindingContext is FitnessActivity activity)
            {
                double energy = double.Parse(energyExpended.Text);
                activity.CaloriesBurned = energy;
                activity.ActivityType = (string)activityBox.SelectedItem!;
                activity.Title = activityTitle.Text;
                activity.Remarks = remarks.Text;
                if (datePicker.SelectedDate is not null)
                {
                    DateTime selectedDate = (DateTime)datePicker.SelectedDate;
                    if (startTimePicker.SelectedTime is TimeSpan startTime)
                    {
                        activity.StartTime = selectedDate.Date + startTime;
                    }

                    if (endTimePicker.SelectedTime is TimeSpan endTime)
                    {
                        activity.EndTime = selectedDate.Date + endTime;
                    }
                }
            }

            OnCloseButtonTapped(sender, e);
        }
    }
}