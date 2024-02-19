using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;
        enemyController.animator.CrossFade(Animations.Enemy.Run, 0f);
    }

    public override void UpdateState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;
        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        Vector3 enemyPosition = enemyController.transform.position;
        
        enemyController.navMeshAgent.SetDestination(playerPosition);
        enemyController.CheckFacing();

        EnemyData.AttackType attackType = enemyController.enemyData.attackType;
        float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

        switch (attackType)
        {
            case EnemyData.AttackType.DISTANCE:
                stateMachine.SwithState(stateMachine.distanceAttackState);
                break;

            case EnemyData.AttackType.MELEE:
                if (distanceToPlayer < enemyController.enemyData.meleeAttackDistance)
                    stateMachine.SwithState(stateMachine.meleeAttackState);
                break;

            case EnemyData.AttackType.HYBRID:
                if (distanceToPlayer < enemyController.enemyData.meleeAttackDistance)
                    stateMachine.SwithState(stateMachine.meleeAttackState);
                else
                    stateMachine.SwithState(stateMachine.distanceAttackState);
                break;

            default:
                break;
        }
    }
}
