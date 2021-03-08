using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTools : MonoBehaviour
{
    public enum Tools { hoe, seed, water, none }
    public Tools currentTool;
    public enum Plants { carrot };
    public Plants currentSeed;
    public string currentToolString;
    public Image[] buttons;
    public Color selected, unselected;

    public List<GameObject> hoeObjects = new List<GameObject>();
    public List<GameObject> seedObjects = new List<GameObject>();
    public List<GameObject> waterObjects = new List<GameObject>();

    void Start()
    {
        currentTool = Tools.none;
        currentToolString = "None";
        buttons[0].color = unselected;
        buttons[1].color = unselected;
        buttons[2].color = unselected;
        foreach (GameObject obj in hoeObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in seedObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in waterObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentTool)
        {
            case Tools.hoe:
                HoeState();
                break;
            case Tools.seed:
                SeedState();
                break;
            case Tools.water:
                WaterState();
                break;
            case Tools.none:
                NoneState();
                break;
        }
    }

    public void HoeState()
    {

    }

    public void SeedState()
    {

    }

    public void WaterState()
    {

    }

    public void NoneState()
    {

    }

    public void SwitchTool(string tool)
    {
        if (tool == currentToolString)
        {
            currentTool = Tools.none;
            currentToolString = "None";
            buttons[0].color = unselected;
            buttons[1].color = unselected;
            buttons[2].color = unselected;
            foreach(GameObject obj in hoeObjects)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in seedObjects)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in waterObjects)
            {
                obj.SetActive(false);
            }
            return;
        }
        currentToolString = tool;
        if (tool == "Hoe")
        {
            currentTool = Tools.hoe;
            buttons[0].color = selected;
            buttons[1].color = unselected;
            buttons[2].color = unselected;
            foreach (GameObject obj in hoeObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in seedObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in waterObjects)
            {
                obj.SetActive(false);
            }
        }
        if (tool == "Seed")
        {
            currentTool = Tools.seed;
            buttons[0].color = unselected;
            buttons[1].color = selected;
            buttons[2].color = unselected;
            foreach (GameObject obj in hoeObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in seedObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in waterObjects)
            {
                obj.SetActive(false);
            }
        }
        if (tool == "Water")
        {
            currentTool = Tools.water;
            buttons[0].color = unselected;
            buttons[1].color = unselected;
            buttons[2].color = selected;
            foreach (GameObject obj in hoeObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in seedObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in waterObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
