using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RDG;

public class SoilBehaviour : MonoBehaviour
{
    public enum SoilStage { empty, planted, growing, done };
    public SoilStage myStage;

    public SwapTools tools;

    public SwapTools.Plants myPlant;

    public ParticleSystem growthParticles;

    public bool isDry, isDone;
    public float dryTimerSet;
    float dryTimer;

    public float GrowTimerSet;
    float growTimer;

    public Sprite wateredSprite, drySprite;
    public SpriteRenderer mySprite;
    public GameObject dirtParticlePrefab;

    public GameObject waterMeObj;

    public AudioSource myAudioSource;
    public AudioClip gloveHarvest, shovelDestroy;

    public List<GameObject> carrotSprites = new List<GameObject>();
    public List<GameObject> strawberrySprites = new List<GameObject>();
    public List<GameObject> aubergineSprites = new List<GameObject>();

    private FarmingGoal quests;

    void Start()
    {
        waterMeObj.SetActive(false);
        tools = FindObjectOfType<SwapTools>();
        quests = FindObjectOfType<FarmingGoal>();
        myStage = SoilStage.empty;
        isDry = true;
        dryTimer = dryTimerSet;
        growTimer = GrowTimerSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDry)
        {
            DryTimer();
        }

        switch (myStage)
        {
            case SoilStage.empty:
                break;
            case SoilStage.planted:
                PlantedState();
                switch (myPlant)
                {
                    case SwapTools.Plants.carrot:
                        carrotSprites[0].SetActive(true);
                        carrotSprites[1].SetActive(false);
                        carrotSprites[2].SetActive(false);
                        break;
                    case SwapTools.Plants.strawberry:
                        strawberrySprites[0].SetActive(true);
                        strawberrySprites[1].SetActive(false);
                        strawberrySprites[2].SetActive(false);
                        break;
                    case SwapTools.Plants.aubergine:
                        aubergineSprites[0].SetActive(true);
                        aubergineSprites[1].SetActive(false);
                        aubergineSprites[2].SetActive(false);
                        break;
                }
                break;
            case SoilStage.growing:
                GrowingState();
                switch (myPlant)
                {
                    case SwapTools.Plants.carrot:
                        carrotSprites[0].SetActive(false);
                        carrotSprites[1].SetActive(true);
                        carrotSprites[2].SetActive(false);
                        break;
                    case SwapTools.Plants.strawberry:
                        strawberrySprites[0].SetActive(false);
                        strawberrySprites[1].SetActive(true);
                        strawberrySprites[2].SetActive(false);
                        break;
                    case SwapTools.Plants.aubergine:
                        aubergineSprites[0].SetActive(false);
                        aubergineSprites[1].SetActive(true);
                        aubergineSprites[2].SetActive(false);
                        break;
                }
                break;
            case SoilStage.done:
                isDone = true;
                switch (myPlant)
                {
                    case SwapTools.Plants.carrot:
                        carrotSprites[0].SetActive(false);
                        carrotSprites[1].SetActive(false);
                        carrotSprites[2].SetActive(true);
                        break;
                    case SwapTools.Plants.strawberry:
                        strawberrySprites[0].SetActive(false);
                        strawberrySprites[1].SetActive(false);
                        strawberrySprites[2].SetActive(true);
                        break;
                    case SwapTools.Plants.aubergine:
                        aubergineSprites[0].SetActive(false);
                        aubergineSprites[1].SetActive(false);
                        aubergineSprites[2].SetActive(true);
                        break;
                }
                break;
        }

        if (isDry)
        {
            mySprite.sprite = drySprite;
            if(myStage != SoilStage.empty && myStage != SoilStage.done)
            {
                waterMeObj.SetActive(true);
            }
        }
        else
        {
            waterMeObj.SetActive(false);
            mySprite.sprite = wateredSprite;
        }
    }

    void DryTimer()
    {
        if (dryTimer <= 0)
        {
            isDry = true;
            dryTimer = dryTimerSet;
        }
        else
        {
            dryTimer -= Time.deltaTime;
        }
    }

    public void PlantSeed(SwapTools.Plants plant)
    {
        VibrationMethods.ShortLowVibration();
        myPlant = plant;
        growTimer = GrowTimerSet;
        myStage = SoilStage.planted;
        growthParticles.Play();
    }

    public void WaterMe()
    {
        isDry = false;
    }

    void PlantedState()
    {
        if (growTimer <= 0)
        {
            growTimer = GrowTimerSet;
            myStage = SoilStage.growing;
            //Vibration.Vibrate(40, 50, true);
            VibrationMethods.ShortLowVibration();
            growthParticles.Play();
        }
        else if (!isDry)
        {
            growTimer -= Time.deltaTime;
        }
    }


    private void OnMouseDown()
    {
        if (isDone && FindObjectOfType<SwapTools>().currentTool == SwapTools.Tools.glove)
        {
            GetComponent<HarvestingBehaviour>().enabled = true;
        }
        if (FindObjectOfType<SwapTools>().currentTool == SwapTools.Tools.shovel)
        {
            //Vibration.Vibrate(300, 150, true);
            VibrationMethods.ShortLowVibration();
            GameObject.Find("ShovelHand").GetComponent<Animator>().Play("HoeAnim");
            GameObject soilParticles = Instantiate(dirtParticlePrefab, transform.position, Quaternion.identity);
            Destroy(soilParticles, 1f);
            ShovelMe(shovelDestroy);
        }
        if (myStage == SoilStage.empty && FindObjectOfType<SwapTools>().currentTool == SwapTools.Tools.seed)
        {
            //Vibration.Vibrate(40, 50, true);
            VibrationMethods.ShortLowVibration();
            PlantSeed(tools.currentSeed);
        }
    }

    public void HarvestNow()
    {
        //Vibration.Vibrate(300, 150, true);
        VibrationMethods.ShortLowVibration();
        GetComponent<Animator>().Play("Harvest");
        tools.alphaScore += 10;
        PickUpPlant(gloveHarvest);
    }

    private void OnMouseUp()
    {
        if (isDone && FindObjectOfType<SwapTools>().currentTool == SwapTools.Tools.glove)
        {
            //GetComponent<HarvestingBehaviour>().enabled = false;
        }
    }

    public void PickUpPlant(AudioClip clip)
    {
        quests.HarvestPlant(myPlant);
        myAudioSource.pitch = Random.Range(0.8f, 1.3f);
        myAudioSource.clip = clip;
        myAudioSource.transform.parent = null;
        myAudioSource.Play();
        Destroy(myAudioSource.gameObject, 2f);
        Destroy(this.gameObject, .3f);
    }
    public void ShovelMe(AudioClip clip)
    {
        myAudioSource.pitch = Random.Range(0.8f, 1.3f);
        myAudioSource.clip = clip;
        myAudioSource.transform.parent = null;
        myAudioSource.Play();
        Destroy(myAudioSource.gameObject, 2f);
        Destroy(this.gameObject, .3f);
    }

    void GrowingState()
    {
        if (growTimer <= 0)
        {
            growTimer = GrowTimerSet;
            GetComponent<BoxCollider>().size = new Vector3(1.3f, 1.15f, 0.36f);
            GetComponent<BoxCollider>().center = new Vector3(0, 0.45f, 0);
            myStage = SoilStage.done;
            //Vibration.Vibrate(40, 50, true);
            VibrationMethods.ShortLowVibration();
            growthParticles.Play();
        }
        else if (!isDry)
        {
            growTimer -= Time.deltaTime;
        }
    }
}
