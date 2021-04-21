/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_QuizEditor : UIWindow
{
    public static event System.Action OnQuizUpdated;


    [SerializeField]
    GameObject listEntry;

    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    TMP_InputField nameField, descriptionField;

    [SerializeField]
    Window_QuestionEditor questionEditor;

    [SerializeField]
    TMP_Dropdown badgeDropdown;

    [SerializeField]
    Toggle badgeToggle;

    //List<InteractiveBadge> availableBadges = new List<InteractiveBadge>();

    public override void Show()
    {
        base.Show();

        CanvasLoading.Instance.Show();

        SetUpEventListeners();

     /*   APIManager.ListBadges(User.current.selectedMembership.team._id, (badges) =>
        {
            availableBadges = badges.ToList();
            Debug.LogError("Badges: " + availableBadges.Count);
            PopulateWindow();
            PopulateBadgeDropdown();

            CanvasLoading.Instance.Hide();
        });*/

        Window_QuestionEditor.OnQuestionUpdated += this.OnQuestionUpdated;
    }

    private void OnQuestionUpdated()
    {
        PopulateWindow();
    }

    public override void Close()
    {
        base.Close();

        Window_QuestionEditor.OnQuestionUpdated -= this.OnQuestionUpdated;
    }

    private void OnDestroy()
    {
        Window_QuestionEditor.OnQuestionUpdated -= this.OnQuestionUpdated;
    }

    void SetUpEventListeners()
    {
        
        nameField.onEndEdit.RemoveAllListeners();
      /*  nameField.onEndEdit.AddListener((val) =>
        {
            string prev = InteractiveQuiz.current.name;
            InteractiveQuiz.current.name = val;

            UpdateQuiz();
        });

        descriptionField.onEndEdit.RemoveAllListeners();
        descriptionField.onEndEdit.AddListener((val) =>
        {
            string prev = InteractiveQuiz.current.description;
            InteractiveQuiz.current.description = val;

            UpdateQuiz();
        });*/

        badgeToggle.onValueChanged.RemoveAllListeners();
        badgeToggle.onValueChanged.AddListener((enabled) =>
        {
            if (enabled == true)
            {

                badgeDropdown.gameObject.SetActive(true);
            /*    if (availableBadges != null)
                {
                    if (availableBadges.Count > 0)
                    {
                        PopulateBadgeDropdown();

                        if (!InteractiveQuiz.current.hasBadge())
                            SetBadge(availableBadges[0]._id);

                    }
                    else
                    {
                        badgeToggle.isOn = false;
                        PopupDialog.Instance.Show("You cannot assign Badges to Quizzes until you have created at least one Badge.");

                    }
                }
                else
                {
                    badgeToggle.isOn = false;
                    PopupDialog.Instance.Show("You cannot assign Badges to Quizzes until you have created at least one Badge.");

                }*/
            }
            else
            {

                badgeDropdown.gameObject.SetActive(false);
                /*    if (InteractiveQuiz.current.onComplete == null) InteractiveQuiz.current.onComplete = new CommandEvent[] { };
                    List<CommandEvent> commandEvents = InteractiveQuiz.current.onComplete.ToList();
                    commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("awardbadge"));

                    InteractiveQuiz.current.onComplete = commandEvents.ToArray();*/

                UpdateQuiz();
            }
        });

        badgeDropdown.onValueChanged.RemoveAllListeners();
     /*   badgeDropdown.onValueChanged.AddListener((index) =>
        {
            SetBadge(availableBadges[index]._id);
        });*/
    }

    void PopulateWindow()
    {
        CanvasLoading.Instance.Show();
       /* nameField.SetTextWithoutNotify(InteractiveQuiz.current.name);
        descriptionField.SetTextWithoutNotify(InteractiveQuiz.current.description);

        badgeToggle.SetIsOnWithoutNotify(InteractiveQuiz.current.hasBadge());*/
        badgeDropdown.gameObject.SetActive(badgeToggle.isOn);

        PopulateQuestionList();
        
        CanvasLoading.Instance.Hide();
    }

    void PopulateQuestionList()
    {
        foreach(Transform child in scrollRect.content)
            Destroy(child.gameObject);

        int i = 1;
       /* foreach(InteractiveQuestion question in InteractiveQuiz.current.questions)
        {
            GameObject obj = Instantiate(listEntry, scrollRect.content);

            TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
            nameLabel.text = i.ToString("n0") + ".) " + question.text;

            TooltipTrigger.AddTooltip(obj, "Click to Edit.");

            Button btn = obj.GetComponent<Button>();

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => 
            {
                InteractiveQuestion.current = question;
                questionEditor.Show();
            });


            Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.onClick.AddListener(() => 
            {
                DeleteQuestionPrompt(question, InteractiveQuiz.current);
            });
            i++;
        }*/
    }


    /*void DeleteQuestionPrompt(InteractiveQuestion question, InteractiveQuiz quiz)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            RemoveQuestionFromQuiz(question, quiz);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                RemoveQuestionFromQuiz(question, quiz);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete question?", "This question will be deleted permanently. Are you sure you want to continue?", buttons.ToArray());

    }

    void RemoveQuestionFromQuiz(InteractiveQuestion question, InteractiveQuiz quiz)
    {
        
        APIManager.DeleteQuestion(question._id, (success) => 
        {
            if(!success)
            {
                PopupDialog.Instance.Show("An error occurred on the server while deleting the question.");
            }

            List<InteractiveQuestion> questions = InteractiveQuiz.current.questions.ToList();
            questions.RemoveAll(x => x._id == question._id);

            InteractiveQuiz.current.questions = questions.ToArray();

            UpdateQuiz();

            CanvasLoading.Instance.Hide();
        });
    }*/


    void UpdateQuiz()
    {
        CanvasLoading.Instance.Show();
    /*    APIManager.UpdateQuiz(InteractiveQuiz.current, (q) =>
        {
            if (q == null)
            {
                PopupDialog.Instance.Show("An error occurred while sending Quiz data to server.");
            }

            InteractiveQuiz.current = q;
            OnQuizUpdated?.Invoke();
            PopulateWindow();

            CanvasLoading.Instance.Hide();
        });*/
    }

    public void AddQuestion()
    {
      /*  InteractiveQuestion newQuestion = new InteractiveQuestion()
        {
            text = "New Question",
            answers = new InteractiveQuestionAnswers() 
            {
                correct = new string[] {"Correct Answer"},
                wrong = new string[] {"Wrong Answer 1", "Wrong Answer 2"}
            }
        };*/

        CanvasLoading.Instance.Show();
   /*     APIManager.CreateQuestion(newQuestion, (question) => 
        {
            if(question != null)
            {
                List<InteractiveQuestion> questions = InteractiveQuiz.current.questions.ToList();
                questions.Add(question);
                InteractiveQuiz.current.questions = questions.ToArray();
                UpdateQuiz();
            }


            CanvasLoading.Instance.Hide();
        });*/
    }


    void PopulateBadgeDropdown()
    {
     /*   if (badgeToggle.isOn)
        {
            if (availableBadges == null || availableBadges.Count == 0)
            {
                badgeToggle.isOn = false;//turn it right back off because we dont have any available badges to pick from.

                Debug.Log("Has badge? " + InteractiveQuiz.current.hasBadge());

                badgeDropdown.gameObject.SetActive(false);
            }
            else
            {
                List<string> options = new List<string>();

                badgeDropdown.ClearOptions();

                int i = 1;
                foreach (InteractiveBadge badge in availableBadges)
                {
                    options.Add("(" + i.ToString("n0") + ") " + badge.name);
                    //Debug.Log("Adding badge to dropdown: " + badge._id + " - " + badge.name);
                    i++;
                }

                badgeDropdown.AddOptions(options);

                //Try to set the value of the dropdown to match what is actually set in the data
                int selected = availableBadges.FindIndex(x => x._id == InteractiveQuiz.current.badgeId());

                if (selected > -1)
                {
                    badgeDropdown.SetValueWithoutNotify(selected);
                }
                else
                {
                    Debug.LogError("Quiz hasBadge == " + InteractiveQuiz.current.hasBadge() + " but id wasnt found in availableBadge list: " + InteractiveQuiz.current.badgeId());
                }


                badgeDropdown.gameObject.SetActive(true);
            }
        }
        else
        {
            badgeDropdown.gameObject.SetActive(false);
        }*/
    }


    void SetBadge(string badgeID)
    {
  /*      if (InteractiveQuiz.current.onComplete == null) InteractiveQuiz.current.onComplete = new CommandEvent[] { };
        List<CommandEvent> commandEvents = InteractiveQuiz.current.onComplete.ToList();
        commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("awardbadge"));

        if (!string.IsNullOrEmpty(badgeID))
        {
            CommandEvent badgeEvent = new CommandEvent()
            {
                eventName = "AwardBadge",
                args = new string[] { badgeID }
            };

            commandEvents.Add(badgeEvent);
        }

        InteractiveQuiz.current.onComplete = commandEvents.ToArray();*/

        UpdateQuiz();
    }
}
