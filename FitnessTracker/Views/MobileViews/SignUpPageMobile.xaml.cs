namespace FitnessTracker;

public partial class SignUpPageMobile : ContentView
{
    PersonalInfo? viewModel;
    PhysicalInfo? physicalInfo;
    bool _passwordupdatedpage = false;
    string? OTP = null;
    bool isPasswordMasked = true;
    public SignUpPageMobile()
    {
        InitializeComponent();
        viewModel= new PersonalInfo();
        physicalInfo = new PhysicalInfo();
        Signinpage.BindingContext = viewModel;
    }

    void Resendbutton_Tapped(object sender, TappedEventArgs e)
    {
        OTP = new Random().Next(100000, 999999).ToString();
        otppopup.BindingContext = new
        {
            otpGreeting = $"Hello Mr./Mrs. {viewModel?.Email}, Use this one-time password to validate your login ",
            otpCode = OTP
        };

        otppopup.IsVisible = true;
        OtpPopup.IsOpen = true;
    }

    void maskedeye_Tapped(object sender, TappedEventArgs e)
    {
        isPasswordMasked = !isPasswordMasked;

        // Toggle between masked and unmasked icons
        maskedeyelabel.Text = isPasswordMasked ? "\ue753" : "\ue752";
        confirmpasswordentry.IsPassword = isPasswordMasked;
    }

    void Maskedeye_Tapped(object sender, TappedEventArgs e)
    {
        isPasswordMasked = !isPasswordMasked;

        // Toggle between masked and unmasked icons
        Maskedeyelabel.Text = isPasswordMasked ? "\ue753" : "\ue752";
        signinpasswordentry.IsPassword = isPasswordMasked;
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
                otpGreeting = $"Hello Mr./Mrs. {viewModel?.Email}, Use this one-time password to validate your login ",
                otpCode = OTP
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
            if(otpinput.Value == OTP)
            {
                Signuppage.IsVisible = false;
                Signinpage.IsVisible = false;
                forgotpasswordpage.IsVisible = false;
                verificationpage.IsVisible = false;
                passwordupdatedpage.IsVisible = false;
                Resetpasswordpage.IsVisible = true;
            }
            else
            {
                otpinput.InputState = Syncfusion.Maui.Toolkit.OtpInput.OtpInputState.Error;
            }
    }
    void ResetPageButton_Clicked(object sender, EventArgs e)
    {
        _passwordupdatedpage = true;
        if ((!string.IsNullOrEmpty((string?)confirmpasswordentry.Text) && !string.IsNullOrEmpty(newPasswordEntry.Text)) &&
        newPasswordEntry.Text == (string?)confirmpasswordentry.Text)
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
            if (string.IsNullOrWhiteSpace(newPasswordEntry.Text))
            {
                newPassword.HelperText = "Password cannot be empty";
                confirmPassword.HelperText = string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(confirmpasswordentry.Text))
            {
                confirmPassword.HelperText = "Confirm Password cannot be empty";
                newPassword.HelperText = string.Empty;
            }
            else if (newPasswordEntry.Text != confirmpasswordentry.Text)
            {
                newPassword.HelperText = "New Password should match confirm password";
                confirmPassword.HelperText = "Confirm Password should match new password";
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

        if (viewModel != null && physicalInfo != null)
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
                physicalInfo.Weight = string.Empty;
                physicalInfo.Height = string.Empty;
                physicalInfo.Gender = string.Empty;
                physicalInfo.ActiveStatus = string.Empty;
                physicalInfo.BodyFat = string.Empty;
                physicalInfo.MeasurementUnit = string.Empty;
                Navigation.PushAsync(new ProfileSetupPageMobile(viewModel,physicalInfo));
            }
            else
            {
                CheckSignUpFields();
            }
        }
    }

    bool CheckSignUpFields()
    {
        bool canEnable = true;
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            Name.HelperText = "Name cannot be empty";
            canEnable = false;
        }
        else
        {
            Name.HelperText = string.Empty;
        }

        // Check Email
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            Email.HelperText = "Email cannot be empty";
            canEnable = false;
        }
        else
        {
            Email.HelperText = string.Empty;
        }

        // Check Password
        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            Password.HelperText = "Password cannot be empty";
            canEnable = false;
        }
        else
        {
            Password.HelperText = string.Empty;
        }

        // Check Confirm Password
        if (string.IsNullOrWhiteSpace(ConfirmpasswordEntry.Text))
        {
            Confirmpassword.HelperText = "Confirm Password cannot be empty";
            canEnable = false;
        }
        else
        {
            Confirmpassword.HelperText = string.Empty;
        }

        if (PasswordEntry.Text != ConfirmpasswordEntry.Text)
        {
            Confirmpassword.HelperText = "Confirm Password should match Password";
            Password.HelperText = "Password should match Confirm Password";
            canEnable = false;
        }
        else if (!string.IsNullOrWhiteSpace(PasswordEntry.Text) && !string.IsNullOrWhiteSpace(ConfirmpasswordEntry.Text))
        {
            Password.HelperText = string.Empty;
            Confirmpassword.HelperText = string.Empty;
        }

        return canEnable;
    }

    void SigninButton_Clicked(object sender, EventArgs e)
    {
        if (viewModel != null && !string.IsNullOrEmpty(signinemail.Text) && !string.IsNullOrEmpty(signinpassword.Text))
        {
            if (viewModel.Email != signinemail.Text && viewModel.Password != signinpassword.Text)
            {
                failurepopup.IsVisible = true;
                failurepopup.Show();
            }
            else
            {
                viewModel.Email = signinemail.Text;
                viewModel.Password = signinpassword.Text;
                Navigation.PushAsync(new MainPageMobile(physicalInfo, viewModel));
            }
        }

        if ( viewModel != null && !string.IsNullOrEmpty(viewModel.Email) && !string.IsNullOrEmpty(viewModel.Password))
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = true;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            
        }
        else if (_passwordupdatedpage)
        {
            Signuppage.IsVisible = false;
            Signinpage.IsVisible = false;
            forgotpasswordpage.IsVisible = false;
            verificationpage.IsVisible = false;
            Resetpasswordpage.IsVisible = false;
            passwordupdatedpage.IsVisible = false;
            Navigation.PushAsync(new MainPageMobile(physicalInfo, viewModel));
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

    void ClosepopupButton_Clicked(object sender, EventArgs e)
    {
        failurepopup.IsVisible = false;
        failurepopup.IsOpen = false;
    }

    void Termscheckbox_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        EnableSignUpButton();
    }

    void Entry_TextChanged(object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        EnableSignUpButton();
    }

    void EnableSignUpButton()
    {
        if (termscheckbox.IsChecked == true && CheckSignUpFields())
        {
            signupbutton.IsEnabled = true;
            signupbutton.Background = (Application.Current!.RequestedTheme == AppTheme.Light) ? Color.FromArgb("#6750A4") : Color.FromArgb("#D0BCFF");
        }
        else
        {
            signupbutton.IsEnabled = false;
            signupbutton.Background = Colors.Gray;
        }
    }
}