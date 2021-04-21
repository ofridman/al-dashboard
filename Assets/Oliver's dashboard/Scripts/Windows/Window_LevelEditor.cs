using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using GameBrewStudios.Networking;
using GameBrewStudios;
using System.Linq;

public class Window_LevelEditor : UIWindow
{
    [SerializeField]
    TMP_InputField levelNameField, levelDescriptionField;


    [SerializeField]
    ScrollRect lessonsScrollRect, quizzesScrollRect;

    [SerializeField]
    GameObject lessonListPrefab, quizListPrefab;


    [SerializeField]
    Window_LessonEditor lessonEditorWindow;

    [SerializeField]
    Window_QuizList quizPickerWindow;


    public override void Show()
    {
        /*levelNameField.SetTextWithoutNotify(string.IsNullOrEmpty(InteractiveLevel.current.name) ? " " : InteractiveLevel.current.name);
        levelDescriptionField.SetTextWithoutNotify(string.IsNullOrEmpty(InteractiveLevel.current.description) ? " " : InteractiveLevel.current.description);*/

        SetUpEventListeners();

        PopulateLists();

        base.Show();

        Window_LessonEditor.OnLessonUpdated += this.OnLessonUpdated;
    }

    public override void Close()
    {
        Window_LessonEditor.OnLessonUpdated -= this.OnLessonUpdated;
        base.Close();
    }

    private void OnDestroy()
    {
        Window_LessonEditor.OnLessonUpdated -= this.OnLessonUpdated;
    }

    private void OnLessonUpdated()
    {
        PopulateLists();
    }

    void SetUpEventListeners()
    {
        levelNameField.onEndEdit.RemoveAllListeners();
  /*      levelNameField.onEndEdit.AddListener((text) =>
        {
            if (InteractiveLevel.current.name != levelNameField.text)
            {
                InteractiveLevel.current.name = levelNameField.text;
                UpdateLevel();
            }
        });*/

        levelDescriptionField.onEndEdit.RemoveAllListeners();
      /*  levelDescriptionField.onEndEdit.AddListener((text) =>
        {
            if (InteractiveLevel.current.description != levelDescriptionField.text)
            {
                InteractiveLevel.current.description = levelDescriptionField.text;
                UpdateLevel();
            }
        });*/
    }

    void UpdateLevel()
    {
        CanvasLoading.Instance.Show();
        /*APIManager.UpdateLevel(InteractiveLevel.current, (level) =>
        {
            if(level == null)
            {
                PopupDialog.Instance.Show("Something went wrong. Please try again.");
                return;
            }

            this.Show();

            CanvasLoading.Instance.Hide();
        });*/
    }

    void PopulateLists()
    {
        CanvasLoading.Instance.Show();
       /* APIManager.GetLevel(InteractiveLevel.current._id, (level) =>
        {
            if (level == null)
            {
                CanvasLoading.Instance.Hide();
                this.Close();

                PopupDialog.Instance.Show("An error occured while trying to retrieve data for this Level from the server. Please try again.");
                return;
            }

            PopulateLessonList(level.lessons);
            PopulateQuizList(level.quizzes);
            CanvasLoading.Instance.Hide();
        });*/
    }

    /*void PopulateLessonList(InteractiveLesson[] lessons)
    {
        foreach (Transform child in lessonsScrollRect.content)
            Destroy(child.gameObject);

        int index = 1;
        foreach (InteractiveLesson lesson in lessons)
        {
            GameObject obj = Instantiate(lessonListPrefab, lessonsScrollRect.content);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                InteractiveLesson.current = lesson;
                lessonEditorWindow.Show();
            });

            Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.onClick.AddListener(() =>
            {
                RemoveLessonPrompt(InteractiveLevel.current, lesson);
            });

            TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
            nameLabel.text = string.Format("Lesson {0}: {1}", index, lesson.name);

            index++;
        }
    }*/

