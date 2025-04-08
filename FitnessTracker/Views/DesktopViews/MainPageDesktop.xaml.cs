using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Toolkit.Buttons;
using Syncfusion.Maui.Toolkit.EffectsView;
using System.Diagnostics;

namespace FitnessTracker
{
    public partial class MainPageDesktop : ContentPage
    {
        #region Fields
        Border _selectedBorder;
        FitnessActivity _activity;
        FitnessViewModel _viewModel;
        VerticalStackLayout _viewactivity;
        Grid _beforeClick;
        Grid _afterClick;
        SfComboBox _trackactivitybox;
        SfDatePicker _datePicker;
        SfTimePicker _startTimePicker;
        SfTimePicker _endTimePicker;
        Entry _datePickerEntry;
        Entry _startTimePickerEntry;
        Entry _endTimePickerEntry;
        SfComboBox _activityBox;
        Entry _activityTitle;
        Entry _energyExpended;
        Editor _remarks;
        Border _addborder;
        Label _addlabel;
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        #endregion

        public MainPageDesktop()
        {
            InitializeComponent();
            _viewModel = new FitnessViewModel(Navigation, selectedtab);
            selectedtab.BindingContext = _viewModel;
            backicon.BindingContext = _viewModel;
            // Set default selection
            _selectedBorder = HomeBorder;
            SetSelected(HomeBorder);

            // Add Tap Gestures
            AddTapGesture(HomeBorder);
            AddTapGesture(ActivityBorder);
            AddTapGesture(JournalBorder);
            AddTapGesture(GoalBorder);

            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
            viewpopup.BindingContext = _activity;
        }

        void AddTapGesture(Border border)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => SetSelected(border);
            border.GestureRecognizers.Add(tapGesture);
        }

        void SetSelected(Border border)
        {
            // Reset previous selection
            if (_selectedBorder != null)
            {
                _selectedBorder.BackgroundColor = Colors.Transparent;

                // Reset color of all spans inside the previous selection
                var prevFormattedText = ((Label)_selectedBorder.Content).FormattedText;
                if (prevFormattedText != null)
                {
                    foreach (var span in prevFormattedText.Spans)
                    {
                        span.TextColor = Color.FromArgb("#313032"); // Default color
                    }
                }
            }

            // Set new selection
            border.BackgroundColor = Color.FromArgb("#7633DA");

            // Update color of all spans inside the newly selected border
            var formattedText = ((Label)border.Content).FormattedText;
            ContentView selectedContent = new();
            var viewModel = selectedtab.BindingContext as FitnessViewModel;
            if (formattedText != null)
            {
                foreach (var span in formattedText.Spans)
                {
                    span.TextColor = Colors.White; // Highlight color
                    if(span.Text == "Home")
                    {
                        headerlabel.Text = "Hi Richard!";
                        selectedContent = new HomePageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                    }
                    else if(span.Text == "Activity")
                    {
                        headerlabel.Text = span.Text;
                        selectedContent = new ActivityPageContentDesktop();
                    }
                    else if(span.Text == "Journal")
                    {
                        headerlabel.Text = span.Text;
                        selectedContent = new JournalPageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                    }
                    else if(span.Text == "Goal")
                    {
                        headerlabel.Text = span.Text;
                        selectedContent = new GoalPageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                    }
                }
            }

            _selectedBorder = border;
            selectedtab.Children.Clear();
            selectedtab.Children.Add(selectedContent);
        }

        void OnCreateTapped(object sender, TappedEventArgs e)
        {
            createpopup.ShowRelativeToView(createbutton, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottom);
        }

