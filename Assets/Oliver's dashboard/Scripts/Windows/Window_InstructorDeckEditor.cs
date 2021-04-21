using GameBrewStudios;
//using GameBrewStudios.Networking;

using System;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Window_InstructorDeckEditor : UIWindow
{
    [SerializeField]
    TMP_InputField deckName;

    [SerializeField]
    GameObject listPrefab;

    [SerializeField]
    Transform questionsContainer, videoContainer, modelContainer, slideShowContainer;


    [SerializeField]
    Window_FileSelector selectFileWindow;

    [SerializeField]
    Window_QuestionSelector selectQuestionWindow;

    [SerializeField]
    Window_QuestionEditor editQuestionWindow;


    [SerializeField]
    Window_VideoURLInput videoURLInput;

    public static event System.Action DeckListUpdated;

    /// <summary>
    /// Call this overload when creating a new deck
    /// </summary>
  /*  public void Show(InteractiveInstructorDeck deck)
    {
        if (deck == null || string.IsNullOrEmpty(deck._id))
        {
            CreateNewDeck();
        }
        else
        {
            InteractiveInstructorDeck.current = deck;
            PopulateWindow();
            base.Show();
        }

    }*/

    public override void Close()
    {
        base.Close();
    }

    public void PopulateWindow()
    {
        /*  InteractiveInstructorDeck d = InteractiveInstructorDeck.current;
          if(d == null)
          {
              Close();
              return;
          }

          deckName.SetTextWithoutNotify(d.name);
        */
        deckName.onEndEdit.RemoveAllListeners();
     /*   deckName.onEndEdit.AddListener((val) =>
        {
            if (val == InteractiveInstructorDeck.current.name) return;

            CanvasLoading.Instance.Show();
            InteractiveInstructorDeck.current.name = val;
            APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, (response) => 
            {
                CanvasLoading.Instance.Hide();
                if(response.success)
                {
                    this.Show(response.deck);
                    DeckListUpdated?.Invoke();
                }
            });
        });*/
        

        foreach (Transform child in videoContainer)
            Destroy(child.gameObject);

        foreach (Transform child in modelContainer)
            Destroy(child.gameObject);

        foreach (Transform child in questionsContainer)
            Destroy(child.gameObject);

        foreach (Transform child in slideShowContainer)
            Destroy(child.gameObject);

   /*     if (d.videos == null) d.videos = new string[] { };
        foreach (string url in d.videos)
        {
            bool isYoutubeLink = url.Contains("//youtube.com") || url.Contains("//www.youtube.com") || url.Contains("//youtu.be");

            string filename = url.Substring(url.LastIndexOf("/") + 1, url.Length - (url.LastIndexOf("/") + 1));
            string name = isYoutubeLink ? url : filename;

            

            CreateSublistEntry(() => 
            {
                PopupDialog.PopupButton[] btns = new PopupDialog.PopupButton[] {
                    new PopupDialog.PopupButton()
                    {
                        text = "Preview Video",
                        onClicked = () =>
                        {
                            if(isYoutubeLink)
                                Application.OpenURL(url);
                            else
                            {
                                Application.OpenURL(url + "?flag=true");
                            }
                        }
                    },
                    new PopupDialog.PopupButton()
                    {
                        text = "Nevermind",
                        onClicked = () => { }
                    }
                };
                PopupDialog.Instance.Show("Video Preview", "Preview video in a new tab?", btns); 
            }, 
            () => RemoveVideo(url), 
            name, 
            videoContainer);
        }
        
        if (d.models == null) d.models = new string[] { };
        foreach (string url in d.models)
        {
            CreateSublistEntry(() => { }, () => RemoveModel(url), url.Substring(url.LastIndexOf("/")+1, url.Length - (url.LastIndexOf("/")+1)), modelContainer);
        }

        if (d.slideShows == null) d.slideShows = new string[] { };
        foreach (string url in d.slideShows)
        {
            
            CreateSublistEntry(() => { }, () => RemoveSlideShow(url), url.Split(new char[] { '/' }).Last(), slideShowContainer);
        }

        if (d.questions == null) d.questions = new string[] { };
        else
        {
            CanvasLoading.Instance.Show();
            APIManager.ListQuestionsByIds(d.questions, (questions) =>
            {

                foreach (InteractiveQuestion q in questions)
                {
                    CreateSublistEntry(() => { }, () => RemoveQuestion(q._id), q.text, questionsContainer);
                }

                CanvasLoading.Instance.Hide();
                DeckListUpdated?.Invoke();
            });
        }*/
        
    }

    public void CreateSublistEntry(System.Action onClicked, System.Action onDeleteClicked, string labelText, Transform container)
    {
        GameObject obj = Instantiate(listPrefab, container, false);
        Button btn = obj.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            onClicked?.Invoke();
        });

        //obj.transform.Find("Rank").gameObject.SetActive(false);

        Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
        deleteBtn.gameObject.SetActive(true);
        deleteBtn.onClick.RemoveAllListeners();
        deleteBtn.onClick.AddListener(() =>
        {
            onDeleteClicked();
        });


        TextMeshProUGUI label = obj.GetComponentInChildren<TextMeshProUGUI>();
        label.text = labelText;
    }

    public void RemoveQuestion(string id)
    {
        CanvasLoading.Instance.Show();

        /*List<string> questionsList = InteractiveInstructorDeck.current.questions.ToList();
        questionsList.Remove(id);
        
        InteractiveInstructorDeck.current.questions = questionsList.ToArray();

        APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, (response) => 
        {
            CanvasLoading.Instance.Hide();
            if(!response.success)
            {
                PopupDialog.Instance.Show("Something went wrong while updating the deck. Please try again.");
            }

            DeckListUpdated?.Invoke();

        });*/
    }

    public void AddQuestion()
    {
        PopupDialog.PopupButton[] buttons = new PopupDialog.PopupButton[]
        {
            new PopupDialog.PopupButton()
            {
                text = "Choose Existing Question",
                buttonColor = PopupDialog.PopupButtonColor.Plain,
                onClicked = () =>
                {
             /*       System.Action<InteractiveQuestion> onFinished = null;
                    onFinished = (question) =>
                    {
                        Window_QuestionSelector.OnQuestionSelected -= onFinished;

                        if (question == null) return;

                        List<string> questionList = InteractiveInstructorDeck.current.videos.ToList();
                        questionList.Add(question._id);

                        InteractiveInstructorDeck.current.questions = questionList.ToArray();
                        APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result => {
                            CanvasLoading.Instance.Hide();
                            Show(result.deck);
                            DeckListUpdated?.Invoke();
                        });
                    };

                    Window_QuestionSelector.OnQuestionSelected += onFinished;*/

                    selectQuestionWindow.Show();
                }
            },
            new PopupDialog.PopupButton()
            {
                text = "Create New Question",
                buttonColor = PopupDialog.PopupButtonColor.Plain,
                onClicked = () =>
                {
                    CreateNewQuestion();
                }
            }
        };

        PopupDialog.Instance.Show("Pick One", "You can choose an existing question from one of your quizzes, or you can create a new one. What would you like to to?", buttons);
    }

    public void RemoveSlideShow(string url)
    {
    /*    List<string> slideShowList = InteractiveInstructorDeck.current.slideShows.ToList();
        slideShowList.Remove(url);
        InteractiveInstructorDeck.current.slideShows = slideShowList.ToArray();

        CanvasLoading.Instance.Show();
        APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result =>
        {
            CanvasLoading.Instance.Hide();
            Show(result.deck);

            DeckListUpdated?.Invoke();
        });*/
    }

    public void AddSlideShow()
    {
        /*System.Action<TeamFile> onFinished = null;
        onFinished = (file) =>
        {
            Window_FileSelector.OnFileSelected -= onFinished;

            if (file == null) return;

            List<string> slideShowList = InteractiveInstructorDeck.current.slideShows.ToList();
            slideShowList.Add(file.FileURL());

            InteractiveInstructorDeck.current.slideShows = slideShowList.ToArray();
            APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result => {
                CanvasLoading.Instance.Hide();
                Show(result.deck);
                DeckListUpdated?.Invoke();
            });
        };

        Window_FileSelector.OnFileSelected += onFinished;*/

        selectFileWindow.Show("Select a Slideshow", ".pptx");

    }

    public void RemoveVideo(string url)
    {
     /*   List<string> vids = InteractiveInstructorDeck.current.videos.ToList();
        vids.Remove(url);
        InteractiveInstructorDeck.current.videos = vids.ToArray();

        CanvasLoading.Instance.Show();
        APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result => 
        {
            CanvasLoading.Instance.Hide();
            Show(result.deck);

            DeckListUpdated?.Invoke();
        });*/
    }

    public void AddVideo()
    {

        PopupDialog.PopupButton[] btns = new PopupDialog.PopupButton[]
        {
            new PopupDialog.PopupButton()
            {
                text = "Select Uploaded Video",
                onClicked = () =>
                {
                /*    System.Action<TeamFile> onFinished = null;
                    onFinished = (file) =>
                    {
                        Window_FileSelector.OnFileSelected -= onFinished;

                        if (file == null) return;

                        List<string> vidList = InteractiveInstructorDeck.current.videos.ToList();
                        vidList.Add(file.FileURL());

                        InteractiveInstructorDeck.current.videos = vidList.ToArray();
                        APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result => {
                            CanvasLoading.Instance.Hide();
                            DeckListUpdated?.Invoke();
                            Show(result.deck);
                        });
                    };

                    Window_FileSelector.OnFileSelected += onFinished;*/

                    selectFileWindow.Show("Select a Video", ".mp4");
                }
            },
            new PopupDialog.PopupButton()
            {
                text = "New Video URL",
                onClicked = () =>
                {

                    videoURLInput.Show((url) =>
                    {
                        if(!string.IsNullOrEmpty(url) && IsValid())
                        {
                            CanvasLoading.Instance.Show();

                     /*       List<string> videoList = InteractiveInstructorDeck.current.videos.ToList();
                            videoList.Add(url);
                            InteractiveInstructorDeck.current.videos = videoList.ToArray();

                            APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, (response) =>
                            {
                                CanvasLoading.Instance.Hide();
                                if (!response.success)
                                {
                                    PopupDialog.Instance.Show("Something went wrong while updating your deck on the server. Please try again.");
                                    return;
                                }

                                this.Show(response.deck);
                            });*/
                        }
                        else
                        {
                            PopupDialog.PopupButton[] buttons = new PopupDialog.PopupButton[]
                            {
                                new PopupDialog.PopupButton()
                                {
                                   text = "Try again.",
                                   onClicked = () =>
                                   {
                                       AddVideo();
                                   }
                                }
                            };
                            PopupDialog.Instance.Show("Invalid URL.", "Please check your URL string and try again.", buttons);
                        }

                        bool IsValid()
                        {
                            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
                        }
                    });
                }
            }
        };

        PopupDialog.Instance.Show("Invalid URL.", "Please check your URL string and try again.", btns);

    }

    private void CreateNewQuestion()
    {
        CanvasLoading.Instance.Show();
      /*  InteractiveQuestion q = new InteractiveQuestion();
        q.text = "New Question";
        q.team = User.current.selectedMembership.team._id;
        q.answers = new InteractiveQuestionAnswers() { correct = new string[0], wrong = new string[0] };
        APIManager.CreateQuestion(q, (question) => 
        {
            InteractiveQuestion.current = question;
            List<string> questionList = InteractiveInstructorDeck.current.questions.ToList();
            questionList.Add(question._id);

            InteractiveInstructorDeck.current.questions = questionList.ToArray();

            APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, (response) => 
            {
                CanvasLoading.Instance.Hide();
                

                System.Action onFinishedEditing = null;
                onFinishedEditing = () => 
                {
                    Window_QuestionEditor.OnClosed -= onFinishedEditing;

                    this.Show(response.deck);
                };

                Window_QuestionEditor.OnClosed += onFinishedEditing;
                editQuestionWindow.Show();
            });

        });*/
    }

    public void RemoveModel(string url)
    {
     /*   List<string> modelsList = InteractiveInstructorDeck.current.models.ToList();
        modelsList.Remove(url);
        InteractiveInstructorDeck.current.models = modelsList.ToArray();*/

        CanvasLoading.Instance.Show();
   /*     APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result =>
        {
            CanvasLoading.Instance.Hide();
            Show(result.deck);
            DeckListUpdated?.Invoke();
        });*/
    }

    public void AddModel()
    {

      /*  System.Action<TeamFile> onFinished = null;
        onFinished = (file) =>
        {
            Window_FileSelector.OnFileSelected -= onFinished;

            if (file == null) return;

            List<string> modelsList = InteractiveInstructorDeck.current.models.ToList();
            modelsList.Add(file.FileURL());

            InteractiveInstructorDeck.current.models = modelsList.ToArray();
            APIManager.UpdateInstructorDeck(InteractiveInstructorDeck.current, result => {
                CanvasLoading.Instance.Hide();
                Show(result.deck);
                DeckListUpdated?.Invoke();
            });
        };

        Window_FileSelector.OnFileSelected += onFinished;*/

        selectFileWindow.Show("Select a Model", ".obj");
    }

    void CreateNewDeck()
    {
        CanvasLoading.Instance.Show();
    /*    APIManager.CreateInstructorDeck((response) => 
        {

            if(response.success)
            {
                Show(response.deck);
            }
            else
            {
                PopupDialog.Instance.Show("Something went wrong while creating a new Instructor Deck. Please check your network connection and try again later.");
            }
            CanvasLoading.Instance.Hide();

            DeckListUpdated?.Invoke();
        });*/
    }

    

}
