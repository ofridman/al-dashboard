using GameBrewStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.UI.Extensions;
//using GameBrewStudios.Networking;

public class Window_DayView : UIWindow
{
    public Sprite blockSprite;

    

    public Transform container;

    public GameObject template;

    public TextMeshProUGUI titleText;

    [SerializeField]
    Window_TeamEventEditor eventEditorWindow;


    public override void Show()
    {
        Window_TeamEventEditor.OnTeamEventEditorClosed += this.Window_TeamEventEditor_OnTeamEventEditorClosed;
        Populate();
        base.Show();


        DOVirtual.DelayedCall(0.15f, () => { 
            Refresh();
        });

    }

    private void Window_TeamEventEditor_OnTeamEventEditorClosed()
    {
        CanvasLoading.Instance.Show();
        /*APIManager.ListTeamEvents(User.current.selectedMembership.team._id, (response) => 
        {
            CanvasLoading.Instance.Hide();
            if (response.success && response.events != null)
            {
                Screen_Events.Instance.teamEvents = response.events.ToList();
                Populate();
                Refresh();
            }
        });*/
        
    }

    public override void Close()
    {
        Window_TeamEventEditor.OnTeamEventEditorClosed -= this.Window_TeamEventEditor_OnTeamEventEditorClosed;
        base.Close();
    }

    void Populate()
    {
        foreach (Transform child in container)
        {
            if (child.gameObject.activeSelf || child.gameObject.name.StartsWith("BLOCKOBJ"))
                Destroy(child.gameObject);
        }


        for (int i = 0; i < 24; i++)
        {
            GameObject obj = Instantiate(template, container);
            obj.SetActive(true);
            obj.name =
            obj.GetComponentInChildren<TextMeshProUGUI>().text = i == 0 ? "12a" : (i - 12 > 0 ? (i - 12).ToString() + "p" : i.ToString() + "a");

            RectTransform top = obj.transform.Find("TopBlock").GetComponent<RectTransform>();
            RectTransform bottom = obj.transform.Find("BottomBlock").GetComponent<RectTransform>();

            Image img = obj.GetComponent<Image>();
            if (i == 0 || i % 2 == 0)
                img.color *= 0.9f;

            timeBlocks.Add(top);
            timeBlocks.Add(bottom);
        }
    }

    public void Refresh()
    {
        titleText.text = "Events for " + Screen_Events.SelectedDay.ToString("MM/dd/yyyy");


    /*    TeamEvent[] selectedDaysEvents = Screen_Events.Instance.teamEvents.FindAll(x => Screen_Events.SelectedDay.IsBetween(x.startDateTime, x.endDateTime, false)).ToArray();//User.current.selectedMembership.team.events.ToList().FindAll(x => Screen_Events.SelectedDay.IsBetween(x.startDateTime, x.endDateTime, false)).ToArray();

        for (int i = 0; i < selectedDaysEvents.Length; i++)
        {
            RectTransform upperLeft = GetBlockByTime(selectedDaysEvents[i].startDateTime);
            RectTransform lowerRight = GetBlockByTime(selectedDaysEvents[i].endDateTime.AddMinutes(-1));

            Image img = CreateBlockObject(selectedDaysEvents[i], upperLeft, lowerRight);
            if(img != null)
                AttachEventText(img, selectedDaysEvents[i]);
        }*/

    }

    [SerializeField]
    List<RectTransform> timeBlocks = new List<RectTransform>();

    public RectTransform GetBlockByTime(DateTime time)
    {
        RectTransform foundBlock = timeBlocks[0];

        if (time.DayOfYear != Screen_Events.SelectedDay.DayOfYear)
        {
            //event carries over to tomorrow, return last block
            return timeBlocks[timeBlocks.Count - 1];
        }

        // there are 48 - 30 minute time blocks in a day
        for (int i = 0; i < timeBlocks.Count; i++)
        {
            int hour = Mathf.FloorToInt((float)i / 2f);

            Debug.Log("Hour == hour? " + time.Hour + " = " + hour);

            if (time.Hour == hour)
            {
                bool halfHourCheck = time.Minute >= 30;

                Debug.Log(halfHourCheck + "  30 <= " + time.Minute);

                if (halfHourCheck)
                {
                    foundBlock = timeBlocks[i + 1];
                }
                else
                {
                    foundBlock = timeBlocks[i];
                }

                break;
            }
        }

        return foundBlock;
    }

 /*   public void AttachEventText(Image parent, TeamEvent eventData)
    {
        GameObject textChild = new GameObject("Event Name");
        RectTransform tcRT = textChild.AddComponent<RectTransform>();
        tcRT.SetParent(parent.transform, false);

        RectTransform parentTransform = tcRT.parent.GetComponent<RectTransform>();
        tcRT.anchorMin = new Vector2(0f, 0f);
        tcRT.anchorMax = new Vector2(1f, 1f);
        tcRT.pivot = new Vector2(0.5f, 0.5f);
        TextMeshProUGUI label = tcRT.gameObject.AddComponent<TextMeshProUGUI>();
        label.text = eventData.name;

        tcRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 8f, parentTransform.rect.height - 16f);
        tcRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 8f, parentTransform.rect.width - 16f);



        //tcRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, parent.rectTransform.sizeDelta.x);
        //tcRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, parent.rectTransform.sizeDelta.y);

    }

    public Image CreateBlockObject(TeamEvent teamEvent, RectTransform upperLeft, RectTransform lowerRight)
    {
        GameObject obj = new GameObject("BLOCKOBJ: " + teamEvent.startDateTime.ToString("HH:mm") + " --- " + teamEvent.endDateTime.ToString("HH:mm"));
        obj.SetActive(true);

        Button btn = obj.AddComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            //Debug.LogError("ADD CODE HERE TO OPEN EVENT EDITOR WINDOW");

            eventEditorWindow.Show(teamEvent);
        });

        Image img = obj.AddComponent<Image>();

        img.color = new Color(0f, 0f, 0.5f, 0.5f);

        //img.sprite = blockSprite;
        img.type = Image.Type.Sliced;


        LayoutElement layoutElement = obj.AddComponent<LayoutElement>();
        layoutElement.ignoreLayout = true;

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.SetParent(container, false);
        

        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(0f, 1f);

        rt.pivot = new Vector2(0f, 1f);

        //DOVirtual.DelayedCall(0.2f, () =>
        //{

        if (upperLeft == null || lowerRight == null) return null;

        Vector3[] topCorners = new Vector3[4];
        upperLeft.GetWorldCorners(topCorners);
        rt.position = topCorners[1];

        Vector3[] bottomCorners = new Vector3[4];
        lowerRight.GetWorldCorners(bottomCorners);

        Debug.Log("Aligning " + obj.name + " to " + upperLeft.transform.parent.gameObject.name + upperLeft.gameObject.name + " and " + lowerRight.transform.parent.gameObject.name + lowerRight.gameObject.name);
        upperLeft.GetComponent<Image>().color += Color.green * 0.1f;
        lowerRight.GetComponent<Image>().color += Color.red * 0.1f;

        float height = 100f * (float)(teamEvent.endDateTime - teamEvent.startDateTime).TotalHours;
        Debug.LogWarning("HEIGHT IS " + height);
        rt.sizeDelta = new Vector2(upperLeft.rect.width, height);
        //});
        rt.parent.GetComponent<RectTransform>().ForceUpdateRectTransforms();
        Canvas.ForceUpdateCanvases();
        rt.parent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();


        return img;
    }*/
}
