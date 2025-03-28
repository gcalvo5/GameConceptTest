using System;
using Assets.Scripts.Enemy;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyLocomotionState locomotionState = new EnemyLocomotionState();
    public EnemyWaitTurnState waitTurnState = new EnemyWaitTurnState();

    [Header("Requirements")]


    [Inject]
    private GameStateManager gameStateManager;

    [Header("Characteristics")]
    [SerializeField] public CharacteristicsModel characteristics;

    public Entitie entitie = new Entitie();

    // Events to Subscribe

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

    private void OnTriggerEnter(Collider other)
    {
        currentState.onTriggerEnter(other);
    }

    public void SwitchState(EnemyBaseState state)
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
    }

    public void UnsubscribeEvent(Action action, PlayerActionType actionType)
    {
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
