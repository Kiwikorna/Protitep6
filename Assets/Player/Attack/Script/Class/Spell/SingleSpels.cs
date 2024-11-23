using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public  class SingleSpels : MonoBehaviour
{  
     [SerializeField] private SpellItem spellItem;
     
     private Vector3 _startPosition;
    private void Awake()
    {
        
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        SpellProjectileFly();
    }

    private  void SpellProjectileFly()
    {
        float distance = Vector3.Distance(transform.position, _startPosition);
        if (distance >= spellItem.spellConfig.GetRange())
            Destroy(gameObject);
    }



    private  void  OnTriggerEnter(Collider other)
    {
        SingleSpels singleSpels = other.GetComponent<SingleSpels>();

        if (singleSpels != null)
        {
            return;
        }
        Damage enemy = other.GetComponent<Damage>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(spellItem.spellConfig.GetDamage());
            
        }
        Destroy(gameObject);
        
    }

    public SpellItem GetSpellItem() => spellItem;
}
