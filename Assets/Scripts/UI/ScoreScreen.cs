using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public List<GameObject> myObjs, otherObjs;
    public HorizontalLayoutGroup layoutGroup;
    public int oneActive, twoActive, allActive;
    public TextMeshProUGUI rewardText, totalScoreText;
    LevelQuests manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LevelQuests>();
        rewardText.text = "" + manager.currentQuest.reward;
        totalScoreText.text = "" + manager.beanStalks;

        for (int i = 0; i < myObjs.Count; i++)
        {
            myObjs[i].GetComponentInChildren<TextMeshProUGUI>().text = otherObjs[i].GetComponentInChildren<TextMeshProUGUI>().text;
            myObjs[i].SetActive(otherObjs[i].activeInHierarchy);
        }

        int activeObjs = 0;
        foreach (GameObject obj in myObjs)
        {
            if (obj.activeInHierarchy) activeObjs++;
        }
        if (activeObjs == 1)
        {
            layoutGroup.padding.left = oneActive;
        }
        else if (activeObjs == 2)
        {
            layoutGroup.padding.left = twoActive;
        }
        else
        {
            layoutGroup.padding.left = allActive;
        }
        Invoke("RewardRoutine", 1f);
    }

    void RewardRoutine()
    {
        StartCoroutine(AddReward(manager.beanStalks, manager.currentQuest.reward));
    }

    public IEnumerator AddReward(int scoreBefore, int reward)
    {
        int newScore = scoreBefore;
        int newReward = reward;
        yield return new WaitForSeconds(2f);
        while(newScore != scoreBefore + reward)
        {
            newReward--;
            newScore++;
            rewardText.text = "" + newReward;
            totalScoreText.text = "" + newScore;
            yield return new WaitForSeconds(.01f);
        }
        FindObjectOfType<LevelQuests>().beanStalks = newScore;
        manager.NextQuest();
        StopAllCoroutines();
    }
}
