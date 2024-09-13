using System;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
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

        float distance = Vector3.Distance(transform.position, _startPosition);
        if (distance >= spellDistance)
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        BehaviourEnemy enemy = other.GetComponent<BehaviourEnemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(playerDamage.attackDamage);
        }

        Destroy(gameObject);

    }
}
