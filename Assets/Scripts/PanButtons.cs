using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanButtons : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Button.ButtonClickedEvent onClick;

    public float pressTime, timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        timer = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        timer += Time.deltaTime;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(timer <= pressTime)
        {
            onClick.Invoke();
        }
        timer = 0;
    }
}
