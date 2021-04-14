using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using GameBrewStudios.Networking;
using GameBrewStudios;
using System.Linq;

public class Window_BadgeEditor : UIWindow
{
    public static event System.Action OnBadgeInfoUpdated;
    [Header("Child Variables")]
    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    TMP_Dropdown iconTypeDropdown; //Standard, Web

    [SerializeField] TMP_InputField badgeNameField, badgeDescriptionField;

    
    //iconDropdown: if iconType == Standard, populate this with a list of built-in icons, else show user upload image files as choices
    [SerializeField] TMP_Dropdown iconDropdown;

    [SerializeField]
    Image iconPreview;

   // TeamFile[] teamImages;

    [SerializeField]
    Sprite[] standardIcons;

    [SerializeField]
    Sprite defaultIcon;


    public override void Show()
    {
        CanvasLoading.Instance.Show();

   /*     badgeNameField.SetTextWithoutNotify(InteractiveBadge.current.name);
        badgeDescriptionField.SetTextWithoutNotify(InteractiveBadge.current.description);*/

        SetEventListeners();
        PopulateIconTypeDropdown();
        PopulateIconDropdown();

        UpdateIconPreview();

        base.Show();
        CanvasLoading.Instance.Hide();
    }

    public override void Close()
    {
        base.Close();

        badgeNameField.onEndEdit.RemoveAllListeners();
        badgeDescriptionField.onEndEdit.RemoveAllListeners();
        iconTypeDropdown.onValueChanged.RemoveAllListeners();

        OnBadgeInfoUpdated?.Invoke();
    }


    private void SetEventListeners()
    {
        badgeNameField.onEndEdit.RemoveAllListeners();
        badgeDescriptionField.onEndEdit.RemoveAllListeners();
        iconTypeDropdown.onValueChanged.RemoveAllListeners();

        badgeNameField.onEndEdit.AddListener((val) =>
        {
         //   InteractiveBadge.current.name = val;

            UpdateBadgeOnServer();
        });

        badgeDescriptionField.onEndEdit.AddListener((val) =>
        {
      //      InteractiveBadge.current.description = val;

            UpdateBadgeOnServer();
        });

        iconTypeDropdown.onValueChanged.AddListener((index) =>
        {
            string newValue = index == 0 ? "Standard" : "Web";
        //    InteractiveBadge.current.iconType = newValue;

            
            //Update the selectable options in the icon dropdown if the type changed
            

            UpdateBadgeOnServer(() => 
            {
                PopulateIconDropdown();
            });

        });

        iconDropdown.onValueChanged.AddListener((index) => 
        {
            //Set icon to standard sprite name if standard, or file url if web
       //     InteractiveBadge.current.icon = iconTypeDropdown.value == 0 ? standardIcons[index].name : teamImages[index].FileURL();

            //Update the sprite preview
            UpdateIconPreview();

            UpdateBadgeOnServer();
        });
    }

    [SerializeField]
    Button clickToPreviewIcon;

    void UpdateIconPreview()
    {
#if UNITY_WEBGL
        if(InteractiveBadge.current.GetIconType() == BadgeIconType.Standard)
        {
            iconPreview.gameObject.SetActive(true);
            clickToPreviewIcon.gameObject.SetActive(false);
            InteractiveBadge.current.GetSprite((sprite) =>
            {
                if (sprite != null)
                    iconPreview.sprite = sprite;
                else
                    iconPreview.sprite = defaultIcon;

            });
        }
        else
        {
            iconPreview.gameObject.SetActive(false);
            clickToPreviewIcon.gameObject.SetActive(true);
            clickToPreviewIcon.onClick.RemoveAllListeners();
            clickToPreviewIcon.onClick.AddListener(() => 
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                    Application.ExternalEval("window.open(\"" + InteractiveBadge.current.icon + "\")");
#else
                Application.OpenURL(InteractiveBadge.current.icon);
#endif

            });
        }
#else
     /*   InteractiveBadge.current.GetSprite((sprite) =>
        {
            if (sprite != null)
                iconPreview.sprite = sprite;
            else
                iconPreview.sprite = defaultIcon;
        });*/
#endif
    }

    void PopulateIconTypeDropdown()
    {
        iconTypeDropdown.ClearOptions();
        iconTypeDropdown.AddOptions(new List<string>() { "Standard", "Custom" });
    //    iconTypeDropdown.SetValueWithoutNotify(InteractiveBadge.current.GetIconType() == BadgeIconType.Standard ? 0 : 1);
    }

    void PopulateIconDropdown()
    {
        CanvasLoading.Instance.Show();

        iconDropdown.interactable = false;

        
        List<string> options = new List<string>();
        
        if(iconTypeDropdown.value == 0)
        {
            //List all the built in badge icon textures

            int iconDropdownValue = 0;
            int i = 0;
            foreach (Sprite sprite in standardIcons)
            {
                options.Add(sprite.name);

                //See if the badge instance icon string was already set to a standard icon
          /*      if (InteractiveBadge.current.icon == sprite.name)
                {
                    iconDropdownValue = i;
                }*/

                i++;
            }

            iconDropdown.ClearOptions();
            iconDropdown.AddOptions(options);
            iconDropdown.interactable = true;
            
            //Only force the icon dropdown value to update if it actually needs to change
            
            iconDropdown.SetValueWithoutNotify(iconDropdownValue);

            CanvasLoading.Instance.Hide();
        }
        else if (iconTypeDropdown.value == 1)
        {
            // Standard/Built-in

        /*    teamImages = new TeamFile[] { }; //Clear the loaded team images array
            
            //Call the server and get the full list of Files the team has uploaded, then filter it to get only the images
            APIManager.ListFiles(User.current.selectedMembership.team._id, (files) => 
            {
                if(files != null && files.Length > 0 && files.Any(x => x.isImage))
                {
                    teamImages = files.Where(x => x.isImage).ToArray();

                    int i = 0;
                    int iconDropdownValue = 0;
                    foreach(TeamFile file in teamImages)
                    {
                        options.Add(file.filename);

                        if (InteractiveBadge.current.icon == file.FileURL())
                            iconDropdownValue = i;

                        i++;
                    }

                    iconDropdown.ClearOptions();
                    iconDropdown.AddOptions(options);
                    iconDropdown.interactable = true;

                    iconDropdown.SetValueWithoutNotify(iconDropdownValue);
                }
                else
                {
                    
                    iconTypeDropdown.value = 0; //Force the type to change back to standard if the team has no files/images to pick
                    PopupDialog.Instance.Show("Before you can assign custom images for Badge Icons, you must first upload images in the Files section.");
                }
        
                
                
                CanvasLoading.Instance.Hide();

            });*/

        }


    }

    void UpdateBadgeOnServer(System.Action OnFinished = null)
    {
        CanvasLoading.Instance.Show();
        /*        APIManager.UpdateBadge(InteractiveBadge.current, (badge) =>
                {
                    if (badge != null)
                    {
                        Debug.Log(badge.icon);
                        OnBadgeInfoUpdated?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("UpdateBadge response returned null");
                    }

                    OnFinished?.Invoke();
                    CanvasLoading.Instance.Hide();
                });
        */
    }

}
