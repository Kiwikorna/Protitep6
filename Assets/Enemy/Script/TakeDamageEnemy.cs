using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamageEnemy : MonoBehaviour
{
   [SerializeField] private SpellSlowDownProjectile spell;
   [SerializeField] private CharacterHealthSO enemyHealth;
   [SerializeField] private EnemyPathFinding enemyPathFinding;

   public void TakeDamage(int damage)
   {
      if (enemyHealth.health > 0)
      {
         enemyHealth.health -= damage;
      }
      else if(enemyHealth.health <= 0)
      {
         enemyHealth.health = 0;
         StartCoroutine(DieEnemy());

      }
      
   }

   private IEnumerator DieEnemy()
   {
      if (enemyPathFinding.GetNavMeshAgent() != null)
      {
         enemyPathFinding.GetNavMeshAgent().enabled = false;
      }
      transform.Rotate(0,0,75);
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
   }
}
