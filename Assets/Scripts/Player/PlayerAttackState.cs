using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    Animator animator;
    LayerMask clickableLayers;
    Transform habilityPlane;
    ParticleSystem abilityParticleSystem;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter PlayerAttackState");
        habilityPlane = player.habilityPlane;
        animator = player.GetComponent<Animator>();
        clickableLayers = player.clickableLayers;
        abilityParticleSystem = player.abilityParticleSystem;
        AssignInputs(player);
        Ability(player);
    }

    public override void ExitState(PlayerStateManager player)
    {
        UnAssignInputs(player);
        habilityPlane.gameObject.SetActive(false);
    }

    public override void onCollisionEnter(Collision collision)
    {
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        HandleAbility();
    }

    public override void AssignInputs(PlayerStateManager player)
    {
        player.SubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);
        player.SubscribeEvent(() => CastAbility(player), PlayerActionType.CastAbility);
    }
    public override void UnAssignInputs(PlayerStateManager player)
    {
        player.UnsubscribeEvent(() => SwitchState(player), PlayerActionType.Ability1);

        player.UnsubscribeEvent(() => CastAbility(player), PlayerActionType.CastAbility);
    }
    
    void SwitchState(PlayerStateManager player)
    {
        player.SwitchState(player.locomotionState);
    }
    void CastAbility(PlayerStateManager player)
    {
        habilityPlane.gameObject.SetActive(false);
        abilityParticleSystem.transform.position = habilityPlane.transform.position;
        abilityParticleSystem.Play();
        SwitchState(player);
    }
    public void HandleAbility()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            Vector3 worldPosition = hit.point;
            worldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.1f, worldPosition.z);
            habilityPlane.position = worldPosition;
        }
    }

    void Ability(PlayerStateManager player)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            Vector3 worldPosition = hit.point;
            worldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.1f, worldPosition.z);
            habilityPlane.position = worldPosition;
        }
        animator.Play("AttackIdle");
        if(habilityPlane.gameObject.activeSelf)
        {
            player.SwitchState(player.locomotionState);
        }
        habilityPlane.gameObject.SetActive(true);
    }
}
