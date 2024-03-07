using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeathState : TurretBaseState
{
    public override void EnterState(TurretStateMachine stateMachine)
    {
        EnemyTurretController turretController = stateMachine.controller;
        turretController.animator.CrossFade(Animations.Turret.Death, 0f);
    }

    public override void UpdateState(TurretStateMachine stateMachine) { }
}
