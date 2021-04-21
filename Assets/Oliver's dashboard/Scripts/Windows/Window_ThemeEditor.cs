/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_ThemeEditor : UIWindow
{
    public static event System.Action OnThemeUpdated;

    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    TMP_InputField themeNameField;

    [SerializeField]
    TMP_Dropdown levelSelectStyleDropdown, lessonAreaStyleDropdown, backgroundDropdown;

    [SerializeField]
    RawImage laptopPreviewBG, laptopPreviewOverlay, lessonAreaPreview;

    [SerializeField]
    Texture[] laptopOverlays, lessonAreaScreenshots;

    [SerializeField]
    Texture laptopDefaultBG;

    //TeamFile[] teamImages;

    public override void Show()
    {
        laptopPreviewBG.texture = laptopDefaultBG;

  /*      APIManager.ListFiles(User.current.selectedMembership.team._id, (files) => 
        {


            themeNameField.SetTextWithoutNotify(InteractiveTheme.current.name);
            
            levelSelectStyleDropdown.SetValueWithoutNotify(GetLevelStyleIndex(InteractiveTheme.current.levelSelectStyle));
            laptopPreviewOverlay.texture = laptopOverlays[levelSelectStyleDropdown.value];

            lessonAreaStyleDropdown.SetValueWithoutNotify(GetLessonAreaIndex(InteractiveTheme.current.lessonAreaStyle));

            lessonAreaPreview.texture = lessonAreaScreenshots[lessonAreaStyleDropdown.value];

            List<string> options = new List<string>
            {
                "Default"
            };

            teamImages = files.Where(x => x.isImage).ToArray();

            int i = 1;
            int selectedIndex = 0;
            foreach (TeamFile file in teamImages)
            {
                options.Add(file.filename);
                
                if (InteractiveTheme.current.bgURL == file.FileURL())
                    selectedIndex = i;

                i++;
            }



            backgroundDropdown.ClearOptions();
            backgroundDropdown.AddOptions(options);

            backgroundDropdown.value = selectedIndex ;

            SetUpEventListeners();
        });*/
        

        base.Show();
    }

    public override void Close()
    {
        
        base.Close();
    }

    void SetUpEventListeners()
    {
        themeNameField.onEndEdit.RemoveAllListeners();
        themeNameField.onEndEdit.AddListener((val) => 
        {
            //InteractiveTheme.current.name = val;
            
            UpdateThemeOnServer();
        });

        levelSelectStyleDropdown.onValueChanged.RemoveAllListeners();
        levelSelectStyleDropdown.onValueChanged.AddListener((index) => 
        {
           // InteractiveTheme.current.levelSelectStyle = GetLevelStyleKey(index);
            
            laptopPreviewOverlay.texture = laptopOverlays[levelSelectStyleDropdown.value];

            UpdateThemeOnServer();
        });


        lessonAreaStyleDropdown.onValueChanged.RemoveAllListeners();
        lessonAreaStyleDropdown.onValueChanged.AddListener((index) =>
        {
           // InteractiveTheme.current.lessonAreaStyle = GetLessonAreaKey(index);

            lessonAreaPreview.texture = lessonAreaScreenshots[lessonAreaStyleDropdown.value];

            UpdateThemeOnServer();
        });
        backgroundDropdown.onValueChanged.RemoveAllListeners();
        backgroundDropdown.onValueChanged.AddListener((index) =>
        {
            SetThemeBackground(index);
            
        });
    }

    void SetThemeBackground(int index)
    {
   /*     bool isDefault = index == 0 || teamImages == null || teamImages.Length == 0;
        InteractiveTheme.current.bgURL = isDefault ? "" : teamImages[index - 1].FileURL();

        laptopPreviewBG.texture = laptopDefaultBG;
        if (!isDefault)
        {
            CanvasLoading.Instance.Show();
            APIManager.DownloadTexture(teamImages[index - 1].FileURL(), (texture) =>
            {
                if (texture != null)
                {
                    laptopPreviewBG.texture = texture;
                }
                else
                {
                    laptopPreviewBG.texture = laptopDefaultBG;
                    backgroundDropdown.value = 0;
                    PopupDialog.Instance.Show("An error occurred while fetching a preview of the selected background image. Switching back to default laptop background. Please try again.");
                }
                CanvasLoading.Instance.Hide();
            });
        }*/

        UpdateThemeOnServer();
    }

    string GetLevelStyleKey(int index)
    {
        switch (index)
        {
            case 1: return "stars";
            case 0: 
            default: return "default";
        }
    }

    int GetLevelStyleIndex(string levelSelectStyle)
    {
        switch(levelSelectStyle)
        {
            case "stars": return 1;
            case "default": return 0;
            default: return 0;
        }
    }

    string GetLessonAreaKey(int index)
    {
        switch (index)
        {
            case 1: return "stars";
            case 0:
            default: return "default";
        }
    }

    

    int GetLessonAreaIndex(string lessonAreaStyle)
    {
        switch (lessonAreaStyle)
        {
            case "stars": return 1;
            case "default": return 0;
            default: return 0;
        }
    }

    void UpdateThemeOnServer(System.Action OnFinished = null)
    {
        CanvasLoading.Instance.Show();
   /*     APIManager.UpdateTheme(InteractiveTheme.current, (theme) => 
        {
            OnFinished?.Invoke();
            OnThemeUpdated?.Invoke();
            CanvasLoading.Instance.Hide();
        });*/

    }

}
