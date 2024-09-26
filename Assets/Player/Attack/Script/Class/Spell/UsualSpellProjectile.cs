using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public  class UsualSpellProjectile : BaseSpellProjectile
{
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {

        SpellProjectileFly();

    }

    public override void SpellProjectileFly()
    {
        float distance = Vector3.Distance(transform.position, _startPosition);
        if (distance >= SpellRange)
            Destroy(gameObject);
    }

    
 

    private  void  OnTriggerEnter(Collider other)
    {
        TakeDamageEnemy enemy = other.GetComponent<TakeDamageEnemy>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(SpellDamage);
            
        }
        Destroy(gameObject);

    }

}
