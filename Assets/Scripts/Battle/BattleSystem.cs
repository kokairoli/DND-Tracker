using UnityEngine;
using System.Collections.Generic;


public enum BattleState
{
    START,
    WON,
    LOST
}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleController battleController;
    public BattleState state;
    private int currentUnitInTurnIndex;
    private int currentTurn = 0;
    SortedList<int, ActionUnit> units = new SortedList<int, ActionUnit>();

    void Start()
    {
        state = BattleState.START;
        DecideTurnOrder();
        currentUnitInTurnIndex = 0;
        CommenceBattle();

    }

    private void DecideTurnOrder()
    {
        ActionUnit[] actionUnits = FindObjectsByType<ActionUnit>(FindObjectsSortMode.None);
        foreach (ActionUnit unit in actionUnits)
        {
            units.Add(unit.RollInitiative(), unit);
        }
    }

    void CommenceBattle()
    {
        ActionUnit unit = units.Values[currentUnitInTurnIndex];
        battleController.SetSelectedUnit(unit);
    }

    public void EndTurn()
    {
        if (currentUnitInTurnIndex + 1 < units.Count)
        {
            currentUnitInTurnIndex++;
        }
        else
        {
            currentUnitInTurnIndex = 0;
            currentTurn++;
        }
        battleController.ClearUnitSelection();
        battleController.ClearTileSelection();
        battleController.ResetSelectedUnitResources();
        CommenceBattle();
    }
}
