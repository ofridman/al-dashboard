//using GameBrewStudios.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class Window_FilePreview : UIWindow
{
    [SerializeField]
    GameObject videoPreviewContainer, imagePreviewContainer;


    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    RawImage imagePreview;
    [SerializeField]
    RawImage videoPreview;

    [SerializeField]
    GameObject videoControlsContainer;

    [SerializeField]
    Sprite playBtnSprite, pauseBtnSprite;

    [SerializeField]
    Image playButtonGraphics;

    [SerializeField]
    Slider videoScrubber;

    private void OnEnable()
    {
        videoPlayer.prepareCompleted += this.OnVideoPrepared;
        videoPlayer.errorReceived += this.OnVideoError;
    }

    

    private void OnDisable()
    {
        videoPlayer.prepareCompleted -= this.OnVideoPrepared;
        videoPlayer.errorReceived -= this.OnVideoError;
    }

    private void OnDestroy()
    {
        OnDisable();
    }

    float elapsedTime = 0f;
    bool downloading = false;
    public override void Show()
    {
        downloading = false;

        base.Show();
        CanvasLoading.Instance.Show();
        
    /*    if(TeamFile.current.isImage)
        {
            //Image
            imagePreviewContainer.SetActive(true);
            videoPreviewContainer.SetActive(false);
            downloading = true;
            elapsedTime = 0f;
            APIManager.DownloadTexture2D(TeamFile.current.FileURL(), (texture) => 
            {
                if (!downloading) return;

                downloading = false;

                if(texture == null)
                {
                    CanvasLoading.Instance.Hide();
                    PopupDialog.Instance.Show("An error occurred while trying to preview this image file. Please try again. If the problem persists, please contact support.");
                    this.Close();
                    return;
                }

                imagePreview.texture = texture;
                AspectRatioFitter fitter = imagePreview.GetComponent<AspectRatioFitter>();

                if (fitter == null)
                    fitter = imagePreview.gameObject.AddComponent<AspectRatioFitter>();

                if(fitter != null)
                {
                    fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
                    fitter.aspectRatio = (float)texture.width / (float)texture.height;
                    if (fitter.aspectRatio == 0f)
                        fitter.aspectRatio = 1f;
                        
                }
                else
                {
                    Debug.LogError("CANNOT FIND AspectRatioFitter component on raw image");
                }

                CanvasLoading.Instance.Hide();
            });
        }
        else if(TeamFile.current.isMPEG4)
        {
            //Video
            imagePreviewContainer.SetActive(false);
            videoPreviewContainer.SetActive(true);

            ToggleVideoControls(false);
            string url = TeamFile.current.FileURL();
            videoPlayer.url = url;
            
            videoPlayer.Prepare(); // will trigger the OnVideoPrepared method when finished
        }
        else
        {
            PopupDialog.Instance.Show("We currently only support previews for Video and Image files. Support for other file types coming soon.");
            this.Close();
        }*/


    }

    public override void Close()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();

            videoPlayer.url = null;
        }

        imagePreview.texture = null;

        base.Close();
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        if(player == null)
        {
            Debug.Log("Player is null? ");
            
            return;
        }

        videoScrubber.wholeNumbers = true;
        videoScrubber.minValue = 0;
        videoScrubber.maxValue = (float)player.frameCount;

        //videoPreview.GetComponent<AspectRatioFitter>().aspectRatio = (float)player.pixelAspectRatioNumerator / (float)player.pixelAspectRatioDenominator;
        CanvasLoading.Instance.ForceHide();
        player.Play();
        ToggleVideoControls(true);
    }

    private void OnVideoError(VideoPlayer source, string message)
    {
        PopupDialog.Instance.Show("Video Error", message + "\n\n Video URL was: " + source.url);
        CanvasLoading.Instance.ForceHide();
        this.Close();
    }

    public void OnScrubberValueChanged(float val)
    {
        if(videoPlayer.isPrepared && videoPlayer.isPlaying && videoPlayer.canSetTime)
        {
            videoPlayer.frame = (long)val;
        }
    }

    private void Update()
    {
        if(videoPlayer.isPrepared && videoPlayer.isPlaying)
        {
            videoScrubber.minValue = 0;
            videoScrubber.maxValue = (int)videoPlayer.frameCount;
            videoScrubber.SetValueWithoutNotify(videoPlayer.frame);

        }

        if(downloading)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= 15f)
                StopDownload();
        }
    }

    Coroutine downloadRoutine;

    void StopDownload()
    {
        downloading = false;
        elapsedTime = 0f;

        this.Close();
        CanvasLoading.Instance.Hide();
        PopupDialog.Instance.Show("We seem to have encountered a problem while fetching your preview. Please try again.");
    }

    void ToggleVideoControls(bool active)
    {
        videoControlsContainer.SetActive(active);
    }

    

    public void OnPlayButtonClicked()
    {
        if (!string.IsNullOrEmpty(videoPlayer.url) && videoPlayer.isPrepared)
        {
            if (videoPlayer.isPlaying)
            {
                //Already playing, Pause it
                videoPlayer.Pause();
                playButtonGraphics.sprite = playBtnSprite;
            }
            else
            {
                //Is paused, resume the video
                videoPlayer.Play();
                playButtonGraphics.sprite = pauseBtnSprite;
            }
        }
    }
}
