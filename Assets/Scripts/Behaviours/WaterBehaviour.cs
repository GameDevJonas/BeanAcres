using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (GetComponent<ParticleSystem>().isPlaying && !hasStarted)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(FadeAudioIn());
        //    hasStarted = true;
        //}
        //if(!GetComponent<ParticleSystem>().isPlaying && hasStarted)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(FadeAudioOut());
        //    hasStarted = false;
        //}
    }

    public void FadeInOut(bool t)
    {
        if (t)
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudioIn());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudioOut());
        }
    }


    public IEnumerator FadeAudioIn()
    {
        AudioSource source = GetComponent<AudioSource>();

        //Make sure volume is 0
        source.volume = 0;

        //Play music
        source.Play();

        // Check Music Volume and Fade In
        while (source.volume < 1f)
        {
            source.volume += Time.deltaTime / 1;
            yield return null;
        }

        // Make sure volume is set to 0
        source.volume = 1;
        yield return null;
    }

    public IEnumerator FadeAudioOut()
    {
        AudioSource source = GetComponent<AudioSource>();

        //Make sure volume is 1
        source.volume = 1;

        // Check Music Volume and Fade Out
        while (source.volume > 0.01f)
        {
            source.volume -= Time.deltaTime / 1;
            yield return null;
        }

        // Make sure volume is set to 0
        source.volume = 0;

        // Stop Music
        source.Stop();
        yield return null;
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Hit");
        if (other.GetComponent<SoilBehaviour>() && other.GetComponent<SoilBehaviour>().isDry)
        {
            other.GetComponent<SoilBehaviour>().WaterMe();
        }
    }
}
