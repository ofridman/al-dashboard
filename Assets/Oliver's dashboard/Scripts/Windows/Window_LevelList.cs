//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_LevelList : UIWindow
{
    [SerializeField]
    GameObject levelListEntryPrefab;

    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    Window_LevelEditor levelEditorWindow;


    public override void Show()
    {
        PopulateLevelList();
        base.Show();
    }

    void PopulateLevelList()
    {
        foreach (Transform child in scrollRect.content)
            Destroy(child.gameObject);

     /*   foreach (InteractiveLevel level in InteractiveCourse.current.levels)
        {
            GameObject obj = Instantiate(levelListEntryPrefab, scrollRect.content);

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

    /*void DeleteLevelPrompt(InteractiveLevel level)
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
    }

    void DeleteLevel(InteractiveLevel level)
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
    }

    public void CreateLevel()
    {
        CanvasLoading.Instance.Show();

        APIManager.CreateLevel("New Level", "Describe the level here", null, null, (level) =>
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
        });
    }*/
}
