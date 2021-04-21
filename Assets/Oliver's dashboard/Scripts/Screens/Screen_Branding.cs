/*using GameBrewStudios;
using GameBrewStudios.Networking;*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Screen_Branding : MenuScreen
{
    [SerializeField]
    TMP_InputField teamNameField;

    [SerializeField]
    TMP_Dropdown logoDropdown;


   // List<TeamFile> imageFiles = null;

    public override void Show()
    {
        
        UpdateAllFields();
        SetUpListeners();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    void PopulateLogoDropdown()
    {
        CanvasLoading.Instance.Show();
     /*   APIManager.ListFiles(User.current.selectedMembership.team._id, (files) =>
        {
            CanvasLoading.Instance.Hide();
            this.imageFiles = new List<TeamFile>();

            for(int i = 0; i < files.Length; i++)
            {
                if (files[i].isImage)
                    this.imageFiles.Add(files[i]);
            }

            logoDropdown.ClearOptions();

            List<string> options = new List<string>() { "Default" };

            int currentIndex = 0;
            for (int i = 0; i < this.imageFiles.Count; i++)
            {
                Debug.Log("Comparing: " + this.imageFiles[i].FileURL() + " to " + User.current.selectedMembership.team.logo);
                if (!string.IsNullOrEmpty(User.current.selectedMembership.team.logo))
                {
                    if (this.imageFiles[i].FileURL() == User.current.selectedMembership.team.logo)
                        currentIndex = i + 1;
                }
                options.Add(this.imageFiles[i].filename);
            }

            logoDropdown.AddOptions(options);

            logoDropdown.SetValueWithoutNotify(currentIndex);
        });*/
    }

    public void UpdateAllFields()
    {
      //  teamNameField.SetTextWithoutNotify(User.current.selectedMembership.team.name);

        PopulateLogoDropdown();
    }

    public void SetUpListeners()
    {
        teamNameField.onEndEdit.RemoveAllListeners();
        teamNameField.onEndEdit.AddListener((text) =>
        {
            CanvasLoading.Instance.Show();
         /*   string logoUrl = logoDropdown.value == 0 ? "" : this.imageFiles[logoDropdown.value - 1].FileURL();
            APIManager.UpdateTeam(User.current.selectedMembership.team._id, teamNameField.text, logoUrl, (team) =>
            {
                CanvasLoading.Instance.Hide();
                User.current.selectedMembership.team.name = team.name;
            });*/
        });

        logoDropdown.onValueChanged.RemoveAllListeners();
        logoDropdown.onValueChanged.AddListener((index) =>
        {
            CanvasLoading.Instance.Show();
          /*  string logoUrl = index == 0 ? "" : this.imageFiles[index - 1].FileURL();
            APIManager.UpdateTeam(User.current.selectedMembership.team._id, teamNameField.text, logoUrl, (team) =>
            {
                CanvasLoading.Instance.Hide();
                
                User.current.selectedMembership.team.logo = team.logo;
                
            });*/
        });
    }
}
