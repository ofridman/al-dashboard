using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
//using GameBrewStudios.Networking;
using GameBrewStudios;
using System.Text.RegularExpressions;

public class Screen_Members : MenuScreen
{
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    RectTransform rt;


 /*   private TeamMember selectedMember;

    [SerializeField]
    TeamMember[] memberList;*/

    [SerializeField]
    TextMeshProUGUI titleLabel;

    [SerializeField]
    private GameObject memberEntryPrefab;

    [SerializeField]
    private ScrollRect memberListScrollRect;

    //Selected Member Panel objects
    [Header("Selected Member")]
    [SerializeField]
    private TextMeshProUGUI memberNameLabel;

    [SerializeField]
    RawImage userProfilePicture;

    [SerializeField]
    Dropdown memberRank;

    [SerializeField]
    ScrollRect assignmentScrollRect, groupListScrollRect;

    [SerializeField]
    GameObject assignmentListEntryPrefab;

    [SerializeField]
    Window_InviteMember inviteMemberWindow;

    [SerializeField]
    Sprite invitedSprite;

    public override void Show()
    {
        Debug.Log("Running show on Members screen");

        if (rt == null) rt = GetComponent<RectTransform>();


        Vector2 om = rt.offsetMax;
        //selectedMember = null;
        //reset it back to 0 before animating so it will always animate sliding onto the screen even if it was already there.
        om.x = 0f;
        rt.offsetMax = om;

        PopulateMemberList();

        base.Show();
    }

    public override void Hide()
    {

        base.Hide();
    }

