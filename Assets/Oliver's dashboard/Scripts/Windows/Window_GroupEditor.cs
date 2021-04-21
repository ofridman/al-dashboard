/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_GroupEditor : UIWindow
{
    [SerializeField]
    GameObject assignmentListEntryPrefab;

    [SerializeField]
    ScrollRect assignmentsScrollRect;

    [SerializeField]
    Window_AssignmentList assignmentListWindow;

    [SerializeField]
    TMP_InputField nameField;

    public static event Action OnGroupInfoUpdated;

    public override void Show()
    {
        PopulateGroupAssignmentsList();
        PopulateGroupMembersList();
        SetupListeners();

        base.Show();
    }


    void SetupListeners()
    {
        nameField.onEndEdit.RemoveAllListeners();
        nameField.onEndEdit.AddListener((text) => 
        {
        //    TeamGroup.current.name = text;
            UpdateGroup();
        });
    }

    void UpdateGroup()
    {
        CanvasLoading.Instance.Show();
        
       /* APIManager.UpdateGroup(TeamGroup.current, (group) =>
        {
            TeamGroup.current = group;

            OnGroupInfoUpdated?.Invoke();

            CanvasLoading.Instance.Hide();
        });*/
    }

    public override void Close()
    {
        base.Close();
        OnGroupInfoUpdated?.Invoke();
    }

    public void AddGroupAssignment()
    {
        
     /*   assignmentListWindow.Show((course) =>
        {
            if (course == null || TeamGroup.current.interactiveCourses.ToList().Any(x => x._id == course._id)) return;

            CanvasLoading.Instance.Show();
            APIManager.GroupAddAssignment(TeamGroup.current, course._id, (group) => 
            {
                TeamGroup.current = group;
                PopulateGroupAssignmentsList();
                
                CanvasLoading.Instance.Hide();
            });
        });*/
    }

    public void RemoveGroupAssignment(string courseId)
    {
        CanvasLoading.Instance.Show();

  /*      APIManager.GroupRemoveAssignment(TeamGroup.current, courseId, (updatedGroup) =>
        {
            TeamGroup.current = updatedGroup;
            
            PopulateGroupAssignmentsList();
            CanvasLoading.Instance.Hide();
        });*/

    }

    public Window_PickMember memberListWindow;

    public void AddGroupMember()
    {
    //    Window_PickMember.OnMemberSelected += this.Window_PickMember_OnMemberSelected;
        memberListWindow.Show();
    }

    /*
    public void RemoveGroupMember(TeamMember member)
    {
        CanvasLoading.Instance.Show();
        APIManager.RemoveGroupFromMember(member, TeamGroup.current, (updatedMember) => 
        {
            CanvasLoading.Instance.Hide();
        });
    }

    private void Window_PickMember_OnMemberSelected(TeamMember selectedMember)
    {
        Window_PickMember.OnMemberSelected -= this.Window_PickMember_OnMemberSelected;

        CanvasLoading.Instance.Show();
        APIManager.AddGroupToMember(selectedMember, TeamGroup.current, (updatedMember) => 
        {
            PopulateGroupMembersList();
            CanvasLoading.Instance.Hide();
            if(updatedMember != null)
            {
                Debug.Log("Successfully added group to member: " + JsonConvert.SerializeObject(updatedMember));
            }
        });
    }
    */

    [SerializeField]
    ScrollRect memberScrollRect;

    void PopulateGroupMembersList()
    {
        CanvasLoading.Instance.Show();
        foreach(Transform child in memberScrollRect.content)
        {
            Destroy(child.gameObject);
        }

     /*   APIManager.ListTeamMembers(User.current.selectedMembership.team._id, (members) => 
        {
            int index = 0;
            foreach(TeamMember member in members)
            {
                //Only list this member if they are part of this group
                if (member.groups.Any(x => x._id == TeamGroup.current._id))
                {
                    GameObject obj = Instantiate(assignmentListEntryPrefab, memberScrollRect.content);
                    Button btn = obj.GetComponent<Button>();
                    btn.interactable = false;
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() =>
                    {
                        Debug.Log("Index is " + index);
                    });

                    //TooltipTrigger.AddTooltip(obj, course._id + "\n" + course.name);


                    Button removeBtn = obj.transform.Find("Button - Remove").GetComponent<Button>();
                    removeBtn.onClick.RemoveAllListeners();
                    removeBtn.onClick.AddListener(() =>
                    {
                        RemoveGroupMember(member);
                    });

                    obj.GetComponentInChildren<TextMeshProUGUI>().text = member.user.displayName;
                    index++;
                }
            }
            CanvasLoading.Instance.Hide();
        });*/
    }

    void PopulateGroupAssignmentsList()
    {
        //nameField.SetTextWithoutNotify(TeamGroup.current.name);

        foreach (Transform child in assignmentsScrollRect.content)
            Destroy(child.gameObject);

        int i = 0;
     /*   foreach (InteractiveCourseAssignment course in TeamGroup.current.interactiveCourses)
        {
            int index = i; //Required to keep the listener in scope during execution

            //InteractiveCourseAssignment course = x;

            GameObject obj = Instantiate(assignmentListEntryPrefab, assignmentsScrollRect.content);
            Button btn = obj.GetComponent<Button>();
            btn.interactable = false;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => 
            {
                Debug.Log("Index is " + index);
            });

            //TooltipTrigger.AddTooltip(obj, course._id + "\n" + course.name);


            Button removeBtn = obj.transform.Find("Button - Remove").GetComponent<Button>();
            removeBtn.onClick.RemoveAllListeners();
            removeBtn.onClick.AddListener(() =>
            {
                Debug.Log("Removing Course: " + JsonConvert.SerializeObject(course));
                RemoveGroupAssignment(course._id);
            });

            //TooltipTrigger.AddTooltip(removeBtn.gameObject, "Index = " + index);

            obj.GetComponentInChildren<TextMeshProUGUI>().text = course.name;
            
            
            i++;
        }*/
    }
}
