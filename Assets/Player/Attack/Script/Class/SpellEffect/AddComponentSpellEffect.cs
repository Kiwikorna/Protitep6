using System;
using System.Collections;
using UnityEngine;

public class AddComponentSpellEffect : MonoBehaviour
{
    private ComponentSpellSlowDown _spellSlowDownComponent;


    private void Awake()
    {
        _spellSlowDownComponent = GetComponent<ComponentSpellSlowDown>();
    }

    /*private void OnTriggerEnter(Collider triger)
    {
        SpellConfig spellConfig = triger.GetComponent<SpellConfig>();
        
        if (spellConfig != null && gameObject.GetComponent<ComponentSpellSlowDown>() == null)
        {
            _spellSlowDownComponent = gameObject.AddComponent<ComponentSpellSlowDown>();
            StartCoroutine(EffectDestroy());

        }
        
    }*/

    private IEnumerator EffectDestroy()
    {
        yield return new WaitForSeconds(_spellSlowDownComponent.GetTimeSlowDownDelete());
        Destroy(_spellSlowDownComponent);
    }
    
}