        void OnAddActivityTapped(object sender, TappedEventArgs e)
        {
            viewpopup.HeaderTitle = "Add Activity";
            viewpopup.IsOpen = true;
            viewpopup.HeightRequest = 624;
            viewpopup.WidthRequest = 480;
            viewpopup.ShowFooter = true;
            _viewactivity.IsVisible = true;
            _activityBox.ItemsSource = activityList;
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

        void OnTrackActivityTapped(object sender, TappedEventArgs e)
        {
            viewpopup.HeaderTitle = "Track Activity";
            viewpopup.IsOpen = true;
            viewpopup.HeightRequest = 624;
            viewpopup.WidthRequest = 480;
            viewpopup.ShowFooter = false;
            _beforeClick.IsVisible = true;
            _trackactivitybox.ItemsSource = activityList;
            _trackactivitybox.SelectedIndex = 0;
        }

        void OnAddActivityCloseTapped(object sender, TappedEventArgs e)
        {
            CloseActivityPopup();
        }

        void Play_Clicked(object sender, EventArgs e)
        {
            _beforeClick.IsVisible = false;
            _afterClick.IsVisible = true;
        }

        void Stop_Clicked(object sender, EventArgs e)
        {
            _beforeClick.IsVisible = true;
            _afterClick.IsVisible = false;
        }

        void Pause_Clicked(object sender, EventArgs e)
        {
            if (sender is SfButton button)
            {
                button.Text = (button.Text == "\ue70e") ? "\ue70f" : "\ue70e";
            }
        }

        void OnChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is not View child || string.IsNullOrEmpty(child.StyleId))
            {
                return;
            }

            SetRef<VerticalStackLayout>("_viewactivity", ref _viewactivity);
            SetRef<Grid>("_beforeClick", ref _beforeClick);
            SetRef<Grid>("_afterClick", ref _afterClick);
            SetRef<SfComboBox>("_trackactivitybox", ref _trackactivitybox);
            SetRef<SfDatePicker>("_datePicker", ref _datePicker);
            SetRef<Entry>("_datePickerEntry", ref _datePickerEntry);
            SetRef<SfTimePicker>("_startTimePicker", ref _startTimePicker);
            SetRef<Entry>("_startTimePickerEntry", ref _startTimePickerEntry);
            SetRef<SfTimePicker>("_endTimePicker", ref _endTimePicker);
            SetRef<Entry>("_endTimePickerEntry", ref _endTimePickerEntry);
            SetRef<SfComboBox>("_activityBox", ref _activityBox);
            SetRef<Entry>("_activityTitle", ref _activityTitle);
            SetRef<Entry>("_energyExpended", ref _energyExpended);
            SetRef<Editor>("_remarks", ref _remarks);
            SetRef<Border>("_addborder", ref _addborder);
            SetRef<Label>("_addlabel", ref _addlabel);

            void SetRef<T>(string styleId, ref T target) where T : View
            {
                if (child.StyleId == styleId && child is T matched)
                {
                    target = matched;
                }
            }
        }

        void SelectedActivityChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            _addborder.Background = Color.FromArgb("#7633DA");
            _addlabel.TextColor = Colors.White;
            _addborder.IsEnabled = true;
        }

        void ResetSelection()
        {
            _activityTitle.Text = string.Empty;
            _datePickerEntry.Text = string.Empty;
            _energyExpended.Text = string.Empty;
            _startTimePickerEntry.Text = string.Empty;
            _endTimePickerEntry.Text += string.Empty;
            _remarks.Text = string.Empty;
            _startTimePicker.SelectedTime = null;
            _endTimePicker.SelectedTime = null;
            _datePicker.SelectedDate = null;
        }

        void OnSaveActivityTapped(object sender, TappedEventArgs e)
        {
            if (viewpopup.BindingContext is FitnessActivity activity && selectedtab.BindingContext is FitnessViewModel viewModel)
            {
                double energy = double.TryParse(_energyExpended.Text, out var value) ? value : 0;
                activity.CaloriesBurned = energy;
                activity.ActivityType = activityList[_activityBox.SelectedIndex];
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

                viewModel?.Activities?.Add(activity);
            }

            CloseActivityPopup();
        }

        void CloseActivityPopup()
        {
            _addborder.Background = Colors.LightGray;
            _addlabel.TextColor = Color.FromArgb("#929092");
            _addborder.IsEnabled = false;
            viewpopup.IsOpen = false;
            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
        }

        void viewpopup_Closed(object sender, EventArgs e)
        {
            _viewactivity.IsVisible = false;
            _beforeClick.IsVisible = false;
            _afterClick.IsVisible = false;
            CloseActivityPopup();
            ResetSelection();
        }
    }
}