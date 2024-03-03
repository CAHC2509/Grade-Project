using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine stateMachine) { }

    public override void UpdateState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;

        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        Vector3 enemyPosition = enemyController.transform.position;
        float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

        bool playerIsInAttackRange = distanceToPlayer < enemyController.enemyData.rangeAttackDistance;
        bool playerIsInFOV = enemyController.IsPlayerInFieldOfVision();

        if (playerIsInAttackRange && playerIsInFOV)
            enemyController.animator.CrossFade(Animations.Enemy.Shoot, 0f);
        else
            stateMachine.SwithState(stateMachine.chaseState);
    }
}