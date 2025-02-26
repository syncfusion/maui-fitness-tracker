using FitnessTracker.Models;

namespace FitnessTracker.Views;

public partial class SignUpPage : ContentPage
{
    PersonalInfo? viewModel;
    bool passwordupdate = false;
    bool _passwordupdatedpage = false;
    string? OTP = null;
    public SignUpPage()
    {
        InitializeComponent();
       viewModel= new PersonalInfo();
       BindingContext = viewModel;
    }

    void Resendbutton_Tapped(object sender, TappedEventArgs e)
    {
        OTP = new Random().Next(100000, 999999).ToString();
        otppopup.BindingContext = new
        {
            otpMessage = $"Hello Mr./Mrs.{viewModel?.Email}, Use this one-time password to validate your login {OTP}"
        };
        otppopup.IsVisible = true;
        OtpPopup.IsOpen = true;
    }
    private bool isPasswordMasked = true;
    void maskedeye_Tapped(object sender, TappedEventArgs e)
    {
        isPasswordMasked = !isPasswordMasked;

        // Toggle between masked and unmasked icons
        maskedeyelabel.Text = isPasswordMasked ? "\uE753" : "\uE752";
        confirmpasswordentry.PasswordChar = isPasswordMasked ? '*' : '\0';
    }

    void Signup_Tapped(object sender, TappedEventArgs e)
    {
        Signinpage.IsVisible = false;
        forgotpasswordpage.IsVisible = false;
        verificationpage.IsVisible = false;
        Resetpasswordpage.IsVisible = false;
        passwordupdatedpage.IsVisible = false;
        Signuppage.IsVisible = true;
    }

    void Signinpage_Tapped(object sender, TappedEventArgs e)
    {
        Signuppage.IsVisible = false;
        forgotpasswordpage.IsVisible = false;
        verificationpage.IsVisible = false;
        Resetpasswordpage.IsVisible = false;
        passwordupdatedpage.IsVisible = false;
        Signinpage.IsVisible = true;
    }

    void forgotpasswordpage_Tapped(object sender, TappedEventArgs e)
    {
        Signuppage.IsVisible = false;
        Signinpage.IsVisible = false;
        verificationpage.IsVisible = false;
        Resetpasswordpage.IsVisible = false;
        passwordupdatedpage.IsVisible = false;
        forgotpasswordpage.IsVisible = true;
    }

