using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCamPan : MonoBehaviour
{
    public Vector2 startPos, endPos;
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
    }
}
