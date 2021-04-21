using GameBrewStudios;
//using GameBrewStudios.Networking;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Window_QuestionSelector : UIWindow
{
    [SerializeField]
    Transform listContainer;

    [SerializeField]
    GameObject listPrefab;

  //  InteractiveQuestion[] questionsList = null;


 //   public static event System.Action<InteractiveQuestion> OnQuestionSelected;

    public override void Show()
    {
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);
        base.Show();

        CanvasLoading.Instance.Show();
    /*    APIManager.ListQuestions(User.current.selectedMembership.team._id, (questions) =>
        {
            this.questionsList = questions;
            
            Populate();
            CanvasLoading.Instance.Hide();
        });*/

    }

    void Populate()
    {
        

    /*    if(this.questionsList == null || this.questionsList.Length == 0)
        {
            Debug.LogError("NO Files?");
            return;
        }


        foreach (InteractiveQuestion question in questionsList)
        {
            GameObject obj = Instantiate(listPrefab, listContainer, false);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => QuestionSelected(question));

            obj.transform.Find("Rank").gameObject.SetActive(false);

            obj.transform.Find("Button - Delete").gameObject.SetActive(false);

            TextMeshProUGUI label = obj.GetComponentInChildren<TextMeshProUGUI>();
            label.text = question.text;
        }*/
    }

 /*   public void QuestionSelected(InteractiveQuestion question)
    {
        OnQuestionSelected?.Invoke(question);
        Close();
    }*/
}