    /*void PopulateQuizList(InteractiveQuiz[] quizzes)
    {
        foreach (Transform child in quizzesScrollRect.content)
            Destroy(child.gameObject);

        foreach (InteractiveQuiz quiz in quizzes)
        {
            GameObject obj = Instantiate(quizListPrefab, quizzesScrollRect.content);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                PopupDialog.Instance.Show("Please visit the Quizzes tab on the main page to edit this quiz.");
            });

            Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
            deleteBtn.onClick.RemoveAllListeners();
            deleteBtn.onClick.AddListener(() =>
            {
                RemoveQuizPrompt(InteractiveLevel.current, quiz);
            });

            TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
            nameLabel.text = "Quiz: " + quiz.name + " (" + (quiz.questions != null ? quiz.questions.Length : 0) + " questions)";
        }
    }

    void RemoveLessonPrompt(InteractiveLevel level, InteractiveLesson lesson)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            RemoveLessonFromLevel(level, lesson);
            return;
        }
        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                RemoveLessonFromLevel(level, lesson);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete Lesson?", "Deleting this lesson will permanently remove it. This operation cannot be undone. Are you sure you want to delete it?", buttons.ToArray());

    }

    void RemoveLessonFromLevel(InteractiveLevel level, InteractiveLesson lesson)
    {
        List<InteractiveLesson> lessons = level.lessons.ToList();
        lessons.RemoveAll(x => x._id == lesson._id);
        level.lessons = lessons.ToArray();

        CanvasLoading.Instance.Show();
        APIManager.UpdateLevel(level, (lvl) => 
        {
            if(lvl != null)
            {
                Debug.Log("Lesson successfully removed from Level, now deleting the lesson in the database");
                CanvasLoading.Instance.Show();
                APIManager.DeleteLesson(lesson, (success) =>
                {
                    if(success)
                    {
                        Debug.Log("Lesson successfully deleted from database");
                        PopulateLists();
                    }

                    CanvasLoading.Instance.Hide();
                });
            }

            CanvasLoading.Instance.Hide();
        });

        
    }


    void RemoveQuizPrompt(InteractiveLevel level, InteractiveQuiz quiz)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            RemoveQuizFromLevel(level, quiz);
            return;
        }
        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                RemoveQuizFromLevel(level, quiz);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really detach Quiz from this Level?", "Detaching this quiz from the level will also unassign it from  any lesson it might be assigned to. Are you sure you want to continue?", buttons.ToArray());

    }

    int lessonUpdatesReceived = 0;

    void RemoveQuizFromLevel(InteractiveLevel level, InteractiveQuiz quiz)
    {

        CanvasLoading.Instance.Show();
        List<InteractiveQuiz> quizzes = level.quizzes.ToList();

        quizzes.RemoveAll(q => q._id == quiz._id);

        level.quizzes = quizzes.ToArray();

        APIManager.UpdateLevel(level, (lvl) =>
        {
            if (lvl == null)
            {
                PopupDialog.Instance.Show("There was a problem while updating the level on the server. Try closing the Edit Level window and opening it again to refresh.");
            }

            int lessonsModified = lessonUpdatesReceived = 0;


            //Go through every lesson any remove any references to the quiz that we deleted to prevent errors 
            for (int x = 0; x < level.lessons.Length; x++)
            {
                List<CommandEvent> cmds = level.lessons[x].onComplete.ToList();
                int removedCount = cmds.RemoveAll(ce => ce.eventName.ToLower() == "doquiz" && ce.args != null && ce.args.Length > 0 && ce.args[0].Equals(quiz._id));

                Debug.Log("Removed " + removedCount + " DoQuiz commands from Lesson ID " + level.lessons[x]._id + " because they matched the Quiz ID " + quiz._id + " being removed from Level " + level._id);
                if (removedCount > 0)
                    lessonsModified++;

                level.lessons[x].onComplete = cmds.ToArray();

                CanvasLoading.Instance.Show();
                APIManager.UpdateLesson(level.lessons[x], (lesson) =>
                {
                    lessonUpdatesReceived++;

                    if(lessonUpdatesReceived >= lessonsModified)
                    {
                        PopulateLists();
                    }

                    CanvasLoading.Instance.Hide();
                });
            }

            CanvasLoading.Instance.Hide();
        });
    }
    */
    public void AddQuiz()
    {
        //Pick an existing quiz from the quiz list to add a reference to it on this level so it can be used in the lessons.

      /*  quizPickerWindow.Show((selectedQuiz) =>
        {
        /*    List<InteractiveQuiz> quizzes = InteractiveLevel.current.quizzes.ToList();

            //Only add the selected quiz to the level if it has not already been added to this level
            if (!quizzes.Any(x => x._id == selectedQuiz._id))
            {
                quizzes.Add(selectedQuiz);
            }

            InteractiveLevel.current.quizzes = quizzes.ToArray();*/
            UpdateLevel();
        //});
    }


    public void AddLesson()
    {
        //Creates a new InteractiveLesson on the server, and then adds it to this level to make it available in the lesson editor.

        CanvasLoading.Instance.Show();
    /*    InteractiveLesson newLesson = new InteractiveLesson()
        {
            name = "New Lesson",
            description = "",
            data = "",
        };

        APIManager.CreateLesson(newLesson, (lesson) =>
        {
            if (lesson == null)
            {
                PopupDialog.Instance.Show("Something went wrong while creating a Lesson. Please try again.");
                CanvasLoading.Instance.Hide();
                return;
            }

            List<InteractiveLesson> lessons = InteractiveLevel.current.lessons.ToList();
            lessons.Add(lesson);
            InteractiveLevel.current.lessons = lessons.ToArray();
            UpdateLevel();

            CanvasLoading.Instance.Hide();
        });*/
    }
}
