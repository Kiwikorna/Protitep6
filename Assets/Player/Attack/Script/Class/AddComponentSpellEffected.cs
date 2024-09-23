using System;
using System.Collections;
using UnityEngine;

public class AddComponentSpellEffected : MonoBehaviour
{
    private ComponentSpellSlowDown _spellSlowDown;
    [SerializeField] private SpellSO spellSo;

    private void Awake()
    {
        _spellSlowDown = GetComponent<ComponentSpellSlowDown>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SpellProjectile spellProjectile = other.GetComponent<SpellProjectile>();
        
        if (spellProjectile != null && gameObject.GetComponent<ComponentSpellSlowDown>() == null  && spellSo.isSpellEffect)
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

