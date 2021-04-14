using DG.Tweening;
using GameBrewStudios;
//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Badges : MenuScreen
{
    [SerializeField]
    ScrollRect badgeListScrollRect;

    [SerializeField]
    GameObject badgeEntryPrefab;

    [SerializeField]
    Window_BadgeEditor BadgeEditorWindow;

    [SerializeField]
    TextMeshProUGUI titleLabel;

   // private InteractiveBadge[] badges;

    public override void Show()
    {


       // if (User.current == null) return;
        base.Show();

        CanvasLoading.Instance?.Show();

        /* if (User.current.selectedMembership == null)
         {
             Debug.LogError("No team selected");
             return;
         }

         APIManager.ListBadges(User.current.selectedMembership.team._id, (badges) =>
         {
             this.badges = badges;
             Debug.Log("Found " + badges.Length + " Badges for selected team");
             PopulateBadgeList();

             UpdateTitle();

             CanvasLoading.Instance.Hide();
         });*/

        Window_BadgeEditor.OnBadgeInfoUpdated += this.OnBadgeInfoUpdated;
    }

    public override void Hide()
    {
        Window_BadgeEditor.OnBadgeInfoUpdated -= this.OnBadgeInfoUpdated;
        base.Hide();
    }


    private void OnBadgeInfoUpdated()
    {
        //Not calling CanvasLoading.Instance.Show() because this will run while a window is still open on top so we can treat it like a background process.
     /*   APIManager.ListBadges(User.current.selectedMembership.team._id, (badges) =>
        {
            this.badges = badges;
            PopulateBadgeList();
        });*/
    }

    public void PopulateBadgeList()
    {
        foreach (Transform child in badgeListScrollRect.content)
            Destroy(child.gameObject);
        int i = 1;
       
        /*foreach (InteractiveBadge badge in this.badges)
        {
            GameObject obj = Instantiate(badgeEntryPrefab, badgeListScrollRect.content);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString("n0") + ".) " + badge.name;

            Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.onClick.AddListener(() => 
            {
                DeleteBadgePrompt(badge);
            });

            Button BadgeButton = obj.GetComponent<Button>();
            BadgeButton.onClick.RemoveAllListeners();
            BadgeButton.onClick.AddListener(() =>
            {
                InteractiveBadge.current = badge;
                BadgeEditorWindow.Show();
            });
            i++;
        }*/
    }

   /* public void DeleteBadgePrompt(InteractiveBadge badge)
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            DeleteBadge(badge);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                DeleteBadge(badge);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete badge?", "This badge will be deleted permanently. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
    }

    void DeleteBadge(InteractiveBadge badge)
    {
        CanvasLoading.Instance.Show();

        APIManager.DeleteBadge(badge._id, (success) =>
        {
            if (!success)
            {
                Debug.LogError("An error occurred on the server while deleting the badge.");
            }

            this.OnBadgeInfoUpdated();
            CanvasLoading.Instance.Hide();
        });
    }


    public void AddBadge()
    {
        if (this.badges.Length < 50)
        {

            CanvasLoading.Instance.Show();

            InteractiveBadge newBadge = new InteractiveBadge();
            newBadge.name = "New Badge";
            newBadge.description = "";
            newBadge.icon = "BadgeIcon_1";
            newBadge.iconType = "Standard";

            APIManager.CreateBadge(newBadge, (badge) =>
            {
                if (badge != null)
                {
                    APIManager.ListBadges(User.current.selectedMembership.team._id, (badges) =>
                    {
                        this.badges = badges;
                        PopulateBadgeList();
                        UpdateTitle();

                        RectTransform lastChild = badgeListScrollRect.content.GetChild(badgeListScrollRect.content.childCount - 1).GetComponent<RectTransform>();
                        badgeListScrollRect.ScrollToChild(lastChild);
                        lastChild.DOPunchScale(new Vector3(1, 1, 1), 1f, 1, 0);
                        CanvasLoading.Instance.Hide();
                    });
                }
                else
                {
                    PopupDialog.Instance.Show("There was a problem creating your new badge. Please try again.");
                    CanvasLoading.Instance.Hide();
                }
            });
        }
        else
        {
            PopupDialog.Instance.Show("You have reached your limit on the number of badges you can create.");
        }
    }

    void UpdateTitle()
    {
        titleLabel.text = string.Format("Badges ({0}/{1})", this.badges.Length, 50);
    }*/
}
