using Syncfusion.Maui.Themes;
namespace FitnessTracker
{
    public partial class MainPageDesktop : ContentPage
    {
        List<string> GendersList = new List<string> { "Male", "Female", "Non-Binary", "Other" };

        List<string> BodyFatLevelsList = new List<string> { "Low", "Medium", "High" };

        List<string> ActiveStatusesList = new List<string> { "Sedentary", "Lightly Active", "Moderately Active", "Very Active" };

        List<string> MeasurementUnitsList = new List<string> { "Cm", "Inches" };
        List<string> weightList = new List<string> { "Kg", "lb" };
        List<string> heightList = new List<string> { "Cm", "m", "in", "ft" };
        Border _selectedBorder;
        PhysicalInfo physicalInfo;
        PersonalInfo personalInfo;
        FitnessViewModel FitnessViewModel ;
        public MainPageDesktop(PersonalInfo personalInfoviewmodel, PhysicalInfo physicalInfoviewmodel)
        {
            InitializeComponent();
            physicalInfo = physicalInfoviewmodel;
            personalInfo = personalInfoviewmodel;
            selectedtab.BindingContext = new FitnessViewModel(Navigation);
            FitnessViewModel = new FitnessViewModel(Navigation);
            grid.BindingContext = FitnessViewModel;
            Header.Text = $"Hi {personalInfo.Name}";

            // Set default selection
            _selectedBorder = HomeBorder;
            SetSelected(HomeBorder);

            //Add Tap Gestures
            AddTapGesture(HomeBorder);
            AddTapGesture(ActivityBorder);
            AddTapGesture(JournalBorder);
            AddTapGesture(GoalBorder);

            if (!string.IsNullOrWhiteSpace(personalInfo.Name))
            {
                var parts = personalInfo.Name.Split(' ', 2);
                personalInfo.FirstName = parts[0];
                personalInfo.LastName = parts.Length > 1 ? parts[1] : string.Empty;
            }
        }

        private void AddTapGesture(Border border)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => SetSelected(border);
            border.GestureRecognizers.Add(tapGesture);
        }

        private void SetSelected(Border border)
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
            if (formattedText != null)
            {
                foreach (var span in formattedText.Spans)
                {
                    span.TextColor = Colors.White; // Highlight color
                }
            }

            _selectedBorder = border;
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            selectedtab.IsVisible = false;
            selectedsettingstab1.IsVisible = false;
            Header.Text = "Settings";
            selectedsettingstab.BindingContext = FitnessViewModel;
            SettingsPage.BindingContext= personalInfo;
            selectedsettingstab.IsVisible = true;
        }

        private void HelpPage_Tapped(object sender, TappedEventArgs e)
        {
            selectedtab.IsVisible = false;
            selectedsettingstab.IsVisible = false;
            selectedsettingstab3.IsVisible = false;
            selectedsettingstab1.IsVisible = true;
        }
        private void Profile_Tapped(object sender, TappedEventArgs e)
        {
            FitnessViewModel.IsVisible = true;
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
            popupgrid.IsVisible = true;
            ProfileGrid.IsVisible = true;;
        }
        private void Notification_Tapped(object sender, TappedEventArgs e)
        {
            FitnessViewModel.IsVisible = true;
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

            FitnessViewModel.IsVisible = true;
            popupgrid.IsVisible = true;
            AppearancePopup.IsVisible = true;
        }

        private void Account_Tapped(object sender, TappedEventArgs e)
        {
            SettingsPage.IsVisible = false;
            selectedtab.IsVisible = false;
            selectedsettingstab.IsVisible = false;
            selectedsettingstab1.IsVisible = false;
            Header.Text = "Account";
            BackArrow.IsVisible = true;
            AccountPageDesktop accountPageDesktop = new AccountPageDesktop(FitnessViewModel,personalInfo);
            selectedsettingstab3.IsVisible = true;
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
            FitnessViewModel.IsVisible = false;
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
            FitnessViewModel.IsVisible = false;
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
                    }
                }
            }
            personalInfo.Name = $"{firstnameentry.Text?.Trim()} {lastnameentry.Text?.Trim()}".Trim();
            personalInfo.FirstName = firstnameentry.Text;
            personalInfo.LastName = lastnameentry.Text;
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
            selectedsettingstab1.IsVisible = false;
            Header.Text = "Settings";
            BackArrow.IsVisible = false;
            selectedsettingstab3.IsVisible = false;
            SettingsPage.IsVisible = true;
            selectedsettingstab.IsVisible = true;
        }

        private void CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            FitnessViewModel.IsVisible = false;
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
    }
}