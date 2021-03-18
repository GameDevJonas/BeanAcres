using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCircle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float speed;

    public ToolIcon tool;

    public Animator seedPickerAnim;

    void Start()
    {
        tool = GameObject.Find("Glove").GetComponent<ToolIcon>();
        tool.isSelected = true;
        FindObjectOfType<SwapTools>().SwitchTool(tool.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (tool != null)
        {
            seedPickerAnim.SetBool("IsActive", false);
            tool.GetComponent<Collider2D>().enabled = true;
            tool.isSelected = false;
            tool = null;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.Rotate(new Vector3(0, 0, -eventData.delta.x) * speed * Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
