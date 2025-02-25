
using FitnessTracker.Models;
using Syncfusion.Maui.Themes;
using Syncfusion.Maui.Toolkit.BottomSheet;

namespace FitnessTracker.Views;

public partial class HomePage : ContentPage
{
    private ProfileSetupViewModel viewModel;
    bool passwordupdate = false;
    bool _passwordupdatedpage = false;
    public HomePage(ProfileSetupViewModel profileviewModel)
	{
		InitializeComponent();
        viewModel = profileviewModel;
        BindingContext = viewModel;
    }

    /// <summary>
    /// Handles the tab item tapped event and updates the header label accordingly.
    /// </summary>
    void SfTabView_TabItemTapped(object sender, Syncfusion.Maui.Toolkit.TabView.TabItemTappedEventArgs e)
    {
        if (e.TabItem!.Header != "Home")
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
        if (tabview.SelectedIndex == 0)
        {
            addButton.IsVisible = false;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = true;
        }
        else
        {
            Navigation.PushAsync(new AddActivityPage());
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
        Navigation.PushAsync(new EditProfilePage(viewModel));
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
                    else if (e.CurrentItem?.Text is "Dark")
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
        Navigation.PushAsync(new MainPage());
    }

    /// <summary>
    /// Closes the bottom sheet when tapped.
    /// </summary>
    void Closebottomsheet_Tapped(object sender, TappedEventArgs e)
    {
        bottomsheet.IsOpen = false;
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
        accounteditingbottomsheet.HalfExpandedRatio = 0.5;
        EmailUpdated.IsVisible = false;
        PasswordUpdated.IsVisible = false;
        VerficationContent.IsVisible = false;
        ChangeEmailContent.IsVisible = false;
        deletecontent.IsVisible = false;
        resetpasswordcontent.IsVisible = false;
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
        accounteditingbottomsheet.HalfExpandedRatio = 0.4;
        ChangeEmailContent.IsVisible = false;
        viewModel.OTP = new Random().Next(10000, 99999).ToString();
        otppopup.IsVisible = true;
        OtpPopup.IsOpen = true;
        VerficationContent.IsVisible = true;
        accounteditingbottomsheet.Show();
    }

    void VerificationNext_Clicked(object sender, EventArgs e)
    {

        VerficationContent.IsVisible = false;
        if (passwordupdate)
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.4;
            resetpasswordcontent.IsVisible = true;
        }
        else
        {
            accounteditingbottomsheet.HalfExpandedRatio = 0.4;
            EmailUpdated.IsVisible = true;

        }
        passwordupdate = false;
        accounteditingbottomsheet.Show();
    }

    void Login_Clicked(object sender, EventArgs e)
    {
        accounteditingbottomsheet.IsOpen = false;
    }

    void ClosebottomsheetContent_Tapped(object sender, TappedEventArgs e)
    {
        accounteditingbottomsheet.IsOpen = false;
    }

    void Resendbutton_Tapped(object sender, TappedEventArgs e)
    {

    }
    private bool isPasswordMasked = true;
    void maskedeye_Tapped(object sender, TappedEventArgs e)
    {
        isPasswordMasked = !isPasswordMasked;

        // Toggle between masked and unmasked icons
         maskedeyelabel.Text = isPasswordMasked ? "\uE753" : "\uE752";
         confirmpasswordentry.PasswordChar = isPasswordMasked ? '*' : '\0';
    }

    void PasswordChange_Clicked(object sender, EventArgs e)
    {
        if ((!string.IsNullOrEmpty(viewModel.Password) && !string.IsNullOrEmpty(viewModel.NewPassword) && !string.IsNullOrEmpty(viewModel.ConfirmPassword))
            && viewModel.NewPassword == viewModel.ConfirmPassword)
        {
            if (viewModel.Password == viewModel.NewPassword)
            {
                newPasswordinput.HelperText = "New password must be different.";
            }
            else
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
        }
        else
        {
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                passwordinput.HelperText = "Password should not be empty";
            }
            else if (string.IsNullOrEmpty(viewModel.NewPassword))
            {
                newPasswordinput.HelperText = "New Password should not be empty";
            }
            else if (string.IsNullOrEmpty(viewModel.ConfirmPassword))
            {
                confirmpasswordinput.HelperText = "Confirm Password should not be empty";
            }
            else if (viewModel.NewPassword != viewModel.ConfirmPassword)
            {
                newPasswordinput.HelperText = "NewPassword should match confirmpassword";
                confirmpasswordinput.HelperText = "Confirm Password should match newpassword";
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
        accounteditingbottomsheet.HalfExpandedRatio = 0.4;
        viewModel.OTP = new Random().Next(10000, 99999).ToString();
        otppopup.IsVisible = true;
        OtpPopup.IsOpen = true;
        EmailUpdated.IsVisible = false;
        VerficationContent.IsVisible = false;
        ChangePassword.IsVisible = false;
        forgetpasswordcontent.IsVisible = false;
        ChangeEmailContent.IsVisible = false;
        VerficationContent.IsVisible = true;
        accounteditingbottomsheet.Show();
    }

    void ResetButton_Clicked(object sender, EventArgs e)
    {
        accounteditingbottomsheet.HalfExpandedRatio = 0.5;
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

    void DeleteButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
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
        await Clipboard.SetTextAsync(viewModel.OTP);
        otppopup.IsVisible = false;
        OtpPopup.IsOpen = false;
    }
}