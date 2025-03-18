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
                    var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
                    if (theme != null)
                    {
                        if (Application.Current.RequestedTheme == AppTheme.Light)
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                            Application.Current.UserAppTheme = AppTheme.Light;
                        }
                        else if (Application.Current.RequestedTheme == AppTheme.Dark || Application.Current.RequestedTheme == AppTheme.Unspecified)
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                            Application.Current.UserAppTheme = AppTheme.Dark;
                        }
                    }
                }
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}