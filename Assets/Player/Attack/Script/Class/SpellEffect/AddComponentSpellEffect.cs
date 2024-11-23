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

    private void OnTriggerEnter(Collider triger)
    {
        SingleSpels spellConfig = triger.GetComponent<SingleSpels>();
        
        if (spellConfig != null && gameObject.GetComponent<ComponentSpellSlowDown>() == null)
        {
            _spellSlowDownComponent = gameObject.AddComponent<ComponentSpellSlowDown>();
            
            _spellSlowDownComponent.SetSpellSlowDown(spellConfig.GetSpellItem() as SpellSlowDownItem);
        }
        
    }

    
    
}

