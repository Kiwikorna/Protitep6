using System;
using UnityEngine;

public class BaseSpall : MonoBehaviour
{
    private const float LifeTime = 3f;
    [SerializeField] private CharacterMagicSO playerMana;
    [SerializeField] private PlayerAttackSO playerDamage;
    
    private void Awake()
    {
        Destroy(gameObject,LifeTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        BehaviourEnemy enemy = other.GetComponent<BehaviourEnemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(playerDamage.attackDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
    }
}
