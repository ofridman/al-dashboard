using DG.Tweening;
using GameBrewStudios;
//using GameBrewStudios.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebGLFileUploader;
//using WebGLFileUploader;

public class Screen_Files : MenuScreen
{
    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    GameObject listEntryPrefab;


    [SerializeField]
    Window_FilePreview filePreviewWindow;

    private void Awake()
    {
        InitFileUploadManager();
    }

    public override void Show()
    {
        base.Show();
        PopulateFileList();
    }

    public override void Hide()
    {
        base.Hide();
    }

    bool populatingFileList = false;

    void PopulateFileList()
    {
        if (populatingFileList) return;

        populatingFileList = true;

        CanvasLoading.Instance.Show();

     /*   APIManager.ListFiles(User.current.selectedMembership.team._id, (files) =>
        {
            foreach (Transform child in scrollRect.content)
                Destroy(child.gameObject);

            foreach (TeamFile file in files)
            {
                GameObject obj = Instantiate(listEntryPrefab, scrollRect.content);
                Button btn = obj.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    TeamFile.current = file;

#if UNITY_WEBGL && !UNITY_EDITOR
                    Application.ExternalEval("window.open(\"" + TeamFile.current.FileURL() + "\")");
#else
                    Application.OpenURL(TeamFile.current.FileURL());
#endif

                    //filePreviewWindow.Show();
                });

                TextMeshProUGUI nameLabel = obj.GetComponentInChildren<TextMeshProUGUI>();
                nameLabel.text = file.filename + "  (" + file.mimetype + ")"; //+ "ID: " + file._id;

#if UNITY_EDITOR
                TooltipTrigger tt = nameLabel.gameObject.AddComponent<TooltipTrigger>();
                    tt.text = $"_id: {file._id}\nFilename: {file.filename}\nMIME Type: {file.mimetype}\nTeam ID: {file.team}";
#endif

                Button deleteBtn = obj.transform.Find("Button - Delete").GetComponent<Button>();
                deleteBtn.onClick.RemoveAllListeners();
                deleteBtn.onClick.AddListener(() => 
                {
                    PopupDialog.PopupButton[] btns = new PopupDialog.PopupButton[]
                    {
                        new PopupDialog.PopupButton()
                        {
                            text = "Yes, Delete it",
                            onClicked = () =>
                            {
                                
                                CanvasLoading.Instance.Show();
                                APIManager.DeleteFile(file._id, (response) =>
                                {

                                    CanvasLoading.Instance.Hide();
                                    if(response.ContainsKey("success"))
                                    {
                                        if((bool)response["success"] == true)
                                        {
                                            PopupDialog.Instance.Show("File successfully deleted.");
                                            this.Show();
                                        }
                                        else
                                        {
                                            PopupDialog.Instance.Show("An error occured. Please try again.");
                                        }
                                    }
                                });
                            },
                            buttonColor = PopupDialog.PopupButtonColor.Red
                        },
                        new PopupDialog.PopupButton()
                        {
                            text = "Nevermind",
                            onClicked = () =>
                            {
                                
                                //APIManager.DeleteFile()
                            },
                            buttonColor = PopupDialog.PopupButtonColor.Plain
                        }
                    };
                    PopupDialog.Instance.Show("Really delete this file?", "Are you sure you want to permanently delete this file <i>\"" + file.filename + "\"</i>?\n\nThis action cannot be undone.", btns);

                });

            }

            CanvasLoading.Instance.Hide();
            populatingFileList = false;
        });*/
    }

    public void AddFile()
    {
        if (Application.isEditor)
        {
            PopupDialog.Instance.Show("Adding a file does not work in the Editor. You must use this from the WebGL Build");
            //Debug.Log("Attempting to upload sample file for testing");

            //byte[] file = File.ReadAllBytes(Application.streamingAssetsPath + "/testImage.png");

            //APIManager.UploadFile(file, (teamFile) => 
            //{
            //    if(teamFile == null)
            //    {
            //        Debug.LogError("FILE UPLOAD FAILED");
            //        return;
            //    }

            //    Debug.Log("FILE RECEIVED!!!");
            //    Debug.Log(JsonConvert.SerializeObject(teamFile));
            //    Debug.Log(teamFile.FileURL());
            //});
        }
        else
        {
            
            WebGLFileUploadManager.PopupDialog("Choose Video or Image Files", "Select files (.png|.jpg|.mp4)");
        }

    }


