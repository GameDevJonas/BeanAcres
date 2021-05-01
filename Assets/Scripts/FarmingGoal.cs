using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RDG;

public class FarmingGoal : MonoBehaviour
{
    public List<QuestElements> questElements = new List<QuestElements>();
    public GameObject tutorialObj;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo(FindObjectOfType<LevelQuests>().currentQuest);
        GetComponent<DialogueTrigger>().dialogue = FindObjectOfType<LevelQuests>().currentQuest.endDialogue;
        if(FindObjectOfType<LevelQuests>().currentQuest.name == "01Tutorial")
        {
            tutorialObj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadInfo(QuestScriptable quest)
    {
        if (quest.carrot.goal < 1)
        {
            questElements[0].obj.SetActive(false);
        }
        else
        {
            questElements[0].obj.SetActive(true);
            questElements[0].goal = quest.carrot.goal;
            questElements[0].scoreText.text = questElements[0].quantity + "/" + questElements[0].goal;
        }
        if (quest.strawberry.goal < 1)
        {
            questElements[1].obj.SetActive(false);
        }
        else
        {
            questElements[1].obj.SetActive(true);
            questElements[1].goal = quest.strawberry.goal;
            questElements[1].scoreText.text = questElements[1].quantity + "/" + questElements[1].goal;
        }
        if (quest.aubergine.goal < 1)
        {
            questElements[2].obj.SetActive(false);
        }
        else
        {
            questElements[2].obj.SetActive(true);
            questElements[2].goal = quest.aubergine.goal;
            questElements[2].scoreText.text = questElements[2].quantity + "/" + questElements[2].goal;
        }
    }

    public void DoneLoad()
    {

    }

    public void HarvestPlant(SwapTools.Plants plant)
    {
        switch (plant)
        {
            case SwapTools.Plants.carrot:
                if (!questElements[0].obj.activeInHierarchy) return;
                var cInfo = questElements[0];
                cInfo.obj.GetComponent<Animator>().Play("QuestUpdate");
                cInfo.quantity++;
                cInfo.scoreText.text = cInfo.quantity + "/" + cInfo.goal;
                if (cInfo.quantity == cInfo.goal)
                {
                    cInfo.scoreText.color = Color.green;
                    cInfo.isDone = true;
                }
                break;
            case SwapTools.Plants.strawberry:
                if (!questElements[1].obj.activeInHierarchy) return;
                var sInfo = questElements[1];
                sInfo.obj.GetComponent<Animator>().Play("QuestUpdate");
                sInfo.quantity++;
                sInfo.scoreText.text = sInfo.quantity + "/" + sInfo.goal;
                if (sInfo.quantity == sInfo.goal)
                {
                    sInfo.scoreText.color = Color.green;
                    sInfo.isDone = true;
                }
                break;
            case SwapTools.Plants.aubergine:
                if (!questElements[2].obj.activeInHierarchy) return;
                var aInfo = questElements[2];
                aInfo.obj.GetComponent<Animator>().Play("QuestUpdate");
                aInfo.quantity++;
                aInfo.scoreText.text = aInfo.quantity + "/" + aInfo.goal;
                if (aInfo.quantity == aInfo.goal)
                {
                    aInfo.scoreText.color = Color.green;
                    aInfo.isDone = true;
                }
                break;
        }
        //Vibration.Vibrate(50, 80, true);
        VibrationMethods.ShortLowVibration();
        CheckForDoneQuests();
    }

    public void CheckForDoneQuests()
    {
        int doneQuests = 0;
        foreach (QuestElements quest in questElements)
        {
            if (quest.isDone || quest.goal < 1)
            {
                doneQuests++;
            }
        }
        if (doneQuests >= questElements.Count)
        {
            Invoke("TriggerDialogueNow", 1f);
            //FindObjectOfType<LevelQuests>().NextQuest();
        }
    }

    void TriggerDialogueNow()
    {
        Vibration.Vibrate(300, 150, true);
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}

[System.Serializable]
public class QuestElements
{
    public SwapTools.Plants plant;
    public GameObject obj;
    public TextMeshProUGUI scoreText;
    public int quantity;
    public int goal;
    public bool isDone;
}
