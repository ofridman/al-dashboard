﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupDialogWindow : UIWindow
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public static event System.Action OnPopupDialogClosed;

    public Transform buttonsContainer;
    public TextMeshProUGUI messageLabel, titleLabel;

    public GameObject greenButton, plainButton, redButton;

    public void SetMessage(PopupDialog.PopupDialogMessage message)
    {
        messageLabel.text = message.body;
        titleLabel.text = message.title;

        //Clear any existing buttons before adding new ones
        foreach (Transform child in buttonsContainer)
            Destroy(child.gameObject);

        int i = 0;
        
        foreach (PopupDialog.PopupButton button in message.buttons)
        {
            GameObject btnPrefab = button.buttonColor == PopupDialog.PopupButtonColor.Plain ? plainButton : (button.buttonColor == PopupDialog.PopupButtonColor.Green ? greenButton : redButton);

            GameObject btnObj = Instantiate(btnPrefab, buttonsContainer);

            if(i == 0)
            {
                EventSystem.current.SetSelectedGameObject(btnObj);
            }

            Button btn = btnObj.GetComponent<Button>();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = button.text;

            btn.onClick.RemoveAllListeners();
            PopupDialog.PopupButton pbtn = button; //make sure scope value becomes global value before calling it inside callback
            btn.onClick.AddListener(() => 
            {
                pbtn.onClicked?.Invoke();
                Close(); //This makes sure that no matter what, every button will close the popup when its clicked
                OnPopupDialogClosed?.Invoke();
            });
            
            
            i++;
        }
    }
}
