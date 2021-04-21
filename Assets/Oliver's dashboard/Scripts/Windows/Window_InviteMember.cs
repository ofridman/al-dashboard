using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using GameBrewStudios.Networking;

public class Window_InviteMember : UIWindow
{
    [SerializeField]
    TMP_InputField emailField;

    [SerializeField]
    Screen_Members memberScreen;

    public override void Show()
    {
        emailField.SetTextWithoutNotify("");
        base.Show();
    }

    public void SubmitInvite()
    {
        CanvasLoading.Instance.Show(true);
        string errorMessage;

    /*    if (Validation.ValidateEmail(emailField.text, out errorMessage))
        {

            APIManager.InviteMember(emailField.text, (invitationResponse) =>
            {
                if (invitationResponse != null && invitationResponse.success == true)
                {
                    this.Close();
                    Debug.Log("Successfully invited " + invitationResponse.invitation);
                    if (invitationResponse.isNew == true)
                    {
                        PopupDialog.Instance.Show("You have invited " + emailField.text + " to join your team. When they have accepted your invitation you will be able to assign them to a course and/or a group.");
                    }
                    else
                    {
                        PopupDialog.Instance.Show($"{emailField.text} member status has been reinstated.");
                    }

                    CanvasLoading.Instance.Hide();
                    memberScreen.Show();
                }
                else
                {
                    this.Close();
                    PopupDialog.Instance.Show("A problem occurred while sending your invitation. Please try again.");
                    CanvasLoading.Instance.Hide();
                }
            });
        }
        else
        {
            CanvasLoading.Instance.Hide();
            PopupDialog.Instance.Show(errorMessage);
        }*/


    }
}
