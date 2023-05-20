using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavmesh : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] Transform[] waypoints;
    int index;
    Vector3 target;
    private NavMeshAgent navmeshAgent;

    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypoint();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        anim.SetBool("isWalking", true);
        target = waypoints[index].position;
        navmeshAgent.SetDestination(target);
    }

    void IterateWaypoint()
    {
        index++;
        if (index == waypoints.Length)
            index = 0;
    }
}
