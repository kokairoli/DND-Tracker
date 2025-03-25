using UnityEngine;
using System.Collections.Generic;


public enum BattleState
{
    PREPARE,
    START,
    WON,
    LOST
}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleController battleController;
    [SerializeField] UIController uiController;
    public BattleState state;
    private int currentUnitInTurnIndex;
    private int currentTurn = 1;
    SortedList<int, ActionUnit> units = new SortedList<int, ActionUnit>();

    void Start()
    {
        state = BattleState.PREPARE;
        DecideTurnOrder();
        currentUnitInTurnIndex = 0;
        UpdateTurnCountText();
        CommenceBattle();

    }

    private void DecideTurnOrder()
    {
        //TODO: Update
        ActionUnit[] actionUnits = FindObjectsByType<ActionUnit>(FindObjectsSortMode.None);
        int roll;
        foreach (ActionUnit unit in actionUnits)
        {
            do
            {
                roll = unit.RollInitiative();
            } while (units.ContainsKey(roll));
            
            units.Add(roll, unit);
            
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
            UpdateTurnCountText();
        }
        battleController.ClearAllHighlightedArea();
        battleController.ClearUnitSelection();
        battleController.ClearTileSelection();
        battleController.ResetSelectedUnitResources();
        battleController.SetInspectAction();
        CommenceBattle();
    }

    private void UpdateTurnCountText()
    {
        uiController.SetTurnCounter(currentTurn);
    }
}
