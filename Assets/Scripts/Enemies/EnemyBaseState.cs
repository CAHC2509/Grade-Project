using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine stateController);

    public abstract void UpdateState(EnemyStateMachine stateController);
}
