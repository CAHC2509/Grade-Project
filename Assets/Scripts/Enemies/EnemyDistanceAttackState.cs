using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine stateMachine) { }

    public override void UpdateState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;
        AnimatorStateInfo currentState = enemyController.animator.GetCurrentAnimatorStateInfo(0);
        bool animatorIsPlaying = currentState.normalizedTime < 1.0f;

        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        Vector3 enemyPosition = enemyController.transform.position;
        float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);
        bool playerIsInAttackRange = distanceToPlayer < enemyController.enemyData.rangeAttackDistance;

        if (!animatorIsPlaying)
        {
            if (playerIsInAttackRange)
                enemyController.animator.CrossFade(Animations.Enemy.Shoot, 0f);
            else
                stateMachine.SwithState(stateMachine.chaseState);
        }
    }
}
