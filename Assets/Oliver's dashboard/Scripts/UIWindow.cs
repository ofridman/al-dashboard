using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIWindow : MonoBehaviour
{
    public enum AppearTransition
    {
        None, //Instantly appear
        FadeIn,
        GrowFromCenter,
        SlideFromLeft,
        SlideFromRight
    }

    public enum DisappearTransition
    {
        None,
        FadeOut,
        ShrinkToCenter,
        SlideToLeft,
        SlideToRight
    }

    [Header("Base Display Controls")]
    public AppearTransition appearTransition = AppearTransition.FadeIn;
    public DisappearTransition disappearTransition = DisappearTransition.FadeOut;


    CanvasGroup canvasGroup;

    [Header("Member Variables")]

    [SerializeField]
    internal bool closeOnBlockerClicked = true;

    [SerializeField]
    bool shown = false;

    public virtual void Awake()
    {
        
        
    }

    public virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!shown)
        {
            gameObject.SetActive(false);
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public static List<UIWindow> activeWindows = new List<UIWindow>();

    public virtual void Show()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        shown = true;
        
        //This would not support recursive window display (like level, lesson, quiz, level would remove the original level window)
        if (activeWindows.Contains(this)) activeWindows.Remove(this);
        
        activeWindows.Add(this);

        //if (WindowBlocker.Instance != null)
        WindowBlocker.Instance?.Block(activeWindows[activeWindows.Count - 1].transform);
        
        gameObject.SetActive(true);
        DoAppearTransition();
    }
    public virtual void Close()
    {
        activeWindows.Remove(this);


        if (activeWindows.Count > 0 && WindowBlocker.Instance != null)
            WindowBlocker.Instance.Block(activeWindows[activeWindows.Count - 1].transform);
        else if(WindowBlocker.Instance != null)
            WindowBlocker.Instance.Hide();
            
        DoDisappearTransition(() => { gameObject.SetActive(false); });
    }


    private void DoAppearTransition()
    {
        switch(appearTransition)
        {
            case AppearTransition.SlideFromLeft:
                SlideFromLeft();
                break;
            case AppearTransition.SlideFromRight:
                SlideFromLeft();
                break;
            case AppearTransition.GrowFromCenter:
                break;
            case AppearTransition.FadeIn:
                FadeIn();
                break;
            case AppearTransition.None:
            default:
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                break;
        }
    }
    private void DoDisappearTransition(System.Action onFinished)
    {

        switch (disappearTransition)
        {
            case DisappearTransition.SlideToLeft:
                SlideToRight(onFinished);
                break;
            case DisappearTransition.SlideToRight:
                SlideToRight(onFinished);
                break;
            case DisappearTransition.ShrinkToCenter:
                
                break;
            case DisappearTransition.FadeOut:
                FadeOut(onFinished);
                break;
            case DisappearTransition.None:
            default:
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                onFinished?.Invoke();
                break;
        }
    }


    private void FadeIn()
    {
        if(canvasGroup != null) canvasGroup.DOFade(1f, 0.25f).OnComplete(EnableCanvasGroup);
    }

    private void FadeOut(System.Action onFinished)
    {
        if (canvasGroup != null) 
            canvasGroup.DOFade(0f, 0.25f).OnComplete(() => {
                DisableCanvasGroup();
                onFinished?.Invoke();
            });
    }

    private void SlideFromLeft()
    {
        transform.position.Set(-(Screen.width / 2), 0f, 0f);
        transform.DOMoveX(Screen.width / 2, 0.25f).OnComplete(EnableCanvasGroup);
    }

    private void SlideToRight(System.Action onFinished)
    {
        transform.DOMoveX(Screen.width + Screen.width / 2, 0.25f).OnComplete(() => {
            DisableCanvasGroup();
            onFinished?.Invoke();
        });
    }

    private void EnableCanvasGroup()
    {
        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void DisableCanvasGroup()
    {
        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}

