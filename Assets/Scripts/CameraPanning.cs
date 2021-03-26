using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    Vector3 mousePos;
    public Vector3 newMousePos;
    public Vector3 clickedPos;

    public Collider2D bounds;

    public bool panning = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        newMousePos.y = 0;

        if (Input.GetMouseButton(0) && !panning)
        {
            clickedPos = newMousePos;
            clickedPos.y = 0;
            panning = true;
        }
        if (panning)
        {
            Camera.main.transform.Translate(clickedPos - newMousePos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            panning = false;
        }
    }

    private void LateUpdate()
    {
        
    }
}
