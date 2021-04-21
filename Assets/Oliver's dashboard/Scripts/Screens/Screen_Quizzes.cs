/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Quizzes : MenuScreen
{
    [SerializeField]
    GameObject quizListEntryPrefab;

    [SerializeField]
    ScrollRect quizListScrollRect;

    [SerializeField]
    Window_QuizEditor quizEditorWindow;

    [SerializeField]
    Sprite listEntryIcon;

    public override void Show()
    {
        CanvasLoading.Instance.Show();
    /*    APIManager.ListQuizs(User.current.selectedMembership.team._id, (quizzes) => 
        {
            PopulateQuizList(quizzes);
            CanvasLoading.Instance.Hide();
        });*/

        base.Show();

        Window_QuizEditor.OnQuizUpdated += this.OnQuizUpdated;
    }

    private void OnQuizUpdated()
    {
        
 /*       APIManager.ListQuizs(User.current.selectedMembership.team._id, (quizzes) =>
        {
            PopulateQuizList(quizzes);
            
        });*/
    }

    public override void Hide()
    {
        base.Hide();

        Window_QuizEditor.OnQuizUpdated -= this.OnQuizUpdated;
    }

    private void OnDestroy()
    {
        Window_QuizEditor.OnQuizUpdated -= this.OnQuizUpdated;
    }

    /* void PopulateQuizList(InteractiveQuiz[] quizzes)
     {
         foreach (Transform child in quizListScrollRect.content)
             Destroy(child.gameObject);

         foreach (InteractiveQuiz quiz in quizzes)
         {
             GameObject obj = Instantiate(quizListEntryPrefab, quizListScrollRect.content);

             TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
             nameLabel.text = quiz.name;

             obj.transform.Find("Rank/Icon").GetComponent<Image>().sprite = listEntryIcon;

             Button btn = obj.GetComponent<Button>();
             btn.onClick.RemoveAllListeners();
             btn.onClick.AddListener(() => 
             {
                 InteractiveQuiz.current = quiz;
                 quizEditorWindow.Show();
             });

             Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
             deleteBtn.onClick.RemoveAllListeners();
             deleteBtn.onClick.AddListener(() =>
             {

                 DeleteQuizPrompt(quiz);
             });

         }
     }

     public void DeleteQuizPrompt(InteractiveQuiz quiz)
     {
         if (Input.GetKey(KeyCode.LeftControl))
         {
             DeleteQuiz(quiz);
             return;
         }
         List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
         {
             new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
             {
                 DeleteQuiz(quiz);
             }},
             new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
         };

         PopupDialog.Instance.Show("Really delete quiz?", "This quiz will be deleted permanently. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
     }

     void DeleteQuiz(InteractiveQuiz quiz)
     {
         CanvasLoading.Instance.Show();

         APIManager.DeleteQuiz(quiz._id, (success) =>
         {
             if (!success)
             {
                 Debug.LogError("An error occurred on the server while deleting the quiz.");
             }


             this.OnQuizUpdated();
             CanvasLoading.Instance.Hide();
         });
     }*/

    public void AddQuiz()
    {
        CanvasLoading.Instance.Show();
      /*  APIManager.CreateQuiz("New Quiz", "", new string[] { }, (quiz) => 
        {
            APIManager.ListQuizs(User.current.selectedMembership.team._id, (quizzes) =>
            {
                PopulateQuizList(quizzes);
                CanvasLoading.Instance.Hide();
            });
        });*/
    }
}
