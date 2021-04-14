using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider, sFXSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMuicVolume()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        Debug.Log(mixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20));
    }

    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sFXSlider.value) * 20);
    }
}
