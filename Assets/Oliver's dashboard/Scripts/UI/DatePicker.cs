using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
//using UnityEngine.UI.Extensions;

public class DatePicker : MonoBehaviour
{
    public static event System.Action<DateTime> OnDateSelectionFinished;

    public static DatePicker Instance;

    public static DateTime Today;
    public static DateTime SelectedDay;
    public static DateTime CurrentMonth;

    [SerializeField]
    TextMeshProUGUI monthYearLabel;

    [SerializeField]
    GameObject template;

    [SerializeField]
    Transform container;

    [SerializeField]
    GameObject window;
    [SerializeField]
    GameObject blocker;


    [SerializeField]
    Color defaultColor, todayColor;

    public Stepper hourStepper, minuteStepper, amPMStepper;

    public TMP_InputField hourField, minuteField;
    public TextMeshProUGUI ampmLabel;

    private void Awake()
    {
        Instance = this;
        CurrentMonth = SelectedDay = Today = DateTime.UtcNow;

        blocker.SetActive(false);
        window.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        template.SetActive(false);

    }

    /// <summary>
    /// Show the datepicker with today's date preselected
    /// </summary>
    public void Show(Vector2 screenPosition)
    {
        RectTransform rt = window.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt.GetParentCanvas().GetComponent<RectTransform>(), screenPosition, Camera.main, out Vector2 localPoint);
        rt.localPosition = localPoint;

        blocker.SetActive(true);
        window.SetActive(true);
        InitCalendar();

        hour = hourStepper.value = 8;
        minute = minuteStepper.value = 0;
        ampm = amPMStepper.value = 0;
    }


    /// <summary>
    /// Show the datepicker with a predetermined date selected
    /// </summary>
    /// <param name="date"></param>
    public void Show(Vector2 screenPosition, DateTime date)
    {
        SelectedDay = date;

        RectTransform rt = window.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt.GetParentCanvas().GetComponent<RectTransform>(), screenPosition, Camera.main, out Vector2 localPoint);
        rt.localPosition = localPoint;

        blocker.SetActive(true);
        window.SetActive(true);
        BuildCalendar(date.Month, date.Year);

        hour = hourStepper.value = date.Hour;
        minute = minuteStepper.value = date.Minute;
        ampm = date.Hour >= 12 ? 1 : 0;
    }

    public void Close()
    {
        blocker.SetActive(false);
        window.SetActive(false);
    }

    [ContextMenu("Init Calendar")]
    public void InitCalendar()
    {
        BuildCalendar(Today.Month, Today.Year);
    }

    public void BuildCalendar(int month, int year)
    {
        DateTime moment = new System.DateTime(year, month, 1);

        monthYearLabel.text = moment.ToString("MMM yyyy");

        DayOfWeek firstDayOfMonth = moment.DayOfWeek;

        ClearVisibleCalendarObjects();

        for (int i = 0; i < 42; i++)
        {
            GameObject obj = Instantiate(template, container);
            obj.SetActive(true);

            double daysToAdd = (double)(i - firstDayOfMonth);

            DateTime day = moment.AddDays(daysToAdd);

            string dayText = "";
            if (i <= 6)
            {
                dayText = day.ToString("ddd")[0] + " ";
            }

            dayText += day.Day;


            TextMeshProUGUI dateLabel = obj.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            dateLabel.text = dayText;

            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                OnDateSelected(day);
            });



            bool dateIsToday = DateTime.UtcNow.DayOfYear == day.DayOfYear && DateTime.UtcNow.Year == day.Year;

            bool isSelected = day.Month == SelectedDay.Month && day.Day == SelectedDay.Day && SelectedDay.Year == day.Year;

            btn.image.color = isSelected ? todayColor * 1.15f : (dateIsToday ? todayColor : defaultColor);

            if (day.Month != CurrentMonth.Month)
            {
                btn.image.color = new Color(btn.image.color.r, btn.image.color.g, btn.image.color.b, 0.5f);
                dateLabel.color /= 2;
            }

            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
            {
                btn.image.color *= 0.5f;
            }


        }


    }


    
    public void OnDateSelected(DateTime date)
    {
        SelectedDay = date;
        BuildCalendar(SelectedDay.Month, SelectedDay.Year);
    }

    public void ChangeMonth(int dir)
    {
        CurrentMonth = CurrentMonth.AddMonths(dir);

        BuildCalendar(CurrentMonth.Month, CurrentMonth.Year);
    }


    void ClearVisibleCalendarObjects()
    {
        foreach (Transform child in container)
        {
            if (child.gameObject.activeSelf)
                Destroy(child.gameObject);
        }
    }

    


    public void SetSelectedHour(string hour)
    {
        Debug.LogWarning("HOUR SET TO: " + hour);
        this.hour = int.Parse(hour);
        

        if(ampm > 0)
        {
        }
    }

    public void SetSelectedMinutes(string minutes)
    {
        Debug.LogWarning("MINUTES SET TO: " + minutes);
        this.minute = int.Parse(minutes);
    }

    public void OnFinishedButtonClicked()
    {
        OnDateSelectionFinished?.Invoke(CreateDate());
        Close();
    }

    int hour = 8, minute = 0, ampm = 0;


    public DateTime CreateDate()
    {
        DateTime d = new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day, hour , minute, 0, DateTimeKind.Utc);
        
        return d;
    }
}
