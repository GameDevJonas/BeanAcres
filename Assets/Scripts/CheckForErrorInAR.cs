using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForErrorInAR : MonoBehaviour
{
    public HoeBehaviour hoePlacement;
    public SoilSeedChecker seedPlacement;

    public float errorTime, errorTimer, cooldownTime;

    public bool encounteredError, inCooldown;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!encounteredError && !inCooldown)
        {
            if (hoePlacement.isActiveAndEnabled)
            {
                if (!hoePlacement.placementPoseIsValid)
                {
                    if (errorTimer >= errorTime)
                    {
                        //Show dialogue thing
                        GetComponent<DialogueTrigger>().TriggerDialogue();
                        //Debug.Log("Hoe is struggling");
                        errorTimer = 0;
                        encounteredError = true;
                    }
                    else
                    {
                        errorTimer += Time.deltaTime;
                    }
                }
            }
            else if (seedPlacement.isActiveAndEnabled)
            {
                if (!seedPlacement.placementPoseIsValid)
                {
                    if (errorTimer >= errorTime)
                    {
                        //Show dialogue thing
                        GetComponent<DialogueTrigger>().TriggerDialogue();
                        //Debug.Log("Seed is struggling");
                        errorTimer = 0;
                        encounteredError = true;
                    }
                    else
                    {
                        errorTimer += Time.deltaTime;
                    }
                }
            }
            else
            {
                errorTimer = 0;
            }
        }
        else if(!inCooldown)
        {
            errorTimer = 0;
        }

        if(encounteredError && !DialogueManager.inDialogue)
        {
            inCooldown = true;
            //encounteredError = false;
        }

        if (inCooldown)
        {
            if(errorTimer >= cooldownTime)
            {
                encounteredError = false;
                errorTimer = 0;
                inCooldown = false;
            }
            else
            {
                errorTimer += Time.deltaTime;
            }
        }
    }
}
