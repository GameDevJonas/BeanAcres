using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private LevelQuests quests;

    void Start()
    {
        quests = FindObjectOfType<LevelQuests>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = quests.beanStalks + "";
    }
}
