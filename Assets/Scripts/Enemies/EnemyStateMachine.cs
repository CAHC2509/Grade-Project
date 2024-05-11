using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private float maxChasingDistance;
    [SerializeField] private bool isFinalBoss;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyMeleeAttackState meleeAttackState = new EnemyMeleeAttackState();
    public EnemyDistanceAttackState distanceAttackState = new EnemyDistanceAttackState();
    public EnemyDeathState deathState = new EnemyDeathState();

    private EnemyBaseState currentState;
    private string currentEnemyState;

    public EnemyController controller => enemyController;
    public bool finalBossStateMachine => isFinalBoss;

    private void Start() => SwithState(idleState);

    private void Update()
    {
        currentState.UpdateState(this);
        currentEnemyState = currentState.ToString();
        CheckPlayerDistance();
    }

    public void SwithState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    [ContextMenu("Chase player")]
    public void ChasePlayer() => SwithState(chaseState);

    private void CheckPlayerDistance()
    {
        Vector2 playerPosition = PlayerMovement.Instance.transform.position;
        Vector2 enemyPosition = transform.position;

        float distanceFromPlayer = Vector2.Distance(playerPosition, enemyPosition);
        bool playerIsNearby = distanceFromPlayer <= maxChasingDistance;

        if (!playerIsNearby)
            SwithState(idleState);
    }
}
