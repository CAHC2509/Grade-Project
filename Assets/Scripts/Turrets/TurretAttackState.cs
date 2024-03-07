using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretBaseState
{
    public override void EnterState(TurretStateMachine stateMachine)
    {
        EnemyTurretController turretController = stateMachine.controller;
        turretController.animator.CrossFade(Animations.Turret.Shoot, 0f);
    }

    public override void UpdateState(TurretStateMachine stateMachine)
    {
        EnemyTurretController turretController = stateMachine.controller;
        bool playerInSight = turretController.CheckLineOfSight();

        if (!playerInSight)
            stateMachine.SwithState(stateMachine.idleState);
    }
}
