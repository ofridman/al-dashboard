/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using GameBrewStudios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Events : MenuScreen
{
    [SerializeField]
    Transform dayContainer;

    public static event System.Action<DateTime> OnSelectionChanged;

    public static DateTime Today;
    public static DateTime SelectedDay;
    public static DateTime CurrentMonth;

    public List<CalendarEntry> calendarDays;

    [SerializeField]
    TextMeshProUGUI monthYearLabel;

    [SerializeField]
    GameObject template;

    [SerializeField]
    Window_DayView dayViewWindow;

    [SerializeField]
    Window_TeamEventEditor teamEventEditor;

 //   public List<TeamEvent> teamEvents;

    public static Screen_Events Instance;

    private void Start()
    {
        Instance = this;
    }

    public override void Show()
    {
        template.gameObject.SetActive(false);

        CanvasLoading.Instance.Show();
        
        /*APIManager.ListTeamEvents(User.current.selectedMembership.team._id, (r) =>
        {
            CanvasLoading.Instance.Hide();
            if (r.success)
            {
                teamEvents = r.events.ToList();
                base.Show();

                CalendarEntry.OnDaySelected += this.CalendarEntry_OnDaySelected;
                
                InitCalendar();
            }
            else
            {
                Debug.LogError("LIST TEAM EVENTS FAILED");
            }
        });*/

        
    }


    public void EditEvents()
    {
        dayViewWindow.Show();
    }

    [SerializeField]
    Button editEventsButton;

    [SerializeField]
    TextMeshProUGUI editEventsDayLabel;

    private void CalendarEntry_OnDaySelected(CalendarEntry obj)
    {
        
        SelectedDay = obj.date;

        editEventsDayLabel.text = SelectedDay.ToString("dd");
        
        OnSelectionChanged?.Invoke(SelectedDay);

        BuildCalendar(SelectedDay.Month, SelectedDay.Year);
    }

    public override void Hide()
    {
        CalendarEntry.OnDaySelected -= this.CalendarEntry_OnDaySelected;
        base.Hide();
    }

    [ContextMenu("Init Calendar")]
    public void InitCalendar()
    {
        CurrentMonth = SelectedDay = Today = DateTime.UtcNow;
        
        BuildCalendar(Today.Month, Today.Year);
    }

    public void BuildCalendar(int month, int year)
    {
        DateTime moment = new System.DateTime(year, month, 1);

        monthYearLabel.text = moment.ToString("MMM yyyy");

        DayOfWeek firstDayOfMonth = moment.DayOfWeek;
        
        ClearVisibleCalendarObjects();

        for(int i = 0; i < 42; i++)
        {
            GameObject obj = Instantiate(template, dayContainer);
            obj.SetActive(true);
            CalendarEntry entry = obj.GetComponent<CalendarEntry>();

            double daysToAdd = (double)(i - firstDayOfMonth);

            DateTime day = moment.AddDays(daysToAdd);

            string dayText = "";
            if(i <= 6)
            {
                dayText = day.ToString("ddd");
            }

            //entry.Init(day, null, dayText);
            Debug.Log("Created Day Entry: " + day.ToString());

            bool isToday = day.Year == Today.Year && day.DayOfYear == Today.DayOfYear;

            TooltipTrigger tooltip = obj.AddComponent<TooltipTrigger>();
            tooltip.text = "<b>" + day.ToString("dddd, MMM dd, yyyy") + "</b>\n<size=17>" + (isToday ? "Today" : "") + "\n";

            obj.transform.Find("Event1").GetComponent<TextMeshProUGUI>().text =
                obj.transform.Find("Event2").GetComponent<TextMeshProUGUI>().text =
            obj.transform.Find("Event3").GetComponent<TextMeshProUGUI>().text = "";

          /*  TeamEvent[] todaysEvents = teamEvents.FindAll(x => x.startDateTime.Month == day.Month && x.startDateTime.Day == day.Day).ToArray();
            if (todaysEvents.Length > 0)
            {
                for (int x = 0; x < todaysEvents.Length; x++)
                {
                    if (x < 3)
                    {
                        TextMeshProUGUI label = obj.transform.Find("Event" + (x + 1)).GetComponent<TextMeshProUGUI>();
                        label.text = todaysEvents[x].startDateTime.ToString("hh:mm tt") + " " + todaysEvents[x].name + "\n";

                    }
                    tooltip.text += todaysEvents[x].startDateTime.ToString("hh:mm tt") + " <i>" + todaysEvents[x].name + "</i>\n";
                }
            }
            else
            {
                tooltip.text += "<i>No events.</i>\n";
            }*/

            if (day.Month == SelectedDay.Month && day.Year == SelectedDay.Year && day.Day == SelectedDay.Day)
                tooltip.text += "\n<color=#28A745>Click the '+' at the top right of your screen to create a new event.</color></size>";
            entry.ToggleHighlight(SelectedDay == day);
        }


    }

    public void ChangeMonth(int dir)
    {
        CurrentMonth = CurrentMonth.AddMonths(dir);

        BuildCalendar(CurrentMonth.Month, CurrentMonth.Year);
    }


    void ClearVisibleCalendarObjects()
    {
        foreach (Transform child in dayContainer)
        {
            if(child.gameObject.activeSelf)
                Destroy(child.gameObject);
        }
    }

    public void CreateEvent()
    {
        DateTime now = SelectedDay.AddHours(8);
        now.AddSeconds(-now.Second);

        float mins = now.Minute / 30f;
        mins = Mathf.Round(mins);
        now.AddMinutes(-now.Minute);
        now.AddMinutes(mins);

        /*teamEventEditor.Show(new TeamEvent() 
        { 
            name = User.current.displayName + "'s Event",
            details = "",
            location = "",
            allDayEvent = false,
            creator = User.current.selectedMembership._id,
            team = User.current.selectedMembership.team._id,
            attendees = new TeamEventInvitation[] {  },
            startDate = now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
            endDate = now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")

        });*/
    }

    public void DeleteEvent()
    {
        
    }

    
}
