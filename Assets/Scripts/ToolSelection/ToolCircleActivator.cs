using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolCircleActivator : MonoBehaviour, IPointerDownHandler
{
    public Animator myAnim;

    public float idleTimerSet, idleTimer;

    public bool isTouching;

    // Start is called before the first frame update
    void Start()
    {
        idleTimer = idleTimerSet;
        myAnim.SetBool("Active", true);
        isTouching = true;
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTouching)
        {
            if(idleTimer <= 0)
            {
                myAnim.SetBool("Active", false);
            }
            else
            {
                idleTimer -= Time.deltaTime;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateMe();
    }

    public void ActivateMe()
    {
        myAnim.SetBool("Active", true);
        isTouching = true;
        idleTimer = idleTimerSet;
    }

    public void DeactivateMe()
    {
        isTouching = false;
        idleTimer = idleTimerSet;
    }
}
