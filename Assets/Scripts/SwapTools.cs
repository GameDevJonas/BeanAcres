using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTools : MonoBehaviour
{
    public enum Tools { hoe, seed, water, none }
    public Tools currentTool;
    public string currentToolString;
    public Image[] buttons;
    public Color selected, unselected;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
            return;
        }
        currentToolString = tool;
        if (tool == "Hoe")
        {
            currentTool = Tools.hoe;
            buttons[0].color = selected;
            buttons[1].color = unselected;
            buttons[2].color = unselected;
        }
        if (tool == "Seed")
        {
            currentTool = Tools.seed;
            buttons[0].color = unselected;
            buttons[1].color = selected;
            buttons[2].color = unselected;
        }
        if (tool == "Water")
        {
            currentTool = Tools.water;
            buttons[0].color = unselected;
            buttons[1].color = unselected;
            buttons[2].color = selected;
        }
    }
}
