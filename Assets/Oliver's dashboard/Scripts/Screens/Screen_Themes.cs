/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Themes : MenuScreen
{
    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    GameObject listEntryPrefab;

    [SerializeField]
    Window_ThemeEditor themeEditorWindow;

    public override void Show()
    {
        PopulateThemeList();

        Window_ThemeEditor.OnThemeUpdated += this.OnThemeUpdated;

        base.Show();
    }

    private void OnThemeUpdated()
    {
        PopulateThemeList();
    }

    public override void Hide()
    {
        Window_ThemeEditor.OnThemeUpdated -= this.OnThemeUpdated;
        base.Hide();
    }

    bool populatingThemeList = false;

    void PopulateThemeList()
    {
        if (populatingThemeList) return;


        populatingThemeList = true;


        CanvasLoading.Instance.Show();

     /*   APIManager.ListThemes(User.current.selectedMembership.team._id, (themes) => 
        {
            if(themes != null)
            {
                if(themes.Length == 0)
                {
                    PopupDialog.Instance.Show("Creating Custom Themes", "You can create new Themes to change look and feel of Interactive, giving your learners a customized experience. Click on the \"+\" symbol in the top right corner to get started.");
                }

                foreach (Transform child in scrollRect.content)
                    Destroy(child.gameObject);

                foreach (InteractiveTheme theme in themes)
                {
                    GameObject obj = Instantiate(listEntryPrefab, scrollRect.content);
                    
                    Button btn = obj.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => 
                    {
                        InteractiveTheme.current = theme;
                        themeEditorWindow.Show();
                    });

                    TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
                    nameLabel.text = theme.name;


                    Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                    deleteBtn.onClick.RemoveAllListeners();
                    deleteBtn.onClick.AddListener(() =>
                    {

                        DeleteThemePrompt(theme);
                    });
                }

                CanvasLoading.Instance.Hide();
            }
            else
            {
                PopupDialog.Instance.Show("An error has occurred. Please try again");
            }

            populatingThemeList = false;
        });*/
    }

   /* public void DeleteThemePrompt(InteractiveTheme theme)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeleteTheme(theme);
            return;
        }
        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                DeleteTheme(theme);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete theme?", "This theme will be deleted permanently. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
    }

    void DeleteTheme(InteractiveTheme theme)
    {
        CanvasLoading.Instance.Show();
        
        APIManager.DeleteTheme(theme, (success) =>
        {
            if (!success)
            {
                Debug.LogError("An error occurred on the server while deleting the quiz.");
            }


            this.OnThemeUpdated();
            CanvasLoading.Instance.Hide();
        });
    }*/

    public void AddTheme()
    {
        CanvasLoading.Instance.Show();

        /*    InteractiveTheme newTheme = new InteractiveTheme() { name = "New Theme", levelSelectStyle = "default", lessonAreaStyle = "default", bgURL = "" };

            APIManager.CreateTheme(newTheme, (theme) =>
            {
                if (theme != null)
                {
                    PopulateThemeList();
                }
                else
                {
                    PopupDialog.Instance.Show("There was a problem while creating your new Theme. Please try again.");
                }

                CanvasLoading.Instance.Hide();
            });*/
    }
}
