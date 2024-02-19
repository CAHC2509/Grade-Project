using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private EnemyBaseState currentState;
    private EnemyPatrolState patrolState = new EnemyPatrolState();
    private EnemyChaseState chaseState = new EnemyChaseState();

    public EnemyController controller => enemyController;

    private void Start()
    {
        currentState = chaseState;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }
}
