using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpellSlowDown : MonoBehaviour
{
     private float _slowDown;
     private float _timeSlowDown;
    private EnemyPathFinding _enemyPathFinding;
    private SlowDownEffect _slowDownEffect;
    private void OnTriggerEnter(Collider other)
    {
        SpellProjectile spellProjectile = other.GetComponent<SpellProjectile>();

        if (spellProjectile != null)
        {
             _enemyPathFinding = GetComponent<EnemyPathFinding>();
             _slowDown = 3;
             _timeSlowDown = 1;
             _slowDownEffect = new SlowDownEffect(_slowDown,_timeSlowDown);

            
            StartCoroutine(TimeSlowDown());

        }
    }

    private IEnumerator TimeSlowDown()
    {
        _enemyPathFinding.GetNavMeshAgent().speed -= _slowDownEffect.SlowDown;
        yield return new WaitForSeconds(_slowDownEffect.TimeSlowDown);
        _enemyPathFinding.GetNavMeshAgent().speed += _slowDownEffect.SlowDown;
    }

    public float GetTimeSlowDownDelete() => _timeSlowDown + 2f;
}
