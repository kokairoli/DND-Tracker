using NUnit.Framework.Internal;
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
    private Dictionary<(int, int), int> currentPaths;
    private Dictionary<(int, int), (int, int)> currentpredecessors;
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
                spawnedTile.SetMovementCost(1);
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
                    UnitCreationUIController unitCreator = uiController.GetComponent<UnitCreationUIController>();
                    if (battleSystem.GetBattleState() == BattleState.PREPARE && unitCreator.IsPrefabSelected())
                    {
                        GameObject unit = Instantiate(unitCreator.GetSelectedPrefab());
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

    public void CalculateAvailablePathsFromStart(int x, int y, int movement)
    {
        var (shortestPaths, predecessors) = PathFinding.FindShortestPaths(map, map[x][y], movement);
        this.currentPaths = shortestPaths;
        this.currentpredecessors = predecessors;
    }

    public void HighLightPathToDestination(int destX,int destY) {
        if (battleController.GetCurrentAction() == Action.MOVE && currentPaths.ContainsKey((destX, destY)))
        {
            var path = PathFinding.GetPath(currentpredecessors, (destX, destY));
            foreach (var tile in path)
            {
                map[tile.Item1][tile.Item2].HighLight();
            }
        }
        else
        {
            map[destX][destY].HighLight();
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

    public void ClearAllHighlightedArea()
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (global::System.Int32 j = 0; j < map[0].Length; j++)
            {
                map[i][j].ClearHighlight();
            }
        }
    }

    public void ClearAllHighlightedAreaOnTileMouse()
    {
        if (battleController.GetCurrentAction() == Action.MOVE)
        {
            ClearAllHighlightedArea();
        }
    }

    public bool IsTileInReachDistance(Tile destiantion)
    {
        return currentPaths.ContainsKey((destiantion.x, destiantion.y));
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
