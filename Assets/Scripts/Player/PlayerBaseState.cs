using UnityEngine;

public abstract class PlayerBaseState 
{
    public abstract void EnterState(PlayerStateManager player);

    public abstract void UpdateState(PlayerStateManager player);

    public abstract void ExitState(PlayerStateManager player);

    public abstract void onCollisionEnter(Collision collision);

    public abstract void AssignInputs(PlayerStateManager player);

    public abstract void UnAssignInputs(PlayerStateManager player);
}
