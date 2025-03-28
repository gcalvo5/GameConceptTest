using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyWaitTurnState : EnemyBaseState
    {
        public override void AssignInputs(EnemyStateManager enemy)
        {
        }
        public override void EnterState(EnemyStateManager enemy)
        {
            Debug.Log("Enter EnemyWaitTurnState");
            enemy.entitie.accionAlCambiarTurno = (int a) => { OnEnterTurn(enemy); };
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
        private void OnEnterTurn(EnemyStateManager enemy)
        {
            enemy.SwitchState(enemy.attackState);
        }   
    }
}