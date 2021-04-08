using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTool : MonoBehaviour
{
    public ToolIcon detectedTool;

    public AudioSource detectToolSource;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ToolPlaces"))
        {
            if(detectedTool != null && detectedTool != collision)
            {
                detectToolSource.gameObject.SetActive(false);
                detectedTool.isSelected = false;
                detectedTool.GetComponent<Collider2D>().enabled = true;
                detectedTool = null;
            }
            detectToolSource.gameObject.SetActive(true);
            detectedTool = collision.GetComponent<ToolIcon>();
            detectedTool.isSelected = true;
            detectedTool.GetComponent<Collider2D>().enabled = false;
        }
    }
}