    public void PopulateMemberList()
    {
        foreach (Transform child in memberListScrollRect.content)
            Destroy(child.gameObject);

        memberListScrollRect.content.anchoredPosition = new Vector2(0f, 0f);

        CanvasLoading.Instance.Show();
     /*   APIManager.ListTeamMembers(User.current.selectedMembership.team._id, (members) =>
        {
            if(members == null)
            {
                PopupDialog.Instance.Show("Something went wrong while trying to retrieve Team Members. Please try again.");
                CanvasLoading.Instance.Hide();
                return;
            }
            
            memberList = members;
            int i = 0;
            foreach (TeamMember member in members)
            {
                Debug.Log("Creating member list entry for member ID: " + member._id);
                if (member.active == false) continue;
                if (member.hidden == true) continue;


                GameObject obj = Instantiate(memberEntryPrefab, memberListScrollRect.content);
                TextMeshProUGUI name = obj.transform.Find("Text - Name").GetComponent<TextMeshProUGUI>();
                name.text = $"{member.user.displayName} ({member.user.email})";

                Image rankIcon = obj.transform.Find("Rank/Icon").GetComponent<Image>();
                rankIcon.sprite = member.GetRoleSprite();

                Button btn = obj.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    OnTeamMemberSelected(member);
                });

                Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                deleteBtn.onClick.RemoveAllListeners();
                deleteBtn.onClick.AddListener(() => 
                {
                    PopupDialog.PopupButton[] buttons = new PopupDialog.PopupButton[]
                    {
                        new PopupDialog.PopupButton()
                        {
                            text = "Yes, Remove them",
                            onClicked = () =>
                            {
                                CanvasLoading.Instance.Show();
                                APIManager.RemoveMember(member, (response) =>
                                {
                                    CanvasLoading.Instance.Hide();
                                    if(response != null && response.ContainsKey("success") && (bool)response["success"] == true)
                                    {
                                        PopupDialog.Instance.Show("Member removed successfully.");
                                        this.Hide();
                                        this.Show();
                                    }
                                    else
                                    {
                                        PopupDialog.Instance.Show("Something went wrong. Remove member failed. Please try again.");
                                    }
                                });
                            },
                            buttonColor = PopupDialog.PopupButtonColor.Red
                        },
                        new PopupDialog.PopupButton()
                        {
                            text = "Cancel",
                            onClicked = () => { },
                            buttonColor = PopupDialog.PopupButtonColor.Plain
                        }
                    };
                    PopupDialog.Instance.Show("Remove Member", "Really remove this member from your team? They will no longer have access to your team's courses and other content on the Interactive platform.", buttons);
                });

                i++;
            }

            titleLabel.text = "Members (" + i.ToString("n0") + "/10)";

            
            APIManager.ListInvitations(User.current.selectedMembership.team._id, (invitations) => 
            {
                foreach (TeamInvitation invitation in invitations)
                {
                    
                    GameObject obj = Instantiate(memberEntryPrefab, memberListScrollRect.content);
                    TextMeshProUGUI name = obj.transform.Find("Text - Name").GetComponent<TextMeshProUGUI>();
                    name.text = invitation.email + " (Invited)";

                    Image rankIcon = obj.transform.Find("Rank/Icon").GetComponent<Image>();
                    rankIcon.sprite = invitedSprite;

                    Button btn = obj.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.targetGraphic = name;
                    btn.interactable = false;


                    Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                    deleteBtn.onClick.RemoveAllListeners();
                    deleteBtn.onClick.AddListener(() =>
                    {
                        PopupDialog.PopupButton[] buttons = new PopupDialog.PopupButton[]
                        {
                        new PopupDialog.PopupButton()
                        {
                            text = "Yes, Delete it",
                            onClicked = () =>
                            {
                                CanvasLoading.Instance.Show();
                                APIManager.DeleteInvitation(invitation._id, (response) =>
                                {
                                    CanvasLoading.Instance.Hide();
                                    if(response != null && response.ContainsKey("success") && (bool)response["success"] == true)
                                    {
                                        PopupDialog.Instance.Show("Invitation removed successfully.");
                                        this.Hide();
                                        this.Show();
                                    }
                                    else
                                    {
                                        PopupDialog.Instance.Show("Something went wrong. Delete invitation failed. Please try again.");
                                    }
                                });
                            },
                            buttonColor = PopupDialog.PopupButtonColor.Red
                        },
                        new PopupDialog.PopupButton()
                        {
                            text = "Cancel",
                            onClicked = () => { },
                            buttonColor = PopupDialog.PopupButtonColor.Plain
                        }
                        };
                        PopupDialog.Instance.Show("Cancel Invitation?", "Really cancel your invitation to this user?", buttons);
                    });

                }
                CanvasLoading.Instance.Hide();
            });

            
        });
     */
    }


   /* public void OnTeamMemberSelected(TeamMember member)
    {
        selectedMember = member;
        PopulateSelectedMemberPanel();
        PopulateSelectedMemberAssignmentList();
        SlideInSelectedMemberPanel();
    }*/

    public void PopulateSelectedMemberPanel()
    {
       /* memberNameLabel.text = selectedMember.user.displayName;
        memberRank.SetValueWithoutNotify(selectedMember.role == "admin" ? 2 : (selectedMember.role == "author" ? 1 : 0));

        memberRank.onValueChanged.RemoveAllListeners();
        memberRank.onValueChanged.AddListener((val) =>
        {
            Debug.Log("MEMBER " + selectedMember._id + " RANK CHANGED TO: " + val);

            CanvasLoading.Instance.Show();
            APIManager.UpdateMemberRank(selectedMember._id, val, (response) => 
            {
                CanvasLoading.Instance.Hide();

                if (response.ContainsKey("success") && (bool)response["success"] == true)
                {
                    PopulateMemberList();
                    PopupDialog.Instance.Show((string)response["message"]);
                }
                else
                {
                    if(response.ContainsKey("error"))
                    {
                        PopupDialog.Instance.Show((string)((Dictionary<string,object>)response["error"])["message"]);

                    }
                    else
                    {
                        PopupDialog.Instance.Show("Oops! Something went wrong!", "If the problem persists, please contact support@agileliteracy.com and provide details about the issue you are having, and what action you are trying to perform when you encounter this issue.");
                    }
                }
            });
        });*/

    }

