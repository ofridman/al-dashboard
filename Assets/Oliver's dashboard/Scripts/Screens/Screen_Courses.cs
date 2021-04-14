using GameBrewStudios;
//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Courses : MenuScreen
{
    [SerializeField]
    ScrollRect courseListScrollRect;

    [SerializeField]
    GameObject courseEntryPrefab;

    [SerializeField]
    Window_CourseEditor courseEditorWindow;

    //private InteractiveCourse[] courses;

    public override void Show()
    {
        base.Show();

        PopulateCourseList();

        Window_CourseEditor.OnCourseInfoUpdated += this.OnCourseInfoUpdated;
    }

    public override void Hide()
    {
        Window_CourseEditor.OnCourseInfoUpdated -= this.OnCourseInfoUpdated;
        base.Hide();
    }


    private void OnCourseInfoUpdated()
    {
        PopulateCourseList();
    }

    private void SortCourses()
    {
        
    }

    public void PopulateCourseList()
    {

        CanvasLoading.Instance?.Show();
      /*  APIManager.ListCourses(User.current.selectedMembership.team._id, (courses) =>
        {
            this.courses = courses;

            SortCourses();
            foreach (Transform child in courseListScrollRect.content)
                Destroy(child.gameObject);
            int i = 1;
            foreach (InteractiveCourse course in this.courses)
            {
                GameObject obj = Instantiate(courseEntryPrefab, courseListScrollRect.content);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString("n0") + ".) " + course.name;

                Button courseButton = obj.GetComponent<Button>();
                courseButton.onClick.RemoveAllListeners();
                courseButton.onClick.AddListener(() =>
                {
                    InteractiveCourse.current = course;
                    courseEditorWindow.Show();
                });

                Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                deleteBtn.onClick.RemoveAllListeners();
                deleteBtn.onClick.AddListener(() =>
                {
                    DeleteCoursePrompt(course);
                });

                i++;
            }
            CanvasLoading.Instance?.Hide();
        });*/
        
    }

  /*  public void DeleteCoursePrompt(InteractiveCourse course)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeleteCourse(course);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                DeleteCourse(course);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete course?", "This course and its levels and lessons will ALL be deleted permanently. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
    }

    void DeleteCourse(InteractiveCourse course)
    {
        CanvasLoading.Instance.Show();

        APIManager.DeleteCourse(course._id, (success) =>
        {
            if (!success)
            {
                Debug.LogError("An error occurred on the server while deleting the course.");
            }

            this.OnCourseInfoUpdated();
            CanvasLoading.Instance.Hide();
        });
    }*/


    public void AddCourse()
    {
        CanvasLoading.Instance.Show();
       /* APIManager.CreateCourse("New Course", "", null, new string[] { }, (course) => 
        {
            if(course == null)
            {
                PopupDialog.Instance.Show("Something went wrong while creating a new course. Please try again.");
                return;
            }
            
            PopulateCourseList();
            CanvasLoading.Instance.Hide();
        });*/
    }


}
