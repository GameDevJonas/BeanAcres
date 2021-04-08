using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeInOut : MonoBehaviour
{
    public float secondsToFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransitionAudio(AudioSource from, AudioSource to)
    {
        //StopAllCoroutines();
        StartCoroutine(FadeAudioOut(from));
        StartCoroutine(FadeAudioIn(to));
    }

    public IEnumerator FadeAudioOut(AudioSource source)
    {
        //Make sure volume is 1
        source.volume = 1;

        // Check Music Volume and Fade Out
        while (source.volume > 0.01f)
        {
            source.volume -= Time.deltaTime / secondsToFade;
            yield return null;
        }

        // Make sure volume is set to 0
        source.volume = 0;

        // Stop Music
        //source.Stop();
        yield return null;
    }
    public IEnumerator FadeAudioIn(AudioSource source)
    {
        //Make sure volume is 0
        source.volume = 0;

        //Play music
        //source.Play();

        // Check Music Volume and Fade In
        while (source.volume < 1f)
        {
            source.volume += Time.deltaTime / secondsToFade;
            yield return null;
        }

        // Make sure volume is set to 0
        source.volume = 1;
        yield return null;
    }
}
