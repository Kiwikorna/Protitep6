using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Damage : MonoBehaviour
{
   [SerializeField] private PathFinding pathFinding;
   private EnemyHealth _enemyHealth;
   [SerializeField] private int health;

   private void Awake()
   {
      _enemyHealth = new EnemyHealth(health);
   }

   public void TakeDamage(float damage)
   {
      _enemyHealth.Health -= damage;

      if (_enemyHealth.Health <= 0)
      {
         _enemyHealth.Health = 0;
         StartCoroutine(EnemyDie());

      }
   }

   private IEnumerator EnemyDie()
   {
      if (pathFinding.GetAgent() != null)
      {
         pathFinding.GetAgent().enabled = false;
      }

      transform.Rotate(0, 0, 75);
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
   }
}
