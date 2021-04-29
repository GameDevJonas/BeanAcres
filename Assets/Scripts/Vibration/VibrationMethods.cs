using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class VibrationMethods : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void ShortLowVibration()
    {
        Vibration.Vibrate(40, 50, true);
    }

    public void ButtonVibrate()
    {
        Vibration.Vibrate(50, 80, true);
    }

    public void ToolVibration()
    {
        Vibration.Vibrate(300, 150, true);
    }
}
