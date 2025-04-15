using Syncfusion.Maui.Themes;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Toolkit.Buttons;
using Syncfusion.Maui.Toolkit.EffectsView;
using System.Diagnostics;

namespace FitnessTracker
{
    public partial class MainPageDesktop : ContentPage
    {
        #region Helper Collecions
        List<string> GendersList = new List<string> { "Male", "Female", "Non-Binary", "Other" };

        List<string> BodyFatLevelsList = new List<string> { "Low", "Medium", "High" };

        List<string> ActiveStatusesList = new List<string> { "Sedentary", "Lightly Active", "Moderately Active", "Very Active" };

        List<string> MeasurementUnitsList = new List<string> { "Cm", "Inches" };
        List<string> weightList = new List<string> { "Kg", "lb" };
        List<string> heightList = new List<string> { "Cm", "m", "in", "ft" };
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        List<Border> _tabBorders = new List<Border>();

        #endregion

        #region Fields
        Border _selectedBorder;
        PhysicalInfo physicalInfo;
        PersonalInfo personalInfo;
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
        #endregion

        public MainPageDesktop(PersonalInfo personalInfoviewmodel, PhysicalInfo physicalInfoviewmodel)
        {
            InitializeComponent();
            physicalInfo = physicalInfoviewmodel;
            personalInfo = personalInfoviewmodel;
            _viewModel = new FitnessViewModel(Navigation, selectedtab);
            selectedtab.BindingContext = _viewModel;
            backicon.BindingContext = _viewModel;
            grid.BindingContext = _viewModel;
            SettingsPage.BindingContext = personalInfo;
            headerlabel.Text = $"Hi {personalInfo.Name}";

            // Set default selection
            _selectedBorder = HomeBorder;
            SetSelected(HomeBorder);

            //Add Tap Gestures
            AddTapGesture(HomeBorder);
            AddTapGesture(ActivityBorder);
            AddTapGesture(JournalBorder);
            AddTapGesture(GoalBorder);
            AddTapGesture(HelpBorder);
            AddTapGesture(SettingsBorder);

            // Add Tab Borders to list
            _tabBorders.Add(HomeBorder);
            _tabBorders.Add(ActivityBorder);
            _tabBorders.Add(JournalBorder);
            _tabBorders.Add(GoalBorder);
            _tabBorders.Add(HelpBorder);
            _tabBorders.Add(SettingsBorder);

            _activity = new FitnessActivity();
            _activity.StartTime = DateTime.Now;
            _activity.EndTime = DateTime.Now;
            viewpopup.BindingContext = _activity;

            if (!string.IsNullOrWhiteSpace(personalInfo.Name))
            {
                var parts = personalInfo.Name.Split(' ', 2);
                personalInfo.FirstName = parts[0];
                personalInfo.LastName = parts.Length > 1 ? parts[1] : string.Empty;
            }
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
            if (_selectedBorder != null && _selectedBorder.Content is HorizontalStackLayout prevLayout)
            {
                _selectedBorder.BackgroundColor = Colors.Transparent;

                if (prevLayout.Children.Count >= 2)
                {
                    var prevIconLabel = prevLayout.Children[0] as Label;
                    var prevTextLabel = prevLayout.Children[1] as Label;

                    if (Application.Current!.RequestedTheme == AppTheme.Dark)
                    {
                        prevIconLabel!.TextColor = Color.FromArgb("#C9C6C8");
                        prevTextLabel!.TextColor = Colors.White;
                    }
                    else
                    {
                        prevIconLabel!.TextColor = Color.FromArgb("#474648"); // icon color light
                        prevTextLabel!.TextColor = Color.FromArgb("#313032"); // content text color light
                    }
                }
            }

            // Set new selection
            border.BackgroundColor = Color.FromArgb("#7633DA");

            if (border.Content is HorizontalStackLayout layout && layout.Children.Count >= 2)
            {
                var iconLabel = layout.Children[0] as Label;
                var textLabel = layout.Children[1] as Label;
                var text = textLabel?.Text;

                // Selected tab color
                iconLabel!.TextColor = Colors.White;
                textLabel!.TextColor = Colors.White;

                ContentView selectedContent = new();
                var viewModel = selectedtab.BindingContext as FitnessViewModel;
                selectedsettingstab.IsVisible = false;
                selectedContent.IsVisible = true;

                switch (text)
                {
                    case "Home":
                        headerlabel.Text = $"Hi {personalInfo.Name}";
                        selectedContent = new HomePageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                        break;
                    case "Activity":
                        headerlabel.Text = text;
                        selectedContent = new ActivityPageContentDesktop();
                        break;
                    case "Journal":
                        headerlabel.Text = text;
                        selectedContent = new JournalPageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                        break;
                    case "Goal":
                        headerlabel.Text = text;
                        selectedContent = new GoalPageContentDesktop();
                        viewModel!.IsBackIconVisible = false;
                        break;
                    case "Help":
                        headerlabel.Text = "Settings";
                        selectedContent = new HelpPageDesktop();
                        viewModel!.IsBackIconVisible = false;
                        break;
                    case "Settings":
                        headerlabel.Text = text;
                        selectedsettingstab.IsVisible = true;
                        selectedContent.IsVisible = false;
                        viewModel!.IsBackIconVisible = false;
                        break;
                }

                _selectedBorder = border;
                selectedtab.Children.Clear();
                selectedtab.Children.Add(selectedContent);
            }
        }

