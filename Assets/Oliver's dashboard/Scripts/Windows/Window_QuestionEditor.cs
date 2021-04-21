using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using GameBrewStudios.Networking;
using GameBrewStudios;
using System.Linq;

public class Window_QuestionEditor : UIWindow
{

    public static event System.Action OnQuestionUpdated;
    public static event System.Action OnClosed;

    [SerializeField]
    GameObject listEntryPrefab;

    [SerializeField]
    ScrollRect correctScrollRect, wrongScrollRect;


    [SerializeField]
    TMP_InputField questionField;


    public override void Show()
    {
        base.Show();

    //    questionField.SetTextWithoutNotify(InteractiveQuestion.current.text);

        SetUpEventListeners();

        PopulateAnswers();


    }

    public override void Close()
    {
        base.Close();
        OnClosed?.Invoke();
    }

    void SetUpEventListeners()
    {
        questionField.onEndEdit.RemoveAllListeners();
        questionField.onEndEdit.AddListener((text) =>
        {
            if (string.IsNullOrEmpty(text))
            {
                questionField.SetTextWithoutNotify("Type Question Here");
                PopupDialog.Instance.Show("Questions cannot be blank.");
                return;
            }

          //  InteractiveQuestion.current.text = text;

            UpdateQuestion();

        });
    }

    void UpdateQuestion()
    {
        CanvasLoading.Instance.Show();
      /*  APIManager.UpdateQuestion(InteractiveQuestion.current, (question) =>
        {
            PopulateAnswers();
            CanvasLoading.Instance.Hide();
            OnQuestionUpdated?.Invoke();
        });*/
    }

    void PopulateAnswers()
    {
       /* PopulateAnswerList(InteractiveQuestion.current.answers.correct, correctScrollRect.content, true);
        PopulateAnswerList(InteractiveQuestion.current.answers.wrong, wrongScrollRect.content, false);*/
    }


    void PopulateAnswerList(string[] answers, Transform container, bool correct)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);

        int i = 0;
        foreach (string answer in answers)
        {
            GameObject obj = Instantiate(listEntryPrefab, container);
            TMP_InputField field = obj.GetComponentInChildren<TMP_InputField>();

            field.onEndEdit.RemoveAllListeners();
            int index = i;
            field.onEndEdit.AddListener((text) =>
            {
                OnEditAnswer(correct, index, text);
            });

            field.SetTextWithoutNotify(answer);

            Button deleteBtn = obj.transform.Find("Button - Remove").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.interactable = answers.Length > 1;

            if (deleteBtn.interactable == false)
            {
                TooltipTrigger tt = deleteBtn.gameObject.AddComponent<TooltipTrigger>();
                tt.text = "You must have at least one answer in this section.";
            }

            deleteBtn.onClick.AddListener(() =>
            {
                DeleteAnswer(correct, index);
            });

            i++;
        }
    }

    void OnEditAnswer(bool correct, int index, string text)
    {
        if (correct)
        {
        //   InteractiveQuestion.current.answers.correct[index] = text;
        }
        else
        {
        //    InteractiveQuestion.current.answers.wrong[index] = text;
        }

        UpdateQuestion();
    }

    void DeleteAnswer(bool correct, int index)
    {
        if (correct)
        {
        /*    List<string> correctAnswers = InteractiveQuestion.current.answers.correct.ToList();
            correctAnswers.RemoveAt(index);

            InteractiveQuestion.current.answers.correct = correctAnswers.ToArray();*/
        }
        else
        {
         /*   List<string> wrongAnswers = InteractiveQuestion.current.answers.wrong.ToList();
            wrongAnswers.RemoveAt(index);

            InteractiveQuestion.current.answers.wrong = wrongAnswers.ToArray();*/
        }

        UpdateQuestion();
    }

    public void AddCorrectAnswer()
    {
      /*  List<string> correct = InteractiveQuestion.current.answers.correct.ToList();
        correct.Add("New Answer");
        InteractiveQuestion.current.answers.correct = correct.ToArray();*/

        UpdateQuestion();
    }

    public void AddWrongAnswer()
    {
        /*List<string> wrong = InteractiveQuestion.current.answers.wrong.ToList();
        wrong.Add("New Answer");
        InteractiveQuestion.current.answers.wrong = wrong.ToArray();*/

        UpdateQuestion();
    }
}
