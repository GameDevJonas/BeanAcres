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
        ToolSwitching(hoeObjects, false);
        ToolSwitching(seedObjects, false);
        ToolSwitching(waterObjects, false);
        ToolSwitching(shovelObjects, false);
    }


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
            ToolSwitching(hoeObjects, false);
            ToolSwitching(seedObjects, false);
            ToolSwitching(waterObjects, false);
            ToolSwitching(shovelObjects, false);
        }
        if (tool == "Hoe")
        {
            currentTool = Tools.hoe;
            ToolSwitching(hoeObjects, true);
            ToolSwitching(seedObjects, false);
            ToolSwitching(waterObjects, false);
            ToolSwitching(shovelObjects, false);
        }
        if (tool == "Seed")
        {
            currentTool = Tools.seed;
            ToolSwitching(hoeObjects, false);
            ToolSwitching(seedObjects, true);
            ToolSwitching(waterObjects, false);
            ToolSwitching(shovelObjects, false);
        }
        if (tool == "Water")
        {
            currentTool = Tools.water;
            ToolSwitching(hoeObjects, false);
            ToolSwitching(seedObjects, false);
            ToolSwitching(waterObjects, true);
            ToolSwitching(shovelObjects, false);
        }
        if (tool == "Shovel")
        {
            currentTool = Tools.shovel;
            ToolSwitching(hoeObjects, false);
            ToolSwitching(seedObjects, false);
            ToolSwitching(waterObjects, false);
            ToolSwitching(shovelObjects, true);
        }
    }

    public void ToolSwitching(List<GameObject> list, bool t)
    {
        foreach (GameObject obj in list)
        {
            if (obj.GetComponent<ParticleSystem>())
            {
                if (t)
                {
                    obj.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    obj.GetComponent<ParticleSystem>().Stop();
                }
            }
            else
            {
                obj.SetActive(t);
            }
        }
    }
}
