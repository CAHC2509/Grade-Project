using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyMeleeAttackState meleeAttackState = new EnemyMeleeAttackState();
    public EnemyDistanceAttackState distanceAttackState = new EnemyDistanceAttackState();
    public EnemyDeathState deathState = new EnemyDeathState();

    private EnemyBaseState currentState;
    private string currentEnemyState;

    public EnemyController controller => enemyController;

    private void Start() => SwithState(idleState);

    private void Update()
    {
        currentState.UpdateState(this);
        currentEnemyState = currentState.ToString();
    }

    public void SwithState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    [ContextMenu("Chase player")]
    public void ChasePlayer() => SwithState(chaseState);
}
