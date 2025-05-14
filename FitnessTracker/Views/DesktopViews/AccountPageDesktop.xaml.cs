

using Syncfusion.Maui.Toolkit.OtpInput;
using System.Linq.Expressions;

namespace FitnessTracker;

public partial class AccountPageDesktop : ContentView
{
    string? OTP = null;
    FitnessViewModel FitnessViewModel;
    PersonalInfo PersonalInfo = new PersonalInfo();
    bool isPasswordMasked = true;
    bool ispassword = false;

    public AccountPageDesktop()
    {
        InitializeComponent();
    }

    public AccountPageDesktop(FitnessViewModel fitnessViewModel,PersonalInfo personalInfo)
	{
		InitializeComponent();
        PersonalInfo = personalInfo;
        ChangeEmailPopup.BindingContext = PersonalInfo;
        AccountEditingPage.BindingContext = fitnessViewModel;
        FitnessViewModel = fitnessViewModel;
    }

    private void DeleteAccount_Tapped(object sender, TappedEventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        PasswordUpdatedPopup.IsVisible = false;
        popupgrid.IsVisible = true;
        DeleteAccountPopup.IsVisible = true;
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = false;
        popupgrid.IsVisible = false;
        if (ChangeEmailPopup.IsVisible)
        {
            ChangeEmailPopup.IsVisible = false;
        }
        else if (ChangePasswordPopup.IsVisible)
        {
            ChangePasswordPopup.IsVisible = false;
        }
        else if (ResetPasswordPopup.IsVisible)
        {
            ResetPasswordPopup.IsVisible = false;
        }
        else if (DeleteAccountPopup.IsVisible)
        {
            DeleteAccountPopup.IsVisible = false;
        }
    }
    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        if (!string.IsNullOrEmpty(newresetpassword.Text) && !string.IsNullOrEmpty((string?)confirmresetpassowrd.Text))
        {
            if (newresetpassword.Text == (string?)confirmresetpassowrd.Text)
            {
                ResetPasswordPopup.IsVisible = false;
                PasswordUpdatedPopup.IsVisible = true;
                PersonalInfo.Password = newresetpassword.Text;
            }
            else
            {
                newpasswordinput.HelperText = "NewPassword should match confirmpassword";
                confirmresetpasswordinput.HelperText = "ConfirmPassword should match newpassword";
            }
        }
        else
        {
            if (string.IsNullOrEmpty(newresetpassword.Text))
            {
                newpasswordinput.HelperText = "NewPassword should not be empty";
            }
            else if (string.IsNullOrEmpty(confirmresetpassowrd.Text))
            {
                confirmresetpasswordinput.HelperText = "ConfirmPassword should not be empty";
            }
        }
    }
    private void NextButton_Clicked(object sender, EventArgs e)
    {
        if (PersonalInfo != null && !string.IsNullOrEmpty(currentemaileditor.Text))
        {
            PersonalInfo.Email = currentemaileditor.Text;
        }

        if (PersonalInfo != null)
        {
            if ((!string.IsNullOrEmpty(PersonalInfo.Email) && !string.IsNullOrEmpty(newemaileditor.Text)) && PersonalInfo.Email != newemaileditor.Text)
            {
                Verificationtextlabel.Text = $"We have sent a verification code to {newemaileditor.Text}";
                OTP = new Random().Next(100000, 999999).ToString();
                OtpPopup.BindingContext = new
                {
                    otpMessage = $"Hello Mr./Mrs.{newemaileditor.Text}, Use this one-time password to validate your login {OTP}"
                };

                OtpPopup.IsOpen = true;
                FitnessViewModel.IsVisible = true;
                popupgrid.IsVisible = true;
                ChangeEmailPopup.IsVisible = false;
                VerificationPopup.IsVisible = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(currentemaileditor.Text))
                {
                    currentemail.HelperText = "Currentemail cannot be empty";
                }
                else if (string.IsNullOrWhiteSpace(newemaileditor.Text))
                {
                    newemail.HelperText = "New Email cannot be empty";
                }
                else if (PersonalInfo.Email == newemaileditor.Text)
                {
                    newemail.HelperText = "New Email should be differenet";
                }
                else
                {
                    currentemail.HelperText = string.Empty;
                    newemail.HelperText = string.Empty;
                }
            }
        }
    }

    private void VerificationNext_Clicked(object sender, EventArgs e)
    {
        otpinput.InputState = Syncfusion.Maui.Toolkit.OtpInput.OtpInputState.Default;
        FitnessViewModel.IsVisible = true;
        if (OTP == otpinput.Value)
        {
            otpinput.Value = string.Empty;
            if (ispassword)
            {
                VerificationPopup.IsVisible = false;
                ResetPasswordPopup.IsVisible = true;
                ispassword = false;
            }
            else
            {
                PersonalInfo.Email = newemaileditor.Text;
                VerificationPopup.IsVisible = false;
                EmailUpdatedPopup.IsVisible = true;
            }
        }
        else
        {
            otpinput.InputState = OtpInputState.Error;
        }
        ispassword = false;
    }
    void Forgotpassword_Tapped(object sender, TappedEventArgs e)
    {
        ChangePasswordPopup.IsVisible = false;
        ForgotPasswordPopup.IsVisible = true;
    }
    private void ChangeEmail_Tapped(object sender, TappedEventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        ChangeEmailPopup.IsVisible = true;
    }
    private void ChangePassword_Tapped(object sender, TappedEventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        ChangePasswordPopup.IsVisible = true;
    }

    void maskedeye_Tapped(object sender, TappedEventArgs e)
    {
        isPasswordMasked = !isPasswordMasked;

        // Toggle between masked and unmasked icons
        maskedeyelabel.Text = isPasswordMasked ? "\uE753" : "\uE752";
        confirmpasswordentry.IsPassword = isPasswordMasked;
        resetpasswordmaskeeye.Text = isPasswordMasked ? "\uE753" : "\uE752";
        confirmresetpassowrd.IsPassword = isPasswordMasked;
    }

    void Resendbutton_Tapped(object sender, TappedEventArgs e)
    {
        OTP = new Random().Next(100000, 999999).ToString();
        OtpPopup.BindingContext = new
        {
            otpMessage = $"Hello Mr./Mrs.{PersonalInfo?.Email}, Use this one-time password to validate your login {OTP}"
        };
        OtpPopup.IsVisible = true;
        OtpPopup.IsOpen = true;
    }

    private void PasswordNextButton_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        if (PersonalInfo != null)
        {
            if ((!string.IsNullOrEmpty(password.Text) && !string.IsNullOrEmpty(newPassword.Text) && !string.IsNullOrEmpty((string?)confirmpasswordentry.Text)))
            {
                if ((password.Text != newPassword.Text) && (newPassword.Text == (string?)confirmpasswordentry.Text))
                {
                    ChangePasswordPopup.IsVisible = false;
                    PasswordUpdatedPopup.IsVisible = true;
                }
                else if (password.Text == newPassword.Text)
                {
                    newPasswordinput.HelperText = "New password must be different.";
                }
                else if (newPassword.Text != (string?)confirmpasswordentry.Text)
                {
                    newPasswordinput.HelperText = "NewPassword should match confirmpassword";
                    confirmpasswordinput.HelperText = "Confirm Password should match newpassword";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(password.Text))
                {
                    passwordinput.HelperText = "Password should not be empty";
                }
                else if (string.IsNullOrEmpty(newPassword.Text))
                {
                    newPasswordinput.HelperText = "New Password should not be empty";
                }
                else if (string.IsNullOrEmpty((string?)confirmpasswordentry.Text))
                {
                    confirmpasswordinput.HelperText = "Confirm Password should not be empty";
                }
            }
        }
    }

    private void ForgotPasswordNext_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        ispassword = true;
        if (!string.IsNullOrEmpty(forgotpasswordemailentry.Text))
        {
            OTP = new Random().Next(100000, 999999).ToString();
            OtpPopup.BindingContext = new
            {
                otpMessage = $"Hello Mr./Mrs.{forgotpasswordemailentry.Text}, Use this one-time password to validate your login {OTP}"
            };

            Verificationtextlabel.Text = $"We have sent a verification code to {forgotpasswordemailentry.Text}";
            OtpPopup.IsOpen = true;
            ForgotPasswordPopup.IsVisible = false;
            VerificationPopup.IsVisible = true;
        }
        else
        {
            forgotpasswordemail.HelperText = "Email cannot be empty";
        }
    }

    private void ForgotBackButton_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        ForgotPasswordPopup.IsVisible = false;
        ChangePasswordPopup.IsVisible = true;
    }

    private void VerificationBackButton_Clicked(object sender, EventArgs e)
    {
        FitnessViewModel.IsVisible = true;
        popupgrid.IsVisible = true;
        VerificationPopup.IsVisible = false;
        if(ispassword)
        {
            ChangePasswordPopup.IsVisible = true;
        }
        else
        {
            ChangeEmailPopup.IsVisible = true;
        }
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPageDesktop());
    }

    async void CopyOtpButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(OTP);
        OtpPopup.IsVisible = false;
        OtpPopup.IsOpen = false;
    }
    private void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        FitnessViewModel.IsVisible = false;
        popupgrid.IsVisible = false;
        if (ChangeEmailPopup.IsVisible)
        {
            ChangeEmailPopup.IsVisible = false;
        }
        else if (ChangePasswordPopup.IsVisible)
        {
            ChangePasswordPopup.IsVisible = false;
        }
        else if(EmailUpdatedPopup.IsVisible)
        {
            EmailUpdatedPopup.IsVisible = false;
        }
        else if(PasswordUpdatedPopup.IsVisible)
        {
            PasswordUpdatedPopup.IsVisible = false;
        }
        else if(VerificationPopup.IsVisible)
        {
            VerificationPopup.IsVisible = false;
        }
        else if (ResetPasswordPopup.IsVisible)
        {
            ResetPasswordPopup.IsVisible = false;
        }
        else if (ForgotPasswordPopup.IsVisible)
        {
            ForgotPasswordPopup.IsVisible = false;
        }
        else if (DeleteAccountPopup.IsVisible)
        {
            DeleteAccountPopup.IsVisible = false;
        }
    }
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is FitnessViewModel viewModel)
        {
            FitnessViewModel = viewModel;
        }
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPageDesktop());
    }
}