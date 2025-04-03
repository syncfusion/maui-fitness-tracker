using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Toolkit.Buttons;

namespace FitnessTracker
{
    public partial class MainPageDesktop : ContentPage
    {
        Border _selectedBorder;
        FitnessActivity _activity;
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        public MainPageDesktop()
        {
            InitializeComponent();
            selectedtab.BindingContext = new FitnessViewModel(Navigation, selectedtab);
            // Set default selection
            _selectedBorder = HomeBorder;
            SetSelected(HomeBorder);

            // Add Tap Gestures
            AddTapGesture(HomeBorder);
            AddTapGesture(ActivityBorder);
            AddTapGesture(JournalBorder);
            AddTapGesture(GoalBorder);

            activityBox.ItemsSource = activityList;
            trackactivitybox.ItemsSource = activityList;
            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
            addactivity.BindingContext = _activity;
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
            if (formattedText != null)
            {
                foreach (var span in formattedText.Spans)
                {
                    span.TextColor = Colors.White; // Highlight color
                    if(span.Text == "Home")
                    {
                        headerlabel.Text = "Hi Richard!";
                        selectedContent = new HomePageContentDesktop();
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
                    }
                    else if(span.Text == "Goal")
                    {
                        headerlabel.Text = span.Text;
                        selectedContent = new GoalPageContent();
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

        private void OnAddActivityTapped(object sender, TappedEventArgs e)
        {
            addactivity.IsVisible = true;
            overlay.IsVisible = true;
        }

        //SfDatePicker datePicker;
        //Entry datePickerEntry;
        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            datePicker.IsOpen = true;
            //if (_stackChildren.TryGetValue("datePicker", out View? datePickerView) && datePickerView is SfDatePicker datePicker)
            //{
            //    datePicker.IsOpen = true;
            //}
        }

        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            datePicker.IsOpen = true;
            //if (_stackChildren.TryGetValue("datePicker", out View? datePickerView) && datePickerView is SfDatePicker datePicker)
            //{
            //    datePicker.IsOpen = true;
            //}
        }

        void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
            //if (_stackChildren.TryGetValue("datePicker", out View? datePickerView) && datePickerView is SfDatePicker datePicker && datePicker.SelectedDate != null)
            //{
            //    if (_stackChildren.TryGetValue("datePickerEntry", out View? datePickerEntryView) && datePickerEntryView is Entry datePickerEntry)
            //    {
            //        datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            //    }
            //}
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
            trackactivity.IsVisible = true;
            overlay.IsVisible = true;
        }

        void Play_Clicked(object sender, EventArgs e)
        {
            beforeClick.IsVisible = false;
            afterClick.IsVisible = true;
            //if (_stackChildren.TryGetValue("beforeClick", out View? beforeClickView) && beforeClickView is Grid beforeClick &&
            //    _stackChildren.TryGetValue("afterClick", out View? afterClickView) && afterClickView is Grid afterClick)
            //{
            //    beforeClick.IsVisible = false;
            //    afterClick.IsVisible = true;
            //}
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

        private Dictionary<string, View> _stackChildren = new Dictionary<string, View>();

        private void VerticalStackLayout_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is View child)
            {
                string key = child.StyleId; // Use StyleId or another unique property
                //if (!string.IsNullOrEmpty(key) && !_stackChildren.ContainsKey(key))
                //{
                //    _stackChildren[key] = child;
                //}

                if (key == "datePicker")
                {
                    datePicker = (SfDatePicker)child;
                }
                else if (key == "datePickerEntry")
                {
                    datePickerEntry = (Entry)child;
                }
            }
        }

        private void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is View child)
            {
                string key = child.StyleId; // Use StyleId or another unique property
                if (!string.IsNullOrEmpty(key) && !_stackChildren.ContainsKey(key))
                {
                    _stackChildren[key] = child;
                }
            }
        }

        void OnTrackActivityCloseTapped(object sender, TappedEventArgs e)
        {
            trackactivity.IsVisible = false;
            overlay.IsVisible = false;
        }

        void OnAddActivityCloseTapped(object sender, TappedEventArgs e)
        {
            CloseActivityPopup();
            ResetSelection();
        }

        void SelectedActivityChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            addborder.Background = Color.FromArgb("#7633DA");
            addlabel.TextColor = Colors.White;
            addborder.IsEnabled = true;
        }

        void ResetSelection()
        {
            activityBox.SelectedItem = null;
            activityTitle.Text = string.Empty;
            datePickerEntry.Text = string.Empty;
            energyExpended.Text = string.Empty;
            startTimePickerEntry.Text = string.Empty;
            endTimePickerEntry.Text += string.Empty;
            remarks.Text = string.Empty;
        }

        void OnSaveActivityTapped(object sender, TappedEventArgs e)
        {
            if (addactivity.BindingContext is FitnessActivity activity && selectedtab.BindingContext is FitnessViewModel viewModel)
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

                viewModel?.Activities?.Add(activity);
            }

            CloseActivityPopup();
        }

        void CloseActivityPopup()
        {
            addactivity.IsVisible = false;
            overlay.IsVisible = false;
            addborder.Background = Colors.LightGray;
            addlabel.TextColor = Colors.Black;
            addborder.IsEnabled = false;
            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
        }
    }
}