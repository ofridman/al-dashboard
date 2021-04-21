/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Groups : MenuScreen
{
    [SerializeField]
    ScrollRect groupListScrollRect;

    [SerializeField]
    GameObject groupEntryPrefab;

    [SerializeField]
    Window_GroupEditor GroupEditorWindow;

    [SerializeField]
    TextMeshProUGUI titleLabel;

 //   private TeamGroup[] groups;

    public override void Show()
    {


      //  if (User.current == null) return;
        base.Show();

        CanvasLoading.Instance?.Show();

      /*  if (User.current.selectedMembership == null)
        {
            Debug.LogError("No team selected");
            return;
        }*/

      /*  APIManager.ListGroups(User.current.selectedMembership.team._id, (groups) =>
        {
            this.groups = groups;
            Debug.Log("Found " + groups.Length + " Groups for selected team");
            PopulateGroupList();

            CanvasLoading.Instance.Hide();
        });*/

        Window_GroupEditor.OnGroupInfoUpdated += this.OnGroupInfoUpdated;
    }

    public override void Hide()
    {
        Window_GroupEditor.OnGroupInfoUpdated -= this.OnGroupInfoUpdated;
        base.Hide();
    }


    private void OnGroupInfoUpdated()
    {
        //Not calling CanvasLoading.Instance.Show() because this will run while a window is still open on top so we can treat it like a background process.
    /*    APIManager.ListGroups(User.current.selectedMembership.team._id, (groups) =>
        {
            this.groups = groups;
            PopulateGroupList();
        });*/
    }

    public void PopulateGroupList()
    {
        foreach (Transform child in groupListScrollRect.content)
            Destroy(child.gameObject);

        /*   foreach (TeamGroup group in this.groups)
           {
               GameObject obj = Instantiate(groupEntryPrefab, groupListScrollRect.content);
               obj.GetComponentInChildren<TextMeshProUGUI>().text = group.name;

               Button btn = obj.GetComponent<Button>();
               btn.onClick.RemoveAllListeners();
               btn.onClick.AddListener(() =>
               {
                   TeamGroup.current = group;
                   GroupEditorWindow.Show();
               });

               Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
               deleteBtn.onClick.RemoveAllListeners();
               deleteBtn.onClick.AddListener(() =>
               {

                   DeleteGroupPrompt(group);
               });
           }

           titleLabel.text = string.Format("Groups{0}", this.groups.Length > 0 ? " (" + this.groups.Length + ")" : "");*/
    }


 /*   public void DeleteGroupPrompt(TeamGroup group)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeleteGroup(group);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                DeleteGroup(group);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete group?", "This group will be deleted permanently. Any users assigned to this group will no longer have access to the courses assigned to it. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
    }*/

/*    void DeleteGroup(TeamGroup group)
    {
        CanvasLoading.Instance.Show();
        APIManager.DeleteGroup(group, (success) =>
        {
            if (!success)
            {
                Debug.LogError("An error occurred on the server while deleting the quiz.");
            }


            this.OnGroupInfoUpdated();
            CanvasLoading.Instance.Hide();
        });
    }*/

   /* public void AddGroup()
    {
        CanvasLoading.Instance.Show();

        TeamGroup newGroup = new TeamGroup() {
            name = "New Group",
            interactiveCourses = new InteractiveCourseAssignment[] { },
        };
        

        APIManager.CreateGroup(newGroup, (group) =>
        {
            if (group != null)
            {
                APIManager.ListGroups(User.current.selectedMembership.team._id, (groups) =>
                {
                    this.groups = groups;
                    PopulateGroupList();
                    CanvasLoading.Instance.Hide();
                });
            }
            else
            {
                PopupDialog.Instance.Show("There was a problem creating your new group. Please try again.");
                CanvasLoading.Instance.Hide();
            }
        });
    }*/
}
