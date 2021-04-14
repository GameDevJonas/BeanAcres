using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : DialogueTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        if (!DialogueTriggers.firstDialogue)
        {
            DialogueManager.inDialogue = true;
            Invoke("TriggerDialogue", 1f);
            DialogueTriggers.firstDialogue = true;
        }
    }
}
