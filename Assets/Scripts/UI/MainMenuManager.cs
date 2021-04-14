using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator settingsMenu;
    private bool inSettings;

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
