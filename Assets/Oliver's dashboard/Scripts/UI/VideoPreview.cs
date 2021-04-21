using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof (VideoPlayer))]
public class VideoPreview : MonoBehaviour
{
    VideoPlayer videoPlayer;

    [SerializeField]
    Button playButton;

    [SerializeField]
    Sprite playSprite, pauseSprite;

    [SerializeField]
    Slider slider;

    [SerializeField]
    AspectRatioFitter aspectFitter;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => 
        {
            OnPlayClicked();
        });


        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        if(videoPlayer.isPrepared && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }

    private void Update()
    {
        UpdateSliderDuringPlayback();
    }

    public void SetVideoURL(string url)
    {
        if (videoPlayer == null) return;
        videoPlayer.url = url;
        
        videoPlayer.Prepare();
    }

    public void OnPlayClicked()
    {
        if(videoPlayer.isPrepared)
        {
            Debug.Log("IsPrepared: " + videoPlayer.isPrepared);
            Debug.Log("isPlaying: " + videoPlayer.isPlaying);
            Debug.Log("isPaused: " + videoPlayer.isPaused);
            Debug.Log("isAudioTrackEnabled[0]: " + videoPlayer.IsAudioTrackEnabled(0));

            if (!videoPlayer.isPaused)
                videoPlayer.Pause();
            else
                videoPlayer.Play();

        }
        else
        {
            if(videoPlayer.source == VideoSource.Url && !string.IsNullOrEmpty(videoPlayer.url))
            {
                
                videoPlayer.prepareCompleted += (source) => { this.OnVideoPreparedToPlay(source, true); };
                videoPlayer.Prepare();
            }
        }

        UpdateButtonSprite();
    }

    private void OnVideoPreparedToPlay(VideoPlayer source, bool playOnPrepared)
    {
        Debug.Log("Video Prepared to play..");
        if (playOnPrepared)
        {
            source.Play();
        }
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        Image btnImage = playButton.GetComponent<Image>();
        btnImage.sprite = videoPlayer != null && !string.IsNullOrEmpty(videoPlayer.url) && videoPlayer.isPrepared && videoPlayer.isPlaying ? pauseSprite : playSprite;
    }

    void OnSliderValueChanged(float val)
    {
        if (videoPlayer != null && videoPlayer.isPrepared)
        {
            videoPlayer.frame = (long)val;
        }
    }

    void UpdateSliderDuringPlayback()
    {
        if (videoPlayer != null && videoPlayer.isPrepared && videoPlayer.isPlaying)
        {
            slider.minValue = 0;
            slider.maxValue = videoPlayer.frameCount;
            slider.wholeNumbers = true;

            slider.SetValueWithoutNotify(videoPlayer.frame);

            if(aspectFitter != null)
                aspectFitter.aspectRatio = videoPlayer.texture.width / videoPlayer.texture.height;
        }
    }
}
