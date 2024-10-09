using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpellSlowDown : MonoBehaviour
{
     private float _slowDown;
     private float _timeSlowDown;
    private PathFinding _pathFinding;
    private SlowDownEffect _slowDownEffect;
    private void OnTriggerEnter(Collider other)
    {
        BaseSpellProjectile spellProjectile = other.GetComponent<BaseSpellProjectile>();

        if (spellProjectile != null)
        {
             _pathFinding = GetComponent<PathFinding>();
             _slowDown = 3;
             _timeSlowDown = 1;
             _slowDownEffect = new SlowDownEffect(_slowDown,_timeSlowDown);

            
            StartCoroutine(TimeSlowDown());

        }
    }

    private IEnumerator TimeSlowDown()
    {
        _pathFinding.GetAgent().speed -= _slowDownEffect.SlowDown;
        yield return new WaitForSeconds(_slowDownEffect.TimeSlowDown);
        _pathFinding.GetAgent().speed += _slowDownEffect.SlowDown;
    }

    public float GetTimeSlowDownDelete() => _timeSlowDown + 2f;
}
