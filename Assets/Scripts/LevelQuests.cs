using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelQuests : MonoBehaviour
{
    public QuestScriptable currentQuest;

    public QuestScriptable[] questArray, queueVisual;

    public Queue<QuestScriptable> quests = new Queue<QuestScriptable>();

    public int beanStalks;

    private void Awake()
    {
        if(FindObjectsOfType<LevelQuests>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        foreach (QuestScriptable quest in questArray)
        {
            quests.Enqueue(quest);
        }
        currentQuest = quests.Peek();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        queueVisual = quests.ToArray();
    }

    [ContextMenu("Next Quest")]
    public void NextQuest()
    {
        quests.Dequeue();
        if(quests.Count == 0) foreach (QuestScriptable quest in questArray) quests.Enqueue(quest);
        currentQuest = quests.Peek();
    }
}
