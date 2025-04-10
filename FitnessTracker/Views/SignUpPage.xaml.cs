namespace FitnessTracker;

public partial class SignUpPage : ContentPage
{
#if ANDROID || IOS
    SignUpPageMobile signUpPageMobile;
#else
	SignUpPageDesktop signUpPageDesktop;
#endif
	public SignUpPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        Navigation.PushAsync(new SignUpPageMobile());
#else
		Navigation.PushAsync(new SignUpPageDesktop());
#endif
    }
}