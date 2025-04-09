using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private BattleController battleController;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private UIController uiController;


    private List<GameObject> unitsOnMap = new List<GameObject>();

    private const float tileSize = 1.0f;

    private Tile[][] map;




    private void Start()
    {
        map = new Tile[_width][];
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            map[x] = new Tile[_height];
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.transform.parent = transform;
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y%2!=0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                map[x][y] = spawnedTile;
                spawnedTile.SetPosition(x, y);
            }
        }
        camera.transform.position = new Vector3((float)_width / 2 - tileSize / 2, (float)_height - tileSize / 2,-10);
        
    }

    private void PlaceUnitOnMap(int x, int y,GameObject unitObject)
    {
        Tile tile = map[x][y];
        tile.SetIsSelected(false);
        unitObject.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
        unitObject.GetComponent<ActionUnit>().SetTilePosition(x, y);
        unitsOnMap.Add(unitObject);
    }

    private SelectableUnit GetSelectableUnitAtPosition(int x, int y)
    {
        return map[x][y];
    }

    private Tile GetTileAtPosition(int x, int y)
    {
        return map[x][y];
    }

    public void UnitSelected(int x, int y, UnitType type)
    {
        switch (type)
        {
            case UnitType.ACTION_UNIT:
                {
                    if (battleSystem.GetBattleState() != BattleState.PREPARE)
                    {
                        ActionUnit unit = unitsOnMap.Find(unitsOnMap => unitsOnMap.GetComponent<ActionUnit>().GetTileX() == x && unitsOnMap.GetComponent<ActionUnit>().GetTileY() == y).GetComponent<ActionUnit>();
                        if (unit)
                        {
                            battleController.ActionUnitSelected(unit);
                        }
                    }
                    break;
                }
            case UnitType.TILE:
                {
                    if (battleSystem.GetBattleState() == BattleState.PREPARE)
                    {
                        GameObject unit = Instantiate(uiController.GetComponent<UnitCreationUIController>().GetSelectedPrefab());
                        PlaceUnitOnMap(x, y, unit);
                    }
                    else
                    {
                        battleController.TileUnitSelected(GetTileAtPosition(x, y));
                    }
                    break;
                }
        }
        
    }

    public void AddPreparedUnitsToBattleSystem()
    {
        battleSystem.PrepareAndDecideTurnOrder(unitsOnMap);
    }

    public void HighLightMoveableArea(int x, int y,int movePoints)
    {
        for (int i = x-movePoints < 0 ? 0 : x-movePoints; i <= x+movePoints && i < _width; i++)
        {
            for (int j = y-movePoints < 0 ? 0 : y-movePoints; j <= y+movePoints && j< _height; j++)
            {
                map[i][j].EnableHighlight();
            }
        }
    }

    public void ClearAllHighlightedArea(int x, int y, int movePoints)
    {
        for (int i = x - movePoints < 0 ? 0 : x - movePoints; i <= x + movePoints && i < _width; i++)
        {
            for (int j = y - movePoints < 0 ? 0 : y - movePoints; j <= y + movePoints && j < _height; j++)
            {
                map[i][j].DisableHighlight();
            }
        }
    }

    public bool IsInReachDistance(ActionUnit start,SelectableUnit destiantion)
    {
        return CalculateDistance(start.GetTileX(), start.GetTileY(), destiantion.x, destiantion.y) <= start.getMovement();
    }

    public bool IsInAttackRange(ActionUnit start, ActionUnit destiantion)
    {
        return CalculateDistance(start.GetTileX(), start.GetTileY(), destiantion.GetTileX(), destiantion.GetTileY()) <= start.GetAttackRange();
    }

    public bool IsInSpellRange(ActionUnit start, ActionUnit destiantion, int spellRange)
    {
        return CalculateDistance(start.GetTileX(), start.GetTileY(), destiantion.GetTileX(), destiantion.GetTileY()) <= spellRange;
    }

    public int CalculateDistance(int startX, int startY, int destX, int destY)
    {
        double distance = Math.Sqrt(Math.Pow(destX - startX, 2) + Math.Pow(destY - startY, 2));
        return (int)Math.Round(distance);
    }
}