        void UpdateTabColorsForTheme()
        {
            foreach (var border in _tabBorders)
            {
                if (border == _selectedBorder)
                    continue;

                border.BackgroundColor = Colors.Transparent;

                if (border.Content is HorizontalStackLayout layout && layout.Children.Count >= 2)
                {
                    var iconLabel = layout.Children[0] as Label;
                    var textLabel = layout.Children[1] as Label;

                    if (Application.Current!.RequestedTheme == AppTheme.Dark)
                    {
                        iconLabel!.TextColor = Color.FromArgb("#C9C6C8");
                        textLabel!.TextColor = Colors.White;
                    }
                    else
                    {
                        iconLabel!.TextColor = Color.FromArgb("#474648"); // light theme icon
                        textLabel!.TextColor = Color.FromArgb("#313032"); // light theme text
                    }
                }
            }
        }


        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            SettingsPage.BindingContext= personalInfo;
            selectedsettingstab.IsVisible = true;
        }

        private void Profile_Tapped(object sender, TappedEventArgs e)
        {
            genderBox.ItemsSource = GendersList;
            bodyFatBox.ItemsSource = BodyFatLevelsList;
            activeStatusBox.ItemsSource = ActiveStatusesList;
            measurementUnitBox.ItemsSource = MeasurementUnitsList;
            weightBox.ItemsSource = weightList;
            heightBox.ItemsSource = heightList;
            weightBox.SelectedIndex = 0;
            heightBox.SelectedIndex = 0;
            personalInfo.Name = $"{firstnameentry.Text?.Trim()} {lastnameentry.Text?.Trim()}".Trim();
            firstnameentry.Text = personalInfo.FirstName;
            lastnameentry.Text = personalInfo.LastName;
            HiddenDatePicker.SelectedDate = personalInfo.DateOfBirth;
            genderBox.SelectedItem = physicalInfo.Gender ;
            weightentry.Text = physicalInfo.Weight;
            heightentry.Text = physicalInfo.Height;
            activeStatusBox.SelectedItem = physicalInfo.ActiveStatus;
            bodyFatBox.SelectedItem = physicalInfo.BodyFat;
            measurementUnitBox.SelectedItem = physicalInfo.MeasurementUnit;
            _viewModel.IsVisible = true;
            popupgrid.IsVisible = true;
            ProfileGrid.IsVisible = true;
        }
        private void Notification_Tapped(object sender, TappedEventArgs e)
        {
            _viewModel.IsVisible = true;
            popupgrid.IsVisible = true;
            NotificationPopup.IsVisible = true;
        }

        private void Appearance_Tapped(object sender, TappedEventArgs e)
        {
            var currentTheme = Application.Current.UserAppTheme;

            if (currentTheme == AppTheme.Dark)
            {
                darkthemebutton.IsChecked = true;
            }
            else
            {
                lightthemebutton.IsChecked = true;
            }

            _viewModel.IsVisible = true;
            popupgrid.IsVisible = true;
            AppearancePopup.IsVisible = true;
        }

