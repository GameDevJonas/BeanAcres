using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public bool debug;
    [Range(0, 24)]
    public float currentTime;
    private float incrementValue = 0.0834f;
    public CanvasGroup imageToUpdate;
    public bool isDay;
    private bool initialStartUp;
    public TextMeshProUGUI clock;

    public MainMenuAudios audios;

    private void Awake()
    {
        initialStartUp = true;
    }

    void Start()
    {
        //isDay = !isDay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!debug) GetTime();
        if (imageToUpdate)
        {
            UpdateImage();
            CheckForDayNight();
            if (clock) UpdateGameClock();
        }
    }

    void UpdateGameClock()
    {
        int hours = System.DateTime.Now.Hour;
        int minutes = System.DateTime.Now.Minute;
        if (minutes < 10)
        {
            clock.text = hours + ":" + "0" + minutes;
        }
        else
        {
            clock.text = hours + ":" + minutes;
        }
    }

    private void GetTime()
    {
        currentTime = (float)System.DateTime.Now.TimeOfDay.TotalHours;
    }

    private void CheckForDayNight()
    {
        if (currentTime >= 7 && currentTime < 17)
        {
            if (!isDay && !initialStartUp)
            {
                audios.audioFader.TransitionAudio(audios.nightAMBX, audios.dayAMBX);
                audios.audioFader.TransitionAudio(audios.nightMusic, audios.dayMusic);
            }
            isDay = true;
        }
        else
        {
            if (isDay && !initialStartUp)
            {
                audios.audioFader.TransitionAudio(audios.dayAMBX, audios.nightAMBX);
                audios.audioFader.TransitionAudio(audios.dayMusic, audios.nightMusic);
            }
            isDay = false;
        }
        if (initialStartUp)
        {
            StartUpMusixAMBX();
        }
    }

    public void StartUpMusixAMBX()
    {
        if (isDay)
        {
            audios.dayAMBX.volume = 1;
            audios.dayMusic.volume = 1;
            audios.nightAMBX.volume = 0;
            audios.nightMusic.volume = 0;
        }
        else
        {
            audios.nightAMBX.volume = 1;
            audios.nightMusic.volume = 1;
            audios.dayAMBX.volume = 0;
            audios.dayMusic.volume = 0;
        }
        initialStartUp = false;
    }

    private void UpdateImage()
    {
        if (currentTime <= 12)
        {
            imageToUpdate.alpha = currentTime * incrementValue;
        }
        else if (currentTime > 12)
        {
            imageToUpdate.alpha = Mathf.Lerp(1, 0, (currentTime - 12) * incrementValue);
        }
    }

    public void SwitchScene(string scene)
    {

        if (isDay)
        {
            if (scene == "Menu")
            {
                audios.audioFader.SwitchClip(audios.dayAMBX, audios.nightAMBX, audios.mDayAMBX, audios.mNightAMBX);
                //audios.audioFader.SwitchClip(audios.dayMusic, audios.nightMusic, audios.mDayMusic, audios.mNightMusic);
            }
            else if (scene == "LevelSelect")
            {
                audios.audioFader.SwitchClip(audios.dayMusic, audios.nightMusic, audios.fDayMusic, audios.fNightMusic);
                //audios.audioFader.SwitchClip(audios.dayMusic, audios.nightMusic, audios.fDayMusic, audios.fNightMusic);
            }
        }
        else
        {
            if (scene == "Menu")
            {
                audios.audioFader.SwitchClip(audios.nightAMBX, audios.dayAMBX, audios.mNightAMBX, audios.mDayAMBX);
                //audios.audioFader.SwitchClip(audios.nightMusic, audios.dayMusic, audios.mNightMusic, audios.mDayMusic);
            }
            else if (scene == "LevelSelect")
            {
                audios.audioFader.SwitchClip(audios.nightAMBX, audios.dayAMBX, audios.fNightAMBX, audios.fDayAMBX);
                //audios.audioFader.SwitchClip(audios.nightMusic, audios.dayMusic, audios.fNightMusic, audios.fDayMusic);
            }
        }
    }
}

[System.Serializable]
public class MainMenuAudios
{
    public AudioFadeInOut audioFader;
    [Space(10)]
    [Header("Audio sources")]
    public AudioSource dayMusic;
    public AudioSource nightMusic;
    [Space(10)]
    public AudioSource dayAMBX;
    public AudioSource nightAMBX;
    [Space(10)]
    [Header("AMBX Clips")]
    public AudioClip fDayAMBX;
    public AudioClip fNightAMBX;
    public AudioClip mDayAMBX, mNightAMBX;
    [Space(10)]
    [Header("Music Clips")]
    public AudioClip fDayMusic;
    public AudioClip fNightMusic;
    public AudioClip mDayMusic, mNightMusic;
}
