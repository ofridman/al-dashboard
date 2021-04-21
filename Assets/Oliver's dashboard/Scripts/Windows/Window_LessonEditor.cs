using GameBrewStudios;
//using GameBrewStudios.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Window_LessonEditor : UIWindow
{
    public static event System.Action OnLessonUpdated;

    //[SerializeField]
    //VideoPreview videoPreview;

    [SerializeField]
    string placeholderVideoUrl = "https://www.w3schools.com/tags/movie.mp4";

    [SerializeField]
    TMP_InputField lessonNameField, lessonDescriptionField;

    [SerializeField]
    TMP_Dropdown videoFileDropdown;

    [SerializeField]
    Toggle quizToggle;


    [SerializeField]
    TMP_Dropdown quizDropdown;

    [SerializeField]
    Toggle badgeToggle;

    [SerializeField]
    TMP_Dropdown badgeDropdown;

  /*  TeamFile[] teamVideos = new TeamFile[] { };
    List<InteractiveQuiz> availableQuizzes = new List<InteractiveQuiz>();
    List<InteractiveBadge> availableBadges = new List<InteractiveBadge>();*/


    public override void Show()
    {
        base.Show();

        //Only get quizzes that are both assigned to the level this lesson is on, and that have not already been assigned to another lesson.

 /*       if (InteractiveLevel.current.quizzes == null || InteractiveLevel.current.quizzes.Length == 0)
        {
            availableQuizzes = null; //this will force a popup to trigger telling the user they need to add quizzes to the level first
        }
        else
        {
            if (availableQuizzes == null) availableQuizzes = new List<InteractiveQuiz>();
            availableQuizzes.Clear();

            foreach (InteractiveQuiz quiz in InteractiveLevel.current.quizzes)
            {
                bool canAddQuiz = true;
                foreach (InteractiveLesson lesson in InteractiveLevel.current.lessons)
                {
                    if (lesson.hasQuiz())
                    {
                        if (lesson._id != InteractiveLesson.current._id && lesson.quizId() == quiz._id)
                        {
                            Debug.LogWarning("SKIPPING QUIZ " + quiz._id + " BECAUSE ITS ALREADY ASSIGNED TO LESSON: " + lesson._id + "  Cannot assign it to current lesson: " + InteractiveLesson.current._id);
                            canAddQuiz = false;
                            break;
                        }

                    }
                }

                if (canAddQuiz) availableQuizzes.Add(quiz);
            }
        }*/

        SetUpEventListeners();

 /*       lessonNameField.SetTextWithoutNotify(InteractiveLesson.current.name);
        lessonDescriptionField.SetTextWithoutNotify(InteractiveLesson.current.description);*/
        
     //   quizToggle.SetIsOnWithoutNotify(InteractiveLesson.current.hasQuiz());
        quizDropdown.gameObject.SetActive(quizToggle.isOn);

     //   badgeToggle.SetIsOnWithoutNotify(InteractiveLesson.current.hasBadge());
        badgeDropdown.gameObject.SetActive(badgeToggle.isOn);

        PopulateQuizDropdown();


        CanvasLoading.Instance.Show();
    /*    APIManager.ListBadges(User.current.selectedMembership.team._id, (badges) =>
        {
            availableBadges = badges.ToList();
            Debug.LogError("Badges: " + availableBadges.Count);
            PopulateBadgeDropdown();

            CanvasLoading.Instance.Hide();
        });*/

        PopulateVideoFileDropdown();

    }

    public override void Close()
    {
        base.Close();
    }

    void PopulateVideoFileDropdown()
    {
        CanvasLoading.Instance.Show();
   /*     APIManager.ListFiles(User.current.selectedMembership.team._id, (files) =>
        {

            List<string> options = new List<string>
            {
                "SELECT A VIDEO"
            };

            teamVideos = files.Where(x => x.isMPEG4).ToArray();

            int i = 1;
            int selectedIndex = 0;
            foreach (TeamFile file in teamVideos)
            {
                options.Add(file.filename);

                if (InteractiveLesson.current.data == file.FileURL())
                    selectedIndex = i;

                i++;
            }

            videoFileDropdown.ClearOptions();
            videoFileDropdown.AddOptions(options);

            videoFileDropdown.SetValueWithoutNotify(selectedIndex);

            if(selectedIndex == 0)
            {
                previewButton.interactable = false;
            }
            else
            {
                previewButton.interactable = true;
            }


            ////videoPreview.SetVideoURL(InteractiveLesson.current.data);

            CanvasLoading.Instance.Hide();
        });*/
    }



    void SetUpEventListeners()
    {
        

        videoFileDropdown.onValueChanged.RemoveAllListeners();
        videoFileDropdown.onValueChanged.AddListener(OnVideoFileSelectionChanged);

        previewButton.onClick.RemoveAllListeners();
   /*     previewButton.onClick.AddListener(() =>
        {

#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval("window.open(\"" + InteractiveLesson.current.data + "\")");
#else
            Application.OpenURL(InteractiveLesson.current.data);
#endif

            
        });*/

        lessonNameField.onEndEdit.RemoveAllListeners();
    /*    lessonNameField.onEndEdit.AddListener((val) =>
        {
            string prev = InteractiveLesson.current.name;
            InteractiveLesson.current.name = val;

            CanvasLoading.Instance.Show();
            APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
            {
                if (lesson == null)
                {
                    InteractiveLesson.current.name = prev;
                    lessonNameField.SetTextWithoutNotify(prev);
                    PopupDialog.Instance.Show("Something went wrong while updating the Lesson name on the server. Changes have been reverted.");

                }
                OnLessonUpdated?.Invoke();
                CanvasLoading.Instance.Hide();
            });
        });*/

        lessonDescriptionField.onEndEdit.RemoveAllListeners();
    /*    lessonDescriptionField.onEndEdit.AddListener((val) =>
        {
            string prev = InteractiveLesson.current.description;
            InteractiveLesson.current.description = val;

            CanvasLoading.Instance.Show();
            APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
            {
                if (lesson == null)
                {
                    InteractiveLesson.current.name = prev;
                    lessonDescriptionField.SetTextWithoutNotify(prev);
                    PopupDialog.Instance.Show("Something went wrong while updating the Lesson name on the server. Changes have been reverted.");

                }
                OnLessonUpdated?.Invoke();
                CanvasLoading.Instance.Hide();
            });
        });*/

        quizToggle.onValueChanged.RemoveAllListeners();
     /*   quizToggle.onValueChanged.AddListener((enabled) =>
        {
            if (enabled == true)
            {
                quizDropdown.gameObject.SetActive(true);

                if (availableQuizzes != null)
                {
                    //Grab only quizzes that have not already been assigned to any lessons.

                    if (availableQuizzes.Count > 0)
                    {
                        PopulateQuizDropdown();
                        if (!InteractiveLesson.current.hasQuiz())
                            SetQuiz(availableQuizzes[0]._id);

                        
                    }
                    else
                    {
                        quizToggle.isOn = false;
                        PopupDialog.Instance.Show("All of your attached quizzes are already assigned to other lessons. Attach more quizzes to the Level that this Lesson belongs to in order to enable a Quiz for this Lesson.");
                        
                    }
                }
                else
                {
                    quizToggle.isOn = false;
                    PopupDialog.Instance.Show("You cannot assign Quizzes to Lessons until you have attached at least one Quiz to the Level first.");
                    
                }
            }
            else
            {
                quizDropdown.gameObject.SetActive(false);
                List<CommandEvent> commandEvents = InteractiveLesson.current.onComplete.ToList();
                commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("doquiz"));

                InteractiveLesson.current.onComplete = commandEvents.ToArray();

                CanvasLoading.Instance.Show();
                APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
                {
                    if (lesson == null)
                    {
                        PopupDialog.Instance.Show("Something went wrong while updating the Lesson on the server. Please try again.");
                    }
                    OnLessonUpdated?.Invoke();
                    CanvasLoading.Instance.Hide();
                });
            }
        });*/

        quizDropdown.onValueChanged.RemoveAllListeners();
      /*  quizDropdown.onValueChanged.AddListener((index) =>
        {
            SetQuiz(availableQuizzes[index]._id);
        });*/


        badgeToggle.onValueChanged.RemoveAllListeners();
      /*  badgeToggle.onValueChanged.AddListener((enabled) =>
        {
            if (enabled == true)
            {
                
                badgeDropdown.gameObject.SetActive(true);
                if (availableBadges != null)
                {
                    if (availableBadges.Count > 0)
                    {
                        //Grab only quizzes that have not already been assigned to any lessons.
                        PopulateBadgeDropdown();

                        if (!InteractiveLesson.current.hasBadge())
                            SetBadge(availableBadges[0]._id);

                    }
                    else
                    {
                        badgeToggle.isOn = false;
                        PopupDialog.Instance.Show("You cannot assign Badges to Lessons until you have created at least one Badge.");
                        
                    }
                }
                else
                {
                    badgeToggle.isOn = false;
                    PopupDialog.Instance.Show("You cannot assign Badges to Lessons until you have created at least one Badge.");
                    
                }
            }
            else
            {

                badgeDropdown.gameObject.SetActive(false);

                List<CommandEvent> commandEvents = InteractiveLesson.current.onComplete.ToList();
                commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("awardbadge"));

                InteractiveLesson.current.onComplete = commandEvents.ToArray();

                CanvasLoading.Instance.Show();
                APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
                {
                    if (lesson == null)
                    {
                        PopupDialog.Instance.Show("Something went wrong while updating the Lesson on the server. Please try again.");
                    }
                    OnLessonUpdated?.Invoke();
                    CanvasLoading.Instance.Hide();
                });
            }
        });*/

        badgeDropdown.onValueChanged.RemoveAllListeners();
      /*  badgeDropdown.onValueChanged.AddListener((index) =>
        {
            SetBadge(availableBadges[index]._id);
        });*/
    }

    [SerializeField]
    Button previewButton;

    void OnVideoFileSelectionChanged(int index)
    {
        CanvasLoading.Instance.Show();

        if (index == 0)
        {
         //   InteractiveLesson.current.data = placeholderVideoUrl;
            previewButton.interactable = false;
        }
        else
        {
        //    InteractiveLesson.current.data = teamVideos[index - 1].FileURL();
            previewButton.interactable = true;
            
        }

        //videoPreview.SetVideoURL(InteractiveLesson.current.data);

     /*   APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
        {
            if (lesson == null)
            {
                //failed to update on server somehow
                PopupDialog.Instance.Show("Something went wrong while updating Lesson on server. Please try again.");
            }

            InteractiveLesson.current.data = lesson.data;
            OnLessonUpdated?.Invoke();
            CanvasLoading.Instance.Hide();
        });*/


    }

    void PopulateQuizDropdown()
    {
        if (quizToggle.isOn)
        {
         /*   if (availableQuizzes == null || availableQuizzes.Count == 0)
            {
                quizToggle.isOn = false;//turn it right back off because we dont have any available quizzes to pick from.

                Debug.Log("Has quiz? " + InteractiveLesson.current.hasQuiz());

                quizDropdown.gameObject.SetActive(false);
            }
            else
            {
                List<string> options = new List<string>();

                quizDropdown.ClearOptions();

                int i = 1;
                foreach (InteractiveQuiz quiz in availableQuizzes)
                {
                    options.Add("(" + i.ToString("n0") + ") " + quiz.name);
                    i++;
                }

                quizDropdown.AddOptions(options);

                //Try to set the value of the dropdown to match what is actually set in the data
                int selected = InteractiveLesson.current.hasQuiz() ? availableQuizzes.FindIndex(x => x._id == InteractiveLesson.current.quizId()) : 0;

                if (selected > -1)
                {
                    quizDropdown.SetValueWithoutNotify(selected);
                }
                else
                {
                    Debug.LogError("Lesson hasQuiz == " + InteractiveLesson.current.hasQuiz() + " but id wasnt found in availableQuiz list: " + InteractiveLesson.current.quizId());
                }


                quizDropdown.gameObject.SetActive(true);
            }*/
        }
        else
        {
            quizDropdown.gameObject.SetActive(false);
        }
    }


    void SetQuiz(string quizID)
    {
        CanvasLoading.Instance.Show();

    /*    List<CommandEvent> commandEvents = InteractiveLesson.current.onComplete.ToList();
        commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("doquiz"));

        CommandEvent quizEvent = new CommandEvent()
        {
            eventName = "DoQuiz",
            args = new string[] { quizID }
        };

        commandEvents.Add(quizEvent);

        InteractiveLesson.current.onComplete = commandEvents.ToArray();

        APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
        {
            if (lesson == null)
            {
                Debug.LogError("LESSON IS NULL AFTER UPDATE");
            }

            InteractiveLesson.current = lesson;
            OnLessonUpdated?.Invoke();
            CanvasLoading.Instance.Hide();
        });*/
    }





    void PopulateBadgeDropdown()
    {
        if (badgeToggle.isOn)
        {
          /*  if (availableBadges == null || availableBadges.Count == 0)
            {
                badgeToggle.isOn = false;//turn it right back off because we dont have any available badges to pick from.

                Debug.Log("Has badge? " + InteractiveLesson.current.hasBadge());

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
                int selected = InteractiveLesson.current.hasBadge() ? availableBadges.FindIndex(x => x._id == InteractiveLesson.current.badgeId()) : 0;

                if (selected > -1)
                {
                    badgeDropdown.SetValueWithoutNotify(selected);
                }
                else
                {
                    Debug.LogError("Lesson hasBadge == " + InteractiveLesson.current.hasBadge() + " but id wasnt found in availableBadge list: " + InteractiveLesson.current.badgeId());
                }


                badgeDropdown.gameObject.SetActive(true);
            }*/
        }
        else
        {
            badgeDropdown.gameObject.SetActive(false);
        }
    }


    void SetBadge(string badgeID)
    {
        CanvasLoading.Instance.Show();

   /*     List<CommandEvent> commandEvents = InteractiveLesson.current.onComplete.ToList();
        commandEvents.RemoveAll(x => x.eventName.ToLower().Equals("awardbadge"));

        CommandEvent badgeEvent = new CommandEvent()
        {
            eventName = "AwardBadge",
            args = new string[] { badgeID }
        };

        commandEvents.Add(badgeEvent);

        InteractiveLesson.current.onComplete = commandEvents.ToArray();

        APIManager.UpdateLesson(InteractiveLesson.current, (lesson) =>
        {
            if (lesson == null)
            {
                Debug.LogError("LESSON IS NULL AFTER UPDATE");
            }

            InteractiveLesson.current = lesson;
            OnLessonUpdated?.Invoke();
            CanvasLoading.Instance.Hide();
        });*/
    }

}
