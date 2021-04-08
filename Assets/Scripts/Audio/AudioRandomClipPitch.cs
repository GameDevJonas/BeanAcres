using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomClipPitch : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource source;
    private float minPitch, maxPitch, firstPitch;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.clip = clips[Random.Range(0, clips.Count)];
        minPitch = firstPitch - 0.2f;
        maxPitch = firstPitch + 0.2f;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        firstPitch = source.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        source.Stop();
    }
}
