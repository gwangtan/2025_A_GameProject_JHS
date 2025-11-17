using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class ChasePlayerAI : MonoBehaviour
{

    public Transform player; 
    public float chaseRange = 50.0f;
    public float attackRange = 2.0f;

    private NavMeshAgent agent; 
    private float distanceToPlayer; 

    void Start()
    {
        GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange )
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        if (distanceToPlayer <= attackRange )
        {
            Attack();
        }
    }

    // 참조 0개
    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position); // 주석: //플레이어 위치로 목적지로 설정한다.
    }

    // 참조 0개
    void StopChasing()
    {
        agent.isStopped = true;
    }


    // 참조 0개
    void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        Debug.Log("Attacking player!");
    }

    // 참조 0개
    void OnDrawGizmosSeleted() // 주석: //Gizmo로 범위 표시
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}