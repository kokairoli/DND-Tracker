using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum Action
{
    INSPECT,
    MOVE,
    ATTACK
}
public class BattleController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIController uiController;
    private ActionUnit selectedUnit;
    private Tile destinationTile;
    private ActionUnit destinationUnit;
    private Action currentAction = Action.INSPECT;


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
        }
        
    }

    private void AttackTarget(ActionUnit target)
    {
        if (selectedUnit.Equals(target))
        {
            Debug.Log("Cannot attack self");
        }
        else if(gridManager.IsInReachDistance(selectedUnit, target))
        {
            Debug.Log("Attacking...");
            destinationUnit = target;
            gridManager.ClearAllHighlightedArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
            //Ide a logika jön, hogy mi történjen, ha két egység van kijelölve
            selectedUnit.Attack(destinationUnit);
            destinationUnit.deselect();
            destinationUnit = null;
        }
    }

    private void MoveToTile(Tile target)
    {
        if (gridManager.IsInReachDistance(selectedUnit, target))
        {
            //Mozgás a kijelölt egységhez
            destinationTile = target;
            gridManager.ClearAllHighlightedArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
            int distance = gridManager.CalculateDistance(selectedUnit.GetTileX(), selectedUnit.GetTileY(), destinationTile.x, destinationTile.y);
            selectedUnit.Move(new Vector3(target.transform.position.x, target.transform.position.y, 0), destinationTile.x, destinationTile.y);
            selectedUnit.SubstractDistanceFromMovement(distance);
            CreateTextAtCursor("Moving...");
            ClearTileSelection();
            ClearUnitSelection();
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
    }

    public void SetAttackAction()
    {
        ClearTileSelection();
        ClearUnitSelection();
        currentAction = Action.ATTACK;
        gridManager.HighLightMoveableArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
    }

    public void SetMoveAction()
    {
        ClearTileSelection();
        ClearUnitSelection();
        currentAction = Action.MOVE;
        gridManager.HighLightMoveableArea(selectedUnit.GetTileX(), selectedUnit.GetTileY(), selectedUnit.getMovement());
        
    }

    public void SetInspectAction()
    {
        ClearTileSelection();
        ClearUnitSelection();
        currentAction = Action.INSPECT;
        
    }

    public void ResetSelectedUnitResources()
    {
        selectedUnit.ResetResources();
    }

    public void CreateTextAtCursor(string text)
    {
        uiController.CreateFloatingText(new Vector3(selectedUnit.transform.position.x, selectedUnit.transform.position.y,-1),text);
    }

}
