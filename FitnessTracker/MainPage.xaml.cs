using Syncfusion.Maui.Themes;

namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {
        PersonalInfo _personalInfoViewModel;
        PhysicalInfo _physicalInfoViewmodel;
        string? OTP ;
        bool passwordupdate = false;
        bool isPasswordMasked = true;

        public MainPage(PhysicalInfo physicalInfoviewmodel,PersonalInfo personalInfoviewModel)
        {
            InitializeComponent();
            _personalInfoViewModel = personalInfoviewModel;
            _physicalInfoViewmodel = physicalInfoviewmodel;
            if (!string.IsNullOrWhiteSpace(personalInfoviewModel.Name))
            {
                var parts = personalInfoviewModel.Name.Split(' ', 2);
                personalInfoviewModel.FirstName = parts[0];
                personalInfoviewModel.LastName = parts.Length > 1 ? parts[1] : string.Empty;
            }
            else
            {
                personalInfoviewModel.FirstName = string.Empty;
                personalInfoviewModel.LastName = string.Empty;
            }

            bottomsheet.BindingContext = _personalInfoViewModel;
            BindingContext = _physicalInfoViewmodel;
            home.BindingContext = new FitnessViewModel(Navigation);
            if(Application.Current.RequestedTheme == AppTheme.Light)
            {
                lightTheme.IsChecked = true;
            }
            else if(Application.Current.RequestedTheme == AppTheme.Dark)
            {
                darkTheme.IsChecked = true;
            }
        }

        /// <summary>
        /// Handles the tab item tapped event and updates the header label accordingly.
        /// </summary>
        void SfTabView_TabItemTapped(object sender, Syncfusion.Maui.Toolkit.TabView.TabItemTappedEventArgs e)
        {
            if(e.TabItem!.Header != "Home")
            {
                headerLabel.Text = e.TabItem!.Header;
            }
            else
            {
                headerLabel.Text = "";
            } 
        }

        /// <summary>
        /// Handles the hamburger menu tap event to open the navigation drawer.
        /// </summary>
        async void Hamburger_Tapped(object sender, TappedEventArgs e)
        {
            home.ZIndex = 0;
            NavigationDrawerGrid.IsVisible = true;
            NavigationDrawerGrid.ZIndex = 1;
            await NavigationDrawerGrid.TranslateTo(0, 0, 250, Easing.CubicIn);
         }

        /// <summary>
        /// Handles the notification icon tap event.
        /// </summary>
        void Notification_Tapped(object sender, TappedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the profile photo tap event.
        /// </summary>
        void ProfilePhoto_Tapped(object sender, TappedEventArgs e)
        {

        }

        /// <summary>
        /// Shows the floating action button menu.
        /// </summary>
        void AddIcon_Clicked(object sender, EventArgs e)
        {
            if(tabview.SelectedIndex == 0)
            {
                addButton.IsVisible = false;
                overlayGrid.IsVisible = floatingButtonGrid.IsVisible = true;
            }
            else if(home.BindingContext is FitnessViewModel viewModel)
            {
                Navigation.PushAsync(new AddActivityPage(viewModel));
            }
        }

        /// <summary>
        /// Closes the floating action button menu.
        /// </summary>
        void CloseIcon_Clicked(object sender, EventArgs e)
        {
            addButton.IsVisible = true;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = false;
        }

        /// <summary>
        /// Handles the overlay grid tap event.
        /// </summary>
        void OverlayGrid_Tapped(object sender, TappedEventArgs e)
        {

        }

        /// <summary>
        /// Navigates to the Track Activity page.
        /// </summary>
        void TrackActivity_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrackActivity());
            addButton.IsVisible = true;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = false;
        }

        /// <summary>
        /// Closes the settings drawer with an animation.
        /// </summary>
        async void Settings_CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            await NavigationDrawerGrid.TranslateTo(-NavigationDrawerGrid.Width, 0, 250, Easing.CubicIn);
            home.ZIndex = 1;
            NavigationDrawerGrid.ZIndex = 0;
            NavigationDrawerGrid.IsVisible = false;
        }

        /// <summary>
        /// Handles the settings profile photo tap event.
        /// </summary>
        void Settings_ProfilePhoto_Tapped(object sender, TappedEventArgs e)
        {

        }

        /// <summary>
        /// Navigates to the Edit Profile page.
        /// </summary>
        void Settings_Profile_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage(_physicalInfoViewmodel, _personalInfoViewModel));
        }

        /// <summary>
        /// Handles the settings notification tap event.
        /// </summary>
        void Settings_Notification_Tapped(object sender, TappedEventArgs e)
        {
            
        }

        /// <summary>
        /// Opens the theme selection bottom sheet.
        /// </summary>
        void Settings_Appearance_Tapped(object sender, TappedEventArgs e)
        {
            themecontent.IsVisible = true;
            logoutcontent.IsVisible = false;
            bottomsheet.Show();
        }

        /// <summary>
        /// Handles the settings account tap event.
        /// </summary>
        async void Settings_Account_Tapped(object sender, TappedEventArgs e)
        {
            NavigationDrawerGrid.ZIndex = 0;
            AccountEditingGrid.IsVisible = true;
            AccountEditingGrid.ZIndex = 1;
            await AccountEditingGrid.TranslateTo(0, 0, 250, Easing.CubicIn);
        }

        /// <summary>
        /// Navigates to the Help page.
        /// </summary>
        void Settings_Help_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        /// <summary>
        /// Opens the logout confirmation bottom sheet.
        /// </summary>
        void Settings_Logout_Tapped(object sender, TappedEventArgs e)
        {
            themecontent.IsVisible = false;
            logoutcontent.IsVisible = true;
            bottomsheet.Show();
        }

        /// <summary>
        /// Handles theme changes when the radio group selection changes.
        /// </summary>
        void SfRadioGroup_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
        {
            if (Application.Current != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
                    if (theme != null)
                    {
                        if (e.CurrentItem?.Text is "Light")
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                            Application.Current.UserAppTheme = AppTheme.Light;
                        }
                        else if(e.CurrentItem?.Text is "Dark")
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                            Application.Current.UserAppTheme = AppTheme.Dark;
                        }
                        else
                        {
                            Application.Current.UserAppTheme = AppTheme.Unspecified;
                            var systemTheme = Application.Current.RequestedTheme; 
                            theme.VisualTheme = systemTheme == AppTheme.Dark ? SfVisuals.MaterialDark : SfVisuals.MaterialLight;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Closes the bottom sheet.
        /// </summary>
        void CloseBottomSheet(object sender, EventArgs e)
        {
            bottomsheet.IsOpen = false;
        }

        /// <summary>
        /// Logs out the user and navigates to the main page.
        /// </summary>
        void LogoutAction(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(_physicalInfoViewmodel, _personalInfoViewModel));
        }

        /// <summary>
        /// Closes the bottom sheet when tapped.
        /// </summary>
        void Closebottomsheet_Tapped(object sender, TappedEventArgs e)
        {
            bottomsheet.IsOpen=false;
        }

        void ChangeEmail_Tapped(object sender, TappedEventArgs e)
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.4;
            EmailUpdated.IsVisible = false;
            VerficationContent.IsVisible = false;
            PasswordUpdated.IsVisible = false;
            ChangePassword.IsVisible = false;
            deletecontent.IsVisible = false;
            ChangeEmailContent.IsVisible = true;
            accounteditingbottomsheet.Show();
        }

        void ChangePassword_Tapped(object sender, TappedEventArgs e)
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.55;
            EmailUpdated.IsVisible = false;
            PasswordUpdated.IsVisible = false;
            VerficationContent.IsVisible = false;
            ChangeEmailContent.IsVisible = false;
            deletecontent.IsVisible = false;
            resetpasswordcontent.IsVisible = false;
            forgetpasswordcontent.IsVisible = false;
            ChangePassword.IsVisible = true;
            accounteditingbottomsheet.Show();
        }

        void DeleteAccount_Tapped(object sender, TappedEventArgs e)
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.3;
            EmailUpdated.IsVisible = false;
            PasswordUpdated.IsVisible = false;
            VerficationContent.IsVisible = false;
            ChangePassword.IsVisible = false;
            forgetpasswordcontent.IsVisible = false;
            ChangeEmailContent.IsVisible = false;
            VerficationContent.IsVisible = false;
            resetpasswordcontent.IsVisible = false;
            deletecontent.IsVisible = true;
            accounteditingbottomsheet.Show();
        }

        void Verification_Clicked(object sender, EventArgs e)
        {
            if (_personalInfoViewModel != null && !string.IsNullOrEmpty(currentemaileditor.Text))
            {
                _personalInfoViewModel.Email = currentemaileditor.Text;
            }

            if (_personalInfoViewModel != null)
            {
                if ((!string.IsNullOrEmpty(_personalInfoViewModel.Email) && !string.IsNullOrEmpty(newemaileditor.Text)) && _personalInfoViewModel.Email != newemaileditor.Text)
                {
                    accounteditingbottomsheet.HalfExpandedRatio = 0.4;
                    Verificationtextlabel.Text = $"We have sent a verification code to {newemaileditor.Text}";
                    ChangeEmailContent.IsVisible = false;
                    OTP = new Random().Next(100000, 999999).ToString();
                    otppopup.BindingContext = new
                    {
                        otpMessage = $"Hello Mr./Mrs.{newemaileditor.Text}, Use this one-time password to validate your login {OTP}"
                    };

                    otppopup.IsVisible = true;
                    OtpPopup.IsOpen = true;
                    VerficationContent.IsVisible = true;
                    accounteditingbottomsheet.Show();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(currentemaileditor.Text))
                    {
                        currentemail.HelperText = "Currentemail cannot be empty";
                    }
                    else if (string.IsNullOrWhiteSpace(newemaileditor.Text))
                    {
                        newemail.HelperText = "New Email cannot be empty";
                    }
                    else if (_personalInfoViewModel.Email == newemaileditor.Text)
                    {
                        newemail.HelperText = "New Email should be differenet";
                    }
                    else
                    {
                        currentemail.HelperText = string.Empty;
                        newemail.HelperText = string.Empty;
                    }
                }
            }
        }

        void VerificationNext_Clicked(object sender, EventArgs e)
        {
            if (OTP == (string?)maskentry.Value)
            {
                VerficationContent.IsVisible = false;
                maskentry.Value = string.Empty;
                if (passwordupdate)
                {
                    accounteditingbottomsheet.HalfExpandedRatio = 0.5;
                    resetpasswordcontent.IsVisible = true;
                }
                else
                {
                    _personalInfoViewModel.Email = newemaileditor.Text;
                    accounteditingbottomsheet.HalfExpandedRatio = 0.45;
                    EmailUpdated.IsVisible = true;
                }
            }

            passwordupdate = false;
            accounteditingbottomsheet.Show();
        }

        void Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        void ClosebottomsheetContent_Tapped(object sender, TappedEventArgs e)
        {
            accounteditingbottomsheet.IsOpen = false;
        }

        void Resendbutton_Tapped(object sender, TappedEventArgs e)
        {
            OTP = new Random().Next(100000, 999999).ToString();
            otppopup.BindingContext = new
            {
                otpMessage = $"Hello Mr./Mrs.{_personalInfoViewModel?.Email}, Use this one-time password to validate your login {OTP}"
            };

            otppopup.IsVisible = true;
            OtpPopup.IsOpen = true;
        }

        void maskedeye_Tapped(object sender, TappedEventArgs e)
        {
            isPasswordMasked = !isPasswordMasked;

            // Toggle between masked and unmasked icons
            maskedeyelabel.Text = isPasswordMasked ? "\uE753" : "\uE752";
            confirmpasswordentry.PasswordChar = isPasswordMasked ? '*' : '\0';
            resetpasswordmaskeeye.Text = isPasswordMasked ? "\uE753" : "\uE752";
            confirmresetpassowrd.PasswordChar = isPasswordMasked ? '*' : '\0';
        }

        void PasswordChange_Clicked(object sender, EventArgs e)
        {
            if (_personalInfoViewModel != null)
            {
                if ((!string.IsNullOrEmpty(password.Text) && !string.IsNullOrEmpty(newPassword.Text) && !string.IsNullOrEmpty((string?)confirmpasswordentry.Value)))
                {
                    if((password.Text != newPassword.Text) &&(newPassword.Text == (string?)confirmpasswordentry.Value))
                    {
                        accounteditingbottomsheet.HalfExpandedRatio = 0.4;
                        EmailUpdated.IsVisible = false;
                        VerficationContent.IsVisible = false;
                        ChangeEmailContent.IsVisible = false;
                        ChangePassword.IsVisible = false;
                        forgetpasswordcontent.IsVisible = false;
                        resetpasswordcontent.IsVisible = false;
                        PasswordUpdated.IsVisible = true;
                        accounteditingbottomsheet.Show();
                    }
                    else if (password.Text == newPassword.Text)
                    {
                        newPasswordinput.HelperText = "New password must be different.";
                    }
                    else if (newPassword.Text != (string?)confirmpasswordentry.Value)
                    {
                        newPasswordinput.HelperText = "NewPassword should match confirmpassword";
                        confirmpasswordinput.HelperText = "Confirm Password should match newpassword";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(password.Text))
                    {
                        passwordinput.HelperText = "Password should not be empty";
                    }
                    else if (string.IsNullOrEmpty(newPassword.Text))
                    {
                        newPasswordinput.HelperText = "New Password should not be empty";
                    }
                    else if (string.IsNullOrEmpty((string?)confirmpasswordentry.Value))
                    {
                        confirmpasswordinput.HelperText = "Confirm Password should not be empty";
                    }
                }
            }
        }

        void Forgotpassword_Tapped(object sender, TappedEventArgs e)
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.4;
            passwordupdate = true;
            EmailUpdated.IsVisible = false;
            VerficationContent.IsVisible = false;
            ChangeEmailContent.IsVisible = false;
            ChangePassword.IsVisible = false;
            forgetpasswordcontent.IsVisible = true;
            accounteditingbottomsheet.Show();
        }

        void NextButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(forgotpasswordemailentry.Text))
            {
                OTP = new Random().Next(100000, 999999).ToString();
                otppopup.BindingContext = new
                {
                    otpMessage = $"Hello Mr./Mrs.{forgotpasswordemailentry.Text}, Use this one-time password to validate your login {OTP}"
                };

                Verificationtextlabel.Text = $"We have sent a verification code to {forgotpasswordemailentry.Text}";
                otppopup.IsVisible = true;
                OtpPopup.IsOpen = true;
                accounteditingbottomsheet.HalfExpandedRatio = 0.4;
                EmailUpdated.IsVisible = false;
                VerficationContent.IsVisible = false;
                ChangePassword.IsVisible = false;
                forgetpasswordcontent.IsVisible = false;
                ChangeEmailContent.IsVisible = false;
                VerficationContent.IsVisible = true;
                accounteditingbottomsheet.Show();
            }
            else
            {
                forgotpasswordemail.HelperText = "Email cannot be empty";
            }
        }

        void ResetButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newresetpassword.Text) && !string.IsNullOrEmpty((string?)confirmresetpassowrd.Value))
            {
                if (newresetpassword.Text == (string?)confirmresetpassowrd.Value)
                {
                    accounteditingbottomsheet.HalfExpandedRatio = 0.45;
                    EmailUpdated.IsVisible = false;
                    VerficationContent.IsVisible = false;
                    ChangePassword.IsVisible = false;
                    forgetpasswordcontent.IsVisible = false;
                    ChangeEmailContent.IsVisible = false;
                    VerficationContent.IsVisible = false;
                    resetpasswordcontent.IsVisible = false;
                    PasswordUpdated.IsVisible = true;
                    accounteditingbottomsheet.Show();
                }
                else
                {
                    newpasswordinput.HelperText = "NewPassword should match confirmpassword";
                    confirmresetpasswordinput.HelperText = "ConfirmPassword should match newpassword";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(newresetpassword.Text))
                {
                    newpasswordinput.HelperText = "NewPassword should not be empty";
                }
                else if (string.IsNullOrEmpty(confirmresetpassowrd.Text))
                {
                    confirmresetpasswordinput.HelperText = "ConfirmPassword should not be empty";
                }
            }
        }

        void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(_physicalInfoViewmodel, _personalInfoViewModel));
        }

        void CloseBottomsheet_Clicked(object sender, EventArgs e)
        {
            accounteditingbottomsheet.IsOpen = false;
        }

        async void BackButton_Tapped(object sender, TappedEventArgs e)
        {
            await AccountEditingGrid.TranslateTo(-AccountEditingGrid.Width, 0, 250, Easing.CubicIn);
            NavigationDrawerGrid.ZIndex = 1;
            AccountEditingGrid.ZIndex = 0;
            AccountEditingGrid.IsVisible = false;

        }

        async void CopyOtpButton_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(OTP);
            otppopup.IsVisible = false;
            OtpPopup.IsOpen = false;
        }
    }
}
