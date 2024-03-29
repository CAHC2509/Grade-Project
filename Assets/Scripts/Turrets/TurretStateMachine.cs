using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyTurretController turretController;

    public TurretIdleState idleState = new TurretIdleState();
    public TurretAttackState attackState = new TurretAttackState();
    public TurretDeathState deathState = new TurretDeathState();

    public TurretBaseState currentState;

    public EnemyTurretController controller => turretController;

    private void Start() => SetIdleState();

    private void Update() => currentState.UpdateState(this);

    [ContextMenu("Set idle state")]
    public void SetIdleState() => SwithState(idleState);

    public void SwithState(TurretBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
