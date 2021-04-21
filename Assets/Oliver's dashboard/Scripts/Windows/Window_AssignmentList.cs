using GameBrewStudios;
//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_AssignmentList : UIWindow
{
    [SerializeField]
    GameObject assignmentListEntryPrefab;

    [SerializeField]
    ScrollRect assignmentListScrollRect;

    //System.Action<InteractiveCourse> OnAssignmentPicked = null;

    public override void Show()
    {
        Debug.LogError("Do not use the normal Show() method for AssignmentList. Instead use the Show(onClicked) override to pass in a listener for when an assignment is clicked.");
    }

   /* public void Show(System.Action<InteractiveCourse> OnAssignmentPicked)
    {

        PopulateAssignmentList(OnAssignmentPicked);
        
        base.Show();

    }

    public override void Close()
    {
        base.Close();
        if (this.OnAssignmentPicked != null)
        {
            this.OnAssignmentPicked?.Invoke(null);
            this.OnAssignmentPicked = null;
        }
    }

    void PopulateAssignmentList(System.Action<InteractiveCourse> OnAssignmentPicked)
    {
        this.OnAssignmentPicked = OnAssignmentPicked;

        CanvasLoading.Instance.Show();

        APIManager.ListCourses(User.current.selectedMembership.team._id, (courses) =>
        {

            foreach (Transform child in assignmentListScrollRect.content)
                Destroy(child.gameObject);

            foreach (InteractiveCourse course in courses)
            {
                GameObject obj = Instantiate(assignmentListEntryPrefab, assignmentListScrollRect.content);

                TextMeshProUGUI assignmentNameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();

                assignmentNameLabel.text = course.name;

                Button btn = obj.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    Debug.Log("User selected course: " + course._id);
                    this.OnAssignmentPicked?.Invoke(course);
                    this.OnAssignmentPicked = null;
                    this.Close();
                });
            }

            CanvasLoading.Instance.Hide();
        });
    }*/
}
