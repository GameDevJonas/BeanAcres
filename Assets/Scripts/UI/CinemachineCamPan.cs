using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCamPan : MonoBehaviour
{
    #region OLD
    /*public Vector2 startPos, endPos;
    public Vector3 mousePos;
    public float startTime, diffTime, distance, speed;
    bool panning;
    public Rigidbody2D followTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        if (DialogueManager.inDialogue)
        {
            return;
        }
        if (Input.GetMouseButton(0) && !panning)
        {
            followTarget.velocity = Vector2.zero;
            startPos = mousePos;
            startPos.y = 0;
            startTime = Time.time;
            panning = true;
        }
        if (panning)
        {
            endPos = mousePos;
            endPos.y = 0;
            distance = Vector2.Distance(startPos, endPos);
            if (distance > 30)
            {
                followTarget.transform.Translate((startPos - endPos).normalized * Time.deltaTime);
            }
            if (endPos.x < startPos.x)
            {
                distance *= -1;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            panning = false;
            endPos = mousePos;
            endPos.y = 0;
            diffTime = startTime - diffTime;
            distance = Vector2.Distance(startPos, endPos);
            if (distance < 30)
            {
                
                return;
            }
            if (endPos.x < startPos.x)
            {
                distance *= -1;
            }

            if (diffTime != 0)
            {
                speed = distance / diffTime;
                followTarget.AddForce(Vector2.left * speed);
                //Debug.Log(speed);
            }
        }
    }*/
    #endregion
    private Vector2 touchPos, lastPos;
    private Vector3 scrollStart;
    private bool panning;
    private float vel, damping;
    [SerializeField] private Transform scrolling;
    [Range(1f, 100f)] [SerializeField] private float scrollSpeed;
    [Range(1f, 10f)] [SerializeField] private float decelTime;

    private void Start()
    {
        scrollSpeed /= 50;
    }

    void Update()
    {
        if (DialogueManager.inDialogue)
        {
            return;
        }

        if (Input.GetMouseButton(0) && !panning)
        {
            panning = true;
            touchPos = Input.mousePosition;
            scrollStart = scrolling.position;
        }

        if (panning)
        {
            scrolling.position = scrollStart - (Vector3)((Vector2)Input.mousePosition - touchPos) * scrollSpeed * 0.01f;
            scrolling.position = new Vector3(scrolling.position.x, 0f, 0f);
        }
        else
        {
            scrolling.position += Vector3.left * damping * 0.01f * Time.deltaTime;
            damping = Mathf.SmoothDamp(damping, 0f, ref vel, decelTime * 0.1f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            panning = false;
            damping = ((Vector2)Input.mousePosition - lastPos).x;
            lastPos = Vector3.zero;
        }

        lastPos = Input.mousePosition;
    }
}

