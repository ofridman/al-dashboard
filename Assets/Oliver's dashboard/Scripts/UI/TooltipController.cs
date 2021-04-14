using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameBrewStudios
{

    public class TooltipController : MonoBehaviour
    {
        public static TooltipController Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.activeSceneChanged += this.OnSceneChanged;
        }

        private void OnSceneChanged(Scene prev, Scene next)
        {
            HideTooltip();
        }

        [SerializeField]
        TooltipWindow window;

        internal void ShowTooltip(string text, PointerEventData eventData)
        {
            if (eventData == null) return;

            Vector3 position = new Vector3(eventData.position.x, eventData.position.y, 0f);
            window.SetPosition(position);
            window.Show(text);
        }

        internal void HideTooltip()
        {
            window.Hide();
        }
    }
}