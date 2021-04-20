using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "BeanAcres/Quest", order = 0)]
public class QuestScriptable : ScriptableObject
{
    public CollectMission carrot, strawberry, aubergine;
    public Dialogue endDialogue;
    public int reward;
}

[System.Serializable]
public class CollectMission
{
    //public SwapTools.Plants plant;
    
    public int goal;
}