    void NextPageButton_Clicked(object sender, EventArgs e)
    {
        if (viewModel !=null &&!string.IsNullOrEmpty(forgotpasswordemail.Text))
        {
            viewModel.Email = forgotpasswordemail.Text;
            Verificationtextlabel.Text = $"We have sent a verification code to {viewModel.Email}";
        }
        if (viewModel != null && !string.IsNullOrEmpty(viewModel.Email))
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            OTP = new Random().Next(100000, 999999).ToString();
            otppopup.BindingContext = new
            {
                otpMessage = $"Hello Mr./Mrs.{viewModel?.Email}, Use this one-time password to validate your login {OTP}"
            };
            otppopup.IsVisible = true;
            OtpPopup.IsOpen = true;
            verificationpage.IsVisible = true;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(forgotpasswordemail.Text))
            {
                forgotpasswordemail.HelperText = "Email cannot be empty";
            }
            else
            {
                forgotpasswordemail.HelperText = string.Empty;
            }
        }
    }

    void VerificationNextpageButton_Clicked(object sender, EventArgs e)
    {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            Resetpasswordpage.IsVisible = true;
    }
    void ResetPageButton_Clicked(object sender, EventArgs e)
    {
        _passwordupdatedpage = true;
        if ((!string.IsNullOrEmpty((string?)confirmpasswordentry.Value) && !string.IsNullOrEmpty(newPasswordEntry.Text)) &&
        newPasswordEntry.Text == (string?)confirmpasswordentry.Value)
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = true;
        }
        else
        {
            // Check Password
            if (string.IsNullOrWhiteSpace(newPassword.Text))
            {
                newPassword.HelperText = "Password cannot be empty";
            }
            else if (string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                confirmPassword.HelperText = "Confirm Password cannot be empty";
            }
            else if (newPassword.Text != confirmPassword.Text)
            {
                newPassword.HelperText = "NewPassword should match confirmpassword";
                confirmPassword.HelperText = "Confirm Password should match newpassword";
            }
            else
            {
                newPassword.HelperText = string.Empty;
                confirmPassword.HelperText = string.Empty;
            }

        }
    }

    void SignupButton_Clicked(object sender, EventArgs e)
    {
        if (viewModel != null && !string.IsNullOrEmpty(NameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text) && !string.IsNullOrEmpty(EmailEntry.Text))
        {
            viewModel.Name = NameEntry.Text;
            viewModel.Password = PasswordEntry.Text;
            viewModel.Email = EmailEntry.Text;
        }
        if (viewModel != null)
        {
            if ((!string.IsNullOrEmpty(viewModel.Email) &&
        !string.IsNullOrEmpty(viewModel.Password) &&
        !string.IsNullOrEmpty(viewModel.Name) &&
        !string.IsNullOrEmpty(Confirmpassword.Text)) &&
        viewModel.Password == Confirmpassword.Text && termscheckbox.IsChecked is true)
            {
                Signuppage.IsVisible = false;
                Signinpage.IsVisible = false;
                forgotpasswordpage.IsVisible = false;
                verificationpage.IsVisible = false;
                Resetpasswordpage.IsVisible = false;
                passwordupdatedpage.IsVisible = false;
                Navigation.PushAsync(new ProfileSetupPage(viewModel));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Name.Text))
                {
                    Name.HelperText = "Name cannot be empty";
                }
                else
                {
                    Name.HelperText = string.Empty;
                }

                // Check Email
                if (string.IsNullOrWhiteSpace(Email.Text))
                {
                    Email.HelperText = "Email cannot be empty";
                }
                else
                {
                    Email.HelperText = string.Empty;
                }

                // Check Password
                if (string.IsNullOrWhiteSpace(Password.Text))
                {
                    Password.HelperText = "Password cannot be empty";
                }
                else
                {
                    Password.HelperText = string.Empty;
                }

                // Check Confirm Password
                if (string.IsNullOrWhiteSpace(Confirmpassword.Text))
                {
                    Confirmpassword.HelperText = "Confirm Password cannot be empty";
                }
                else
                {
                    Confirmpassword.HelperText = string.Empty;
                }

                if (viewModel.Password != Confirmpassword.Text)
                {
                    Confirmpassword.HelperText = "Confirm Password should match Password";
                    Password.HelperText = "Password should match Confirm Password";
                }
            }
        }
    }

    void SigninButton_Clicked(object sender, EventArgs e)
    {
        if (viewModel != null && !string.IsNullOrEmpty(signinemail.Text) && !string.IsNullOrEmpty(signinpassword.Text))
        {
            viewModel.Email = signinemail.Text;
            viewModel.Password = signinpassword.Text;
        }
        if ( viewModel != null && !string.IsNullOrEmpty(viewModel.Email) && !string.IsNullOrEmpty(viewModel.Password))
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            Navigation.PushAsync(new ProfileSetupPage(viewModel));
        }
        else if (_passwordupdatedpage)
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            Navigation.PushAsync(new ProfileSetupPage(viewModel));
        }
        else
        {
            // Check Email
            if (string.IsNullOrWhiteSpace(signinemail.Text))
            {
                signinemail.HelperText = "Email cannot be empty";
            }
            else
            {
                signinemail.HelperText = string.Empty;
            }

            // Check Password
            if (string.IsNullOrWhiteSpace(signinpassword.Text))
            {
                signinpassword.HelperText = "Password cannot be empty";
            }
            else
            {
                signinpassword.HelperText = string.Empty;
            }
        }
    }

    async void CopyOtpButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(OTP);
        otppopup.IsVisible = false;
        OtpPopup.IsOpen = false;
    }
}