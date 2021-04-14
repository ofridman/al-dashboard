using DG.Tweening;
//using GameBrewStudios;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Window_PickTeam : UIWindow
{
    public GameObject teamListEntryPrefab;

    public ScrollRect teamListScrollRect;

    private void Awake()
    {
        this.Close();
    }

    public void PopulateTeamList()
    {
        foreach (Transform child in teamListScrollRect.content)
            Destroy(child.gameObject);

        int permittedAccounts = 0;
 /*       TeamMember firstPermittedMembership = null;
        foreach (TeamMember membership in User.current.memberships)
        {
            GameObject obj = Instantiate(teamListEntryPrefab, teamListScrollRect.content);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            TextMeshProUGUI teamNameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
            //Debug.Log(membership);
            //Debug.Log(membership.team);
            //Debug.Log(membership.team.name);
            string teamName = membership.team.name;
            string role = membership.isOwner ? "Owner" : new CultureInfo("en-US", false).TextInfo.ToTitleCase(membership.role == "player" ? "Member" : membership.role);
            teamNameLabel.text = string.Format("{0} ({1})", teamName, role);

            TeamMember member = membership;

            if (membership.role.ToLower() == "player" && !membership.isOwner)
            {
                btn.interactable = false;
                teamNameLabel.color = Color.red;
            }
            else
            {
                permittedAccounts++;
                
                if (firstPermittedMembership == null) 
                    firstPermittedMembership = membership;

                btn.onClick.AddListener(() =>
                {
                    User.current.selectedMembership = member;
                    if (this.OnTeamSelected != null)
                    {
                        this.OnTeamSelected.Invoke();
                    }

                    this.Close();
                });
            }
        }

        if(permittedAccounts == 1 && firstPermittedMembership != null)
        {
            User.current.selectedMembership = firstPermittedMembership;
            this.OnTeamSelected.Invoke();
            this.Close();
            
        }*/
    }

    System.Action OnTeamSelected = null;

    public void Show(System.Action OnTeamSelected)
    {
        this.OnTeamSelected = OnTeamSelected;
        Show(); 
    }

    public override void Show()
    {
        PopulateTeamList();

     /*   if(User.current.selectedMembership == null)
            base.Show();*/
    }

    public override void Close()
    {
        base.Close();
    }
}
