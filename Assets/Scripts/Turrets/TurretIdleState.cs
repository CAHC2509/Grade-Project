using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretBaseState
{
    public override void EnterState(TurretStateMachine stateMachine)
    {
        EnemyTurretController turretController = stateMachine.controller;
        turretController.animator.CrossFade(Animations.Turret.Idle, 0f);
    }

    public override void UpdateState(TurretStateMachine stateMachine)
    {
        EnemyTurretController turretController = stateMachine.controller;
        bool playerInSight = turretController.CheckLineOfSight();

        if (playerInSight)
            stateMachine.SwithState(stateMachine.attackState);
        else
            stateMachine.SwithState(stateMachine.idleState);
    }
}
