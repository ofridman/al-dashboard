using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationEntry : MonoBehaviour
{
    public System.Action OnEntryClicked;

    [SerializeField]
    TextMeshProUGUI label;

    public void Init(string text, System.Action onClicked = null)
    {
        label.text = text;
        OnEntryClicked = onClicked;
    }

    public void OnClicked()
    {
        OnEntryClicked?.Invoke();
    }
}
