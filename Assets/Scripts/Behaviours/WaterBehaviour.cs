using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class WaterBehaviour : MonoBehaviour
{
    public bool watering, hasStopped, hasStarted;

    ParticleSystem system;

    ToolCircleActivator toolCircleCheck;

    // Start is called before the first frame update
    void Start()
    {
        toolCircleCheck = FindObjectOfType<ToolCircleActivator>();
        system = GetComponent<ParticleSystem>();
        watering = false;
        hasStopped = false;
    }

    private void OnEnable()
    {
        watering = false;
        hasStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.inDialogue || toolCircleCheck.isTouching)
        {
            watering = false;
            hasStopped = false;
            return;
        }

        UpdateParticleSystem();
        watering = Input.GetMouseButton(0);
        if (Input.GetMouseButton(0))
        {
            hasStopped = false;
        }
        if (watering)
        {
            //Vibration.Vibrate(40, 100);
            //VibrationMethods.ShortLowVibration();
        }
        else
        {
            //Vibration.Cancel();}
        }
    }

    void UpdateParticleSystem()
    {
        if (!watering && !hasStopped)
        {
            VibrationMethods.ShortLowVibration();
            hasStarted = false;
            //Debug.Log("Stopping");
            FadeInOut(false);
            system.Stop();
        }
        else if (watering && !hasStarted)
        {
            VibrationMethods.ShortLowVibration();
            //Debug.Log("Started");
            system.Play();
            FadeInOut(true);
            hasStarted = true;
        }
    }

    public void FadeInOut(bool t)
    {
        StopAllCoroutines();
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
        hasStopped = false;
        AudioSource source = GetComponent<AudioSource>();

        //Make sure volume is 0
        //source.volume = 0;

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
        hasStopped = true;
        AudioSource source = GetComponent<AudioSource>();

        //Make sure volume is 1
        //source.volume = 1;

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
