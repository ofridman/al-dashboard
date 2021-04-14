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
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using DG.Tweening;

public class Window_Login : UIWindow
{
    public static List<System.Action> PostLoginActions = new List<Action>();

    [SerializeField]
    InputField emailField, passwordField;

    [SerializeField]
    Window_PickTeam pickTeamWindow;

    [SerializeField]
    Window_LoginRegister window_Register;
    public override void Awake()
    {

        //The default action of the Awake function disables the gameObject, we dont want that for the login window
        this.Show();
        

        LoadSavedCredentials();
    }
    
    void LoadSavedCredentials()
    {
        emailField.SetTextWithoutNotify(PlayerPrefs.GetString("UN", ""));
        passwordField.SetTextWithoutNotify(PlayerPrefs.GetString("UP", ""));
    }

    void ProcessQueryParameter(string key, object value)
    {
        if (key.ToLower() == "invitation")
        {
            string inviteId = value.ToString();
        /*    if (User.current.invitations != null & User.current.invitations.Length > 0)
            {
                PostLoginActions.Add(() =>
                {
                    bool inviteFound = false;
                    for (int i = 0; i < User.current.invitations.Length; i++)
                    {
                        if (User.current.invitations[i]._id == inviteId)
                        {
                            inviteFound = true;
                            CanvasLoading.Instance.Show();
                            APIManager.GetTeam(User.current.invitations[i].team, (team) =>
                            {
                                CanvasLoading.Instance.Hide();

                                PopupDialog.Instance.Show("New Team Invitation", "You received an invitation to join " + team.name + ". Would you like to accept the invitation?", new PopupDialog.PopupButton[]
                                {
                                        new PopupDialog.PopupButton()
                                        {
                                            buttonColor = PopupDialog.PopupButtonColor.Green,
                                            text = "Accept",
                                            onClicked = () =>
                                            {
                                                CanvasLoading.Instance.Show();
                                                APIManager.AcceptInvitation(inviteId, (memberships) =>
                                                {
                                                    User.current.memberships = memberships;
                                                    CanvasLoading.Instance.Hide();
                                                    PopLoginActionQueue();
                                                });
                                            }
                                        },
                                        new PopupDialog.PopupButton()
                                        {
                                            buttonColor = PopupDialog.PopupButtonColor.Plain,
                                            text = "Decline",
                                            onClicked = () =>
                                            {
                                                CanvasLoading.Instance.Show();
                                                APIManager.DeleteInvitation(inviteId, (response) =>
                                                {
                                                    Debug.LogWarning("DeclineInvite Response: " + JsonConvert.SerializeObject(response));
                                                    CanvasLoading.Instance.Hide();
                                                    PopLoginActionQueue();
                                                });
                                            }
                                        }
                                });
                            });

                            break;
                        }

                    }

                    if (!inviteFound)
                    {
                        Debug.LogWarning($"<color=red>Inivitation with id: {inviteId} not found for this user.</color>");
                        PopLoginActionQueue();
                    }
                });
                
            }
            else
            {
                Debug.Log("Current user does not have any pending invitations.");
            }*/
        }
    }

    public static void PopLoginActionQueue()
    {
        Debug.Log("POST-LOGIN ACTION FINISHED, POPPING THE QUEUE, MOVING TO NEXT");
        PostLoginActions.RemoveAt(0);
        HandleNextLoginAction();
    }

    public static void HandleNextLoginAction()
    {
        if (PostLoginActions == null || PostLoginActions.Count <= 0)
        {
            Debug.Log("<color=Yellow>PostLoginActions Count:</color> 0");
            return;
        }

        Debug.Log($"<color=maroon>{PostLoginActions[0] == null}</color>");
        Debug.Log("<color=Yellow>PostLoginActions Count:</color>" + PostLoginActions.Count);
        PostLoginActions[0].Invoke();
    }

    public Dictionary<string,object> GetUrlParameters(string url)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        string[] parts = url.Split(new char[] { '?', '#' });
        if (parts.Length < 2)
            return parameters;

        string[] pairs = parts[1].Split(new char[] { '&' });

        for(int i = 0; i < pairs.Length; i++)
        {
            string[] pair = pairs[i].Split(new char[] { '=' });
            Debug.Log("Found queryParam in url: " + pair[0] + " = " + pair[1]);
            parameters.Add(pair[0], pair[1]);
        }

        return parameters;
    }

    public override void Show()
    {
        ResetInputFields();
        base.Show();

    }

    void ResetInputFields()
    {
        emailField.text = passwordField.text = "";
    }

    void StartURLProcessing()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        string url = Application.absoluteURL;
#else
        string url = "https://dashboard.agileliteracy.com/?invitation=5ef9328f6484192c8e6d0094#openWindow=Login";
#endif

        Dictionary<string, object> parameters = GetUrlParameters(url);

        foreach (KeyValuePair<string, object> pair in parameters)
        {
            Debug.Log($"Query Param Found: {pair.Key}={pair.Value}");
            ProcessQueryParameter(pair.Key, pair.Value);
        }

        HandleNextLoginAction();
    }

    public void Login()
    {
        string emailError = "";
        string passwordError = "";

   /*     if (Validation.ValidateEmail(emailField.text, out emailError) && Validation.ValidatePassword(passwordField.text, out passwordError))
        {
            CanvasLoading.Instance.Show();
            APIManager.Authenticate(emailField.text, passwordField.text, (success) => 
            {
                if (success)
                {
                    //Debug.Log("Login Successful");
                    PlayerPrefs.SetString("UN", emailField.text);
                    PlayerPrefs.SetString("UP", passwordField.text);
                    PostLoginActions.Add(() => 
                    {
                        Debug.Log("CHECKING USER MEMBERSHIPCOUNT BEFORE FINISHING LOGIN");
                        if (User.current.memberships.Length == 1)
                        {
                            this.Close();
                            User.current.selectedMembership = User.current.memberships[0];
                            SceneChanger.ChangeScene("Dashboard");
                            CanvasLoading.Instance.Hide();
                        }
                        else if (User.current.memberships.Length > 1)
                        {
                            //Let the user pick a team before going to the dashboard screens
                            this.Close();
                            CanvasLoading.Instance.Hide();
                            pickTeamWindow.Show(() => {
                                SceneChanger.ChangeScene("Dashboard");
                            });
                        }
                        else
                        {
                            Debug.LogError("USER HAS NO TEAM MEMBERSHIPS BUT SHOULD ALWAYS HAVE AT LEAST ONE!!!!");
                        }

                        PopLoginActionQueue();
                    });

                    StartURLProcessing();

                    

                }
                else
                {
                    CanvasLoading.Instance.Hide();
                    PopupDialog.Instance.Show("An error occurred while trying to log you in. Please try again.");
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

    public void Register()
    {
        this.Close();
        window_Register.Show();

    }

    

    
}
