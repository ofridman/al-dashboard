using DG.Tweening;
//using GameBrewStudios.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
//using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebGLFileUploader;

public class Window_FileUploadDialog : UIWindow
{
    [SerializeField]
    Slider progressSlider;

    [SerializeField]
    TextMeshProUGUI statusLabel;

    [SerializeField]
    Screen_Files filesScreen;

    ImportedFileData[] files = null;

    List<ImportedFileData> failedUploads = new List<ImportedFileData>();

    int urlRequestsCompleted = 0;
    int filesProcessedCompletely = 0;

    Dictionary<string, float> progress = new Dictionary<string, float>();

    string status = "";

    [SerializeField]
    Button closeWindowBtn;

    public override void Show()
    {
        Debug.LogError("Window_FileUploadDialog does not support default Show() method. Use Show(ImportedFileData[] files) instead.");
    }

    public void Show(ImportedFileData[] files)
    {
        closeWindowBtn.gameObject.SetActive(false);

        //We dont want this window to close if the user clicks off of it because it would interrupt the flow of file uploads
        this.closeOnBlockerClicked = false;


        if(files == null || files.Length == 0)
        {
            PopupDialog.Instance.Show("No files selected. Please choose one more more files to upload and try again.");
            return;
        }

        this.files = files;

        progressSlider.SetValueWithoutNotify(0f);
        statusLabel.text = "Preparing files for upload. Please wait...";

        progress.Add("getUrls", 0f);
        foreach(ImportedFileData file in files)
        {
            progress.Add(file.name, 0f);
        }

        WebGLFileUploadManager.OnFileUploadFinished += this.WebGLFileUploadManager_OnFileUploadFinished;

        WebGLFileUploadManager.OnFileUploadProgressUpdated += this.WebGLFileUploadManager_OnFileUploadProgressUpdated;

        base.Show();

        DOVirtual.DelayedCall(1f, ProcessFiles);
    }

    private void WebGLFileUploadManager_OnFileUploadFinished(string filename)
    {
        if(this.progress.ContainsKey(filename))
        {
            ImportedFileData file = this.files.FirstOrDefault(x => x.name == filename);
            if (file == null)
            {
                Debug.LogError("Could not find imported file with name: " + filename);
                return;
            }

        /*    APIManager.StoreTeamFileRecord(file, (response) => 
            {
                if (response.ContainsKey("success"))
                {
                    this.progress[filename] = 1f;
                    UpdateProgress();
                }
                else
                {
                    Debug.LogError("StoreTeamFileRecord failed: " + JsonConvert.SerializeObject(response));
                }

            });*/

        }

    }

    private void WebGLFileUploadManager_OnFileUploadProgressUpdated(string filename, float progress)
    {
        if(this.progress.ContainsKey(filename))
        {
            this.progress[filename] = progress;
            this.status = "Uploading " + filename + "(" + (progress * 100f).ToString("n0") + "%)";
            Debug.Log($"Progress update recieved for: {filename} to {progress}");
        }

        UpdateProgress();
    }

    void ProcessFiles()
    {
        StartCoroutine(DOProcessFiles());
    }

    IEnumerator DOProcessFiles()
    { 
        this.failedUploads.Clear();
        this.urlRequestsCompleted = 0;
        this.filesProcessedCompletely = 0;



        foreach(ImportedFileData file in files)
        {
            ImportedFileData curFile = file;
            
            status = "Handshaking with server..";
            CanvasLoading.Instance.Show();
         /*   APIManager.GetSignedUploadURL(curFile.name, curFile.type, (result) => 
            {
                CanvasLoading.Instance.Hide();
                this.urlRequestsCompleted++;
                
                //Update the progress of the prep section of the progress bar.
                this.progress["getUrls"] = (float)this.urlRequestsCompleted / (float)this.files.Length;

                Debug.Log($"GetSignedUploadURL result: {JsonConvert.SerializeObject(result)}");
                if(result.ContainsKey("success") && (bool)result["success"] == true)
                {
                    curFile.signedUploadURL_S3 = (string)result["data"];

                    
                    //Send the upload url to javascript and upload from there:
                    WebGLFileUploadManager.AddToUploadQueue(JsonConvert.SerializeObject(curFile));
                }
                else
                {
                    this.failedUploads.Add(curFile);
                }

            });*/


            float timeElapsed = 0f;
            while(this.urlRequestsCompleted + failedUploads.Count < this.files.Length && timeElapsed < 30f)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            if(timeElapsed >= 30f)
            {
                this.Close();
                PopupDialog.Instance.Show("File upload failed. \n\nReason: Timed out waiting for response from file server.\n\nPlease try again.");
            }
            else
            {
                Debug.Log("ALL URLS RECEIVED FOR FILE UPLOADS, BEGINNING UPLOADS NOW");
                WebGLFileUploadManager.StartUploadingQueuedFiles();
            }

        }

    }

    

    public override void Close()
    {
        this.progress.Clear();
        this.files = null;

        WebGLFileUploadManager.OnFileUploadProgressUpdated -= WebGLFileUploadManager_OnFileUploadProgressUpdated;
        WebGLFileUploadManager.OnFileUploadFinished -= this.WebGLFileUploadManager_OnFileUploadFinished;
        base.Close();

        //Call Show() on files screen here to force it to refresh
        filesScreen.Show();
    }

    private void UpdateProgress()
    {
        float uploadProgress = 0f;


        float getUrlProgress = 0f;

        foreach(KeyValuePair<string, float> pair in progress)
        {
            if (pair.Key == "getUrls")
                getUrlProgress = pair.Value;
            else
                uploadProgress += pair.Value;
        }

        float tenPercent = (getUrlProgress * 0.1f);
        float ninety = ((uploadProgress / (progress.Count - 1)) * 0.9f);

        float totalProgress = tenPercent + ninety;

        //

        Debug.Log($"tenPercent = {tenPercent}  ninetyPercent = {ninety}  getUrlProgress = {getUrlProgress}  uploadProgress = {uploadProgress}  totalProgress = {totalProgress}");

        string text = $"Uploading {progress.Count-1} file" + (progress.Count > 2 ? "s" : "") + "... ";
        progressSlider.value = totalProgress;

        statusLabel.text = text + (totalProgress * 100f).ToString("n0") + "%" + "  (" + this.status + ")";

        if(totalProgress == 1f)
        {
            UploadingFinished();
        }
    }

    void UploadingFinished()
    {
        statusLabel.text = $"Upload Complete. Uploaded {(this.files.Length - failedUploads.Count).ToString("n0")}/{this.files.Length} successfully.";
        DOVirtual.DelayedCall(2f, () => 
        {
            closeWindowBtn.gameObject.SetActive(true);
            filesScreen.Show();
        });
    }

}
