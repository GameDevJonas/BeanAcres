using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneInfo : MonoBehaviour
{
    public CanvasGroup imageToFade;

    void Start()
    {
        FindObjectOfType<TimeManager>().imageToUpdate = imageToFade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
