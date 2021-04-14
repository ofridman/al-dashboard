using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMenu : MonoBehaviour
{
    public MenuScreen[] tabScreens;

    public void ShowTab(int index)
    {
        for (int i = 0, imax = tabScreens.Length; i < imax; i++)
        {
            if (i == index) tabScreens[i].Show();
            else tabScreens[i].Hide();
        }
    }

    [ContextMenu("Show")]
    public void Show()
    {
        DOTween.Kill("navmenu");
        gameObject.SetActive(true);
        transform.position = new Vector3(-(transform.GetComponent<RectTransform>().sizeDelta.x + 50f), transform.position.y, transform.position.z);
        transform.DOMove(new Vector3(0f, transform.position.y, transform.position.z), 1f).SetId("navmenu").SetEase(Ease.Linear);
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        gameObject.SetActive(true);
        DOTween.Kill("navmenu");
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        transform.DOMove(new Vector3(-(transform.GetComponent<RectTransform>().sizeDelta.x + 50f), transform.position.y, transform.position.z), 1f).SetId("navmenu").SetEase(Ease.Linear).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }
}
