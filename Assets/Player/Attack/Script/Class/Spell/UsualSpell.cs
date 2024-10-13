using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public  class UsualSpell : BaseSpell
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
        if (distance >= Range)
            Destroy(gameObject);
    }

    
 

    private  void  OnTriggerEnter(Collider other)
    {
        Damage enemy = other.GetComponent<Damage>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
            
        }
        Destroy(gameObject);

    }

}