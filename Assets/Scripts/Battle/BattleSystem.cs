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
    [SerializeField] InventoryUIController inventoryUIController;
    public BattleState state;
    private int currentUnitInTurnIndex = 0;
    private int currentTurn = 1;
    List<ActionUnit> preparedUnits = new List<ActionUnit>();
    SortedList<int, ActionUnit> units = new SortedList<int, ActionUnit>();

    void Start()
    {
        state = BattleState.PREPARE;
    }

    public void PrepareAndDecideTurnOrder(List<GameObject> actionUnitObjects)
    {
        //TODO: Update
        uiController.DisableStartBattleAndAddUnitButton();
        state = BattleState.START;
        actionUnitObjects.ForEach(unitObject =>
        {
            preparedUnits.Add(unitObject.GetComponent<ActionUnit>());
        });
        UpdateTurnCountText();
        int roll;
        foreach (ActionUnit unit in preparedUnits)
        {
            do
            {
                roll = unit.RollInitiative();
            } while (units.ContainsKey(roll));
            
            units.Add(roll, unit);
            
        }

        uiController.OpenAvailableActionPanel();

        CommenceBattle();
    }

    void CommenceBattle()
    {
        ActionUnit unit = units.Values[currentUnitInTurnIndex];
        battleController.SetSelectedUnit(unit);
        inventoryUIController.CreateInventory(unit.GetInventory());
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
        inventoryUIController.FlushInventory();
        CommenceBattle();
    }

    public BattleState GetBattleState()
    {
        return state;
    }

    private void UpdateTurnCountText()
    {
        uiController.SetTurnCounter(currentTurn);
    }
}
