using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void AssignInputs(EnemyStateManager enemy)
    {
    }
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter EnemyAttackState");
        TurnStart(enemy);
        RunAsyncOperation(enemy);
    }
    public override void ExitState(EnemyStateManager enemy)
    {
    }
    public override void onCollisionEnter(Collision collision)
    {
    }
    public override void onTriggerEnter(Collider collider)
    {
    }

    public override void UnAssignInputs(EnemyStateManager enemy)
    {
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
    }

    private async Task RunAsyncOperation(EnemyStateManager enemy)
    {
        // Simulación de una operación asíncrona
        await Task.Delay(2000); // Esperar 2 segundos
        enemy.entitie.finalizarTurno = true;
        enemy.SwitchState(enemy.waitTurnState);
    }
    void TurnStart(EnemyStateManager enemy)
    {
        enemy.entitie.characteristics.movementDistance = enemy.entitie.characteristics.movementDistanceMax;
        enemy.entitie.characteristics.actionPoints = enemy.entitie.characteristics.actionPointsMax;
    }
}
