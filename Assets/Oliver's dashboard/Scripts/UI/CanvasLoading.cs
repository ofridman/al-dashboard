﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoading : MonoBehaviour
{
    public static CanvasLoading Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Instance.Hide();

        if (canvas == null) canvas = GetComponent<Canvas>();
        if (popupDialogCanvas == null) popupDialogCanvas = PopupDialog.Instance.gameObject.GetComponent<Canvas>();
    }

    [SerializeField]
    CanvasGroup canvasGroup;

    private static int waiterCount;

    [SerializeField]
    Canvas popupDialogCanvas;

    Canvas canvas;

    public void Show(bool forceAbovePopupMessages = false)
    {
        waiterCount++;

        if (popupDialogCanvas != null && canvas != null)
        {
            canvas.sortingOrder = forceAbovePopupMessages ? popupDialogCanvas.sortingOrder + 1 : popupDialogCanvas.sortingOrder - 1;
        }
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (DOTween.IsTweening("LoadingFadeOut"))
        {
            DOTween.Kill("LoadingFadeOut", true);
        }

        if (canvasGroup.alpha == 1f || DOTween.IsTweening("LoadingFadeIn"))
        {
            return;
        }


        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.25f).SetId("LoadingFadeIn").SetEase(Ease.Linear);
    }

    

    public void Hide()
    {
        waiterCount--;

        if (waiterCount <= 0)
        {
            waiterCount = 0;

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if (DOTween.IsTweening("LoadingFadeIn"))
            {
                DOTween.Kill("LoadingFadeIn", true);
            }

            if (canvasGroup.alpha == 0f || DOTween.IsTweening("LoadingFadeOut"))
            {
                return;
            }

            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0f, 0.25f).SetId("LoadingFadeOut").SetEase(Ease.Linear);
        }
    }

    public void ForceHide()
    {
        waiterCount = 0;
        Hide();
    }
}