    // Use this for initialization
    void InitFileUploadManager()
    {
        Debug.Log("WebGLFileUploadManager.getOS: " + WebGLFileUploadManager.getOS);
        Debug.Log("WebGLFileUploadManager.isMOBILE: " + WebGLFileUploadManager.IsMOBILE);
        Debug.Log("WebGLFileUploadManager.getUserAgent: " + WebGLFileUploadManager.GetUserAgent);

        WebGLFileUploadManager.SetDebug(true);
//        if (
//#if UNITY_WEBGL && !UNITY_EDITOR
//                    WebGLFileUploadManager.IsMOBILE 
//#else
//                    Application.isMobilePlatform
//#endif
//            )
//        {
            //WebGLFileUploadManager.Show(false);
            //WebGLFileUploadManager.SetDescription("Select image/movie files (.png|.jpg|.mp4)");

        //}
        //else
        //{
        //    WebGLFileUploadManager.Show(false);
        //    WebGLFileUploadManager.SetDescription("Drop image files (.png|.jpg|.gif) here");
        //}
        WebGLFileUploadManager.SetImageEncodeSetting(true);
        WebGLFileUploadManager.SetAllowedFileName("\\.(png|jpe?g|mp4)$");
        WebGLFileUploadManager.SetImageShrinkingSize(1280, 960);
        

#if UNITY_WEBGL && !UNITY_EDITOR
        //string fileUploadUrl = ServerAPI.SERVER_URL + "/teams/files/" + User.current.selectedMembership.team._id + "/upload";
        //WebGLFileUploadManager.SetFileUploadURL(fileUploadUrl);
#endif

        WebGLFileUploadManager.OnSelectedFileDataReceived += OnFileReceived;
    }


    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        WebGLFileUploadManager.OnSelectedFileDataReceived -= OnFileReceived;
        WebGLFileUploadManager.Dispose();
    }



    ImportedFileData[] importedFiles;

    [SerializeField]
    Window_FileUploadDialog fileUploadDialog;

    /// <summary>
    /// Raises the file uploaded event.
    /// </summary>
    /// <param name="result">Uploaded file infos.</param>
    private void OnFileReceived(ImportedFileData[] result)
    {

        if (result == null || result.Length == 0)
        {
            Debug.Log("File import Error!");
            importedFiles = null;
            return;
        }
        else
        {
            Debug.Log("File import success! (result.Length: " + result.Length + ")");
        }

        importedFiles = result;

        Debug.LogWarning($"UPLOAD FILES HERE: Count = {importedFiles.Length}");

        fileUploadDialog.Show(importedFiles);
    }


    ///// <summary>
    ///// Raises the back button click event.
    ///// </summary>
    //public void OnBackButtonClick()
    //{
    //    SceneManager.LoadScene("WebGLFileUploaderExample");
    //}

    ///// <summary>
    ///// Raises the switch button overlay state button click event.
    ///// </summary>
    //public void OnSwitchButtonOverlayStateButtonClick()
    //{
    //    WebGLFileUploadManager.Show(false, !WebGLFileUploadManager.IsOverlay);
    //}

    ///// <summary>
    ///// Raises the switch drop overlay state button click event.
    ///// </summary>
    //public void OnSwitchDropOverlayStateButtonClick()
    //{
    //    WebGLFileUploadManager.Show(true, !WebGLFileUploadManager.IsOverlay);
    //}

    ///// <summary>
    ///// Raises the popup dialog button click event.
    ///// </summary>
    //public void OnPopupDialogButtonClick()
    //{
    //    WebGLFileUploadManager.PopupDialog(null, "Select image files (.png|.jpg|.gif)");
    //}

    ///// <summary>
    ///// Raises the enable button click event.
    ///// </summary>
    //public void OnEnableButtonClick()
    //{
    //    WebGLFileUploadManager.Enable();
    //}

    ///// <summary>
    ///// Raises the disable button click event.
    ///// </summary>
    //public void OnDisableButtonClick()
    //{
    //    WebGLFileUploadManager.Disable();
    //}
}
