using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowLogin : UIWindow
{
    public static List<System.Action> PostLoginActions = new List<Action>();

    [SerializeField]
    InputField emailField, passwordField;

  /*  [SerializeField]
    Window_PickTeam pickTeamWindow;

    [SerializeField]
    Window_LoginRegister window_Register;*/

    public void Show()
    {
        ResetInputFields();
    }

    public void Login()
    {
        string emailError = "";
        string passwordError = "";

    }
    public void Register()
    {
        this.Close();
    //    window_Register.Show();
    }

    void ResetInputFields()
    {
        emailField.text = passwordField.text = "";
    }
}
