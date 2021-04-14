using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler
{


    public Image highlight;

    private TabButtonGroup group;

    public UnityEvent OnTabActivated;


    void Awake()
    {
        group = transform.parent.gameObject.GetComponent<TabButtonGroup>();
    }


    public void SetHighlight(bool active)
    {
        highlight.enabled = active;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        group.SetActiveTab(this);
    }

}
