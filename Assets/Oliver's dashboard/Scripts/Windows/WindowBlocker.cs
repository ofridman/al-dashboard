using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowBlocker : MonoBehaviour, IPointerClickHandler
{
    public static WindowBlocker Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    CanvasGroup canvasGroup;

    private Transform currentWindow;

    public void Block(Transform windowTransform) 
    {
        
        gameObject.SetActive(true);

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if(transform.parent != windowTransform.parent)
        {
            transform.SetParent(windowTransform.parent);
        }

        transform.SetAsLastSibling();
        windowTransform.SetAsLastSibling();

        currentWindow = windowTransform;
    }

    public void Hide()
    {
        transform.SetAsFirstSibling();
        gameObject.SetActive(false);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIWindow window = currentWindow.GetComponent<UIWindow>();
        if (window != null && window.closeOnBlockerClicked)
        {
            window.Close();
        }
    }
}
