using UnityEngine;
using UnityEngine.AI;

public class PlayerLocomotionState : PlayerBaseState
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    LayerMask clickableLayers;
    private Vector3 moveDestination = new Vector3(0, 0, 0);
    private float rotationSpeed = 8f;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter PlayerLocomotionState");
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        animator = player.GetComponent<Animator>();
        clickableLayers = player.clickableLayers;
        rotationSpeed = player.rotationSpeed;
        moveDestination = player.transform.position;
        AssignInputs(player);
    }

    public override void onCollisionEnter(Collision collision)
    {

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Vector3.Distance(player.transform.position, moveDestination) > navMeshAgent.stoppingDistance)
        {
            FaceTarget(player);
        }
        SetAnimations(player);
    }

    public override void ExitState(PlayerStateManager player)
    {
        navMeshAgent.ResetPath();
        UnAssignInputs(player);
    }

    public override void AssignInputs(PlayerStateManager player)
    {
        player.SubscribeEvent(() => ClickToMove(), PlayerActionType.Move);
        player.SubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);
    }
    public override void UnAssignInputs(PlayerStateManager player)
    {
        player.UnsubscribeEvent(() => ClickToMove(), PlayerActionType.Move);
        player.UnsubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);
    }
    void SwitchState(PlayerStateManager player)
    {
        player.SwitchState(player.attackState);
    }
   
    void ClickToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            moveDestination = hit.point;
        }
        navMeshAgent.SetDestination(moveDestination);
    }
    void FaceTarget(PlayerStateManager player)
    {

        Vector3 direction = (moveDestination - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }

    private void SetAnimations(PlayerStateManager player)
    {
        // Calcular la distancia al destino
        float distanceToDestination = Vector3.Distance(player.transform.position, moveDestination);

        // Verificar si la distancia es menor o igual a un pequeño umbral para considerar que ha llegado al destino
        if (distanceToDestination <= navMeshAgent.stoppingDistance)
        {
            animator.Play("Idle");
        }
        else
        {
            animator.Play("Walk");
        }
    }
}
