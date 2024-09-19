using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpellProjectile : BaseSpellProjectile
{

    [SerializeField] private SpellSO playerDamage;
    [SerializeField] private float spellDistance;
   
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {

        SpellProjectileFlyed();

    }

    public override void SpellProjectileFlyed()
    {
        float distance = Vector3.Distance(transform.position, _startPosition);
        if (distance >= spellDistance)
            Destroy(gameObject);
    }

    private  void  OnTriggerEnter(Collider other)
    {
        TakeDamageEnemy enemy = other.GetComponent<TakeDamageEnemy>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(playerDamage.attackDamage);
            
        }
        Destroy(gameObject);

    }

}
