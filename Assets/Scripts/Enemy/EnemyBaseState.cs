using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);

    public abstract void UpdateState(EnemyStateManager enemy);

    public abstract void ExitState(EnemyStateManager enemy);

    public abstract void onCollisionEnter(Collision collision);

    public abstract void onTriggerEnter(Collider collider);

    public abstract void AssignInputs(EnemyStateManager enemy);

    public abstract void UnAssignInputs(EnemyStateManager enemy);
}
