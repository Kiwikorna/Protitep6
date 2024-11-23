using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSpellSlowDown : MonoBehaviour
{
    private PathFinding _pathFinding;
    private SpellSlowDownItem _spellSlowDownConfig;
    
    private void OnTriggerEnter(Collider other)
    {
        SingleSpels spellConfig = other.GetComponent<SingleSpels>();

        if (spellConfig != null)
        {
             _pathFinding = GetComponent<PathFinding>();
             
            StartCoroutine(TimeSlowDown());

        }
    }

    private IEnumerator TimeSlowDown()
    {
        _pathFinding.GetAgent().speed -= _spellSlowDownConfig._slowDownValue;
        yield return new WaitForSeconds(_spellSlowDownConfig._timeSlowDownValue);
        _pathFinding.GetAgent().speed += _spellSlowDownConfig._slowDownValue;
    }

    public void SetSpellSlowDown(SpellSlowDownItem spellSlowDownItem) => _spellSlowDownConfig = spellSlowDownItem;
}
