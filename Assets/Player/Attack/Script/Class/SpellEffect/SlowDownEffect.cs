using System;
using System.Collections;
using UnityEngine;

public class SlowDownEffect 
{
    public float SlowDown { get; set; }
    public float TimeSlowDown { get; set; }

    public SlowDownEffect(float slowDown, float timeSlowDown)
    {
        SlowDown = slowDown;
        TimeSlowDown = timeSlowDown;
    }
    
}
