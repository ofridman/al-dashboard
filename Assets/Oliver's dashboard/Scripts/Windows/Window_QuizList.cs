using GameBrewStudios;
//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_QuizList : UIWindow
{
    [SerializeField]
    GameObject quizListEntryPrefab;

    [SerializeField]
    ScrollRect quizListScrollRect;


    public override void Show()
    {
        Debug.LogError("Do not use the normal Show() method for AssignmentList. Instead use the Show(courses, onClicked) override to pass in a listener for when an assignment is clicked.");
    }

 /*   public void Show(System.Action<InteractiveQuiz> OnQuizSelected)
    {
        CanvasLoading.Instance.Show();

        APIManager.ListQuizs(User.current.selectedMembership.team._id, (quizzes) =>
        {
            PopulateQuizList(quizzes, OnQuizSelected);
            CanvasLoading.Instance.Hide();

            base.Show();
        });
    }

    void PopulateQuizList(InteractiveQuiz[] quizzes, System.Action<InteractiveQuiz> OnQuizSelected)
    {
        foreach (Transform child in quizListScrollRect.content)
            Destroy(child.gameObject);

        if (quizzes == null) return;

        foreach (InteractiveQuiz quiz in quizzes)
        {
            GameObject obj = Instantiate(quizListEntryPrefab, quizListScrollRect.content);

            TextMeshProUGUI quizNameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();

            quizNameLabel.text = quiz.name;

            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                Debug.Log("User selected quiz: " + quiz._id);
                OnQuizSelected?.Invoke(quiz);

                this.Close();
            });
        }
    }*/
}
