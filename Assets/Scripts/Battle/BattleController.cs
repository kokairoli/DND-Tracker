using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum Action
{
    INSPECT,
    SPELL,
    MOVE,
    ATTACK
}
public class BattleController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private SpellController spellController;
    private ActionUnit selectedUnit;
    private Tile destinationTile;
    private ActionUnit destinationUnit;
    private Action currentAction = Action.INSPECT;



    private void Start()
    {
        UpdateCurrentActionToText();
    }
    public void TileUnitSelected(Tile unit)
    {
        switch (currentAction)
        {
            case Action.INSPECT:
                InspectTile(unit);
                break;
            case Action.MOVE:
                MoveToTile(unit);
                break;
        }
    }

    public void ActionUnitSelected(ActionUnit unit)
    {
        ClearTileSelection();
        switch (currentAction)
        {
            case Action.INSPECT:
                Debug.Log("Inspecting...");
                break;
            case Action.MOVE:
                Debug.Log("Cannot move to an enemy location");
                break;
            case Action.ATTACK:
                AttackTarget(unit);
                break;
            case Action.SPELL:
                castSpell(unit);
                break;
        }
        
    }

    private void AttackTarget(ActionUnit target)
    {
        if (selectedUnit.Equals(target))
        {
            Debug.Log("Cannot attack self");
        }
        else if(gridManager.IsInAttackRange(selectedUnit, target))
        {
            destinationUnit = target;
            ClearHighLightOfAttackableArea();
            //Ide a logika jön, hogy mi történjen, ha két egység van kijelölve
            selectedUnit.Attack(destinationUnit);
            uiController.UpdateResourcesPanel(selectedUnit.GetStats().SubstractCost(CostType.ACTION));
            ClearUnitSelection();
        }
    }

    private void castSpell(ActionUnit target)
    {
        if (gridManager.IsInSpellRange(selectedUnit, target,spellController.GetSpellRange()))
        {
            destinationUnit = target;
            spellController.CastSpell(selectedUnit, target);
            ClearUnitSelection();
        }
    }

    private void MoveToTile(Tile target)
    {
        if (gridManager.IsTileInReachDistance(target))
        {
            //Mozgás a kijelölt egységhez
            destinationTile = target;
            ClearHighLightOfMoveableArea();
            int distance = gridManager.CalculateDistance(selectedUnit.GetTileX(), selectedUnit.GetTileY(), destinationTile.x, destinationTile.y);
            selectedUnit.Move(new Vector3(target.transform.position.x, target.transform.position.y, 0), destinationTile.x, destinationTile.y);
            selectedUnit.SubstractDistanceFromMovement(distance);
            //HighLightMoveableArea();
            ClearTileSelection();
            gridManager.CalculateAvailablePathsFromStart(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
        }
        else
        {
            target.deselect();
        }
        
    }

    private void InspectTile(Tile target)
    {
        if (destinationTile)
        {
            destinationTile.deselect();
        }
        destinationTile = target;
        destinationTile.EnableHighlight();
    }

    public void ClearUnitSelection() {
        
        if (destinationUnit)
        {
            destinationUnit.deselect();
            destinationUnit = null;
        }
    }

    public void ClearTileSelection() {
        if (destinationTile)
        {
            destinationTile.deselect();
            destinationTile = null;
        }
    }

    public void SetSelectedUnit(ActionUnit unit)
    {
        if (selectedUnit)
        {
            selectedUnit.deselect();
        }
        selectedUnit = unit;
        selectedUnit.Select();
        uiController.UpdateResourcesPanel(selectedUnit.GetStats().GetResources());
        gridManager.CalculateAvailablePathsFromStart(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
    }

    public void SetAttackAction()
    {
        ActionClicked(Action.ATTACK);
        HighLightAttackableArea();
    }

    public void SetMoveAction()
    {
        ActionClicked(Action.MOVE);
        
    }

    public void SetInspectAction()
    {
        ActionClicked(Action.INSPECT);
    }

    public void SetSpellAction()
    {
        ActionClicked(Action.SPELL);
        uiController.OpenSpellPanel();
    }

    private void ActionClicked(Action action)
    {
        ClearAllHighlightedArea();
        ClearTileSelection();
        ClearUnitSelection();
        currentAction = action;
        UpdateCurrentActionToText();
    }

    public void ResetSelectedUnitResources()
    {
        selectedUnit.ResetResources();
    }

    public void CreateTextAtCursor(string text)
    {
        uiController.CreateFloatingText(new Vector3(selectedUnit.transform.position.x, selectedUnit.transform.position.y,-1),text);
    }

    

    private void HighLightAttackableArea()
    {
        gridManager.HighLightMoveableArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.GetAttackRange());
    }

    public void HighLightSpellRangeArea(int spellRange)
    {
        gridManager.HighLightMoveableArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), spellRange);
    }

    private void ClearHighLightOfMoveableArea()
    {
        gridManager.ClearAllHighlightedArea();
    }

    private void ClearHighLightOfAttackableArea()
    {
        gridManager.ClearAllHighlightedArea();
    }

    public void ClearHighLightOfSpell(int spellrange)
    {
        gridManager.ClearAllHighlightedArea();
    }

    public void ClearAllHighlightedArea()
    {
        ClearHighLightOfAttackableArea();
        ClearHighLightOfMoveableArea();
    }

    private void UpdateCurrentActionToText()
    {
        uiController.SetActionText(currentAction);
    }

    public Action GetCurrentAction()
    {
        return currentAction;
    }

}