    public void PopulateSelectedMemberAssignmentList()
    {
        foreach (Transform child in assignmentScrollRect.content)
            Destroy(child.gameObject);

        foreach (Transform child in groupListScrollRect.content)
            Destroy(child.gameObject);

   /*     foreach (InteractiveCourseAssignment assignment in selectedMember.interactiveCourses)
        {
            if (assignment == null) continue;

            GameObject obj = Instantiate(assignmentListEntryPrefab, assignmentScrollRect.content);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = assignment.name;

            Button deleteButton = obj.transform.Find("Button - Remove").GetComponent<Button>();
            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(() => 
            {
                CanvasLoading.Instance.Show();
                APIManager.MemberRemoveAssignment(selectedMember, assignment._id, (courses) => 
                {
                    CanvasLoading.Instance.Hide();
                    OnTeamMemberSelected(selectedMember);
                });
            });

        }*/


        CanvasLoading.Instance.Show();

     /*   APIManager.ListGroups(selectedMember.team._id, (groups) => 
        {

            for(int i = 0; i < groups.Length; i++)
            {
                
                TeamGroup grp = groups[i];

                for(int j = 0; j < selectedMember.groups.Length; j++)
                {
                    if(selectedMember.groups[j]._id == groups[i]._id)
                    {
                        GameObject obj = Instantiate(assignmentListEntryPrefab, groupListScrollRect.content);
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = grp.name;

                        Button deleteButton = obj.transform.Find("Button - Remove").GetComponent<Button>();
                        deleteButton.onClick.RemoveAllListeners();
                        deleteButton.onClick.AddListener(() =>
                        {
                            CanvasLoading.Instance.Show();
                            APIManager.RemoveGroupFromMember(selectedMember, grp, (courses) =>
                            {
                                CanvasLoading.Instance.Hide();
                                OnTeamMemberSelected(selectedMember);
                            });
                        });
                    }
                }
            }
            CanvasLoading.Instance.Hide();

        });*/

        
        
    }

    public void SlideInSelectedMemberPanel()
    {

        Vector2 om = rt.offsetMax;

        //reset it back to 0 before animating so it will always animate sliding onto the screen even if it was already there.
        om.x = 0f;
        rt.offsetMax = om;


        // Tween a Vector3 called myVector to 3,4,8 in 1 second
        DOTween.To(() => om, xt => om = xt, new Vector2(-380f, om.y), .25f).OnUpdate(() =>
        {
            rt.offsetMax = om;
        });
    }

    public void SlideOutSelectedMemberPanel()
    {
     //   selectedMember = null;

        Vector2 offsetMax = rt.offsetMax;

        //reset it back to -380 before animating so it will always animate sliding off of the screen even if it was already there.
        offsetMax.x = -380f;
        rt.offsetMax = offsetMax;


        DOTween.To(() => offsetMax, xt => offsetMax = xt, new Vector2(0f, offsetMax.y), .25f).OnUpdate(() =>
        {
            rt.offsetMax = offsetMax;
        });
    }

    [SerializeField]
    Window_AssignmentList assignmentPicker;

    public void AddAssignement()
    {
    /*    assignmentPicker.Show((selectedCourse) => 
        {
            CanvasLoading.Instance.Show();
            APIManager.MemberAddAssignment(selectedMember, selectedCourse._id, (memberCourses) => 
            {
                string selMemberId = selectedMember._id;
                PopulateMemberList();
                for(int i = 0; i < memberList.Length; i++)
                {
                    if(memberList[i]._id == selMemberId)
                    {
                        OnTeamMemberSelected(memberList[i]);
                        break;
                    }
                }
                CanvasLoading.Instance.Hide();

            });
        });*/
    }

    public void AddMember()
    {
        inviteMemberWindow.Show();
    }
}
