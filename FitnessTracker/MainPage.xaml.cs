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

        private void SfTabView_TabItemTapped(object sender, Syncfusion.Maui.Toolkit.TabView.TabItemTappedEventArgs e)
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

        private async void Hamburger_Tapped(object sender, TappedEventArgs e)
        {
            home.ZIndex = 0;
            NavigationDrawerGrid.IsVisible = true;
            NavigationDrawerGrid.ZIndex = 1;
            await NavigationDrawerGrid.TranslateTo(0, 0, 250, Easing.CubicIn);
         }

        private void Notification_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void ProfilePhoto_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void AddIcon_Clicked(object sender, EventArgs e)
        {
            addButton.IsVisible = false;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = true;
        }

        private void CloseIcon_Clicked(object sender, EventArgs e)
        {
            addButton.IsVisible = true;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = false;
        }

        private void OverlayGrid_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void TrackActivity_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrackActivity());
            addButton.IsVisible = true;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = false;
        }
        private async void Settings_CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            await NavigationDrawerGrid.TranslateTo(-NavigationDrawerGrid.Width, 0, 250, Easing.CubicIn);
            home.ZIndex = 1;
            NavigationDrawerGrid.ZIndex = 0;
            NavigationDrawerGrid.IsVisible = false;
        }

        private void Settings_ProfilePhoto_Tapped(object sender, TappedEventArgs e)
        {

        }
        private void Settings_Profile_Tapped(object sender, TappedEventArgs e)
        {
           Navigation.PushAsync(new EditProfilePage());
        }

        private void Settings_Notification_Tapped(object sender, TappedEventArgs e)
        {
            
        }

        private void Settings_Appearance_Tapped(object sender, TappedEventArgs e)
        {
            themecontent.IsVisible = true;
            logoutcontent.IsVisible = false;
            bottomsheet.Show();
        }

        private void Settings_Account_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void Settings_Help_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        private void Settings_Logout_Tapped(object sender, TappedEventArgs e)
        {
            themecontent.IsVisible = false;
            logoutcontent.IsVisible = true;
            bottomsheet.Show();
        }

        private void SfRadioGroup_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
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

        private void CloseBottomSheet(object sender, EventArgs e)
        {
            bottomsheet.IsOpen = false;
        }

        private void LogoutAction(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }

}
