using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamageEnemy : MonoBehaviour
{
   [SerializeField] private EnemyPathFinding enemyPathFinding;
   private EnemyHealth _enemyHealth;
   [SerializeField] private int health;

   private void Awake()
   {
      _enemyHealth = new EnemyHealth(health);
   }

   public void TakeDamage(int damage)
   {
      _enemyHealth.Health -= damage;

      if (_enemyHealth.Health <= 0)
      {
         _enemyHealth.Health = 0;
         StartCoroutine(DieEnemy());

      }
   }

   private IEnumerator DieEnemy()
   {
      if (enemyPathFinding.GetNavMeshAgent() != null)
      {
         enemyPathFinding.GetNavMeshAgent().enabled = false;
      }

      transform.Rotate(0, 0, 75);
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
   }
}
