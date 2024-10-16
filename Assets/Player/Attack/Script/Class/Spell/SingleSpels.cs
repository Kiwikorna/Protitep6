using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public  class SingleSpels : MonoBehaviour
{
    private Vector3 _startPosition;
     [SerializeField] private SpellConfig spellItem;
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
        if (distance >= spellItem.GetRange())
            Destroy(gameObject);
    }

    
    private  void  OnTriggerEnter(Collider other)
    {
        Damage enemy = other.GetComponent<Damage>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(spellItem.GetDamage());
            
        }
        Destroy(gameObject);

    }

}
