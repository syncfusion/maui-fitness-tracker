using FitnessTracker.Models;
using FitnessTracker.Views;
using Syncfusion.Maui.Themes;

namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
            addButton.IsVisible = false;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = true;
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
           Navigation.PushAsync(new EditProfilePage());
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
        void Settings_Account_Tapped(object sender, TappedEventArgs e)
        {

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
            Navigation.PushAsync(new MainPage());
        }

        /// <summary>
        /// Closes the bottom sheet when tapped.
        /// </summary>
        void Closebottomsheet_Tapped(object sender, TappedEventArgs e)
        {
            bottomsheet.IsOpen=false;
        }
    }

}
