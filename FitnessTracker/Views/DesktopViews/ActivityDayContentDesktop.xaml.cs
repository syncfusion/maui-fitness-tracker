using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Popup;
using Syncfusion.Maui.Toolkit.EffectsView;

namespace FitnessTracker
{
	public partial class ActivityDayContentDesktop : ContentView
	{
        #region Fields
        Border _details;
        SfDatePicker _datePicker;
        SfTimePicker _startTimePicker;
        SfTimePicker _endTimePicker;
        Entry _datePickerEntry;
        Entry _startTimePickerEntry;
        Entry _endTimePickerEntry;
        VerticalStackLayout _viewActivityContent;
        ScrollView _editActivityContent;
        Label _deleteActivityContent;
        Grid _viewActivityFooter;
        Grid _editActivityFooter;
        Grid _deleteActivityFooter;
        SfComboBox _activityBox;
        Entry _activityTitle;
        Entry _energyExpended;
        Editor _remarks;
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        #endregion

        public ActivityDayContentDesktop ()
		{
			InitializeComponent ();
            calendar.MaximumDate = DateTime.Today;
            calendar.SelectedDate = DateTime.Today;
            var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
            nextIconLabel.TextColor = (calendar.SelectedDate.Value.Date == DateTime.Today.Date) ? Colors.LightGray : color;
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
                nextIcon.IsEnabled = (viewModel.SelectedDate.Date != DateTime.Today.Date);
                var color = (Application.Current!.UserAppTheme == AppTheme.Light) ? Color.FromArgb("#474648") : Color.FromArgb("#C9C6C8");
                nextIconLabel.TextColor = (viewModel.SelectedDate.Date == DateTime.Today.Date) ? Colors.LightGray : color;
            }
        }

        void OnDetailsTapped(object sender, TappedEventArgs e)
        {
            if(sender is SfEffectsView effectsview && effectsview.BindingContext is FitnessActivity activity)
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


        void OnChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is not View child || string.IsNullOrEmpty(child.StyleId))
            {
                return;
            }

            SetRef<Border>("_details", ref _details);
            SetRef<SfDatePicker>("_datePicker", ref _datePicker);
            SetRef<Entry>("_datePickerEntry", ref _datePickerEntry);
            SetRef<SfTimePicker>("_startTimePicker", ref _startTimePicker);
            SetRef<Entry>("_startTimePickerEntry", ref _startTimePickerEntry);
            SetRef<SfTimePicker>("_endTimePicker", ref _endTimePicker);
            SetRef<Entry>("_endTimePickerEntry", ref _endTimePickerEntry);
            SetRef<VerticalStackLayout>("_viewActivityContent", ref _viewActivityContent);
            SetRef<ScrollView>("_editActivityContent", ref _editActivityContent);
            SetRef<Label>("_deleteActivityContent", ref _deleteActivityContent);
            SetRef<Grid>("_viewActivityFooter", ref _viewActivityFooter);
            SetRef<Grid>("_editActivityFooter", ref _editActivityFooter);
            SetRef<Grid>("_deleteActivityFooter", ref _deleteActivityFooter);
            SetRef<SfComboBox>("_activityBox", ref _activityBox);
            SetRef<Entry>("_activityTitle", ref _activityTitle);
            SetRef<Entry>("_energyExpended", ref _energyExpended);
            SetRef<Editor>("_remarks", ref _remarks);

            void SetRef<T>(string styleId, ref T target) where T : View
            {
                if (child.StyleId == styleId && child is T matched)
                {
                    target = matched;
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
                _viewActivityContent.IsVisible = true;
                _viewActivityFooter.IsVisible = true;
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
                _editActivityContent.IsVisible = true;
                _editActivityFooter.IsVisible = true;
                _activityBox.ItemsSource = activityList;
                _activityBox.SelectedItem = activity.ActivityType;
            }
        }

        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            _datePicker.IsOpen = true;
        }

        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            _datePicker.IsOpen = true;
        }

        void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (_datePicker.SelectedDate != null)
            {
                _datePickerEntry.Text = _datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
        }

        void startTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            _startTimePicker.IsOpen = true;
        }

        void StartTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            _startTimePicker.IsOpen = true;
        }

        void startTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (_startTimePicker.SelectedTime != null)
            {
                _startTimePickerEntry.Text = DateTime.Today.Add(_startTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        void endTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            _endTimePicker.IsOpen = true;
        }

        void EndTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            _endTimePicker.IsOpen = true;
        }

        void endTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (_endTimePicker.SelectedTime != null)
            {
                _endTimePickerEntry.Text = DateTime.Today.Add(_endTimePicker.SelectedTime.Value).ToString("hh:mm tt");
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
                _deleteActivityContent.IsVisible = true;
                _deleteActivityFooter.IsVisible = true;
            }
        }

        void editpopup_Closed(object sender, EventArgs e)
        {
            _viewActivityContent.IsVisible = false;
            _editActivityContent.IsVisible = false;
            _deleteActivityContent.IsVisible = false;
            _viewActivityFooter.IsVisible = false;
            _editActivityFooter.IsVisible = false;
            _deleteActivityFooter.IsVisible = false;
        }

        void OnEditButtonTapped(object sender, TappedEventArgs e)
        {
            _viewActivityContent.IsVisible = false;
            _viewActivityFooter.IsVisible = false;
            OnEditTapped(sender, e);
        }

        void OnDeleteButtonTapped(object sender, TappedEventArgs e)
        {
            _viewActivityContent.IsVisible = false;
            _viewActivityFooter.IsVisible = false;
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
                double energy = double.TryParse(_energyExpended.Text, out var value) ? value : 0;
                activity.CaloriesBurned = energy;
                activity.ActivityType = (string)_activityBox.SelectedItem!;
                activity.Title = _activityTitle.Text;
                activity.Remarks = _remarks.Text;
                if (_datePicker.SelectedDate is not null)
                {
                    DateTime selectedDate = (DateTime)_datePicker.SelectedDate;
                    if (_startTimePicker.SelectedTime is TimeSpan startTime)
                    {
                        activity.StartTime = selectedDate.Date + startTime;
                    }

                    if (_endTimePicker.SelectedTime is TimeSpan endTime)
                    {
                        activity.EndTime = selectedDate.Date + endTime;
                    }
                }
            }

            OnCloseButtonTapped(sender, e);
        }
    }
}