using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using GameBrewStudios.Networking;
using GameBrewStudios;
using System.Linq;

public class Window_CourseEditor : UIWindow
{
    public static event System.Action OnCourseInfoUpdated;

    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    ScrollRect levelListScrollRect;

    [SerializeField]
    TMP_Dropdown themeDropdown;

    [SerializeField] TMP_InputField courseNameField, courseDescriptionField;

    [SerializeField]
    GameObject levelListEntryPrefab;

/*    [SerializeField]
    Window_LevelEditor levelEditorWindow;

    private InteractiveTheme[] themesList;*/


    public override void Show()
    {
    //    APIManager.ListThemes(User.current.selectedMembership.team._id, PopulateThemeDropdown);

    //    SetEventListeners();

      /*  courseNameField.SetTextWithoutNotify(InteractiveCourse.current.name);
        courseDescriptionField.SetTextWithoutNotify(InteractiveCourse.current.description);*/

        PopulateLevelList();

        base.Show();
    }
    
    void PopulateLevelList()
    {
        foreach (Transform child in levelListScrollRect.content)
            Destroy(child.gameObject);

     /*   foreach (InteractiveLevel level in InteractiveCourse.current.levels)
        {
            GameObject obj = Instantiate(levelListEntryPrefab, levelListScrollRect.content);

            TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
            nameLabel.text = level.name;

            Button levelButton = obj.GetComponent<Button>();
            levelButton.onClick.RemoveAllListeners();
            levelButton.onClick.AddListener(() =>
            {
                InteractiveLevel.current = level;
                levelEditorWindow.Show();
            });


            Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.onClick.AddListener(() =>
            {
                DeleteLevelPrompt(level);
            });
        }*/
    }
    public override void Close()
    {
        base.Close();

        courseNameField.onEndEdit.RemoveAllListeners();
        courseDescriptionField.onEndEdit.RemoveAllListeners();
        themeDropdown.onValueChanged.RemoveAllListeners();

        OnCourseInfoUpdated?.Invoke();
    }

  /*  void DeleteLevelPrompt(InteractiveLevel level)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeleteLevel(level);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton(){
                buttonColor = PopupDialog.PopupButtonColor.Red, text = "Yes, Delete it", onClicked = () =>
                {
                    DeleteLevel(level);
                }
            },
            new PopupDialog.PopupButton()
            {
                buttonColor = PopupDialog.PopupButtonColor.Plain,
                text = "Cancel"
            }
        };

        PopupDialog.Instance.Show("Delete Level", "Really delete " + level.name + "? This can not be undone.", buttons.ToArray());
    }*/

 /*   void DeleteLevel(InteractiveLevel level)
    {
        CanvasLoading.Instance.Show();

        List<InteractiveLevel> levels = InteractiveCourse.current.levels.ToList();
        levels.RemoveAll(x => x._id == level._id);
        InteractiveCourse.current.levels = levels.ToArray();

        //Remove the level from the course first, then we delete the level from the database after we know there are no references to it.
        APIManager.UpdateCourse(InteractiveCourse.current, (course) =>
        {
            APIManager.DeleteLevel(level._id, (dict) =>
            {
                Debug.Log("Finished updating course after deleting a level");
                InteractiveCourse.current.levels = course.levels;
                PopulateLevelList();
                CanvasLoading.Instance.Hide();
            });


        });
    }*/

    public void CreateLevel()
    {
     /*   if(InteractiveCourse.current.levels.Length >= 12)
        {
            PopupDialog.Instance.Show("You already have the max number (12) of levels attached to this course.");
            return;
        }*/
        
        CanvasLoading.Instance.Show();

   /*     APIManager.CreateLevel("New Level", "Describe the level here", null, null, (level) =>
        {
            if (level != null)
            {

                List<InteractiveLevel> levels = InteractiveCourse.current.levels.ToList();
                levels.Add(level);

                InteractiveCourse.current.levels = levels.ToArray();

                APIManager.UpdateCourse(InteractiveCourse.current, (course) =>
                {
                    //Finished updating course to add new level to its levels list
                    Debug.Log("Course updated.");

                    InteractiveCourse.current = course;
                    PopulateLevelList();
                    CanvasLoading.Instance.Hide();
                });
            }
            else
            {
                PopupDialog.Instance.Show("Something went wrong while trying to create a new Level. Please try again later");
            }
        });*/
    }


    /*private void PopulateThemeDropdown(InteractiveTheme[] themes)
    {
        this.themesList = themes;

        themeDropdown.ClearOptions();

        List<string> options = new List<string>();
        options.Add("Default");

        int selectedIndex = 0;

        for (int i = 0; i < this.themesList.Length; i++)
        {
            options.Add(this.themesList[i].name);
            if (InteractiveCourse.current.theme != null && InteractiveCourse.current.theme._id == this.themesList[i]._id)
            {
                selectedIndex = i + 1;
            }
        }

        themeDropdown.AddOptions(options);

        themeDropdown.SetValueWithoutNotify(selectedIndex);
    }

    private void SetEventListeners()
    {
        courseNameField.onEndEdit.RemoveAllListeners();
        courseDescriptionField.onEndEdit.RemoveAllListeners();
        themeDropdown.onValueChanged.RemoveAllListeners();

        courseNameField.onEndEdit.AddListener((val) =>
        {
            InteractiveCourse.current.name = val;
            CanvasLoading.Instance.Show();
            APIManager.UpdateCourse(InteractiveCourse.current, (course) =>
            {
                OnCourseInfoUpdated?.Invoke();
                CanvasLoading.Instance.Hide();
            });
        });

        courseDescriptionField.onEndEdit.AddListener((val) =>
        {
            InteractiveCourse.current.description = val;

            CanvasLoading.Instance.Show();
            APIManager.UpdateCourse(InteractiveCourse.current, (course) =>
            {
                OnCourseInfoUpdated?.Invoke();
                CanvasLoading.Instance.Hide();
            });
        });

        themeDropdown.onValueChanged.AddListener((index) =>
        {
            if (index == 0)
                InteractiveCourse.current.theme = null;
            else
            {
                InteractiveCourse.current.theme = themesList[index - 1];
            }
            CanvasLoading.Instance.Show();
            APIManager.UpdateCourse(InteractiveCourse.current, (course) => 
            {
                OnCourseInfoUpdated?.Invoke();
                CanvasLoading.Instance.Hide();
            });
        });
    }*/
}
