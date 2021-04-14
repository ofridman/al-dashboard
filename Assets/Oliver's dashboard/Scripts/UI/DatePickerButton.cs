using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DatePickerButton : MonoBehaviour
{
    Button btn;

    public DateTime lastSelectedDate;

    public event System.Action<DateTime> OnDateChanged;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => 
        {

            if(DatePicker.Instance != null)
            {
                
                //Set up one time listener that will store the selected date for this button
                System.Action<DateTime> oneTimeListener = null;

                oneTimeListener = (d) => 
                {
                    DatePicker.OnDateSelectionFinished -= oneTimeListener;
                    lastSelectedDate = d;
                    
                    OnDateChanged?.Invoke(d);
                };
                DatePicker.OnDateSelectionFinished += oneTimeListener;


                if(lastSelectedDate == null)
                    DatePicker.Instance.Show(RectTransformUtility.WorldToScreenPoint(Camera.main, btn.transform.position));
                else
                {
                    DatePicker.Instance.Show(RectTransformUtility.WorldToScreenPoint(Camera.main, btn.transform.position), lastSelectedDate);
                }

            }
        });
    }

    public void RemoveAllListeners()
    {
        OnDateChanged = null;
    }

    public void AddListener(System.Action<DateTime> listener)
    {
        OnDateChanged += listener;
    }
}
