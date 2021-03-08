using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit");
        if (other.GetComponent<SoilBehaviour>() && other.GetComponent<SoilBehaviour>().isDry)
        {
            other.GetComponent<SoilBehaviour>().WaterMe();
        }
    }
}
