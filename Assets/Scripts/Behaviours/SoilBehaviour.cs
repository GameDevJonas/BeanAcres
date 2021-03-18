using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilBehaviour : MonoBehaviour
{
    public enum SoilStage { empty, planted, growing, done };
    public SoilStage myStage;

    public SwapTools.Plants myPlant;

    public bool isDry, isDone;
    public float dryTimerSet;
    float dryTimer;

    public float GrowTimerSet;
    float growTimer;

    public Sprite wateredSprite, drySprite;
    public SpriteRenderer mySprite;

    public List<GameObject> carrotSprites = new List<GameObject>();

    void Start()
    {
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
                }
                break;
        }

        if (isDry)
        {
            mySprite.sprite = drySprite;
        }
        else
        {
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
        myPlant = plant;
        growTimer = GrowTimerSet;
        myStage = SoilStage.planted;
    }

    public void WaterMe()
    {
        isDry = false;
    }

    public void PickUpPlant()
    {
        //Add plant score
        Destroy(this.gameObject);
    }

    void PlantedState()
    {
        if (growTimer <= 0)
        {
            growTimer = GrowTimerSet;
            myStage = SoilStage.growing;
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
            //Add score and other effects later!
            Destroy(this.gameObject);
        }
        if(FindObjectOfType<SwapTools>().currentTool == SwapTools.Tools.shovel)
        {
            //Minus points?
            GameObject.Find("ShovelHand").GetComponent<Animator>().Play("HoeAnim");
            Destroy(this.gameObject);
        }
    }

    void GrowingState()
    {
        if (growTimer <= 0)
        {
            growTimer = GrowTimerSet;
            myStage = SoilStage.done;
        }
        else if (!isDry)
        {
            growTimer -= Time.deltaTime;
        }
    }
}
