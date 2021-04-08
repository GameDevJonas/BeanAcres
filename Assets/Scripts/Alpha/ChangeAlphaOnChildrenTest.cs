using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaOnChildrenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = new Color(1, 1, 1, GetComponent<CanvasGroup>().alpha);
        }
    }
}
