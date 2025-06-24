namespace FitnessTracker;

public partial class FitnessTrackerDemo : ContentPage
{
	public FitnessTrackerDemo()
	{
		InitializeComponent();

#if ANDROID || IOS
        var fitnessTracker = new SignUpPageMobile();
		this.Content = fitnessTracker;
#else
        var fitnessTracker = new SignUpPageDesktop();
		this.Content = fitnessTracker;
#endif
    }


}