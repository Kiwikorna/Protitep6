using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpellSlowDown : MonoBehaviour
{
    [SerializeField] private float slowDown;
    [SerializeField] private float timeSlowDown;
    private EnemyPathFinding _enemyPathFinding;
    private SlowDownEffect _slowDownEffect;
    private void OnTriggerEnter(Collider other)
    {
        SpellProjectile spellProjectile = other.GetComponent<SpellProjectile>();

        if (spellProjectile != null)
        {
             _enemyPathFinding = GetComponent<EnemyPathFinding>();
             _slowDownEffect = new SlowDownEffect(slowDown,timeSlowDown);

            
            StartCoroutine(TimeSlowDown());

        }
    }

    private IEnumerator TimeSlowDown()
    {
        _enemyPathFinding.GetNavMeshAgent().speed -= _slowDownEffect.SlowDown;
        yield return new WaitForSeconds(_slowDownEffect.TimeSlowDown);
        _enemyPathFinding.GetNavMeshAgent().speed += _slowDownEffect.SlowDown;
    }
}
