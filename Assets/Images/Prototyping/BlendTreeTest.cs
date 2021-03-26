using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeTest : MonoBehaviour
{
    public Animator anim;
    public float growth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Blend", growth);
    }
}
