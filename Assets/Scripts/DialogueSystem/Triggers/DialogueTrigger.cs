using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public Dialogue dialogue;
    public Button.ButtonClickedEvent finishedDialogueEvents;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
    }
    private void Start()
    {
        //manager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue, this);
    }
}
