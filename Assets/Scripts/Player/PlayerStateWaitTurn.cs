using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateWaitTurn : PlayerBaseState
    {
        public override void AssignInputs(PlayerStateManager player)
        {
        }

        public override void EnterState(PlayerStateManager player)
        {
            Debug.Log("Enter PlayerStateWaitTurn");
            player.entitie.accionAlCambiarTurno = (int a) => { OnEnterTurn(player); };
        }

        public override void ExitState(PlayerStateManager player)
        {
        }

        public override void onCollisionEnter(Collision collision)
        {
        }

        public override void UnAssignInputs(PlayerStateManager player)
        {
        }

        public override void UpdateState(PlayerStateManager player)
        {
        }
        private void OnEnterTurn(PlayerStateManager player)
        {
            player.SwitchState(player.combatLocomotionState);
        }
    }
}