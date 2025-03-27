using UnityEngine;

public class EnemyLocomotionState : EnemyBaseState
{
    EnemyStateManager enemy;
    public override void AssignInputs(EnemyStateManager enemy)
    {
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter EnemyLocomotionState");
        this.enemy = enemy;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void onCollisionEnter(Collision collision)
    {

    }

    public override void onTriggerEnter(Collider collider)
    {
        enemy.EnterCombat();
    }

    public override void UnAssignInputs(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
    }

}
