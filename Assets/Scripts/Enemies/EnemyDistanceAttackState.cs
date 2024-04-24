using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttackState : EnemyBaseState
{
    private int attacksPerformed = 0;

    public override void EnterState(EnemyStateMachine stateMachine) { }

    public override void UpdateState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;

        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        Vector3 enemyPosition = enemyController.transform.position;
        float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

        bool playerIsInAttackRange = distanceToPlayer < enemyController.enemyData.rangeAttackDistance;
        bool playerIsInFOV = enemyController.IsPlayerInFieldOfVision();
        bool lineOfSightClear = enemyController.LineOfSightClear();

        bool canShoot = playerIsInAttackRange && playerIsInFOV && lineOfSightClear;
        bool isFinalBoss = stateMachine.finalBossStateMachine;

        if (!isFinalBoss)
        {
            if (canShoot)
                enemyController.animator.CrossFade(Animations.Enemy.Shoot, 0f);
            else
                stateMachine.SwithState(stateMachine.chaseState);
        }
        else
        {
            if (canShoot)
            {
                if (attacksPerformed % 2 == 0)
                    enemyController.animator.CrossFade(Animations.Boss.Missiles, 0f);
                else
                    enemyController.animator.CrossFade(Animations.Boss.Shoot, 0f);
            }
            else
            {
                stateMachine.SwithState(stateMachine.chaseState);
            }

            attacksPerformed++;
        }
    }
}