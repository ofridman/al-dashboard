using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace GameBrewStudios
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {

        public static void AddTooltip(GameObject obj, string text)
        {
            if(obj != null)
            {
                TooltipTrigger trigger = obj.AddComponent<TooltipTrigger>();
                trigger.text = text;
            }
        }

        public string text;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverStarted(eventData);
        }
        public void OnSelect(BaseEventData eventData)
        {
            OnHoverStarted(eventData as PointerEventData);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverEnded();
        }
        public void OnDeselect(BaseEventData eventData)
        {
            OnHoverEnded();
        }

        void OnHoverStarted(PointerEventData eventData)
        {
            if (Application.isEditor)
                Debug.Log("SHOWING TOOLTIP: " + text);
            TooltipController.Instance.ShowTooltip(text, eventData);
        }
        void OnHoverEnded()
        {
            TooltipController.Instance.HideTooltip();
        }

    }
}