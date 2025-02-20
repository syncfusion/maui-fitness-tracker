using FitnessTracker.Views;

namespace FitnessTracker
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MDAxQDMyMzgyZTMwMmUzMGFMV081MVlpVnJ5aHJyQzUzMFFBOUJwZ1B0YWkxNC80WnA3SHdvbmdqNzA9");
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}