using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpellSlowDown : MonoBehaviour
{
     private float _slowDownValue;
     private float _timeSlowDownValue;
    private PathFinding _pathFinding;
    private SlowDownEffect _slowDownEffect;
    private const float DeleteSpellSlowDownEffect = 3.0f;
    private void OnTriggerEnter(Collider other)
    {
        BaseSpell spell = other.GetComponent<BaseSpell>();

        if (spell != null)
        {
             _pathFinding = GetComponent<PathFinding>();
             _slowDownValue = 3;
             _timeSlowDownValue = 1;
             _slowDownEffect = new SlowDownEffect(_slowDownValue,_timeSlowDownValue);

            
            StartCoroutine(TimeSlowDown());

        }
    }

    private IEnumerator TimeSlowDown()
    {
        _pathFinding.GetAgent().speed -= _slowDownEffect.SlowDownValue;
        yield return new WaitForSeconds(_slowDownEffect.TimeSlowDownValue);
        _pathFinding.GetAgent().speed += _slowDownEffect.SlowDownValue;
    }

    public float GetTimeSlowDownDelete() => _timeSlowDownValue + DeleteSpellSlowDownEffect;
}
