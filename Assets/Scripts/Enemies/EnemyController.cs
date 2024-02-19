using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private Animator enemyAnimator;

    public NavMeshAgent navMeshAgent => meshAgent;
    public Animator animator => enemyAnimator;

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        CheckFacing();
    }

    private void CheckFacing()
    {
        Vector3 directionToPlayer = PlayerMovement.Instance.transform.position - transform.position;
        bool playerIsLeftSide = directionToPlayer.x < 0f;

        transform.localScale = playerIsLeftSide ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
}
