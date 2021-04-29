using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RDG;

public class SeedPicker : MonoBehaviour, IPointerDownHandler
{
    public SoilSeedChecker soilChecker;
    public SwapTools.Plants mySeed;
    public Color unselected, selected;
    public Transform spriteTransform;
    public Vector3 spriteFirstScale;
    public bool active;

    public SwapTools tools;

    // Start is called before the first frame update
    void Start()
    {
        tools = FindObjectOfType<SwapTools>();
        spriteFirstScale = spriteTransform.localScale;
        unselected = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (tools.currentTool == SwapTools.Tools.seed && tools.currentSeed == mySeed)
        {
            active = true;
        }
        else
        {
            active = false;
        }

        if (active)
        {
            GetComponent<Image>().color = selected;
            GetComponent<AudioRandomClipPitch>().enabled = true;
            //spriteTransform.localScale = transform.localScale;
            
        }
        else
        {
            GetComponent<Image>().color = unselected;
            GetComponent<AudioRandomClipPitch>().enabled = false;
            //spriteTransform.localScale = spriteFirstScale;
            GetComponent<Animator>().Play("UnSelected");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vibration.Vibrate(50, 80, true);
        GetComponent<Animator>().Play("Selected");
        //soilChecker.SwitchSeed(mySeed);
        tools.currentSeed = mySeed;
    }
}
