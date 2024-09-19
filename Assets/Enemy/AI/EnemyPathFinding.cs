using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    private NavMeshAgent _agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_agent.enabled == true)
        {
            _agent.SetDestination(playerPosition.position);
           
        }
        
    }
    public NavMeshAgent GetNavMeshAgent() => _agent;
    
    
}
