using System;
using System.Collections;
using UnityEngine;

public class SlowDownEffect 
{
    public float SlowDownValue { get; private set; }
    public float TimeSlowDownValue { get; private set; }

    public SlowDownEffect(float slowDownValue, float timeSlowDownValue)
    {
        SlowDownValue = slowDownValue;
        TimeSlowDownValue = timeSlowDownValue;
    }
    
}
