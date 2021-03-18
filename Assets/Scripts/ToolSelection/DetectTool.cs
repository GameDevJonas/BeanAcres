using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTool : MonoBehaviour
{
    public ToolIcon detectedTool;

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
            detectedTool = collision.GetComponent<ToolIcon>();
        }
    }
}
