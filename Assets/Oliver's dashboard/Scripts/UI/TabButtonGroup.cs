using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabButtonGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject selected;

    private TabButton[] tabButtons
    {
        get 
        {
            return GetComponentsInChildren<TabButton>(true);
        }
    }


    private void Awake()
    {
        tabButtons[0].OnPointerClick(null);
    }

    public void SetActiveTab(TabButton tb)
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            TabButton tab = tabButtons[i];
            if (tab == tb)
            {
                tab.SetHighlight(true);
                tab.OnTabActivated?.Invoke();
                selected = tab.gameObject;
            }
            else
            {
                tab.SetHighlight(false);
            }
        }
    }
}
