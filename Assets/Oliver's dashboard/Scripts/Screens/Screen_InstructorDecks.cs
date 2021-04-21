using GameBrewStudios;
//using GameBrewStudios.Networking;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Screen_InstructorDecks : MenuScreen
{
    [SerializeField]
    ScrollRect deckListScrollRect;

    [SerializeField]
    GameObject listEntryPrefab;

    [SerializeField]
    Window_InstructorDeckEditor deckEditor;

   // private InteractiveInstructorDeck[] decks;

    public override void Show()
    {
        base.Show();

        PopulateDeckList();

        Window_InstructorDeckEditor.DeckListUpdated += this.OnDeckListUpdated;
    }

    public override void Hide()
    {
        Window_InstructorDeckEditor.DeckListUpdated -= this.OnDeckListUpdated;
        base.Hide();
    }


    private void OnDeckListUpdated()
    {
        PopulateDeckList();
    }


    public void PopulateDeckList()
    {

        CanvasLoading.Instance?.Show();
        foreach (Transform child in deckListScrollRect.content)
            Destroy(child.gameObject);
       /* APIManager.ListInstructorDecks((response) =>
        {
            if (response.success)
            {
                this.decks = response.decks;

                int i = 1;
                foreach (InteractiveInstructorDeck deck in this.decks)
                {
                    GameObject obj = Instantiate(listEntryPrefab, deckListScrollRect.content);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString("n0") + ".) " + deck.name;

                    Button btn = obj.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() =>
                    {
                        
                        deckEditor.Show(deck);
                    });

                    Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                    deleteBtn.onClick.RemoveAllListeners();
                    deleteBtn.onClick.AddListener(() =>
                    {
                        DeleteDeckPrompt(deck);
                    });

                    i++;
                }
            }
            CanvasLoading.Instance?.Hide();
        });*/

    }

    /*public void DeleteDeckPrompt(InteractiveInstructorDeck deck)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeleteDeck(deck);
            return;
        }

        List<PopupDialog.PopupButton> buttons = new List<PopupDialog.PopupButton>()
        {
            new PopupDialog.PopupButton() {text = "Yes, Delete it", buttonColor = PopupDialog.PopupButtonColor.Red, onClicked = () =>
            {
                DeleteDeck(deck);
            }},
            new PopupDialog.PopupButton() {text = "Cancel", buttonColor = PopupDialog.PopupButtonColor.Plain }
        };

        PopupDialog.Instance.Show("Really delete Deck?", "This deck will be deleted permanently. This action cannot be undone. Are you sure you want to continue?", buttons.ToArray());
    }

    void DeleteDeck(InteractiveInstructorDeck deck)
    {
        CanvasLoading.Instance.Show();

        APIManager.DeleteInstructorDeck(deck._id, (response) =>
        {
            if (!response.success)
            {
                Debug.LogError("An error occurred on the server while deleting the deck.");
            }

            this.OnDeckListUpdated();
            CanvasLoading.Instance.Hide();
        });
    }*/

    public void AddDeck()
    {
        CanvasLoading.Instance.Show();
      /*  APIManager.CreateInstructorDeck((response) =>
        {
            if (!response.success)
            {
                PopupDialog.Instance.Show("Something went wrong while creating a new deck. Please try again.");
                return;
            }

            PopulateDeckList();
            CanvasLoading.Instance.Hide();
        });*/
    }


}
