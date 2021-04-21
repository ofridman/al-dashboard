using GameBrewStudios;
//using GameBrewStudios.Networking;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_PickMember : UIWindow
{
 //   public static event System.Action<TeamMember> OnMemberSelected;

    [SerializeField]
    GameObject memberListEntryPrefab;

    [SerializeField]
    Transform listContainer;

    [SerializeField]
    Window_GroupEditor groupEditorWindow;

    public override void Show()
    {
        base.Show();
        PopulateMemberList();
        
    }

    public override void Close()
    {
        base.Close();
    }



    void PopulateMemberList()
    {
        CanvasLoading.Instance.Show();

        
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);

       /* APIManager.ListTeamMembers(User.current.selectedMembership.team._id, (members) => 
        {
            
            foreach (TeamMember member in members)
            {
                TeamMember memberObj = member;
                GameObject obj = Instantiate(memberListEntryPrefab, listContainer);
                TextMeshProUGUI label = obj.GetComponentInChildren<TextMeshProUGUI>();
                label.text = member.user.displayName;

                Button btn = obj.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => 
                {
                    CanvasLoading.Instance.Show();
                    this.Close();
                    groupEditorWindow.Close();

                    APIManager.AddGroupToMember(memberObj, TeamGroup.current, (updatedMember) =>  
                    {
                        groupEditorWindow.Show();
                        CanvasLoading.Instance.Hide();
                    });
                });
            }

            CanvasLoading.Instance.Hide();
        });*/
    }
}
