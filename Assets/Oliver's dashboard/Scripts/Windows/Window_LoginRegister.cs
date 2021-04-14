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

public class Window_LoginRegister : UIWindow
{
    [SerializeField]
    InputField emailField, emailConfirmField, passwordField, passwordConfirmField, displayNameField, teamNameField;

    [SerializeField]
    Window_PickTeam pickTeamWindow;

    [SerializeField]
    Window_Login window_Login;

    public override void Show()
    {
        ResetInputFields();
        base.Show();

    }

    void ResetInputFields()
    {
        emailField.text = emailConfirmField.text = passwordField.text = passwordConfirmField.text = displayNameField.text = teamNameField.text = "";
    }


    public void Register()
    {
        string emailError = "";
        string passwordError = "";
        Debug.Log("register");
        if(emailField.text != emailConfirmField.text)
        {
            PopupDialog.Instance.Show("Oops!", "The email addresses you provided do not match.");
            EventSystem.current.SetSelectedGameObject(emailConfirmField.gameObject);
            return;
        }

        if (passwordField.text != passwordConfirmField.text)
        {
            PopupDialog.Instance.Show("Oops!", "The passwords you provided do not match.");
            EventSystem.current.SetSelectedGameObject(passwordConfirmField.gameObject);
            return;
        }

        if (string.IsNullOrEmpty(teamNameField.text) || teamNameField.text.Length < 3)
        {
            PopupDialog.Instance.Show("Oops!", "Please enter a Team name. It must be at least three characters long.");
            EventSystem.current.SetSelectedGameObject(teamNameField.gameObject);
            return;
        }

       /* if (Validation.ValidateEmail(emailField.text, out emailError) && Validation.ValidatePassword(passwordField.text, out passwordError))
        {
            CanvasLoading.Instance.Show();
            APIManager.Register(emailField.text, passwordField.text, displayNameField.text, teamNameField.text, (success) => 
            {
                if (success)
                {
                    User.current = null;
                    CanvasLoading.Instance.Hide();
                    PopupDialog.Instance.Show("Success", "Your account has been created. You may now sign in.");
                    GoBackToLogin();
                }
                else
                {
                    CanvasLoading.Instance.Hide();
                    PopupDialog.Instance.Show("Oops!", "An error occurred while trying to create your account. Please try again. If the problem persists, contact support at support@agileliteracy.com");
                }
            });
        }
        else
        {
            if (!string.IsNullOrEmpty(emailError))
            {
                PopupDialog.Instance.Show(emailError);
            }
            else if (!string.IsNullOrEmpty(passwordError))
            {
                PopupDialog.Instance.Show(passwordError);
            }
            else
            {
                PopupDialog.Instance.Show("Invalid email or password.");
            }
        }*/
    }

    public void GoBackToLogin()
    {
        this.Close();
        window_Login.Show();
    }

    

    
}
