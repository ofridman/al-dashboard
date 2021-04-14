using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameBrewStudios
{
    public class UserDropdownWindow : UIWindow
    {
        public static UserDropdownWindow Instance;


        [SerializeField]
        TextMeshProUGUI userEmailLabel;

        [SerializeField]
        Button logoutButton;

        bool showing = false;

        public override void Awake()
        {
            Instance = this;
        }

        public override void Show()
        {
            base.Show();
            showing = true;


            Populate();
        }

        public override void Close()
        {
            base.Close();
            showing = false;
        }

        public void Populate()
        {
          //  userEmailLabel.text = "Signed in as: " + User.current.email;
            logoutButton.onClick.RemoveAllListeners();
            logoutButton.onClick.AddListener(() => 
            {
                CanvasLoading.Instance.Show();
                Logout();
                CanvasLoading.Instance.Hide();
            });
        }


        public void Logout()
        {
            SceneManager.LoadScene(0);
        }

        public void ToggleVisibility()
        {
            if (showing) Close();
            else Show();
        }

    }
}