using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine stateMachine)
    {
        EnemyController enemyController = stateMachine.controller;
        enemyController.animator.CrossFade(Animations.Enemy.Death, 0f);
    }

    public override void UpdateState(EnemyStateMachine stateController) { }
}
