using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateCombatLocomotion : PlayerBaseState
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    private NavMeshPath path;
    LayerMask clickableLayers;
    private LineRenderer movementLine;
    private Vector3 moveDestination = new Vector3(0, 0, 0);
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter PlayerStateCombatLocomotion");
        TurnStart(player);
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        animator = player.GetComponent<Animator>();
        path = new NavMeshPath();
        movementLine = player.movementLine;
        movementLine.enabled = true;
        clickableLayers = player.clickableLayers;
        moveDestination = player.transform.position;
        AssignInputs(player);
    }
    public override void onCollisionEnter(Collision collision)
    {
    }
    public override void UpdateState(PlayerStateManager player)
    {
        
        CalculateMovementPosition(player);
        UpdateLineRenderer(player);
        navMeshAgent.CalculatePath(moveDestination, path);
        if (Vector3.Distance(player.transform.position, moveDestination) > navMeshAgent.stoppingDistance)
        {
            FaceTarget(player);
        }
        SetAnimations(player);
    }
    public override void ExitState(PlayerStateManager player)
    {
        movementLine.enabled = false;
        UnAssignInputs(player);
    }

    public override void AssignInputs(PlayerStateManager player)
    {

        player.endTurnButton.onClick.AddListener(() => OnEndTurnButtonClicked(player));
        player.SubscribeEvent(() => ClickToMove(player), PlayerActionType.Move);
        player.SubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);
    }
    public override void UnAssignInputs(PlayerStateManager player)
    {
        player.endTurnButton.onClick.RemoveListener(() => OnEndTurnButtonClicked(player));
        player.UnsubscribeEvent(() => ClickToMove(player), PlayerActionType.Move);
        player.UnsubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);
    }
    void SwitchState(PlayerStateManager player)
    {
        player.SwitchState(player.attackState);
    }

    void ClickToMove(PlayerStateManager player)
    {
        
        float distance = Vector3.Distance(player.transform.position, movementLine.GetPosition(1));
        if (player.entitie.characteristics.movementDistance > 0)
        {
            player.entitie.characteristics.movementDistance -= distance;
            navMeshAgent.SetPath(path);
        }
    }
    private void SetAnimations(PlayerStateManager player)
    {
        float distanceToDestination = Vector3.Distance(navMeshAgent.transform.position, navMeshAgent.destination);
        if (distanceToDestination > navMeshAgent.stoppingDistance)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
    }
    void FaceTarget(PlayerStateManager player)
    {

        Vector3 direction = (moveDestination - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, lookRotation, Time.deltaTime * player.rotationSpeed);

    }
    void CalculateMovementPosition(PlayerStateManager player)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            Vector3 targetPosition = hit.point;
            float distance = Vector3.Distance(player.transform.position, targetPosition);

            if (distance > player.entitie.characteristics.movementDistance)
            {
                Vector3 direction = (targetPosition - player.transform.position).normalized;
                targetPosition = player.transform.position + direction * player.entitie.characteristics.movementDistance;
            }
            moveDestination = targetPosition;
        }
    }
    void TurnStart(PlayerStateManager player)
    {
        player.entitie.characteristics.movementDistance = player.entitie.characteristics.movementDistanceMax;
        player.entitie.characteristics.actionPoints = player.entitie.characteristics.actionPointsMax;
    }
    private void UpdateLineRenderer(PlayerStateManager player)
    {
        if (path.corners.Length > 1)
        {
            movementLine.positionCount = path.corners.Length;
            movementLine.SetPositions(path.corners);
        }
        else
        {
            movementLine.positionCount = 2;
            movementLine.SetPosition(0, player.transform.position);
            movementLine.SetPosition(1, moveDestination);
        }
    }
    private void OnEndTurnButtonClicked(PlayerStateManager player)
    {
        player.entitie.finalizarTurno = true;
        player.SwitchState(player.waitTurnState);
    }
}
