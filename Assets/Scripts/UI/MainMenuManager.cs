using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator settingsMenu;
    private bool inSettings, inQuest;
    public GameObject questBoard;
    public Animator fader;

    // Start is called before the first frame update
    void Start()
    {
        inSettings = false;
        if (settingsMenu != null)
            settingsMenu.SetBool("Active", inSettings);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        StartCoroutine(LoadScene(1, 2f));
    }

    public IEnumerator LoadScene(int scene, float waitTime)
    {
        if (scene == 0) FindObjectOfType<TimeManager>().SwitchScene("Menu");
        if (scene == 1) FindObjectOfType<TimeManager>().SwitchScene("LevelSelect");
        if (scene == 2) FindObjectOfType<TimeManager>().SwitchScene("LevelSelect");
        fader.SetBool("DoFade", true);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(scene);
    }

    public void OpenCloseQuest()
    {
        if(questBoard == null)
        {
            if (!GameObject.Find("QuestScreen"))
            {
                return;
            }
            questBoard = GameObject.Find("QuestScreen");
        }
        inQuest = !inQuest;
        LoadQuestInfo();
        questBoard.GetComponent<Animator>().SetBool("InQuest", inQuest);
    }

    public void LoadQuestInfo()
    {
        ScoreScreen board = questBoard.GetComponent<ScoreScreen>();

        var currentQuest = FindObjectOfType<LevelQuests>().currentQuest;
        if(currentQuest.carrot.goal < 1)
        {
            board.myObjs[0].SetActive(false);
        }
        else
        {
            board.myObjs[0].GetComponentInChildren<TextMeshProUGUI>().text = "   " + currentQuest.carrot.goal;
        }
        if(currentQuest.strawberry.goal < 1)
        {
            board.myObjs[1].SetActive(false);
        }
        else
        {
            board.myObjs[1].GetComponentInChildren<TextMeshProUGUI>().text = "   " + currentQuest.strawberry.goal;
        }
        if (currentQuest.aubergine.goal < 1)
        {
            board.myObjs[2].SetActive(false);
        }
        else
        {
            board.myObjs[2].GetComponentInChildren<TextMeshProUGUI>().text = "   " + currentQuest.aubergine.goal;
        }

        int activeObjs = 0;
        foreach (GameObject obj in board.myObjs)
        {
            if (obj.activeInHierarchy) activeObjs++;
        }
        if (activeObjs == 1)
        {
            board.layoutGroup.padding.left = board.oneActive;
        }
        else if (activeObjs == 2)
        {
            board.layoutGroup.padding.left = board.twoActive;
        }
        else
        {
            board.layoutGroup.padding.left = board.allActive;
        }

        board.rewardText.text = ""+currentQuest.reward;
    }

    public void LoadFarmingGame()
    {
        StartCoroutine(LoadScene(2, 2f));
    }

    public void Return()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1, 2f));
    }

    public void Settings()
    {
        inSettings = !inSettings;
        settingsMenu.SetBool("Active", inSettings);
    }
}
