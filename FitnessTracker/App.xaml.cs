using Syncfusion.Maui.Themes;

namespace FitnessTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Application.Current != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    var theme1 = mergedDictionaries.OfType<Syncfusion.Maui.Toolkit.Themes.SyncfusionThemeResourceDictionary>().FirstOrDefault();
                    var theme2 = mergedDictionaries.OfType<Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary>().FirstOrDefault();
                    if (theme1 != null && theme2 != null)
                    {
                        if (Application.Current.RequestedTheme == AppTheme.Light)
                        {
                            theme1.VisualTheme = Syncfusion.Maui.Toolkit.Themes.SfVisuals.MaterialLight;
                            theme2.VisualTheme = SfVisuals.MaterialLight;
                            Application.Current.UserAppTheme = AppTheme.Light;
                        }
                        else if (Application.Current.RequestedTheme == AppTheme.Dark || Application.Current.RequestedTheme == AppTheme.Unspecified)
                        {
                            theme1.VisualTheme = Syncfusion.Maui.Toolkit.Themes.SfVisuals.MaterialDark;
                            theme2.VisualTheme = SfVisuals.MaterialDark;
                            Application.Current.UserAppTheme = AppTheme.Dark;
                        }
                    }
                }
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
#if ANDROID || IOS
            return new Window(new NavigationPage(new SignUpPageMobile()));
#else
            return new Window(new NavigationPage(new SignUpPageDesktop()));
#endif
        }
    }
}