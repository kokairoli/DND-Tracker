using UnityEngine;

public class BattleController : MonoBehaviour
{
    private ActionUnit selectedUnit;
    private SelectableUnit selectedTile;
    private SelectableUnit destinationTile;
    private ActionUnit destinationUnit;


    public void TileUnitSelected(SelectableUnit unit)
    {

        if (selectedUnit)
        {
            //Mozg�s a kijel�lt egys�ghez
            destinationTile = unit;
            selectedUnit.Move(new Vector3(unit.transform.position.x, unit.transform.position.y, 0));
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
        }
        else if(selectedUnit.Equals(unit))
        {
            selectedUnit.deselect();
            selectedUnit = null;
        }
        else
        {
            destinationUnit = unit;
            //Ide a logika j�n, hogy mi t�rt�njen, ha k�t egys�g van kijel�lve
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
