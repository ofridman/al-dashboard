using GameBrewStudios;
//using GameBrewStudios.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window_TeamEventEditor : UIWindow
{
    public static event System.Action OnTeamEventEditorClosed;

   // public TeamEvent teamEvent;

    [SerializeField]
    TMP_InputField nameField, locationField, detailsField;

    [SerializeField]
    Toggle allDayEvent;

    [SerializeField]
    TextMeshProUGUI startDateField, endDateField;

    [SerializeField]
    GameObject createEventSection;

 /*   public void Show(TeamEvent teamEvent)
    {
        this.teamEvent = teamEvent;
        PopulateFields();

        createEventSection.SetActive(string.IsNullOrEmpty(teamEvent._id));

        base.Show();
    }

    public override void Close()
    {
        base.Close();
        OnTeamEventEditorClosed?.Invoke();
    }

    public void PopulateFields()
    {
        nameField.SetTextWithoutNotify(teamEvent.name);
        nameField.onEndEdit.RemoveAllListeners();
        nameField.onEndEdit.AddListener(OnNameChanged);

        detailsField.SetTextWithoutNotify(teamEvent.details);
        detailsField.onEndEdit.RemoveAllListeners();
        detailsField.onEndEdit.AddListener(OnDetailsChanged);

        locationField.SetTextWithoutNotify(teamEvent.location);
        locationField.onEndEdit.RemoveAllListeners();
        locationField.onEndEdit.AddListener(OnLocationChanged);

        SetDate(startDateField, teamEvent.startDateTime);


        SetDate(endDateField, teamEvent.endDateTime);



        allDayEvent.SetIsOnWithoutNotify(teamEvent.allDayEvent);
        allDayEvent.onValueChanged.RemoveAllListeners();
        allDayEvent.onValueChanged.AddListener((isOn) =>
        {
            endDateField.gameObject.SetActive(!isOn);
            teamEvent.allDayEvent = isOn;

            Save();
        });
    }

    void SetDate(TextMeshProUGUI field, DateTime date)
    {
        DatePickerButton dpBtn = field.GetComponentInChildren<DatePickerButton>();
        dpBtn.RemoveAllListeners();
        dpBtn.AddListener((d) =>
        {
            field.text = d.ToString("MM/dd/yyyy hh:mm tt");
            if (field == startDateField)
            {
                
                teamEvent.startDate = d.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

                Debug.Log("COMPARING: " + d.ToString() + " TO " + teamEvent.endDateTime.ToString());
                int c = DateTime.Compare(d, teamEvent.endDateTime);
                Debug.Log(c);
                if (c > 0)
                {
                    Debug.Log("Adjusting end date too");
                    SetDate(endDateField, teamEvent.startDateTime.AddMinutes(30));
                }
            }
            else if (field == endDateField)
            {
                teamEvent.endDate = d.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

                Debug.Log("COMPARING: " + d.ToString() + " TO " + teamEvent.startDateTime.ToString());

                int c = DateTime.Compare(d, teamEvent.startDateTime);
                Debug.Log(c);
                if (c < 0)
                {
                    Debug.Log("Adjusting start date too");
                    SetDate(startDateField, teamEvent.endDateTime.AddMinutes(-30));
                }
            }

            Save();
        });

        if (date != null)
        {
            dpBtn.lastSelectedDate = date;
        }

        field.text = date.ToString("MM/dd/yyyy hh:mm tt");


    }

    public void OnNameChanged(string name)
    {
        bool isInvalid = string.IsNullOrEmpty(name) || name.Length < 3;
        nameField.textComponent.color = isInvalid ? Color.red : Color.black;
        teamEvent.name = name;

        Save();
    }

    public void OnDetailsChanged(string details)
    {
        teamEvent.details = details;
        Save();
    }

    public void OnLocationChanged(string location)
    {
        teamEvent.location = location;
        Save();
    }

    public void Save()
    {
        if (string.IsNullOrEmpty(teamEvent._id))
        {
            //NEW EVENT, DO NOTHING
        }
        else
        {

            Debug.Log("Updating team event on server..");
            //EXISTING EVENT, USE UPDATE METHOD
            CanvasLoading.Instance.Show();

            Debug.Log("Start/End Date before submit: " + teamEvent.startDate + " / " + teamEvent.endDate);

            APIManager.UpdateTeamEvent(teamEvent, (response) =>
            {
                CanvasLoading.Instance.Hide();
                if (response.success == false || response.teamEvent == null)
                {
                    Debug.LogError("UpdateTeamEvent REQUEST FAILED: success = " + response.success + "  teamEvent == null ? " + (teamEvent == null).ToString());
                    PopupDialog.Instance.Show("Something went wrong while updating your event with the server. Please try again.");
                    return;
                }

                teamEvent = response.teamEvent;

                Debug.Log("Start/End Date AFTER submit: " + teamEvent.startDate + " / " + teamEvent.endDate);
                PopulateFields();
            });
        }
    }

    public void DoCreateEvent()
    {
        //Validate that the necessary information is set up properly
        CanvasLoading.Instance.Show();
        APIManager.CreateTeamEvent(teamEvent, (r) => 
        {
            CanvasLoading.Instance.Hide();

            if(!r.success || r.teamEvent == null || string.IsNullOrEmpty(r.teamEvent._id))
            {
                Debug.LogError("Failed to create team event");
                PopupDialog.Instance.Show("Oops! Something went wrong while scheduling your event. Please try again.");
                return;

            }

            this.Close();
            PopupDialog.Instance.Show("Your event (" + teamEvent.name + ") has been successfully scheduled for " + teamEvent.startDateTime.ToString("MMMM dd, yyyy") + " at " + teamEvent.startDateTime.ToString("H:mm tt") + ".");

            Screen_Events.Instance.Show();
        });

    }*/
}
