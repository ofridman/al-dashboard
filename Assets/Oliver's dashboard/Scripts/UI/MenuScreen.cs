using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuScreen : MonoBehaviour
{
    [ContextMenu("Show")]
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    [ContextMenu("Hide")]
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
