using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBrewStudios
{

    public class Notification
    {
        public string message;
        public System.Action action;
    }

    public class NotificationWindow : UIWindow
    {
        public static NotificationWindow Instance;
        
        bool showing = false;

        [SerializeField]
        GameObject notificationEntryPrefab;

        [SerializeField]
        Transform container;

        public override void Awake()
        {
            Instance = this;
        }


        public override void Show()
        {
            base.Show();
            showing = true;


            List<Notification> notifications = new List<Notification>();

            
            //Create notifications for TeamInvitations this user has pending.
         /*   for(int i = 0; i < User.current.invitations.Length; i++)
            {
                notifications.Add(new Notification()
                {
                    message = "You've been invited to join: <b>" + User.current.invitations[i].team + "</b>",
                    action = () =>
                    {
                        AcceptInvite(User.current.invitations[i]);
                    }
                });
            }*/

            Populate(notifications);
        }

        public override void Close()
        {
            base.Close();
            showing = false;
        }

        public void Populate(List<Notification> notifications)
        {
            foreach (Transform child in container)
                Destroy(child.gameObject);

            for (int i = 0; i < notifications.Count; i++)
            {
                GameObject newObj = Instantiate(notificationEntryPrefab, container);
                NotificationEntry entry = newObj.GetComponent<NotificationEntry>();
                entry.Init(notifications[i].message, notifications[i].action);
            }
        }


        public void ToggleVisibility()
        {
            if (showing) Close();
            else Show();
        }

    /*    void AcceptInvite(TeamInvitation invitation)
        {
            Close();
            Debug.Log("PUT CODE HERE TO ACCEPT THE INVITE VIA A POPUP DIALOG");
        }*/
    }
}