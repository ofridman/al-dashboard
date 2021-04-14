using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace GameBrewStudios
{
    public class TooltipWindow : MonoBehaviour
    {
        public TextMeshProUGUI tooltipText;

        [SerializeField]
        Image windowGraphics;

        

        RectTransform rectTransform;
        CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            Hide();
        }

        public void Show(string text)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1f;
            tooltipText.text = text;

            rectTransform.ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();

            tooltipText.text += " ";


            //DOTween.Kill("tooltip", true);

            //if(canvasGroup != null)
            //{
            //    canvasGroup.alpha = 0f;
            //    canvasGroup.DOFade(1f, 0.1f).SetId("tooltip");
            //}

        }

        public void Hide()
        {
            if (gameObject == null) return;
            //if (canvasGroup != null)
            //{
            //    DOTween.Kill("tooltip", true);

            //    canvasGroup.DOFade(0f, 0.1f).OnComplete(() => 
            //    {
            //        gameObject.SetActive(false);
            //    }).SetId("tooltip");
            //}
            //else
            //gameObject.SetActive(false);

            canvasGroup.alpha = 0f;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition, Time.deltaTime * 18);
        }

        internal void SetPosition(Vector3 position)
        {
            //Calculate position based on the pointer position that was passed in, and also prevent the tooltip from going off the edge of the screen.

            Vector3 offset = Vector3.zero;

            //if (position.y > Screen.height / 2)
            //    offset.y = (-rectTransform.rect.height * windowGraphics.canvas.scaleFactor) - 64;

            //if (position.x > Screen.width / 4)
            //    offset.x -= ((rectTransform.rect.width * windowGraphics.canvas.scaleFactor));

            rectTransform.position = position + offset;
        }
    }
}
