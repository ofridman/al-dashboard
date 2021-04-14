﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDialog : MonoBehaviour
{
    [ContextMenu("Test Message 1")]
    public void TestMessage1()
    {
        Show("ATTENTION:", "This is a public service announcement from the CDC. An outbreak of a deadly contagion has been discovered in your area. Please stay indoors.");
    }

    [ContextMenu("Test Message 2")]
    public void TestMessage2()
    {
        Show("An Error has Occurred!", "Sorry, an error occurred. Please try again later.", new PopupButton[] { new PopupButton() { buttonColor = PopupButtonColor.Red, text = "Delete Yourself" } });
    }

    [ContextMenu("Test Message 3")]
    public void TestMessage3()
    {
        Show("Delete File?", "Are you sure you want to delete \"MyCompanyLogo.png\"?", new PopupButton[] { new PopupButton() { buttonColor = PopupButtonColor.Red, text = "Delete It" }, new PopupButton() { text = "Cancel", buttonColor = PopupButtonColor.Plain} });
    }

    public static PopupDialog Instance;

    public enum PopupButtonColor
    {
        Plain,
        Green,
        Red
    }

    public struct PopupButton
    {
        public string text;
        public PopupButtonColor buttonColor;
        public System.Action onClicked;
    }

    public struct PopupDialogMessage
    {
        public string title;
        public string body;
        public PopupButton[] buttons;
    }

    public CanvasGroup canvasGroup;

    public PopupDialogWindow dialogWindow;
    

    public static bool isShowing
    {
        get {
            return Instance.dialogWindow.gameObject.activeSelf;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        PopupDialogWindow.OnPopupDialogClosed += this.OnPopupDialogClosed;
    }

    private void OnPopupDialogClosed()
    {
        if (messages != null && messages.Count > 0)
        {
            PopupDialogMessage message = messages[0];
            messages.RemoveAt(0);
            ShowMessage(message);
        }
        else
        {
            Close();
        }
    }

    public void Show(string body)
    {
        Show("", body);
    }

    public void Show(string title, string body)
    {
        Show(title, body, null);
    }

    public void Show(string title, string body, PopupButton[] buttons)
    {
        if (buttons == null)
        {
            buttons = new PopupButton[]
            {
                new PopupButton() { text = "Okay", buttonColor = PopupButtonColor.Plain }
            };
        }

        PopupDialogMessage pdm = new PopupDialogMessage() { title = title, body = body, buttons = buttons };

        if(isShowing)
        {
            Debug.Log("Queing message because one is already showing.");
            QueueMessage(pdm);
            return;
        }

        Debug.Log($"<color=lime>Showing PopupDialog: {body}</color>");
        ShowMessage(pdm);
    }


    void QueueMessage(PopupDialogMessage message)
    {
        messages.Add(message);
    }

    void ShowMessage(PopupDialogMessage message)
    {
        this.canvasGroup.alpha = 1f;
        this.canvasGroup.interactable = true;
        this.canvasGroup.blocksRaycasts = true;
        dialogWindow.SetMessage(message);
        dialogWindow.Show();
    }

    static List<PopupDialogMessage> messages = new List<PopupDialogMessage>();


    public float stuckTime = 0f;

    private void Update()
    {
        if(!this.dialogWindow.gameObject.activeSelf && canvasGroup.alpha > 0f)
        {
            stuckTime += Time.deltaTime;
        }

        if(stuckTime > 1f)
        {
            Close();
            stuckTime = 0f;
        }
    }

    public void Close()
    {
        this.canvasGroup.alpha = 0f;
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }


    public void ToggleCanvasGroup(bool active)
    {
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
        if (active)
        {
            canvasGroup.DOFade(1f, 0.25f);
        }
        else
        {
            canvasGroup.DOFade(0f, 0.25f);
        }
    }
}
