using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private ActionUnit selectedUnit;
    private SelectableUnit selectedTile;
    private SelectableUnit destinationTile;
    private ActionUnit destinationUnit;


    public void TileUnitSelected(SelectableUnit unit)
    {

        if (selectedUnit)
        {
            //Mozgás a kijelölt egységhez
            destinationTile = unit;
            gridManager.ClearAllHighlightedArea(selectedUnit.GetTileX(), selectedUnit.GetTileY());
            selectedUnit.Move(new Vector3(unit.transform.position.x, unit.transform.position.y, 0),destinationTile.x,destinationTile.y);
            ClearTileSelection();
            ClearUnitSelection();
        }
        else
        {
            if (selectedTile)
            {
                selectedTile.deselect();
            }
            selectedTile = unit;
            ClearUnitSelection();
        }
        

    }

    public void ActionUnitSelected(ActionUnit unit)
    {
        ClearTileSelection();
        if (!selectedUnit)
        {
            selectedUnit = unit;
            gridManager.HighLightMoveableArea(selectedUnit.GetTileX(), selectedUnit.GetTileY());
        }
        else if(selectedUnit.Equals(unit))
        {
            selectedUnit.deselect();
            gridManager.ClearAllHighlightedArea(selectedUnit.GetTileX(), selectedUnit.GetTileY());
            selectedUnit = null;
        }
        else
        {
            destinationUnit = unit;
            gridManager.ClearAllHighlightedArea(selectedUnit.GetTileX(), selectedUnit.GetTileY());
            selectedUnit.deselect();
            //Ide a logika jön, hogy mi történjen, ha két egység van kijelölve
            selectedUnit.Attack(destinationUnit);
            selectedUnit = null;
            destinationUnit.deselect();
            destinationUnit = null;
        }
    }

    private void ClearUnitSelection() {
        if (selectedUnit)
        {
            selectedUnit.deselect();
            selectedUnit = null;
        }

        if (destinationUnit)
        {
            destinationUnit.deselect();
            destinationUnit = null;
        }
    }

    private void ClearTileSelection() {
        if (selectedTile)
        {
            selectedTile.deselect();
            selectedTile = null;
        }
        if (destinationTile)
        {
            destinationTile.deselect();
            destinationTile = null;
        }
    }



}
