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
        UpdateImage();
        CheckForDayNight();
        UpdateGameClock();
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
}

[System.Serializable]
public class MainMenuAudios
{
    public AudioFadeInOut audioFader;
    [Space(10)]
    public AudioSource dayMusic;
    public AudioSource nightMusic;
    [Space(10)]
    public AudioSource dayAMBX;
    public AudioSource nightAMBX;
}
