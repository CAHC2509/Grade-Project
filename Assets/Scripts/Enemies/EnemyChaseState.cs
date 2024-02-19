using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine stateMachine) { }

    public override void UpdateState(EnemyStateMachine stateMachine)
    {
        Vector2 playerPosition = PlayerMovement.Instance.transform.position;
        stateMachine.controller.navMeshAgent.SetDestination(playerPosition);

        stateMachine.controller.animator.CrossFade(Animations.Enemy.Run, 0f);
    }
}
