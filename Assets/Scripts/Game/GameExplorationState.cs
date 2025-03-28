using UnityEngine;
using Zenject;

public class GameExplorationState : GameBaseState
{
    private UIManager _uiManager;

    public override void EnterState(GameStateManager game)
    {
        _uiManager = game._uiManager;
        Debug.Log("Enter GameExplorationState");
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is not initialized");
        }
        else
        {
            _uiManager.ShowExplorationUI();
        }
        if(game._playerStateManager != null && game._enemyStateManagers != null)
        {
            game._playerStateManager.SwitchState(game._playerStateManager.locomotionState);
            foreach (var enemyStateManager in game._enemyStateManagers)
            {
                enemyStateManager.SwitchState(enemyStateManager.locomotionState);
            }
        }
    }

    public override void UpdateState(GameStateManager game)
    {
    }

    public override void ExitState(GameStateManager game)
    {
    }
}
