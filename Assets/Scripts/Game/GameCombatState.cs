using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameCombatState : GameBaseState
{
    private UIManager _uiManager;
    private Dictionary<string, Entitie> combatEntitiesList = new Dictionary<string, Entitie>();
    private GameStateManager _gameStateManager;
    public override void EnterState(GameStateManager game)
    {
        Debug.Log("Enter GameCombatState");
        game._playerStateManager.SwitchState(game._playerStateManager.waitTurnState);
        foreach (var enemyStateManager in game._enemyStateManagers)
        {
            enemyStateManager.SwitchState(enemyStateManager.waitTurnState);
        }
        _gameStateManager = game;
        _uiManager = game._uiManager;
        AddAndSortByInitiative(game);
        game.StartCoroutine(ControlTurns());
        _uiManager.ShowCombatUI(combatEntitiesList);
        
    }

    public override void UpdateState(GameStateManager game)
    {
    }

    public override void ExitState(GameStateManager game)
    {
        combatEntitiesList.Clear();
        _uiManager.ClearCombatUi();
    }

    private void AddAndSortByInitiative(GameStateManager game)
    {
        int i = 0;
        foreach (var enemie in game._enemyStateManagers)
        {
            combatEntitiesList.Add("Enemy " + i, enemie.entitie);
                i++;
        }
        combatEntitiesList.Add("Player", game._playerStateManager.entitie);

        // Ordenar el diccionario por la propiedad 'initiative' en los valores
        var sortedCombatEntitiesList = combatEntitiesList.OrderByDescending(entry => entry.Value.characteristics.initiative).ToList();

        // Si necesitas el diccionario ordenado, puedes convertirlo de nuevo a un diccionario
        combatEntitiesList = sortedCombatEntitiesList.ToDictionary(entry => entry.Key, entry => entry.Value);

    }

    private System.Collections.IEnumerator ControlTurns()
    {
        int numTurnos = 0;
        while (numTurnos < 4)
        {
            foreach (var entity in combatEntitiesList)
            {
                yield return entity.Value.EntrarTurno();
            }
            numTurnos++;
        }
        EndCombat();


    }
    private void EndCombat()
    {
        _gameStateManager.SwitchState(_gameStateManager.explorationState);
    }
}
