using UnityEngine;
using Zenject;

public class GameStateManager : MonoBehaviour
{

    public UIManager _uiManager;
    public GameBaseState currentState;
    public GameExplorationState explorationState = new GameExplorationState();
    public GameCombatState combatState = new GameCombatState();
    public PlayerStateManager _playerStateManager;
    public EnemyStateManager[] _enemyStateManagers;

    [Inject]
    private void Construct(UIManager uiManager, PlayerStateManager playerStateManager, EnemyStateManager[] enemyStateManagers)
    {
        _uiManager = uiManager;
        _playerStateManager = playerStateManager;
        _enemyStateManagers = enemyStateManagers;
        currentState = explorationState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}