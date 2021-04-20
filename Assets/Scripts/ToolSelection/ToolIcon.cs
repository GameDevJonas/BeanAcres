using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolIcon : MonoBehaviour
{
    public Transform layoutTransform, selectTransform, myRotationPicker;
    Vector3 normalScale;

    public bool isSelected;

    void Start()
    {
        normalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            SelectedPosition();
        }
        else
        {
            RotationPosition();
        }
        if (GetComponent<CircleCollider2D>().enabled || isSelected)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.gray;
        }
    }

    public void SelectedPosition()
    {
        transform.position = selectTransform.position;
        transform.localScale = selectTransform.localScale;
    }

    public void RotationPosition()
    {
        transform.position = layoutTransform.position;
        transform.localScale = normalScale;
    }
}
