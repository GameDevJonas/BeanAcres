using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwapTools : MonoBehaviour
{
    public enum Tools { hoe, seed, water, glove, shovel }
    public Tools currentTool;
    public enum Plants { carrot, strawberry, aubergine };
    public Plants currentSeed;
    public string currentToolString;
    //public Image[] buttons;
    //public Color selected, unselected;

    public int alphaScore;
    public TextMeshProUGUI scoreText;

    public List<GameObject> hoeObjects = new List<GameObject>();
    public List<GameObject> seedObjects = new List<GameObject>();
    public List<GameObject> waterObjects = new List<GameObject>();
    public List<GameObject> shovelObjects = new List<GameObject>();

    void Start()
    {
        currentTool = Tools.glove;
        currentToolString = "Glove";
        //buttons[0].color = unselected;
        //buttons[1].color = unselected;
        //buttons[2].color = unselected;
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
        foreach (GameObject obj in shovelObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + alphaScore;

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
            case Tools.glove:
                GloveState();
                break;
            case Tools.shovel:
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

    public void GloveState()
    {

    }

    public void SwitchTool(string tool)
    {
        currentToolString = tool;
        if (tool == "Glove")
        {
            currentTool = Tools.glove;
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
            foreach (GameObject obj in shovelObjects)
            {
                obj.SetActive(false);
            }
        }
        if (tool == "Hoe")
        {
            currentTool = Tools.hoe;
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
            foreach (GameObject obj in shovelObjects)
            {
                obj.SetActive(false);
            }
        }
        if (tool == "Seed")
        {
            currentTool = Tools.seed;
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
            foreach (GameObject obj in shovelObjects)
            {
                obj.SetActive(false);
            }
        }
        if (tool == "Shovel")
        {
            currentTool = Tools.shovel;
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
            foreach (GameObject obj in shovelObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
