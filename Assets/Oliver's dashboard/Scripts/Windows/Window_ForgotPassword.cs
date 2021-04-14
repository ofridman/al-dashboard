using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using GameBrewStudios.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using System.Globalization;
//using GameBrewStudios;
using UnityEngine.EventSystems;

public class Window_ForgotPassword: UIWindow
{
    [SerializeField]
    GameObject forgotPasswordForm, resetPasswordForm;
    
    [SerializeField]
    InputField emailField_forgot, passwordField_reset, passwordField2_reset, codeField_reset;
    
    [SerializeField]
    Window_Login window_Login;

    public override void Show()
    {
        ResetForms();
        base.Show();

    }

    void ResetForms()
    {
        ResetInputFields();
        forgotPasswordForm.SetActive(true);
        resetPasswordForm.SetActive(false);
    }

    void ResetInputFields()
    {
        emailField_forgot.text = "";
        passwordField_reset.text = "";
        passwordField2_reset.text = "";
        codeField_reset.text = "";

        
    }

    public void ShowResetForm()
    {
        ResetInputFields();
        resetPasswordForm.SetActive(true);
        forgotPasswordForm.SetActive(false);
    }


    public void SubmitForgotPassword()
    {
        string emailError = "";
        Debug.Log("On Click SubmitForgotPassword");
      /*  if (Validation.ValidateEmail(emailField_forgot.text, out emailError))
        {
            PopupDialog.PopupButton[] buttons = new PopupDialog.PopupButton[]
            {
                new PopupDialog.PopupButton()
                {
                    buttonColor = PopupDialog.PopupButtonColor.Green,
                    text = "Continue",
                    onClicked = () => {
                        CanvasLoading.Instance.Show();
                        APIManager.ForgotPassword(emailField_forgot.text, (response) =>
                        {
                            CanvasLoading.Instance.Hide();
                            resetPasswordForm.SetActive(true);
                            forgotPasswordForm.SetActive(false);

                        });
                    }
                },
                new PopupDialog.PopupButton()
                {
                    buttonColor = PopupDialog.PopupButtonColor.Plain,
                    text = "Cancel",
                    onClicked = () => {
                        GoBackToLogin();
                    }
                }
            };

            PopupDialog.Instance.Show("Really reset password?", "By clicking Continue below, you acknowledge that you own or have been authorized to have access to the email address that was entered. You will receive an email containing a 6-Digit code that you can use on the next page to change your password.", buttons);
        }
        else
        {
            if (!string.IsNullOrEmpty(emailError))
            {
                PopupDialog.Instance.Show(emailError);
            }
            else
            {
                PopupDialog.Instance.Show("Invalid email or password.");
            }
        }*/
    }

    public void SubmitResetPassword()
    {
        string errorString = "";

     /*   if(passwordField_reset.text == passwordField2_reset.text && Validation.ValidatePassword(passwordField_reset.text, out errorString))
        {
            CanvasLoading.Instance.Show();

            ServerAPI.OnError += this.ServerAPI_OnError;

            APIManager.ResetPassword(emailField_forgot.text, codeField_reset.text, passwordField_reset.text, (response) => 
            {
                CanvasLoading.Instance.Hide();
                ServerAPI.OnError -= this.ServerAPI_OnError;
                if (response == null)
                {
                    Debug.LogError("NULL RESPONSE FROM SERVER");
                    PopupDialog.Instance.Show(serverError.text);
                }
                else if(response != null && response.ContainsKey("error"))
                {
                    if(response.ContainsKey("error"))
                    {
                        PopupDialog.Instance.Show("Oops! Something went wrong!", "Server Error: " + response["error"]);
                    }
                    else
                    {
                        //Theoretically this should never happen
                        Debug.LogError("EMPTY RESPONSE FROM SERVER???");

                    }
                }
                else
                {
                    GoBackToLogin();
                    PopupDialog.Instance.Show("Success!", "Your password has been successfully reset. You may now log in.");
                }

                
            });
        }
        else
        {
            if (!string.IsNullOrEmpty(errorString))
            {
                PopupDialog.Instance.Show(errorString);
            }
            else
            {
                if (passwordField_reset.text == passwordField2_reset.text)
                {
                    PopupDialog.Instance.Show("Invalid Password", "Passwords must be at least 6 characters in length and contain at least 1 number, 1 uppercase letter, and 1 lowercase letter.");
                }
                else
                {
                    PopupDialog.Instance.Show("Password Mismatch", "Password fields do not match. Please ensure you have correctly typed the password into both of the password fields. These two fields should be identical.");
                }
            }
        }*/
    }

   /* ServerAPI.ServerError serverError;

    private void ServerAPI_OnError(ServerAPI.ServerError serverError)
    {
        this.serverError = serverError;
    }*/

    public void Submit()
    {
        if(forgotPasswordForm.activeSelf)
        {
            SubmitForgotPassword();
        }
        else
        {
            SubmitResetPassword();
        }
    }


    public void GoBackToLogin()
    {
        this.Close();
        window_Login.Show();
    }

    

    
}
