using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCircle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public float speed;

    public ToolIcon tool;

    public Animator seedPickerAnim;

    public ToolCircleActivator activator;

    void Start()
    {
        activator = transform.parent.GetComponent<ToolCircleActivator>();
        tool = GameObject.Find("Glove").GetComponent<ToolIcon>();
        tool.isSelected = true;
        FindObjectOfType<SwapTools>().SwitchTool(tool.name);
#if UNITY_ANDROID
        speed /= 3;
#endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        activator.ActivateMe();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //activator.ActivateMe();
        //if (tool != null)
        //{
        //    seedPickerAnim.SetBool("IsActive", false);
        //    tool.GetComponent<Collider2D>().enabled = true;
        //    tool.isSelected = false;
        //    tool = null;
        //}
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.Rotate(new Vector3(0, 0, -eventData.delta.x) * speed * Time.deltaTime);
        if (tool.name == "Seed")
        {
            seedPickerAnim.SetBool("IsActive", false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        activator.DeactivateMe();
        tool = FindObjectOfType<DetectTool>().detectedTool;
        tool.isSelected = true;
        FindObjectOfType<SwapTools>().SwitchTool(tool.name);
        tool.GetComponent<Collider2D>().enabled = false;
        if (tool.name == "Seed")
        {
            seedPickerAnim.SetBool("IsActive", true);
        }
        else
        {
            seedPickerAnim.SetBool("IsActive", false);
        }
    }
}
