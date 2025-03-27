using System;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerAttackState attackState = new PlayerAttackState();
    public PlayerLocomotionState locomotionState = new PlayerLocomotionState();
    public PlayerStateCombatLocomotion combatLocomotionState = new PlayerStateCombatLocomotion();
    public PlayerStateWaitTurn waitTurnState = new PlayerStateWaitTurn();

    [Header("Player Settings")]
    [SerializeField] public float rotationSpeed = 8f;

    [Header("Locomotion Requirements")]
    [SerializeField] public Transform habilityPlane;
    [SerializeField] public LayerMask clickableLayers;
    [SerializeField] public LineRenderer movementLine;

    [Header("Attack Requirements")]
    [SerializeField] public ParticleSystem abilityParticleSystem;
    [SerializeField] public Button endTurnButton;

    [Header("Characteristics")]
    [SerializeField] public CharacteristicsModel characteristics;

    public Entitie entitie = new Entitie();

    [Inject]
    private GameStateManager gameStateManager;

    // Events to Subscribe
    private Action<InputAction.CallbackContext> moveAction;
    private Action<InputAction.CallbackContext> ability1Action;
    private Action<InputAction.CallbackContext> castAbilityAction;

    CustomAction input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new CustomAction();
        entitie.characteristics = characteristics;
        input.Enable();
        currentState = locomotionState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
            currentState = state;
            state.EnterState(this);
        }
    }

    public void SubscribeEvent(Action action, PlayerActionType actionType)
    {
        switch (actionType)
        {
            case PlayerActionType.Move:
                moveAction = ctx => action();
                input.Main.Move.performed += moveAction;
                break;
            case PlayerActionType.Ability1:
                ability1Action = ctx => action();
                input.Main.Ability1.performed += ability1Action;
                break;
            case PlayerActionType.CastAbility:
                castAbilityAction = ctx => action();
                input.Main.CastAbility.performed += castAbilityAction;
                break;
        }
    }

    public void UnsubscribeEvent(Action action, PlayerActionType actionType)
    {
        switch (actionType)
        {
            case PlayerActionType.Move:
                input.Main.Move.performed -= moveAction;
                break;
            case PlayerActionType.Ability1:
                input.Main.Ability1.performed -= ability1Action;
                break;
            case PlayerActionType.CastAbility:
                input.Main.CastAbility.performed -= castAbilityAction;
                break;
        }
    }
    public void EnterCombat()
    {
        gameStateManager.SwitchState(gameStateManager.combatState);
    }
    public void EnterExploration()
    {
        gameStateManager.SwitchState(gameStateManager.explorationState);
    }
}
