using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeguimientoPJ : MonoBehaviour
{
    //Seguimiento al player
    public Transform target;
    public float visionRange;
    public LayerMask visionMask;
    private NavMeshAgent _agent;

    //Vagabundeo
    public float wanderingRadius = 2000.0f;
    public float timeBetweenWandering = 5.0f;
    public Transform[] waypoints;
    private int _currentWaypointIndex;
    private float _timeSinceLastWandering;


    void Start()
    {
        _timeSinceLastWandering = timeBetweenWandering;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToTarget, out hit, visionRange, visionMask))
        {
            if (hit.transform == target)
            {
                _agent.SetDestination(target.position);
            }

        }



    }
}
