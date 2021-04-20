using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    public List<Button.ButtonClickedEvent> tutorialEvents = new List<Button.ButtonClickedEvent>();
    public Queue<Button.ButtonClickedEvent> eventQueue = new Queue<Button.ButtonClickedEvent>();
    public Button.ButtonClickedEvent[] queueVisual;

    public List<Dialogue> tutorialDialogue = new List<Dialogue>();
    public Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    public Dialogue[] dQueueVisual;

    public enum TutorialStates { placeSoil, plantSeeds, waterPatches, harvesting };
    public TutorialStates currentState;

    public List<SoilBehaviour> soilPatches = new List<SoilBehaviour>();
    public List<SoilBehaviour> carrotPatches = new List<SoilBehaviour>();
    public List<SoilBehaviour> strawberryPatches = new List<SoilBehaviour>();
    public List<SoilBehaviour> auberginePatches = new List<SoilBehaviour>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button.ButtonClickedEvent events in tutorialEvents)
        {
            eventQueue.Enqueue(events);
        }
        foreach (Dialogue dialogue in tutorialDialogue)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        queueVisual = eventQueue.ToArray();
        dQueueVisual = dialogueQueue.ToArray();
        Invoke("StartDialogue", .5f);
    }

    void StartDialogue()
    {
        NextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TutorialStates.placeSoil:
                RegisterSoils();
                CheckForPatches();
                break;
            case TutorialStates.plantSeeds:
                RegisterSoils();
                CheckForSeeds();
                break;
            case TutorialStates.waterPatches:
                CheckForFullyGrown();
                break;
            case TutorialStates.harvesting:
                break;
        }

        if (soilPatches.Count > 0)
            foreach (SoilBehaviour soil in soilPatches)
            {
                if (soil == null)
                {
                    soilPatches.Remove(soil);
                }
            }
        if (carrotPatches.Count > 0)
            foreach (SoilBehaviour soil in carrotPatches)
            {
                if (soil == null)
                {
                    soilPatches.Remove(soil);
                }
            }
        if (strawberryPatches.Count > 0)
            foreach (SoilBehaviour soil in strawberryPatches)
            {
                if (soil == null)
                {
                    soilPatches.Remove(soil);
                }
            }
        if (auberginePatches.Count > 0)
            foreach (SoilBehaviour soil in auberginePatches)
            {
                if (soil == null)
                {
                    soilPatches.Remove(soil);
                }
            }
    }

    public void RegisterSoils()
    {
        if (FindObjectsOfType<SoilBehaviour>().Length > 0)
        {
            foreach (SoilBehaviour soil in FindObjectsOfType<SoilBehaviour>())
            {
                if (!soilPatches.Contains(soil))
                {
                    soilPatches.Add(soil);
                }
            }
        }
    }

    public void CheckForPatches()
    {
        if (soilPatches.Count > 2)
        {
            currentState = TutorialStates.plantSeeds;
            NextDialogue();
        }
    }

    public void CheckForSeeds()
    {
        foreach (SoilBehaviour soil in soilPatches)
        {
            if (soil.myStage != SoilBehaviour.SoilStage.empty)
            {
                switch (soil.myPlant)
                {
                    case SwapTools.Plants.carrot:
                        AddPatchToPlantList(soil, carrotPatches);
                        break;
                    case SwapTools.Plants.strawberry:
                        AddPatchToPlantList(soil, strawberryPatches);
                        break;
                    case SwapTools.Plants.aubergine:
                        AddPatchToPlantList(soil, auberginePatches);
                        break;
                }
            }
        }
        if (carrotPatches.Count > 0 && strawberryPatches.Count > 0 && auberginePatches.Count > 0)
        {
            currentState = TutorialStates.waterPatches;
            NextDialogue();
        }
    }

    public void AddPatchToPlantList(SoilBehaviour soil, List<SoilBehaviour> list)
    {
        if (!list.Contains(soil))
        {
            list.Add(soil);
        }
    }

    public void CheckForFullyGrown()
    {
        if (carrotPatches.Count > 0)
            foreach (SoilBehaviour soil in carrotPatches)
            {
                if (soil.myStage == SoilBehaviour.SoilStage.done)
                {
                    carrotPatches.Remove(soil);
                }
            }
        if (strawberryPatches.Count > 0)
            foreach (SoilBehaviour soil in strawberryPatches)
            {
                if (soil.myStage == SoilBehaviour.SoilStage.done)
                {
                    strawberryPatches.Remove(soil);
                }
            }
        if (auberginePatches.Count > 0)
            foreach (SoilBehaviour soil in auberginePatches)
            {
                if (soil.myStage == SoilBehaviour.SoilStage.done)
                {
                    auberginePatches.Remove(soil);
                }
            }

        if (carrotPatches.Count < 1 && strawberryPatches.Count < 1 && auberginePatches.Count < 1)
        {
            currentState = TutorialStates.harvesting;
            NextDialogue();
        }
    }

    public void NextEvent()
    {
        if (eventQueue.Count < 1) return;
        eventQueue.Peek().Invoke();
        eventQueue.Dequeue();
        queueVisual = eventQueue.ToArray();
    }

    public void NextDialogue()
    {
        if (dialogueQueue.Count < 1) return;
        NextEvent();
        GetComponent<DialogueTrigger>().dialogue = dialogueQueue.Peek();
        GetComponent<DialogueTrigger>().TriggerDialogue();
        dialogueQueue.Dequeue();
        dQueueVisual = dialogueQueue.ToArray();
    }
}
