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
        Vector3 targetPosition = CalculateLateralPosition(playerPosition, enemyPosition);

        Debug.DrawLine(enemyPosition, targetPosition, Color.cyan);

        enemyController.navMeshAgent.SetDestination(targetPosition);
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

    private Vector3 CalculateLateralPosition(Vector3 playerPosition, Vector3 enemyPosition)
    {
        Vector3 directionToPlayer = playerPosition - enemyPosition;
        directionToPlayer.y = 0f;
        Vector3 lateralDirection = Vector3.Cross(directionToPlayer.normalized, Vector3.up);
        float lateralDistance = 5f;

        return playerPosition + lateralDirection * lateralDistance;
    }
}
