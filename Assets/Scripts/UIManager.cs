using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas explorationUI;

    [SerializeField] private Canvas combatUI;
    [SerializeField] private HorizontalLayoutGroup turnLayoutGroup;
    private List<Button> characterButtons;

    private Canvas currentUI;
    [Inject]
    private void Construct()
    {
    }

    public void ShowExplorationUI()
    {
        if(currentUI != null)
        {
            currentUI.gameObject.SetActive(false);
        }
        currentUI = explorationUI;
        currentUI.gameObject.SetActive(true);
    }

    public void ShowCombatUI(Dictionary<string, Entitie> combatEntities)
    {
        if (currentUI != null)
        {
            currentUI.gameObject.SetActive(false);
        }

        foreach (var entitie in combatEntities)
        {
            var button = Instantiate(Resources.Load<Button>("Prefabs/CharacterButton"), turnLayoutGroup.transform);
            button.onClick.AddListener(() => Debug.Log("FuturoMostrarEstados"));
            if (entitie.Key.Equals("Player"))
            {
                button.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/LukaDoncic");
            }
            else
            {
                button.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/NicoHarrison");
            }
        }
        currentUI = combatUI;
        currentUI.gameObject.SetActive(true);
    }
    public void ClearCombatUi()
    {
        foreach (Transform child in turnLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
