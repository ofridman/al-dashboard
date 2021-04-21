using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarEntry : MonoBehaviour
{

    public static event System.Action<CalendarEntry> OnDaySelected;

    public DateTime date;
    //public TeamEvent[] events;
    private float lastClickTime = 0f;

    [SerializeField]
    TextMeshProUGUI dateLabel;

    [SerializeField]
    Image background;

    [SerializeField]
    Color defaultColor, todayColor;

    [SerializeField]
    GameObject outlineObj;

    private void OnEnable()
    {
        Screen_Events.OnSelectionChanged += this.Screen_Events_OnSelectionChanged;
    }

    private void OnDisable()
    {
        Screen_Events.OnSelectionChanged -= this.Screen_Events_OnSelectionChanged;
    }

    private void Screen_Events_OnSelectionChanged(DateTime selectedDate)
    {
        ToggleHighlight(selectedDate == date);
    }

    public void ToggleHighlight(bool show)
    {
        if(show)
            Debug.Log("Highlighting " + date.ToString(), this.gameObject);

        outlineObj.SetActive(show);
    }

  /*  public void Init(DateTime date, TeamEvent[] events, string dayText)
    {
        this.date = date;
        this.events = events;

        dateLabel.text = dayText + " " + date.Day.ToString();
        bool dateIsToday = DateTime.UtcNow.DayOfYear == date.DayOfYear && DateTime.UtcNow.Year == date.Year;
        ToggleHighlight(dateIsToday);

        background.color = dateIsToday ? todayColor : defaultColor;

        if (date.Month != Screen_Events.CurrentMonth.Month)
        {
            background.color = new Color(background.color.r, background.color.g, background.color.b, 0.5f);
            dateLabel.color /= 2;
        }

        if(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            background.color *= 0.5f;
        }
    }*/

    public void OnClicked()
    {
        OnDaySelected?.Invoke(this);
    }

    
}
