using System;
using System.Collections;
using UnityEngine;

public class AddComponentSpellEffected : MonoBehaviour
{
    private ComponentSpellSlowDown _spellSlowDown;


    private void Awake()
    {
        _spellSlowDown = GetComponent<ComponentSpellSlowDown>();
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseSpellProjectile spellProjectile = other.GetComponent<BaseSpellProjectile>();
        
        if (spellProjectile != null && gameObject.GetComponent<ComponentSpellSlowDown>() == null)
        {
            _spellSlowDown = gameObject.AddComponent<ComponentSpellSlowDown>();
            StartCoroutine(EffectDestroed());

        }
        
    }

    private IEnumerator EffectDestroed()
    {
        yield return new WaitForSeconds(_spellSlowDown.GetTimeSlowDownDelete());
        Destroy(_spellSlowDown);
    }
    
}