        private void Account_Tapped(object sender, TappedEventArgs e)
        {
            AccountPageDesktop accountPageDesktop = new AccountPageDesktop(_viewModel,personalInfo);
            selectedtab.Children.Clear();
            selectedtab.Children.Add(accountPageDesktop);
            selectedsettingstab.IsVisible = false;
            headerlabel.Text = "Account";
            if (selectedtab.BindingContext is FitnessViewModel vm)
            {
                vm.SetDesktopContent?.Invoke(new AccountPageDesktop());
                vm.IsBackIconVisible = true;
                selectedsettingstab.IsVisible = false;
                vm.BackAction = () =>
                {
                    selectedtab.Children.Clear();
                    vm.SetDesktopContent?.Invoke(selectedsettingstab);
                    selectedsettingstab.IsVisible = true;
                };
            }
        }
        private void SfRadioGroup_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
        {

        }
        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
             HiddenDatePicker.IsVisible = true;
             HiddenDatePicker.IsOpen = true;
        }

        void HiddenDatePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (e.NewValue is DateTime dateValue)
            {
                string formattedDate = dateValue.Date.ToString("dd/MM/yyyy");
                DateEntry.Text = formattedDate;
                HiddenDatePicker.IsVisible = false;
            }
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IsVisible = false;
            popupgrid.IsVisible = false;
            if (ProfileGrid.IsVisible)
            {
                ProfileGrid.IsVisible = false;
            }
            else if (NotificationPopup.IsVisible)
            {
                NotificationPopup.IsVisible = false;
            }
            else if (AppearancePopup.IsVisible)
            {
                AppearancePopup.IsVisible = false;
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IsVisible = false;
            popupgrid.IsVisible = false;
            if (ProfileGrid.IsVisible)
            {
                ProfileGrid.IsVisible = false;
            }
            else if (NotificationPopup.IsVisible)
            {
                NotificationPopup.IsVisible = false;
            }
            else if (AppearancePopup.IsVisible)
            {
                AppearancePopup.IsVisible = false;
                if (Application.Current != null)
                {
                    ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                    if (mergedDictionaries != null)
                    {
                        var theme1 = mergedDictionaries.OfType<Syncfusion.Maui.Toolkit.Themes.SyncfusionThemeResourceDictionary>().FirstOrDefault();
                        var theme2 = mergedDictionaries.OfType<Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary>().FirstOrDefault();
                        if (theme1 != null && theme2 != null)
                        {
                            if (lightthemebutton.IsChecked==true)
                            {
                                theme1.VisualTheme = Syncfusion.Maui.Toolkit.Themes.SfVisuals.MaterialLight;
                                theme2.VisualTheme = SfVisuals.MaterialLight;
                                Application.Current.UserAppTheme = AppTheme.Light;
                            }
                            else if (darkthemebutton.IsChecked == true)
                            {
                                theme1.VisualTheme = Syncfusion.Maui.Toolkit.Themes.SfVisuals.MaterialDark;
                                theme2.VisualTheme = SfVisuals.MaterialDark;
                                Application.Current.UserAppTheme = AppTheme.Dark;
                            }
                        }

                        UpdateTabColorsForTheme();
                    }
                }
            }
            personalInfo.Name = !string.IsNullOrWhiteSpace(firstnameentry.Text) && !string.IsNullOrWhiteSpace(lastnameentry.Text) ?
                                $"{firstnameentry.Text.Trim()} {lastnameentry.Text.Trim()}".Trim() : personalInfo.Name;
            personalInfo.FirstName = !string.IsNullOrWhiteSpace(firstnameentry.Text) ? firstnameentry.Text.Trim() : personalInfo.FirstName;
            personalInfo.LastName = !string.IsNullOrWhiteSpace(lastnameentry.Text) ? lastnameentry.Text.Trim() : personalInfo.LastName;
            personalInfo.DateOfBirth = HiddenDatePicker.SelectedDate;
            physicalInfo.Gender = (string?)genderBox.SelectedItem;
            physicalInfo.ActiveStatus = (string?)activeStatusBox.SelectedItem;
            physicalInfo.BodyFat = (string?)bodyFatBox.SelectedItem;
            physicalInfo.MeasurementUnit = (string?)measurementUnitBox.SelectedItem;
            physicalInfo.Weight = (string?)weightBox.SelectedItem;
            physicalInfo.Height = (string?)heightBox.SelectedItem;
            firstname.Text = firstnameentry.Text;
            lastname.Text = lastnameentry.Text;
        }

        private void BackArrow_Tapped(object sender, TappedEventArgs e)
        {
            selectedtab.IsVisible = false;
            headerlabel.Text = "Settings";
        }

        private void CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            _viewModel.IsVisible = false;
            popupgrid.IsVisible = false;
            if (ProfileGrid.IsVisible)
            {
                ProfileGrid.IsVisible = false;
            }
            else if (NotificationPopup.IsVisible)
            {
                NotificationPopup.IsVisible = false;
            }
            else if (AppearancePopup.IsVisible)
            {
                AppearancePopup.IsVisible = false;
            }
        }

        private void Logout_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new SignUpPageDesktop());
        }
        void OnCreateTapped(object sender, TappedEventArgs e)
        {
            createpopup.ShowRelativeToView(createbutton, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottom);
        }

        void OnAddActivityTapped(object sender, TappedEventArgs e)
        {
            viewpopup.HeaderTitle = "Add Activity";
            viewpopup.IsOpen = true;
            viewpopup.HeightRequest = 584;
            viewpopup.WidthRequest = 480;
            viewpopup.ShowFooter = true;
            _viewactivity.IsVisible = true;
            _activityBox.ItemsSource = activityList;
            _datePicker.SelectedDate = DateTime.Today.Date;
            _startTimePicker.SelectedTime = DateTime.Now.TimeOfDay;
            _endTimePicker.SelectedTime = DateTime.Now.TimeOfDay;
        }

        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            _datePicker.IsOpen = true;
        }

        void datePicker_Tapped(object sender, TappedEventArgs e)
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
            viewpopup.HeightRequest = 584;
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