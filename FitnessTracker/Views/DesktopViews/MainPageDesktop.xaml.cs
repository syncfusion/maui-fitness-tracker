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
        VerticalStackLayout viewactivity;
        Grid beforeClick;
        Grid afterClick;
        SfComboBox trackactivitybox;
        SfDatePicker datePicker;
        SfTimePicker startTimePicker;
        SfTimePicker endTimePicker;
        Entry datePickerEntry;
        Entry startTimePickerEntry;
        Entry endTimePickerEntry;
        SfComboBox activityBox;
        Entry activityTitle;
        Entry energyExpended;
        Editor remarks;
        Border addborder;
        Label addlabel;
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
            viewactivity.IsVisible = true;
            activityBox.ItemsSource = activityList;
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

        void OnTrackActivityTapped(object sender, TappedEventArgs e)
        {
            viewpopup.HeaderTitle = "Track Activity";
            viewpopup.IsOpen = true;
            viewpopup.HeightRequest = 624;
            viewpopup.WidthRequest = 480;
            viewpopup.ShowFooter = false;
            beforeClick.IsVisible = true;
            trackactivitybox.ItemsSource = activityList;
            trackactivitybox.SelectedIndex = 0;
        }

        void OnAddActivityCloseTapped(object sender, TappedEventArgs e)
        {
            CloseActivityPopup();
        }

        void Play_Clicked(object sender, EventArgs e)
        {
            beforeClick.IsVisible = false;
            afterClick.IsVisible = true;
        }

        void Stop_Clicked(object sender, EventArgs e)
        {
            beforeClick.IsVisible = true;
            afterClick.IsVisible = false;
        }

        void Pause_Clicked(object sender, EventArgs e)
        {
            if (sender is SfButton button)
            {
                button.Text = (button.Text == "\ue70e") ? "\ue70f" : "\ue70e";
            }
        }

        void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is not View child || string.IsNullOrEmpty(child.StyleId))
            {
                return;
            }

            SetRef<VerticalStackLayout>("viewactivity", ref viewactivity);
            SetRef<Grid>("beforeClick", ref beforeClick);
            SetRef<Grid>("afterClick", ref afterClick);
            SetRef<SfComboBox>("trackactivitybox", ref trackactivitybox);
            SetRef<SfDatePicker>("datePicker", ref datePicker);
            SetRef<Entry>("datePickerEntry", ref datePickerEntry);
            SetRef<SfTimePicker>("startTimePicker", ref startTimePicker);
            SetRef<Entry>("startTimePickerEntry", ref startTimePickerEntry);
            SetRef<SfTimePicker>("endTimePicker", ref endTimePicker);
            SetRef<Entry>("endTimePickerEntry", ref endTimePickerEntry);
            SetRef<SfComboBox>("activityBox", ref activityBox);
            SetRef<Entry>("activityTitle", ref activityTitle);
            SetRef<Entry>("energyExpended", ref energyExpended);
            SetRef<Editor>("remarks", ref remarks);
            SetRef<Border>("addborder", ref addborder);
            SetRef<Label>("addlabel", ref addlabel);

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
            addborder.Background = Color.FromArgb("#7633DA");
            addlabel.TextColor = Colors.White;
            addborder.IsEnabled = true;
        }

        void ResetSelection()
        {
            activityTitle.Text = string.Empty;
            datePickerEntry.Text = string.Empty;
            energyExpended.Text = string.Empty;
            startTimePickerEntry.Text = string.Empty;
            endTimePickerEntry.Text += string.Empty;
            remarks.Text = string.Empty;
            startTimePicker.SelectedTime = null;
            endTimePicker.SelectedTime = null;
            datePicker.SelectedDate = null;
        }

        void OnSaveActivityTapped(object sender, TappedEventArgs e)
        {
            if (viewpopup.BindingContext is FitnessActivity activity && selectedtab.BindingContext is FitnessViewModel viewModel)
            {
                double energy = double.Parse(energyExpended.Text);
                activity.CaloriesBurned = energy;
                activity.ActivityType = activityList[activityBox.SelectedIndex];
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

                viewModel?.Activities?.Add(activity);
            }

            CloseActivityPopup();
        }

        void CloseActivityPopup()
        {
            addborder.Background = Colors.LightGray;
            addlabel.TextColor = Color.FromArgb("#929092");
            addborder.IsEnabled = false;
            viewpopup.IsOpen = false;
            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
        }

        void viewpopup_Closed(object sender, EventArgs e)
        {
            viewactivity.IsVisible = false;
            beforeClick.IsVisible = false;
            afterClick.IsVisible = false;
            CloseActivityPopup();
            ResetSelection();
        }
    }
}