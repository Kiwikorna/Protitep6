using System.Collections;
using UnityEngine;

public class SpellSlowDownProjectile : BaseSpellProjectile
{
    [SerializeField] private SpellSlowDownSO spellSlowDownSo;
    [SerializeField] private float spellDistance;
    [SerializeField] private EnemyPathFinding enemyPathFinding;
   
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
        enemyPathFinding = other.GetComponent<EnemyPathFinding>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(spellSlowDownSo.attackDamage);
            
        }

        if (enemyPathFinding != null)
        {
            StartCoroutine(EnemySlowDown());
            enemyPathFinding.GetNavMeshAgent().speed += spellSlowDownSo.slowDown;
        }
        Destroy(gameObject);

    }

    private IEnumerator EnemySlowDown()
    {
        enemyPathFinding.GetNavMeshAgent().speed -= spellSlowDownSo.slowDown;
        Debug.Log(enemyPathFinding.GetNavMeshAgent().speed);

        yield return new WaitForSeconds(3.0f);
    }
}
