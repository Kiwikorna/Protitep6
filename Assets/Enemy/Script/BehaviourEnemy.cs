using System;
using UnityEngine;

public class BehaviourEnemy : MonoBehaviour
{
   [SerializeField] private SpellProjectile spell;
   [SerializeField] private CharacterHealthSO enemyHealth;
   public void TakeDamage(int damage)
   {
      if (enemyHealth.health > 0)
      {
         enemyHealth.health -= damage;
      }
      else
      {
         enemyHealth.health = 0;
         Destroy(gameObject);
      }
      
   }
}
