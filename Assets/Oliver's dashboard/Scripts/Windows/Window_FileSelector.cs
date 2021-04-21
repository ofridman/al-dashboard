using GameBrewStudios;
//using GameBrewStudios.Networking;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Window_FileSelector : UIWindow
{
    [SerializeField]
    Transform listContainer;

    [SerializeField]
    GameObject listPrefab;

   /* List<TeamFile> files = new List<TeamFile>();


    public static event System.Action<TeamFile> OnFileSelected;*/

    public void Show(string title, params string[] extensions)
    {
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);
        base.Show();

        CanvasLoading.Instance.Show();
      /*  APIManager.ListFiles(User.current.selectedMembership.team._id, (files) =>
        {
            this.files = new List<TeamFile>();
            foreach (TeamFile file in files)
            {
                foreach (string ext in extensions)
                {
                    if (file.filename.EndsWith(ext))
                    {
                        this.files.Add(file);
                    }
                }
            }
            Populate();
            CanvasLoading.Instance.Hide();
        });*/

    }

    void Populate()
    {
        

       /* if(this.files == null || this.files.Count == 0)
        {
            Debug.LogError("NO Files?");
            return;
        }


        foreach (TeamFile file in files)
        {
            GameObject obj = Instantiate(listPrefab, listContainer, false);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => FileSelected(file));

            obj.transform.Find("Rank").gameObject.SetActive(false);

            obj.transform.Find("Button - Delete").gameObject.SetActive(false);

            TextMeshProUGUI label = obj.GetComponentInChildren<TextMeshProUGUI>();
            label.text = file.filename;
        }*/
    }

   /* public void FileSelected(TeamFile file)
    {
        OnFileSelected?.Invoke(file);
        Close();
    }*/
}
